using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000054 RID: 84
	internal class AmMultiNodeCopyStatusFetcher : AmMultiNodeRpcMap
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0001415C File Offset: 0x0001235C
		public AmMultiNodeCopyStatusFetcher(List<AmServerName> nodeList, Dictionary<AmServerName, IEnumerable<IADDatabase>> databasesMap, RpcGetDatabaseCopyStatusFlags2 rpcFlags, ActiveManager activeManager, bool isGetHealthStates) : this(nodeList, databasesMap, rpcFlags, activeManager, isGetHealthStates, RegistryParameters.BCSGetCopyStatusRPCTimeoutInMSec)
		{
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014170 File Offset: 0x00012370
		public AmMultiNodeCopyStatusFetcher(List<AmServerName> nodeList, Guid[] mdbGuids, Dictionary<AmServerName, IEnumerable<IADDatabase>> databasesMap, RpcGetDatabaseCopyStatusFlags2 rpcFlags, ActiveManager activeManager, bool isGetHealthStates = false) : this(nodeList, mdbGuids, databasesMap, rpcFlags, activeManager, RegistryParameters.BCSGetCopyStatusRPCTimeoutInMSec, isGetHealthStates)
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00014186 File Offset: 0x00012386
		public AmMultiNodeCopyStatusFetcher(List<AmServerName> nodeList, Dictionary<AmServerName, IEnumerable<IADDatabase>> databasesMap, RpcGetDatabaseCopyStatusFlags2 rpcFlags, ActiveManager activeManager, bool isGetHealthStates, int rpcTimeoutInMs) : this(nodeList, null, databasesMap, rpcFlags, activeManager, rpcTimeoutInMs, isGetHealthStates)
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014198 File Offset: 0x00012398
		public AmMultiNodeCopyStatusFetcher(List<AmServerName> nodeList, Guid[] mdbGuids, Dictionary<AmServerName, IEnumerable<IADDatabase>> databasesMap, RpcGetDatabaseCopyStatusFlags2 rpcFlags, ActiveManager activeManager, int rpcTimeoutInMs, bool isGetHealthStates = false) : base(nodeList, "AmMultiNodeCopyStatusFetcher")
		{
			this.m_rpcTimeoutInMs = rpcTimeoutInMs;
			this.m_mdbGuids = mdbGuids;
			this.m_databaseMap = databasesMap;
			this.m_rpcFlags = rpcFlags;
			this.m_activeManager = activeManager;
			this.m_isGetHealthStates = isGetHealthStates;
			this.m_healthStateTable = new Dictionary<AmServerName, RpcHealthStateInfo[]>(16);
			if (mdbGuids == null || mdbGuids.Length == 0)
			{
				this.m_copyStatusMap = new Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>>(160);
				return;
			}
			this.m_copyStatusMap = new Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>>(mdbGuids.Length);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014213 File Offset: 0x00012413
		internal Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> GetStatus()
		{
			return this.GetStatus(InvokeWithTimeout.InfiniteTimeSpan);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014220 File Offset: 0x00012420
		internal Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> GetStatus(TimeSpan timeout)
		{
			Dictionary<AmServerName, RpcHealthStateInfo[]> dictionary = null;
			return this.GetStatus(timeout, out dictionary);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014238 File Offset: 0x00012438
		internal Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> GetStatus(out Dictionary<AmServerName, RpcHealthStateInfo[]> healthStateTable)
		{
			return this.GetStatus(InvokeWithTimeout.InfiniteTimeSpan, out healthStateTable);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014246 File Offset: 0x00012446
		internal Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> GetStatus(TimeSpan timeout, out Dictionary<AmServerName, RpcHealthStateInfo[]> healthStateTable)
		{
			healthStateTable = null;
			if (this.m_copyStatusMap.Count > 0)
			{
				healthStateTable = this.m_healthStateTable;
				return this.m_copyStatusMap;
			}
			base.RunAllRpcs(timeout);
			healthStateTable = this.m_healthStateTable;
			this.Cleanup();
			return this.m_copyStatusMap;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00014284 File Offset: 0x00012484
		internal virtual bool TryGetCopyStatus(AmServerName node, Guid[] mdbGuids, out CopyStatusClientCachedEntry[] results, out RpcHealthStateInfo[] healthStates, out Exception exception)
		{
			int timeoutMs = (this.m_rpcTimeoutInMs > 0) ? this.m_rpcTimeoutInMs : RegistryParameters.BCSGetCopyStatusRPCTimeoutInMSec;
			results = null;
			healthStates = null;
			exception = null;
			if (mdbGuids != null)
			{
				results = CopyStatusHelper.GetCopyStatus(node, this.m_rpcFlags, mdbGuids, timeoutMs, this.m_activeManager, this.m_isGetHealthStates, out healthStates, out exception);
			}
			else
			{
				results = CopyStatusHelper.GetAllCopyStatuses(node, this.m_rpcFlags, this.m_databaseMap[node], timeoutMs, this.m_activeManager, this.m_isGetHealthStates, out healthStates, out exception);
			}
			bool flag = exception == null;
			if (!flag)
			{
				ExTraceGlobals.ActiveManagerTracer.TraceError<AmServerName, Exception>(0L, "AmMultiNodeCopyStatusFetcher: GetCopyStatus RPC to server '{0}' failed with ex: {1}", node, exception);
			}
			return flag;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014328 File Offset: 0x00012528
		internal override void TestInitialState()
		{
			base.TestInitialState();
			DiagCore.RetailAssert(this.m_copyStatusMap != null, "m_copyStatusMap should not be null at the start.", new object[0]);
			DiagCore.RetailAssert(this.m_copyStatusMap.Count == 0, "m_copyStatusMap.Count should be 0 at the start.", new object[0]);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014378 File Offset: 0x00012578
		protected override Exception RunServerRpc(AmServerName node, out object result)
		{
			result = null;
			Exception ex = null;
			CopyStatusClientCachedEntry[] key = null;
			RpcHealthStateInfo[] value = null;
			bool flag = this.TryGetCopyStatus(node, this.m_mdbGuids, out key, out value, out ex);
			DiagCore.AssertOrWatson(flag || ex != null, "ex cannot be null when TryGetCopyStatus() returns false!", new object[0]);
			DiagCore.AssertOrWatson(!flag || ex == null, "ex has to be null when TryGetCopyStatus() returns true! Actual: {0}", new object[]
			{
				ex
			});
			result = new KeyValuePair<CopyStatusClientCachedEntry[], RpcHealthStateInfo[]>(key, value);
			return ex;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000143F4 File Offset: 0x000125F4
		protected override void UpdateStatus(AmServerName node, object result)
		{
			KeyValuePair<CopyStatusClientCachedEntry[], RpcHealthStateInfo[]> keyValuePair = (KeyValuePair<CopyStatusClientCachedEntry[], RpcHealthStateInfo[]>)result;
			CopyStatusClientCachedEntry[] key = keyValuePair.Key;
			RpcHealthStateInfo[] value = keyValuePair.Value;
			if (key != null && key.Length > 0)
			{
				foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in key)
				{
					Guid dbGuid = copyStatusClientCachedEntry.DbGuid;
					if (this.m_copyStatusMap.ContainsKey(dbGuid))
					{
						Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary = this.m_copyStatusMap[dbGuid];
						dictionary[node] = copyStatusClientCachedEntry;
					}
					else
					{
						Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary2 = new Dictionary<AmServerName, CopyStatusClientCachedEntry>(5);
						dictionary2[node] = copyStatusClientCachedEntry;
						this.m_copyStatusMap[dbGuid] = dictionary2;
					}
				}
			}
			this.m_healthStateTable[node] = value;
		}

		// Token: 0x040001A6 RID: 422
		private const int NumberOfDbCopiesHint = 5;

		// Token: 0x040001A7 RID: 423
		private readonly bool m_isGetHealthStates;

		// Token: 0x040001A8 RID: 424
		protected Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> m_copyStatusMap;

		// Token: 0x040001A9 RID: 425
		protected Dictionary<AmServerName, RpcHealthStateInfo[]> m_healthStateTable;

		// Token: 0x040001AA RID: 426
		private Guid[] m_mdbGuids;

		// Token: 0x040001AB RID: 427
		private ActiveManager m_activeManager;

		// Token: 0x040001AC RID: 428
		private readonly int m_rpcTimeoutInMs;

		// Token: 0x040001AD RID: 429
		private Dictionary<AmServerName, IEnumerable<IADDatabase>> m_databaseMap;

		// Token: 0x040001AE RID: 430
		private RpcGetDatabaseCopyStatusFlags2 m_rpcFlags;
	}
}
