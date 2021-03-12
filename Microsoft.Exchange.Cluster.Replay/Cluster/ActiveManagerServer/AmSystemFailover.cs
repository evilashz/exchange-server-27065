using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200000E RID: 14
	internal class AmSystemFailover : AmSystemMoveBase
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000049E8 File Offset: 0x00002BE8
		internal AmSystemFailover(AmServerName nodeName, AmDbActionReason reasonCode, bool isForce, bool skipIfReplayRunning = false) : base(nodeName)
		{
			this.m_reasonCode = reasonCode;
			this.m_isForce = isForce;
			this.skipIfReplayRunning = skipIfReplayRunning;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004A08 File Offset: 0x00002C08
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.InitiatingServerFailover.Log<AmServerName>(this.m_nodeName);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2976263485U);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A60 File Offset: 0x00002C60
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.CompletedFailover.Log<AmServerName, int>(this.m_nodeName, this.m_moveRequests);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004AAC File Offset: 0x00002CAC
		protected IADDatabase[] GetDatabasesToBeMoved(IADDatabase[] dbList, MdbStatus[] mdbStatuses)
		{
			List<IADDatabase> list = new List<IADDatabase>();
			foreach (IADDatabase iaddatabase in dbList)
			{
				bool flag = true;
				if (mdbStatuses != null)
				{
					foreach (MdbStatus mdbStatus in mdbStatuses)
					{
						if (mdbStatus.MdbGuid == iaddatabase.Guid && mdbStatus.Status == MdbStatusFlags.Online)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					list.Add(iaddatabase);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004B2C File Offset: 0x00002D2C
		protected override void RunInternal()
		{
			IADDatabase[] dbList = null;
			if (!this.m_isForce)
			{
				AmMultiNodeMdbStatusFetcher amMultiNodeMdbStatusFetcher = base.StartMdbStatusFetcher();
				IADDatabase[] databases = base.GetDatabases();
				amMultiNodeMdbStatusFetcher.WaitUntilStatusIsReady();
				Dictionary<AmServerName, MdbStatus[]> mdbStatusMap = amMultiNodeMdbStatusFetcher.MdbStatusMap;
				AmMdbStatusServerInfo amMdbStatusServerInfo = amMultiNodeMdbStatusFetcher.ServerInfoMap[this.m_nodeName];
				MdbStatus[] mdbStatuses = mdbStatusMap[this.m_nodeName];
				if (amMdbStatusServerInfo.IsReplayRunning)
				{
					if (!this.skipIfReplayRunning)
					{
						if (RegistryParameters.TransientFailoverSuppressionDelayInSec > 0)
						{
							dbList = this.GetDatabasesToBeMoved(databases, mdbStatuses);
							base.AddDelayedFailoverEntryAsync(this.m_nodeName, this.m_reasonCode);
						}
					}
					else
					{
						ReplayCrimsonEvents.FailoverOnReplDownSkipped.Log<AmServerName, string, string>(this.m_nodeName, "ReplRunning", "MoveAll");
					}
				}
				else
				{
					dbList = databases;
				}
			}
			else
			{
				dbList = base.GetDatabases();
			}
			AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Automatic, this.m_reasonCode, AmDbActionCategory.Move);
			base.MoveDatabases(actionCode, dbList);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected override void PrepareMoveArguments(ref AmDbMoveArguments moveArgs)
		{
			moveArgs.MountDialOverride = DatabaseMountDialOverride.None;
		}

		// Token: 0x0400003B RID: 59
		private readonly bool m_isForce;

		// Token: 0x0400003C RID: 60
		private AmDbActionReason m_reasonCode;

		// Token: 0x0400003D RID: 61
		private readonly bool skipIfReplayRunning;
	}
}
