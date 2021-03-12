using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000011 RID: 17
	internal class AmSystemSwitchoverOnComponentMove : AmSystemMoveBase
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00004DD4 File Offset: 0x00002FD4
		internal AmSystemSwitchoverOnComponentMove(AmDbMoveArguments args) : base(args.SourceServer)
		{
			this.Arguments = args;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004DE9 File Offset: 0x00002FE9
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004DF1 File Offset: 0x00002FF1
		internal AmDbMoveArguments Arguments { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00004DFC File Offset: 0x00002FFC
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.InitiatingServerMoveAllDatabasesByComponentRequest.Log<AmServerName, string>(this.m_nodeName, this.Arguments.ComponentName);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004E50 File Offset: 0x00003050
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.CompletedServerMoveAllDatabasesByComponentRequest.Log<AmServerName, int, string, string>(this.m_nodeName, this.m_moveRequests, this.Arguments.MoveComment, this.Arguments.ComponentName);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004EB2 File Offset: 0x000030B2
		protected override void RunInternal()
		{
			base.MoveDatabases(this.Arguments.ActionCode);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004EC5 File Offset: 0x000030C5
		protected override void PrepareMoveArguments(ref AmDbMoveArguments moveArgs)
		{
			moveArgs = this.Arguments;
		}
	}
}
