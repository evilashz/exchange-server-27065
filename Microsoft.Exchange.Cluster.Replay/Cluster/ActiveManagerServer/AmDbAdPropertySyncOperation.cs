using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000042 RID: 66
	internal class AmDbAdPropertySyncOperation : AmDbOperation
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000FFE6 File Offset: 0x0000E1E6
		internal AmDbAdPropertySyncOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000FFF6 File Offset: 0x0000E1F6
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000FFFE File Offset: 0x0000E1FE
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x060002DF RID: 735 RVA: 0x00010007 File Offset: 0x0000E207
		public override string ToString()
		{
			return string.Format("Synchronize AD property for database {0} (actionCode={1})", base.Database.Name, this.ActionCode);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010040 File Offset: 0x0000E240
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.CheckIfOperationIsAllowedOnCurrentRole();
				AmDbAction.SyncDatabaseOwningServerAndLegacyDn(base.Database, this.ActionCode);
			});
			base.LastException = lastException;
		}
	}
}
