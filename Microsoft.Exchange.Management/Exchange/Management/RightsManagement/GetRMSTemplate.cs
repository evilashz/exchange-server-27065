using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000709 RID: 1801
	[Cmdlet("Get", "RMSTemplate", DefaultParameterSetName = "OrganizationSet")]
	public sealed class GetRMSTemplate : GetTenantADObjectWithIdentityTaskBase<RmsTemplateIdParameter, RmsTemplatePresentation>
	{
		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x00105BC6 File Offset: 0x00103DC6
		// (set) Token: 0x06003FFB RID: 16379 RVA: 0x00105BDD File Offset: 0x00103DDD
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "OrganizationSet")]
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

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x00105BF0 File Offset: 0x00103DF0
		// (set) Token: 0x06003FFD RID: 16381 RVA: 0x00105C07 File Offset: 0x00103E07
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override RmsTemplateIdParameter Identity
		{
			get
			{
				return (RmsTemplateIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x00105C1A File Offset: 0x00103E1A
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x00105C31 File Offset: 0x00103E31
		[Parameter(Mandatory = false)]
		public RmsTrustedPublishingDomainIdParameter TrustedPublishingDomain
		{
			get
			{
				return (RmsTrustedPublishingDomainIdParameter)base.Fields["TrustedPublishingDomain"];
			}
			set
			{
				base.Fields["TrustedPublishingDomain"] = value;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x00105C44 File Offset: 0x00103E44
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x00105C4C File Offset: 0x00103E4C
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x00105C55 File Offset: 0x00103E55
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x00105C6C File Offset: 0x00103E6C
		[Parameter(Mandatory = false)]
		public RmsTemplateType Type
		{
			get
			{
				return (RmsTemplateType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x00105C84 File Offset: 0x00103E84
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x00105C8C File Offset: 0x00103E8C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 98, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\rms\\GetRMSTemplate.cs");
			RmsTemplateType typeToFetch = (this.TrustedPublishingDomain != null) ? RmsTemplateType.All : RmsTemplateType.Distributed;
			bool flag = false;
			if (base.Fields.IsModified("Type"))
			{
				typeToFetch = this.Type;
				flag = true;
			}
			RMSTrustedPublishingDomain rmstrustedPublishingDomain = null;
			if (this.TrustedPublishingDomain != null)
			{
				rmstrustedPublishingDomain = (RMSTrustedPublishingDomain)base.GetDataObject<RMSTrustedPublishingDomain>(this.TrustedPublishingDomain, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorTrustedPublishingDomainNotFound(this.TrustedPublishingDomain.ToString())), new LocalizedString?(Strings.ErrorTrustedPublishingDomainNotUnique(this.TrustedPublishingDomain.ToString())));
			}
			return new RmsTemplateDataProvider(tenantOrTopologyConfigurationSession, typeToFetch, flag || rmstrustedPublishingDomain != null, rmstrustedPublishingDomain);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x00105D4C File Offset: 0x00103F4C
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 153, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\rms\\GetRMSTemplate.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x00105DF9 File Offset: 0x00103FF9
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception);
		}
	}
}
