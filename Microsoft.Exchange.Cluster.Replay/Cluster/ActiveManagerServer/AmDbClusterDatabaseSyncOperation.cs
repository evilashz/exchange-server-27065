using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000040 RID: 64
	internal class AmDbClusterDatabaseSyncOperation : AmDbOperation
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000FE3A File Offset: 0x0000E03A
		internal AmDbClusterDatabaseSyncOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000FE4A File Offset: 0x0000E04A
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000FE52 File Offset: 0x0000E052
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x060002D3 RID: 723 RVA: 0x0000FE5B File Offset: 0x0000E05B
		public override string ToString()
		{
			return string.Format("Synchronize cluster database {0} (actionCode={1})", base.Database.Name, this.ActionCode);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000FE94 File Offset: 0x0000E094
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.CheckIfOperationIsAllowedOnCurrentRole();
				AmDbAction.SyncClusterDatabaseState(base.Database, this.ActionCode);
			});
			base.LastException = lastException;
		}
	}
}
