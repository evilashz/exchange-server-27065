using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000062 RID: 98
	internal class AmClusterDbState : AmDbState
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x000168A4 File Offset: 0x00014AA4
		internal AmClusterDbState(IAmCluster cluster)
		{
			this.m_cluster = cluster;
			this.InitializeHandles();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000168BC File Offset: 0x00014ABC
		protected override void InitializeHandles()
		{
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(this.m_cluster.Handle, null, null, DxStoreKeyAccessMode.Write, false))
			{
				using (IDistributedStoreKey distributedStoreKey = clusterKey.OpenKey("ExchangeActiveManager", DxStoreKeyAccessMode.CreateIfNotExist, false, null))
				{
					this.m_regDbHandle = distributedStoreKey.OpenKey("DbState", DxStoreKeyAccessMode.CreateIfNotExist, false, null);
					this.m_regLastLogHandle = distributedStoreKey.OpenKey("LastLog", DxStoreKeyAccessMode.CreateIfNotExist, false, null);
					this.m_dbgOptionHandle = distributedStoreKey.OpenKey("DebugOption", DxStoreKeyAccessMode.CreateIfNotExist, false, null);
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00016964 File Offset: 0x00014B64
		protected override void CloseHandles()
		{
			if (this.m_regLastLogHandle != null)
			{
				this.m_regLastLogHandle.Dispose();
				this.m_regLastLogHandle = null;
			}
			if (this.m_regDbHandle != null)
			{
				this.m_regDbHandle.Dispose();
				this.m_regDbHandle = null;
			}
			if (this.m_dbgOptionHandle != null)
			{
				this.m_dbgOptionHandle.Dispose();
				this.m_dbgOptionHandle = null;
			}
			this.m_cluster = null;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000169C6 File Offset: 0x00014BC6
		protected override void WriteInternal(string guidStr, string stateInfoStr, AmServerName activeServerName)
		{
			this.m_regDbHandle.SetValue(guidStr, stateInfoStr, RegistryValueKind.String, false, null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000169DC File Offset: 0x00014BDC
		protected override Guid[] ReadDatabaseGuids(bool isBestEffort)
		{
			Guid[] result = null;
			try
			{
				string[] dbGuidStrings = this.m_regDbHandle.GetValueNames(null).ToArray<string>();
				result = base.ConvertGuidStringsToGuids(dbGuidStrings);
			}
			catch (ClusterException ex)
			{
				AmTrace.Error("ReadDatabaseGuids({0}): GetValueNames() failed with error {1}", new object[]
				{
					isBestEffort,
					ex.Message
				});
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00016A44 File Offset: 0x00014C44
		protected override AmDbStateInfo[] ReadAllInternal(bool isBestEffort)
		{
			List<AmDbStateInfo> list = new List<AmDbStateInfo>();
			try
			{
				IEnumerable<Tuple<string, object>> allValues = this.m_regDbHandle.GetAllValues(null);
				if (allValues != null)
				{
					foreach (Tuple<string, object> tuple in allValues)
					{
						try
						{
							Guid databaseGuid = new Guid(tuple.Item1);
							string entryStr = (string)tuple.Item2;
							AmDbStateInfo item = AmDbStateInfo.Parse(databaseGuid, entryStr);
							list.Add(item);
						}
						catch (InvalidCastException ex)
						{
							AmTrace.Error("ReadAllInternal invalid cast exception: {0}", new object[]
							{
								ex
							});
							if (!isBestEffort)
							{
								throw;
							}
						}
						catch (FormatException ex2)
						{
							AmTrace.Error("ReadAllInternal format exception: {0}", new object[]
							{
								ex2
							});
							if (!isBestEffort)
							{
								throw;
							}
						}
					}
				}
			}
			catch (ClusterException ex3)
			{
				AmTrace.Error("ReadAllInternal({0}): GetValueNames() failed with error {1}", new object[]
				{
					isBestEffort,
					ex3.Message
				});
				if (!isBestEffort)
				{
					throw;
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00016B78 File Offset: 0x00014D78
		protected override bool ReadInternal(string guidStr, out string stateInfoStr)
		{
			stateInfoStr = this.m_regDbHandle.GetValue(guidStr, null, null);
			return stateInfoStr != null;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00016B92 File Offset: 0x00014D92
		protected override void DeleteInternal(string guidStr)
		{
			this.m_regDbHandle.DeleteValue(guidStr, true, null);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00016BA3 File Offset: 0x00014DA3
		protected override void SetLastLogPropertyInternal(string name, string value)
		{
			this.m_regLastLogHandle.SetValue(name, value, false, null);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00016BB5 File Offset: 0x00014DB5
		protected override bool GetLastLogPropertyInternal(string name, out string value)
		{
			value = this.m_regLastLogHandle.GetValue(name, null, null);
			return value != null;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00016BD0 File Offset: 0x00014DD0
		protected override T GetDebugOptionInternal<T>(string serverName, string propertyName, T defaultValue, out bool doesValueExist)
		{
			doesValueExist = false;
			T result = defaultValue;
			try
			{
				if (serverName == null)
				{
					result = this.m_dbgOptionHandle.GetValue(propertyName, defaultValue, null);
				}
				else
				{
					using (IDistributedStoreKey distributedStoreKey = this.m_dbgOptionHandle.OpenKey(serverName, DxStoreKeyAccessMode.Read, true, null))
					{
						if (distributedStoreKey != null)
						{
							result = distributedStoreKey.GetValue(propertyName, defaultValue, null);
						}
					}
				}
			}
			catch (ClusterException ex)
			{
				AmTrace.Debug("Ignoring cluster exception while trying to get debug option (serverName={0}, propertyName={1}, exception={2})", new object[]
				{
					serverName,
					propertyName,
					ex
				});
			}
			return result;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00016C60 File Offset: 0x00014E60
		protected override bool SetDebugOptionInternal<T>(string serverName, string propertyName, T propertyValue)
		{
			try
			{
				if (serverName == null)
				{
					this.m_dbgOptionHandle.SetValue(propertyName, propertyValue, false, null);
				}
				else
				{
					using (IDistributedStoreKey distributedStoreKey = this.m_dbgOptionHandle.OpenKey(serverName, DxStoreKeyAccessMode.CreateIfNotExist, false, null))
					{
						distributedStoreKey.SetValue(propertyName, propertyValue, false, null);
					}
				}
				return true;
			}
			catch (ClusterException ex)
			{
				AmTrace.Debug("Ignoring cluster exception while trying to set debug option (serverName={0}, propertyName={1}, propertyValue={2}, exception={3})", new object[]
				{
					serverName,
					propertyName,
					propertyValue.ToString(),
					ex
				});
			}
			return false;
		}

		// Token: 0x040001DB RID: 475
		protected const string LastLogKeyName = "LastLog";

		// Token: 0x040001DC RID: 476
		public const string AmRootKeyName = "ExchangeActiveManager";

		// Token: 0x040001DD RID: 477
		private IAmCluster m_cluster;

		// Token: 0x040001DE RID: 478
		private IDistributedStoreKey m_regDbHandle;

		// Token: 0x040001DF RID: 479
		private IDistributedStoreKey m_regLastLogHandle;

		// Token: 0x040001E0 RID: 480
		private IDistributedStoreKey m_dbgOptionHandle;
	}
}
