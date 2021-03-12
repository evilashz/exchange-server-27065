using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003C RID: 60
	internal class AmDbDismountOperation : AmDbOperation
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000FA42 File Offset: 0x0000DC42
		internal AmDbDismountOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
			this.Flags = UnmountFlags.SkipCacheFlush;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000FA5A File Offset: 0x0000DC5A
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000FA62 File Offset: 0x0000DC62
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000FA73 File Offset: 0x0000DC73
		internal UnmountFlags Flags { get; set; }

		// Token: 0x060002BB RID: 699 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		public override string ToString()
		{
			return string.Format("Dismount database {0} with action code {1} (Dismount flags {2})", base.Database.Name, this.ActionCode, this.Flags);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FACC File Offset: 0x0000DCCC
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				AmDbAction amDbAction = base.PrepareDbAction(this.ActionCode);
				amDbAction.Dismount(this.Flags);
			});
			base.LastException = lastException;
		}
	}
}
