using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003D RID: 61
	internal class AmDbMoveOperation : AmDbOperation
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
		internal AmDbMoveOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.Arguments = new AmDbMoveArguments(actionCode);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000FB07 File Offset: 0x0000DD07
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000FB0F File Offset: 0x0000DD0F
		internal AmDbMoveArguments Arguments { get; set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FB18 File Offset: 0x0000DD18
		public override string ToString()
		{
			return string.Format("Move database {0} from {1} to {2} with action code {3}.  (Mount flags {4}, dismount flags {5}, MountDialOverride {6}, IncludeOtherServers {7}, SkipValidationChecks {8}, MoveComment '{9}', ComponentName '{10}')", new object[]
			{
				base.Database.Name,
				this.Arguments.SourceServer,
				(this.Arguments.TargetServer != null) ? this.Arguments.TargetServer.ToString() : "(no target)",
				this.Arguments.ActionCode,
				this.Arguments.MountFlags,
				this.Arguments.DismountFlags,
				this.Arguments.MountDialOverride,
				this.Arguments.TryOtherHealthyServers,
				this.Arguments.SkipValidationChecks,
				this.Arguments.MoveComment,
				this.Arguments.ComponentName
			});
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		protected override void RunInternal()
		{
			AmDbOperationDetailedStatus moveStatus = null;
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				AmDbAction amDbAction = this.PrepareDbAction(this.Arguments.ActionCode);
				amDbAction.Move(this.Arguments.MountFlags, this.Arguments.DismountFlags, this.Arguments.MountDialOverride, this.Arguments.SourceServer, this.Arguments.TargetServer, this.Arguments.TryOtherHealthyServers, this.Arguments.SkipValidationChecks, this.Arguments.MoveComment, this.Arguments.ComponentName, ref moveStatus);
			});
			base.DetailedStatus = moveStatus;
			base.LastException = lastException;
		}
	}
}
