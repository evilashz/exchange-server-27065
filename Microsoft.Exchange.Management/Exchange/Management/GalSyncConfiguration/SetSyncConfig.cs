using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.GalSyncConfiguration
{
	// Token: 0x0200040B RID: 1035
	[Cmdlet("Set", "SyncConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Managed")]
	public sealed class SetSyncConfig : SetMultitenancySingletonSystemConfigurationObjectTask<SyncOrganization>
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000909DB File Offset: 0x0008EBDB
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x000909E3 File Offset: 0x0008EBE3
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "Federated", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "Managed", ValueFromPipeline = true)]
		public new OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000909EC File Offset: 0x0008EBEC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.FederatedTenant)
			{
				if (this.DataObject.IsChanged(SyncOrganizationSchema.DisableWindowsLiveID) || this.DataObject.IsChanged(SyncOrganizationSchema.PasswordFilePath) || this.DataObject.IsChanged(SyncOrganizationSchema.ResetPasswordOnNextLogon))
				{
					base.WriteError(new ArgumentException(Strings.ErrorParameterInvalidForFederatedTenant(this.DataObject.Identity.ToString())), (ErrorCategory)1000, null);
				}
			}
			else if (this.DataObject.IsChanged(SyncOrganizationSchema.FederatedIdentitySourceADAttribute))
			{
				base.WriteError(new ArgumentException(Strings.ErrorParameterInvalidForManagedTenant(this.DataObject.Identity.ToString())), (ErrorCategory)1000, null);
			}
			if (this.DataObject.IsChanged(SyncOrganizationSchema.ProvisioningDomain))
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				ADPagedReader<AcceptedDomain> adpagedReader = configurationSession.FindPaged<AcceptedDomain>(null, QueryScope.SubTree, null, null, 0);
				bool flag = false;
				using (IEnumerator<AcceptedDomain> enumerator = adpagedReader.GetEnumerator())
				{
					while (enumerator.MoveNext() && !flag)
					{
						if (enumerator.Current.DomainName.Match(this.DataObject.ProvisioningDomain.Address) >= 0)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					base.WriteError(new ArgumentException(Strings.ErrorProvisioningDomainNotMatchAcceptedDomain(this.DataObject.ProvisioningDomain.Address, this.DataObject.Identity.ToString())), (ErrorCategory)1000, null);
				}
			}
			if (this.DataObject.IsChanged(SyncOrganizationSchema.FederatedIdentitySourceADAttribute) && !string.Equals(this.DataObject.FederatedIdentitySourceADAttribute, "objectGuid", StringComparison.OrdinalIgnoreCase))
			{
				this.WriteWarning(Strings.WarningChangeOfFederatedIdentitySourceADAttribute("objectGuid"));
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00090BA8 File Offset: 0x0008EDA8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSyncConfig(this.DataObject.Identity.ToString());
			}
		}
	}
}
