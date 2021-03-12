using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200000F RID: 15
	internal class AmSystemSwitchover : AmSystemMoveBase
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004C03 File Offset: 0x00002E03
		internal AmSystemSwitchover(AmServerName nodeName, AmDbActionReason reasonCode) : base(nodeName)
		{
			this.m_reasonCode = reasonCode;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C14 File Offset: 0x00002E14
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.InitiatingServerSwitchover.Log<AmServerName>(this.m_nodeName);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(4050005309U);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004C6C File Offset: 0x00002E6C
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.CompletedSwitchover.Log<AmServerName, int>(this.m_nodeName, this.m_moveRequests);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004CB8 File Offset: 0x00002EB8
		protected override void RunInternal()
		{
			AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Automatic, this.m_reasonCode, AmDbActionCategory.Move);
			base.MoveDatabases(actionCode);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004CDB File Offset: 0x00002EDB
		protected override void PrepareMoveArguments(ref AmDbMoveArguments moveArgs)
		{
			moveArgs.MountDialOverride = DatabaseMountDialOverride.Lossless;
		}

		// Token: 0x0400003E RID: 62
		private AmDbActionReason m_reasonCode;
	}
}
