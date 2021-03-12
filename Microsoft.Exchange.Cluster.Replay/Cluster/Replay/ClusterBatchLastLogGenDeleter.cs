using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClusterBatchLastLogGenDeleter
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00023DBF File Offset: 0x00021FBF
		public ClusterBatchLastLogGenDeleter(IAmCluster cluster, List<Database> databases, ILogTraceHelper logger)
		{
			this.m_cluster = cluster;
			this.m_databases = databases;
			this.m_logger = logger;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00023DDC File Offset: 0x00021FDC
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterTracer;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00023DE4 File Offset: 0x00021FE4
		public void DeleteTimeStamps()
		{
			if (this.m_databases == null || this.m_databases.Count == 0)
			{
				ClusterBatchLastLogGenDeleter.Tracer.TraceDebug((long)this.GetHashCode(), "No databases specified, so skipping cluster batch timestamp deletion.");
				return;
			}
			AmClusterHandle amClusterHandle;
			if (this.m_cluster == null)
			{
				amClusterHandle = ClusapiMethods.OpenCluster(null);
			}
			else
			{
				amClusterHandle = this.m_cluster.Handle;
			}
			if (amClusterHandle.IsInvalid)
			{
				Exception ex = new ClusterApiException("OpenCluster", new Win32Exception());
				ClusterBatchLastLogGenDeleter.Tracer.TraceError<Exception>((long)this.GetHashCode(), "The cluster handle is invalid! Exception: {0}", ex);
				throw ex;
			}
			try
			{
				this.DeleteTimeStampsInternal(amClusterHandle);
			}
			finally
			{
				if (this.m_cluster == null)
				{
					amClusterHandle.Close();
				}
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00023E94 File Offset: 0x00022094
		private void DeleteTimeStampsInternal(AmClusterHandle clusterHandle)
		{
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(clusterHandle, null, null, DxStoreKeyAccessMode.Write, false))
			{
				using (IDistributedStoreKey distributedStoreKey = clusterKey.OpenKey("ExchangeActiveManager", DxStoreKeyAccessMode.Write, true, null))
				{
					if (distributedStoreKey != null)
					{
						using (IDistributedStoreKey distributedStoreKey2 = distributedStoreKey.OpenKey("LastLog", DxStoreKeyAccessMode.Write, true, null))
						{
							if (distributedStoreKey2 != null)
							{
								using (IDistributedStoreBatchRequest distributedStoreBatchRequest = distributedStoreKey2.CreateBatchUpdateRequest())
								{
									foreach (Database database in this.m_databases)
									{
										string name = database.Name;
										string text = database.Guid.ToString();
										string propertyName = AmDbState.ConstructLastLogTimeStampProperty(text);
										string value = distributedStoreKey2.GetValue(propertyName, null, null);
										if (value != null)
										{
											ClusterBatchLastLogGenDeleter.Tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "LastLogGeneration time stamp for database [{0} ({1})] found with value '{2}'.", name, text, value);
											this.m_logger.AppendLogMessage("Deleting LastLogGeneration time stamp from cluster registry for database [{0} ({1})] with existing value: '{2}'.", new object[]
											{
												name,
												text,
												value
											});
											distributedStoreBatchRequest.DeleteValue(propertyName);
										}
										else
										{
											ClusterBatchLastLogGenDeleter.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "LastLogGeneration time stamp for database [{0} ({1})] does not exist.", name, text);
										}
									}
									distributedStoreBatchRequest.Execute(null);
									goto IL_151;
								}
							}
							ClusterBatchLastLogGenDeleter.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ActiveManager LastLog key '{0}\\{1}' does not exist in the cluster registry. Skipping deletion.", "ExchangeActiveManager", "LastLog");
							IL_151:
							goto IL_178;
						}
					}
					ClusterBatchLastLogGenDeleter.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveManager root key '{0}' does not exist in the cluster registry. Skipping deletion.", "ExchangeActiveManager");
					IL_178:;
				}
			}
		}

		// Token: 0x0400033C RID: 828
		private const string AmRootKeyName = "ExchangeActiveManager";

		// Token: 0x0400033D RID: 829
		private const string LastLogKeyName = "LastLog";

		// Token: 0x0400033E RID: 830
		private readonly List<Database> m_databases;

		// Token: 0x0400033F RID: 831
		private readonly IAmCluster m_cluster;

		// Token: 0x04000340 RID: 832
		private ILogTraceHelper m_logger;
	}
}
