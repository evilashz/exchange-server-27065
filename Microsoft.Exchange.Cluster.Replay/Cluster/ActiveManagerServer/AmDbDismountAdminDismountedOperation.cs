using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000041 RID: 65
	internal class AmDbDismountAdminDismountedOperation : AmDbOperation
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		internal AmDbDismountAdminDismountedOperation(IADDatabase db, AmDbActionCode actionCode) : base(db)
		{
			this.ActionCode = actionCode;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000FECA File Offset: 0x0000E0CA
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000FED2 File Offset: 0x0000E0D2
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FEDB File Offset: 0x0000E0DB
		public override string ToString()
		{
			return string.Format("Dismount admin dismounted database {0} (actionCode={1})", base.Database.Name, this.ActionCode);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		protected override void RunInternal()
		{
			Exception lastException = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.CheckIfOperationIsAllowedOnCurrentRole();
				AmDbStateInfo amDbStateInfo = AmSystemManager.Instance.Config.DbState.Read(base.Database.Guid);
				if (!amDbStateInfo.IsAdminDismounted)
				{
					AmTrace.Debug("AmDbDismountAdminDismountedOperation skipped since database is not marked as admin dismounted anymore", new object[0]);
					return;
				}
				MountStatus storeDatabaseMountStatus = AmStoreHelper.GetStoreDatabaseMountStatus(null, base.Database.Guid);
				if (storeDatabaseMountStatus == MountStatus.Mounted)
				{
					AmDbAction amDbAction = base.PrepareDbAction(this.ActionCode);
					ReplayCrimsonEvents.DismountingAdminDismountRequestedDatabase.Log<string, Guid, AmServerName>(base.Database.Name, base.Database.Guid, amDbStateInfo.ActiveServer);
					amDbAction.Dismount(UnmountFlags.None);
					return;
				}
				AmTrace.Debug("AmDbDismountAdminDismountedOperation skipped since database is not mounted (db={0}, mountStatus={1})", new object[]
				{
					base.Database.Name,
					storeDatabaseMountStatus
				});
			});
			base.LastException = lastException;
		}
	}
}
