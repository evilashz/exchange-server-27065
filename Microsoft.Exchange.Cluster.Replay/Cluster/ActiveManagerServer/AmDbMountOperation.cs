using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003A RID: 58
	internal class AmDbMountOperation : AmDbOperation
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		internal AmDbMountOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
			this.StoreMountFlags = MountFlags.None;
			this.AmMountFlags = AmMountFlags.None;
			this.MountDialOverride = DatabaseMountDialOverride.None;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F7EA File Offset: 0x0000D9EA
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000F7F2 File Offset: 0x0000D9F2
		internal MountFlags StoreMountFlags { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000F803 File Offset: 0x0000DA03
		internal AmMountFlags AmMountFlags { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000F80C File Offset: 0x0000DA0C
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000F814 File Offset: 0x0000DA14
		internal DatabaseMountDialOverride MountDialOverride { get; set; }

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F820 File Offset: 0x0000DA20
		public override string ToString()
		{
			return string.Format("Mount database {0} with action code {1} (StoreMountFlags {2}, AmMountFlags {3}, MountDialOverride {4})", new object[]
			{
				base.Database.Name,
				this.ActionCode,
				this.StoreMountFlags,
				this.AmMountFlags,
				this.MountDialOverride
			});
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F8DC File Offset: 0x0000DADC
		protected override void RunInternal()
		{
			AmDbOperationDetailedStatus mountStatus = null;
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				AmDbAction amDbAction = this.PrepareDbAction(this.ActionCode);
				amDbAction.Mount(this.StoreMountFlags, this.AmMountFlags, this.MountDialOverride, ref mountStatus);
			});
			base.DetailedStatus = mountStatus;
			base.LastException = lastException;
		}
	}
}
