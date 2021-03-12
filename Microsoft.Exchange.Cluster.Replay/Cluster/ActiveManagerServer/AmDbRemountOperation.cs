using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200003B RID: 59
	internal class AmDbRemountOperation : AmDbOperation
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000F922 File Offset: 0x0000DB22
		internal AmDbRemountOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
			this.Flags = MountFlags.None;
			this.MountDialOverride = DatabaseMountDialOverride.None;
			this.FromServer = null;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000F947 File Offset: 0x0000DB47
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000F94F File Offset: 0x0000DB4F
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000F958 File Offset: 0x0000DB58
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000F960 File Offset: 0x0000DB60
		internal MountFlags Flags { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000F969 File Offset: 0x0000DB69
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000F971 File Offset: 0x0000DB71
		internal DatabaseMountDialOverride MountDialOverride { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000F97A File Offset: 0x0000DB7A
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000F982 File Offset: 0x0000DB82
		internal AmServerName FromServer { get; set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public override string ToString()
		{
			return string.Format("Remount database {0} on server {1} (Mount flags {2}, MountDialOverride {3}, ActionCode {4})", new object[]
			{
				base.Database.Name,
				this.FromServer,
				this.Flags,
				this.MountDialOverride,
				this.ActionCode
			});
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				AmDbAction amDbAction = base.PrepareDbAction(this.ActionCode);
				amDbAction.Remount(this.Flags, this.MountDialOverride, this.FromServer);
			});
			base.LastException = lastException;
		}
	}
}
