﻿using SmartCore.ConfigCenter.Apollo.Core.Utils;
using SmartCore.ConfigCenter.Apollo.Enums;
using SmartCore.ConfigCenter.Apollo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
namespace SmartCore.ConfigCenter.Apollo.Internals
{
    public abstract class AbstractConfig : IConfig
    {
        //private static readonly Func<Action<LogLevel, string, Exception?>> Logger = () => LogManager.CreateLogger(typeof(AbstractConfig));
        public event ConfigChangeEvent ConfigChanged = default!;
        private static readonly TaskFactory ExecutorService;

        static AbstractConfig() => ExecutorService = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(5));

        public abstract bool TryGetProperty(string key, [NotNullWhen(true)] out string? value);

        public abstract IEnumerable<string> GetPropertyNames();
 
        protected void FireConfigChange(IReadOnlyDictionary<string, ConfigChange> actualChanges)
 
        {
            if (ConfigChanged != null)
            {
                foreach (var @delegate in ConfigChanged.GetInvocationList())
                {
                    var handlerCopy = (ConfigChangeEvent)@delegate;
                    ExecutorService.StartNew(() =>
                    {
                        try
                        {
                            handlerCopy(this, new ConfigChangeEventArgs(this, actualChanges));
                        }
                        catch (Exception ex)
                        {
                           // Logger().Error($"Failed to invoke config change handler {(handlerCopy.Target == null ? handlerCopy.Method.Name : $"{handlerCopy.Target.GetType()}.{handlerCopy.Method.Name}")}", ex);
                        }
                    });
                }
            }
        }

        protected ICollection<ConfigChange> CalcPropertyChanges(Properties previous, Properties current)
        {
            if (previous == null)
            {
                previous = new Properties();
            }

            if (current == null)
            {
                current = new Properties();
            }

            var previousKeys = previous.GetPropertyNames();
            var currentKeys = current.GetPropertyNames();

            var commonKeys = previousKeys.Intersect(currentKeys).ToArray();
            var newKeys = currentKeys.Except(commonKeys);
            var removedKeys = previousKeys.Except(commonKeys);

            ICollection<ConfigChange> changes = new LinkedList<ConfigChange>();

            foreach (var newKey in newKeys)
            {
                changes.Add(new ConfigChange(this, newKey, null, current.GetProperty(newKey), PropertyChangeType.Added));
            }

            foreach (var removedKey in removedKeys)
            {
                changes.Add(new ConfigChange(this, removedKey, previous.GetProperty(removedKey), null, PropertyChangeType.Deleted));
            }

            foreach (var commonKey in commonKeys)
            {
                var previousValue = previous.GetProperty(commonKey);
                var currentValue = current.GetProperty(commonKey);
                if (string.Equals(previousValue, currentValue))
                {
                    continue;
                }
                changes.Add(new ConfigChange(this, commonKey, previousValue, currentValue, PropertyChangeType.Modified));
            }

            return changes;
        }
    }
}
