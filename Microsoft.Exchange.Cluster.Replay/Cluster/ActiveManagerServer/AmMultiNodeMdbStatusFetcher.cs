using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000056 RID: 86
	internal class AmMultiNodeMdbStatusFetcher : AmMultiNodeRpcMap
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00014521 File Offset: 0x00012721
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00014529 File Offset: 0x00012729
		internal Dictionary<AmServerName, MdbStatus[]> MdbStatusMap { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00014532 File Offset: 0x00012732
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0001453A File Offset: 0x0001273A
		internal Dictionary<AmServerName, AmMdbStatusServerInfo> ServerInfoMap { get; private set; }

		// Token: 0x060003BF RID: 959 RVA: 0x00014544 File Offset: 0x00012744
		private static Dictionary<AmServerName, AmMdbStatusServerInfo> GetMultiNodeServerInfo(AmConfig cfg, List<AmServerName> serversList)
		{
			Dictionary<AmServerName, AmMdbStatusServerInfo> dictionary = new Dictionary<AmServerName, AmMdbStatusServerInfo>();
			foreach (AmServerName amServerName in serversList)
			{
				if (!cfg.IsUnknown)
				{
					if (cfg.IsDebugOptionsEnabled() && cfg.IsIgnoreServerDebugOptionEnabled(amServerName))
					{
						AmTrace.Warning("Server {0} is ignored from batch mounter operation since debug option {1} is enabled", new object[]
						{
							amServerName.NetbiosName,
							AmDebugOptions.IgnoreServerFromAutomaticActions.ToString()
						});
					}
					else if (cfg.IsStandalone || cfg.DagConfig.IsNodePubliclyUp(amServerName))
					{
						dictionary[amServerName] = new AmMdbStatusServerInfo(amServerName, true, TimeSpan.FromSeconds((double)RegistryParameters.MdbStatusFetcherServerUpTimeoutInSec));
					}
					else if (RegistryParameters.TransientFailoverSuppressionDelayInSec > 0)
					{
						dictionary[amServerName] = new AmMdbStatusServerInfo(amServerName, false, TimeSpan.FromSeconds((double)RegistryParameters.MdbStatusFetcherServerDownTimeoutInSec));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00014634 File Offset: 0x00012834
		internal AmMultiNodeMdbStatusFetcher() : base("AmMultiNodeMdbStatusFetcher")
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001464C File Offset: 0x0001284C
		private void Initialize(AmConfig cfg, List<AmServerName> serverList, bool isBasicInformation)
		{
			Dictionary<AmServerName, AmMdbStatusServerInfo> multiNodeServerInfo = AmMultiNodeMdbStatusFetcher.GetMultiNodeServerInfo(cfg, serverList);
			List<AmServerName> nodeList = multiNodeServerInfo.Keys.ToList<AmServerName>();
			base.Initialize(nodeList);
			this.ServerInfoMap = multiNodeServerInfo;
			this.m_isBasicInformation = isBasicInformation;
			this.MdbStatusMap = new Dictionary<AmServerName, MdbStatus[]>(this.m_expectedCount);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000146C8 File Offset: 0x000128C8
		internal void Start(AmConfig cfg, Func<List<AmServerName>> getServersFunc)
		{
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				this.Initialize(cfg, getServersFunc(), true);
				this.Run();
			});
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00014704 File Offset: 0x00012904
		internal void Run()
		{
			lock (this.m_locker)
			{
				if (!this.m_isRunning)
				{
					this.m_isRunning = true;
					base.RunAllRpcs();
				}
			}
			this.WaitUntilStatusIsReady();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001475C File Offset: 0x0001295C
		internal void WaitUntilStatusIsReady()
		{
			TimeSpan timeout = TimeSpan.FromSeconds((double)(RegistryParameters.MdbStatusFetcherServerUpTimeoutInSec + 15));
			base.WaitForCompletion(timeout);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00014780 File Offset: 0x00012980
		internal override void TestInitialState()
		{
			base.TestInitialState();
			DiagCore.RetailAssert(this.MdbStatusMap != null, "this.MdbStatusMap should not be null.", new object[0]);
			DiagCore.RetailAssert(this.MdbStatusMap.Count == 0, "this.MdbStatusMap should be 0 at the start.", new object[0]);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000147CD File Offset: 0x000129CD
		internal override void TestFinalState()
		{
			base.TestFinalState();
			DiagCore.RetailAssert(base.IsTimedout || this.MdbStatusMap.Count == this.m_expectedCount, "this.MdbStatusMap should have m_expectedCount entries.", new object[0]);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001482C File Offset: 0x00012A2C
		protected override Exception RunServerRpc(AmServerName node, out object result)
		{
			Exception result2 = null;
			Exception storeException = null;
			MdbStatus[] mdbStatuses = null;
			result = null;
			AmMdbStatusServerInfo amMdbStatusServerInfo = this.ServerInfoMap[node];
			bool flag = false;
			try
			{
				InvokeWithTimeout.Invoke(delegate()
				{
					storeException = this.RunServerRpcInternal(node, out mdbStatuses);
				}, amMdbStatusServerInfo.TimeOut);
			}
			catch (TimeoutException ex)
			{
				result2 = ex;
				flag = true;
			}
			if (!flag)
			{
				result2 = storeException;
				amMdbStatusServerInfo.IsStoreRunning = true;
				amMdbStatusServerInfo.IsReplayRunning = AmHelper.IsReplayRunning(node);
				result = mdbStatuses;
			}
			return result2;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000148E4 File Offset: 0x00012AE4
		private Exception RunServerRpcInternal(AmServerName node, out MdbStatus[] results)
		{
			results = null;
			Exception ex = null;
			MdbStatus[] array = null;
			if (!AmStoreHelper.GetAllDatabaseStatuses(node, this.m_isBasicInformation, out array))
			{
				Thread.Sleep(2000);
				if (!AmStoreHelper.GetAllDatabaseStatuses(node, this.m_isBasicInformation, out array, out ex))
				{
					AmTrace.Error("Failed to get mounted database information from store on server {0}. Exception: {1}", new object[]
					{
						node,
						ex
					});
					ReplayCrimsonEvents.ListMdbStatusFailed.LogPeriodic<string, string>(node.NetbiosName, TimeSpan.FromMinutes(15.0), node.NetbiosName, ex.ToString());
					return ex;
				}
			}
			results = array;
			return ex;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001496F File Offset: 0x00012B6F
		protected override void UpdateStatus(AmServerName node, object result)
		{
			this.MdbStatusMap[node] = (MdbStatus[])result;
		}

		// Token: 0x040001B4 RID: 436
		private object m_locker = new object();

		// Token: 0x040001B5 RID: 437
		private bool m_isRunning;

		// Token: 0x040001B6 RID: 438
		private bool m_isBasicInformation;
	}
}
