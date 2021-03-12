using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync;
using Microsoft.Exchange.EdgeSync.Ehf;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200003B RID: 59
	[Cmdlet("Set", "EdgeSyncEhfConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetEdgeSyncEhfConnector : SetTopologySystemConfigurationObjectTask<EdgeSyncEhfConnectorIdParameter, EdgeSyncEhfConnector>
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000078C2 File Offset: 0x00005AC2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEdgeSyncEhfConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000078D4 File Offset: 0x00005AD4
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 53, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\SetEdgeSyncEHFConnector.cs");
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007900 File Offset: 0x00005B00
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.DataObject.PrimaryLeaseLocation))
			{
				base.WriteError(new ArgumentException(Strings.InvalidPrimaryLeaseLocation), ErrorCategory.InvalidArgument, this.DataObject);
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.DataObject.BackupLeaseLocation))
			{
				base.WriteError(new ArgumentException(Strings.InvalidBackupLeaseLocation), ErrorCategory.InvalidArgument, this.DataObject);
			}
			try
			{
				EhfSynchronizationProvider.ValidateProvisioningUrl(this.DataObject.ProvisioningUrl, this.DataObject.AuthenticationCredential, this.DataObject.Name);
			}
			catch (ExDirectoryException ex)
			{
				base.WriteError(ex.InnerException ?? ex, ErrorCategory.InvalidArgument, this.DataObject);
			}
			if (this.DataObject.Enabled)
			{
				EdgeSyncEhfConnector edgeSyncEhfConnector = Utils.FindEnabledEhfSyncConnector((IConfigurationSession)base.DataSession, this.DataObject.Id);
				if (edgeSyncEhfConnector != null)
				{
					base.WriteError(new ArgumentException(Strings.EnabledEhfConnectorAlreadyExists(edgeSyncEhfConnector.DistinguishedName)), ErrorCategory.InvalidArgument, this.DataObject);
				}
			}
		}
	}
}
