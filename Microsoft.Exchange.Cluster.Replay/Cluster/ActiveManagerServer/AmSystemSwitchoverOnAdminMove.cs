using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000010 RID: 16
	internal class AmSystemSwitchoverOnAdminMove : AmSystemMoveBase
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004CE5 File Offset: 0x00002EE5
		internal AmSystemSwitchoverOnAdminMove(AmDbMoveArguments args) : base(args.SourceServer)
		{
			this.Arguments = args;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004CFA File Offset: 0x00002EFA
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004D02 File Offset: 0x00002F02
		internal AmDbMoveArguments Arguments { get; private set; }

		// Token: 0x0600009D RID: 157 RVA: 0x00004D0C File Offset: 0x00002F0C
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.InitiatingServerMoveAllDatabases.Log<AmServerName>(this.m_nodeName);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D54 File Offset: 0x00002F54
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.CompletedServerMoveAllDatabases.Log<AmServerName, int, string>(this.m_nodeName, this.m_moveRequests, this.Arguments.MoveComment);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004DAC File Offset: 0x00002FAC
		protected override void RunInternal()
		{
			AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Admin, AmDbActionReason.Cmdlet, AmDbActionCategory.Move);
			base.MoveDatabases(actionCode);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004DCA File Offset: 0x00002FCA
		protected override void PrepareMoveArguments(ref AmDbMoveArguments moveArgs)
		{
			moveArgs = this.Arguments;
		}
	}
}
