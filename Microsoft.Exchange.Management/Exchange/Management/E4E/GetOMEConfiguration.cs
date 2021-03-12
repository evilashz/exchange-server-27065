using System;
using System.Management.Automation;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.E4E
{
	// Token: 0x0200032C RID: 812
	[Cmdlet("Get", "OMEConfiguration", DefaultParameterSetName = "Identity")]
	public sealed class GetOMEConfiguration : GetTenantADObjectWithIdentityTaskBase<OMEConfigurationIdParameter, EncryptionConfiguration>
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0007AD7C File Offset: 0x00078F7C
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0007AD93 File Offset: 0x00078F93
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0007ADA8 File Offset: 0x00078FA8
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Organization != null)
			{
				this.SetCurrentOrganizationId();
			}
			if (base.CurrentOrganizationId == null || base.CurrentOrganizationId.OrganizationalUnit == null || string.IsNullOrWhiteSpace(base.CurrentOrganizationId.OrganizationalUnit.Name))
			{
				base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("Organization")), ErrorCategory.InvalidArgument, null);
			}
			string organizationRawIdentity;
			if (this.Organization == null)
			{
				organizationRawIdentity = base.CurrentOrganizationId.OrganizationalUnit.Name;
			}
			else
			{
				organizationRawIdentity = this.Organization.RawIdentity;
			}
			return new EncryptionConfigurationDataProvider(organizationRawIdentity);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007AE3C File Offset: 0x0007903C
		protected override void WriteResult(IConfigurable dataObject)
		{
			EncryptionConfiguration encryptionConfiguration = (EncryptionConfiguration)dataObject;
			encryptionConfiguration.EmailText = HttpUtility.HtmlDecode(encryptionConfiguration.EmailText);
			encryptionConfiguration.PortalText = HttpUtility.HtmlDecode(encryptionConfiguration.PortalText);
			encryptionConfiguration.DisclaimerText = HttpUtility.HtmlDecode(encryptionConfiguration.DisclaimerText);
			base.WriteResult(encryptionConfiguration);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0007AE8C File Offset: 0x0007908C
		private void SetCurrentOrganizationId()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 97, "SetCurrentOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\E4E\\GetEncryptionConfiguration.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
			base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
		}
	}
}
