using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200003C RID: 60
	[Cmdlet("Set", "EdgeSyncMservConnector", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetEdgeSyncMservConnector : SetTopologySystemConfigurationObjectTask<EdgeSyncMservConnectorIdParameter, EdgeSyncMservConnector>
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007A24 File Offset: 0x00005C24
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEdgeSyncMservConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007A36 File Offset: 0x00005C36
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 45, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\SetEdgeSyncMservConnector.cs");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007A60 File Offset: 0x00005C60
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.PrimaryLeaseLocation != null && !Utils.IsLeaseDirectoryValidPath(this.DataObject.PrimaryLeaseLocation))
			{
				base.WriteError(new InvalidOperationException(Strings.InvalidPrimaryLeaseLocation), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.BackupLeaseLocation != null && !Utils.IsLeaseDirectoryValidPath(this.DataObject.BackupLeaseLocation))
			{
				base.WriteError(new InvalidOperationException(Strings.InvalidBackupLeaseLocation), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}
	}
}
