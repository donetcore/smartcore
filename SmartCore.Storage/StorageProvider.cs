﻿using System;

namespace SmartCore.Storage
{
    public static class StorageProvider
    {
        //private static readonly Lazy<IStorageProvider> CurrentStorageProvider = new Lazy<IStorageProvider>(() =>
        //{
        //    var type = SiteConstants.Instance.StorageProviderType;
        //    if (string.IsNullOrEmpty(type))
        //    {
        //        return new DiskStorageProvider();
        //    }

        //    try
        //    {
        //        return TypeFactory.GetInstanceOf<IStorageProvider>(type);
        //    }
        //    catch (Exception)
        //    {
        //        return new DiskStorageProvider();
        //    }
        //});

        //public static IStorageProvider Current => CurrentStorageProvider.Value;
    }
}
