using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003F RID: 63
	internal class AmDbDismountMismountedOperation : AmDbOperation
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000FDA5 File Offset: 0x0000DFA5
		internal AmDbDismountMismountedOperation(IADDatabase db, AmDbActionCode actionCode, List<AmServerName> mismountedNodes) : base(db)
		{
			this.ActionCode = actionCode;
			this.MismountedNodes = mismountedNodes;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000FDCD File Offset: 0x0000DFCD
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000FDD5 File Offset: 0x0000DFD5
		internal List<AmServerName> MismountedNodes { get; set; }

		// Token: 0x060002CD RID: 717 RVA: 0x0000FDDE File Offset: 0x0000DFDE
		public override string ToString()
		{
			return string.Format("Force dismount database {0} (actionCode={1})", base.Database.Name, this.ActionCode);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000FE14 File Offset: 0x0000E014
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				AmDbAction.DismountIfMismounted(base.Database, this.ActionCode, this.MismountedNodes);
			});
			base.LastException = lastException;
		}
	}
}
