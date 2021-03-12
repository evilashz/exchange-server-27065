using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E8 RID: 2536
	[Cmdlet("Get", "SharingPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetSharingPolicy : GetMultitenancySystemConfigurationObjectTask<SharingPolicyIdParameter, SharingPolicy>
	{
		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0017B1FB File Offset: 0x001793FB
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06005A88 RID: 23176 RVA: 0x0017B1FE File Offset: 0x001793FE
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x0017B204 File Offset: 0x00179404
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			SharingPolicy sharingPolicy = (SharingPolicy)dataObject;
			sharingPolicy.Default = sharingPolicy.Id.Equals(this.FederatedOrganizationId.DefaultSharingPolicyLink);
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06005A8A RID: 23178 RVA: 0x0017B25C File Offset: 0x0017945C
		private FederatedOrganizationId FederatedOrganizationId
		{
			get
			{
				if (this.federatedOrganizationId == null)
				{
					IConfigurationSession configurationSession;
					if (base.SharedConfiguration != null)
					{
						configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(base.CurrentOrganizationId);
					}
					else
					{
						configurationSession = this.ConfigurationSession;
						configurationSession.SessionSettings.IsSharedConfigChecked = true;
					}
					this.federatedOrganizationId = configurationSession.GetFederatedOrganizationId(configurationSession.SessionSettings.CurrentOrganizationId);
				}
				return this.federatedOrganizationId;
			}
		}

		// Token: 0x040033D3 RID: 13267
		private FederatedOrganizationId federatedOrganizationId;
	}
}
