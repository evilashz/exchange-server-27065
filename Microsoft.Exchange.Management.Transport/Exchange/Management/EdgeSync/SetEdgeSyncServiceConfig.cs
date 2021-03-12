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
	// Token: 0x0200003D RID: 61
	[Cmdlet("Set", "EdgeSyncServiceConfig", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetEdgeSyncServiceConfig : SetTopologySystemConfigurationObjectTask<EdgeSyncServiceConfigIdParameter, EdgeSyncServiceConfig>
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007AFA File Offset: 0x00005CFA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEdgeSyncServiceConfig(this.Identity.ToString());
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007B0C File Offset: 0x00005D0C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 44, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\SetEdgeSyncServiceConfig.cs");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007B38 File Offset: 0x00005D38
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ADObjectId siteId = this.DataObject.Id.AncestorDN(1);
			if (!EdgeSyncServiceConfig.ValidLogSizeCompatibility(this.DataObject.LogMaxFileSize, this.DataObject.LogMaxDirectorySize, siteId, (ITopologyConfigurationSession)base.DataSession))
			{
				base.WriteError(new InvalidOperationException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}
	}
}
