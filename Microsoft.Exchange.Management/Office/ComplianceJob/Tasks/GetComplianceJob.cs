using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000142 RID: 322
	public abstract class GetComplianceJob<TDataObject> : GetTenantADObjectWithIdentityTaskBase<ComplianceJobIdParameter, TDataObject> where TDataObject : ComplianceJob, new()
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00035CBE File Offset: 0x00033EBE
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x00035CD5 File Offset: 0x00033ED5
		[Parameter]
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00035CE8 File Offset: 0x00033EE8
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00035CF0 File Offset: 0x00033EF0
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00035CF9 File Offset: 0x00033EF9
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00035D00 File Offset: 0x00033F00
		protected override IConfigDataProvider CreateSession()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			return new ComplianceJobProvider(base.ExchangeRunspaceConfig.OrganizationId);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00035D34 File Offset: 0x00033F34
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization == null)
			{
				return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.RescopeToSubtree(sessionSettings), 96, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ComplianceJob\\GetComplianceJob.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			return adorganizationalUnit.OrganizationId;
		}
	}
}
