// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Specification.ClusterApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IClusterAllocationExplainRequest : IRequest<ClusterAllocationExplainRequestParameters>
	{
	}

	///<summary>Request for AllocationExplain <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</para></summary>
	public partial class ClusterAllocationExplainRequest : PlainRequestBase<ClusterAllocationExplainRequestParameters>, IClusterAllocationExplainRequest
	{
		protected IClusterAllocationExplainRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterAllocationExplain;
		// values part of the url path
		// Request parameters
		///<summary>Return information about disk usage and shard sizes (default: false)</summary>
		public bool? IncludeDiskInfo
		{
			get => Q<bool? >("include_disk_info");
			set => Q("include_disk_info", value);
		}

		///<summary>Return 'YES' decisions in explanation (default: false)</summary>
		public bool? IncludeYesDecisions
		{
			get => Q<bool? >("include_yes_decisions");
			set => Q("include_yes_decisions", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IDeleteVotingConfigExclusionsRequest : IRequest<DeleteVotingConfigExclusionsRequestParameters>
	{
	}

	///<summary>Request for DeleteVotingConfigExclusions <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/voting-config-exclusions.html</para></summary>
	public partial class DeleteVotingConfigExclusionsRequest : PlainRequestBase<DeleteVotingConfigExclusionsRequestParameters>, IDeleteVotingConfigExclusionsRequest
	{
		protected IDeleteVotingConfigExclusionsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterDeleteVotingConfigExclusions;
		// values part of the url path
		// Request parameters
		///<summary>Specifies whether to wait for all excluded nodes to be removed from the cluster before clearing the voting configuration exclusions list.</summary>
		public bool? WaitForRemoval
		{
			get => Q<bool? >("wait_for_removal");
			set => Q("wait_for_removal", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterGetSettingsRequest : IRequest<ClusterGetSettingsRequestParameters>
	{
	}

	///<summary>Request for GetSettings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public partial class ClusterGetSettingsRequest : PlainRequestBase<ClusterGetSettingsRequestParameters>, IClusterGetSettingsRequest
	{
		protected IClusterGetSettingsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterGetSettings;
		// values part of the url path
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Whether to return all default clusters setting.</summary>
		public bool? IncludeDefaults
		{
			get => Q<bool? >("include_defaults");
			set => Q("include_defaults", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterHealthRequest : IRequest<ClusterHealthRequestParameters>
	{
		[IgnoreDataMember]
		Indices Index
		{
			get;
		}
	}

	///<summary>Request for Health <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-health.html</para></summary>
	public partial class ClusterHealthRequest : PlainRequestBase<ClusterHealthRequestParameters>, IClusterHealthRequest
	{
		protected IClusterHealthRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterHealth;
		///<summary>/_cluster/health</summary>
		public ClusterHealthRequest(): base()
		{
		}

		///<summary>/_cluster/health/{index}</summary>
		///<param name = "index">Optional, accepts null</param>
		public ClusterHealthRequest(Indices index): base(r => r.Optional("index", index))
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Indices IClusterHealthRequest.Index => Self.RouteValues.Get<Indices>("index");
		// Request parameters
		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public ExpandWildcards? ExpandWildcards
		{
			get => Q<ExpandWildcards? >("expand_wildcards");
			set => Q("expand_wildcards", value);
		}

		///<summary>Specify the level of detail for returned information</summary>
		public Level? Level
		{
			get => Q<Level? >("level");
			set => Q("level", value);
		}

		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}

		///<summary>Wait until the specified number of shards is active</summary>
		public string WaitForActiveShards
		{
			get => Q<string>("wait_for_active_shards");
			set => Q("wait_for_active_shards", value);
		}

		///<summary>Wait until all currently queued events with the given priority are processed</summary>
		public WaitForEvents? WaitForEvents
		{
			get => Q<WaitForEvents? >("wait_for_events");
			set => Q("wait_for_events", value);
		}

		///<summary>Whether to wait until there are no initializing shards in the cluster</summary>
		public bool? WaitForNoInitializingShards
		{
			get => Q<bool? >("wait_for_no_initializing_shards");
			set => Q("wait_for_no_initializing_shards", value);
		}

		///<summary>Whether to wait until there are no relocating shards in the cluster</summary>
		public bool? WaitForNoRelocatingShards
		{
			get => Q<bool? >("wait_for_no_relocating_shards");
			set => Q("wait_for_no_relocating_shards", value);
		}

		///<summary>Wait until the specified number of nodes is available</summary>
		public string WaitForNodes
		{
			get => Q<string>("wait_for_nodes");
			set => Q("wait_for_nodes", value);
		}

		///<summary>Wait until cluster is in a specific state</summary>
		public WaitForStatus? WaitForStatus
		{
			get => Q<WaitForStatus? >("wait_for_status");
			set => Q("wait_for_status", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterPendingTasksRequest : IRequest<ClusterPendingTasksRequestParameters>
	{
	}

	///<summary>Request for PendingTasks <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-pending.html</para></summary>
	public partial class ClusterPendingTasksRequest : PlainRequestBase<ClusterPendingTasksRequestParameters>, IClusterPendingTasksRequest
	{
		protected IClusterPendingTasksRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterPendingTasks;
		// values part of the url path
		// Request parameters
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IPostVotingConfigExclusionsRequest : IRequest<PostVotingConfigExclusionsRequestParameters>
	{
	}

	///<summary>Request for PostVotingConfigExclusions <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/voting-config-exclusions.html</para></summary>
	public partial class PostVotingConfigExclusionsRequest : PlainRequestBase<PostVotingConfigExclusionsRequestParameters>, IPostVotingConfigExclusionsRequest
	{
		protected IPostVotingConfigExclusionsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterPostVotingConfigExclusions;
		// values part of the url path
		// Request parameters
		///<summary>
		/// A comma-separated list of the persistent ids of the nodes to exclude from the voting configuration. If specified, you may not also specify
		/// ?node_names.
		///</summary>
		public string NodeIds
		{
			get => Q<string>("node_ids");
			set => Q("node_ids", value);
		}

		///<summary>
		/// A comma-separated list of the names of the nodes to exclude from the voting configuration. If specified, you may not also specify
		/// ?node_ids.
		///</summary>
		public string NodeNames
		{
			get => Q<string>("node_names");
			set => Q("node_names", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterPutSettingsRequest : IRequest<ClusterPutSettingsRequestParameters>
	{
	}

	///<summary>Request for PutSettings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public partial class ClusterPutSettingsRequest : PlainRequestBase<ClusterPutSettingsRequestParameters>, IClusterPutSettingsRequest
	{
		protected IClusterPutSettingsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterPutSettings;
		// values part of the url path
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IRemoteInfoRequest : IRequest<RemoteInfoRequestParameters>
	{
	}

	///<summary>Request for RemoteInfo <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-remote-info.html</para></summary>
	public partial class RemoteInfoRequest : PlainRequestBase<RemoteInfoRequestParameters>, IRemoteInfoRequest
	{
		protected IRemoteInfoRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterRemoteInfo;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IClusterRerouteRequest : IRequest<ClusterRerouteRequestParameters>
	{
	}

	///<summary>Request for Reroute <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-reroute.html</para></summary>
	public partial class ClusterRerouteRequest : PlainRequestBase<ClusterRerouteRequestParameters>, IClusterRerouteRequest
	{
		protected IClusterRerouteRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterReroute;
		// values part of the url path
		// Request parameters
		///<summary>Simulate the operation only and return the resulting state</summary>
		public bool? DryRun
		{
			get => Q<bool? >("dry_run");
			set => Q("dry_run", value);
		}

		///<summary>Return an explanation of why the commands can or cannot be executed</summary>
		public bool? Explain
		{
			get => Q<bool? >("explain");
			set => Q("explain", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Limit the information returned to the specified metrics. Defaults to all but metadata</summary>
		public string[] Metric
		{
			get => Q<string[]>("metric");
			set => Q("metric", value);
		}

		///<summary>Retries allocation of shards that are blocked due to too many subsequent allocation failures</summary>
		public bool? RetryFailed
		{
			get => Q<bool? >("retry_failed");
			set => Q("retry_failed", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterStateRequest : IRequest<ClusterStateRequestParameters>
	{
		[IgnoreDataMember]
		Metrics Metric
		{
			get;
		}

		[IgnoreDataMember]
		Indices Index
		{
			get;
		}
	}

	///<summary>Request for State <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-state.html</para></summary>
	public partial class ClusterStateRequest : PlainRequestBase<ClusterStateRequestParameters>, IClusterStateRequest
	{
		protected IClusterStateRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterState;
		///<summary>/_cluster/state</summary>
		public ClusterStateRequest(): base()
		{
		}

		///<summary>/_cluster/state/{metric}</summary>
		///<param name = "metric">Optional, accepts null</param>
		public ClusterStateRequest(Metrics metric): base(r => r.Optional("metric", metric))
		{
		}

		///<summary>/_cluster/state/{metric}/{index}</summary>
		///<param name = "metric">Optional, accepts null</param>
		///<param name = "index">Optional, accepts null</param>
		public ClusterStateRequest(Metrics metric, Indices index): base(r => r.Optional("metric", metric).Optional("index", index))
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		Metrics IClusterStateRequest.Metric => Self.RouteValues.Get<Metrics>("metric");
		[IgnoreDataMember]
		Indices IClusterStateRequest.Index => Self.RouteValues.Get<Indices>("index");
		// Request parameters
		///<summary>
		/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes `_all` string or when no indices have
		/// been specified)
		///</summary>
		public bool? AllowNoIndices
		{
			get => Q<bool? >("allow_no_indices");
			set => Q("allow_no_indices", value);
		}

		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public ExpandWildcards? ExpandWildcards
		{
			get => Q<ExpandWildcards? >("expand_wildcards");
			set => Q("expand_wildcards", value);
		}

		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Whether specified concrete indices should be ignored when unavailable (missing or closed)</summary>
		public bool? IgnoreUnavailable
		{
			get => Q<bool? >("ignore_unavailable");
			set => Q("ignore_unavailable", value);
		}

		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Wait for the metadata version to be equal or greater than the specified metadata version</summary>
		public long? WaitForMetadataVersion
		{
			get => Q<long? >("wait_for_metadata_version");
			set => Q("wait_for_metadata_version", value);
		}

		///<summary>The maximum time to wait for wait_for_metadata_version before timing out</summary>
		public Time WaitForTimeout
		{
			get => Q<Time>("wait_for_timeout");
			set => Q("wait_for_timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IClusterStatsRequest : IRequest<ClusterStatsRequestParameters>
	{
		[IgnoreDataMember]
		NodeIds NodeId
		{
			get;
		}
	}

	///<summary>Request for Stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-stats.html</para></summary>
	public partial class ClusterStatsRequest : PlainRequestBase<ClusterStatsRequestParameters>, IClusterStatsRequest
	{
		protected IClusterStatsRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterStats;
		///<summary>/_cluster/stats</summary>
		public ClusterStatsRequest(): base()
		{
		}

		///<summary>/_cluster/stats/nodes/{node_id}</summary>
		///<param name = "nodeId">Optional, accepts null</param>
		public ClusterStatsRequest(NodeIds nodeId): base(r => r.Optional("node_id", nodeId))
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		NodeIds IClusterStatsRequest.NodeId => Self.RouteValues.Get<NodeIds>("node_id");
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}
}