using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003E RID: 62
	internal class AmDbSkippedMoveOperation : AmDbOperation
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000FD1E File Offset: 0x0000DF1E
		internal AmDbSkippedMoveOperation(IADDatabase db, LocalizedString reason) : base(db)
		{
			this.MoveSkipReason = reason;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000FD2E File Offset: 0x0000DF2E
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000FD36 File Offset: 0x0000DF36
		internal LocalizedString MoveSkipReason { get; private set; }

		// Token: 0x060002C6 RID: 710 RVA: 0x0000FD3F File Offset: 0x0000DF3F
		public override string ToString()
		{
			return string.Format("Skipping move of database '{0}'. Reason: {1}", base.Database.Name, this.MoveSkipReason);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000FD64 File Offset: 0x0000DF64
		protected override void RunInternal()
		{
			AmDbOperationDetailedStatus detailedStatus = new AmDbOperationDetailedStatus(base.Database);
			base.DetailedStatus = detailedStatus;
			base.LastException = new AmDbMoveOperationSkippedException(base.Database.Name, this.MoveSkipReason);
		}
	}
}
