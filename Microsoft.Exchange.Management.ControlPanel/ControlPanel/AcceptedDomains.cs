using System;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000251 RID: 593
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class AcceptedDomains : DataSourceService, IAcceptedDomains, IGetListService<AcceptedDomainFilter, AcceptedDomain>
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x0007FDF0 File Offset: 0x0007DFF0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain@C:OrganizationConfig")]
		public PowerShellResults<AcceptedDomain> GetList(AcceptedDomainFilter filter, SortOptions sort)
		{
			PowerShellResults<AcceptedDomain> list = base.GetList<AcceptedDomain, AcceptedDomainFilter>("Get-AcceptedDomain", filter, sort);
			if (list.HasValue)
			{
				list.Output = (from x in list.Output
				where !((AcceptedDomain)x.ConfigurationObject).DomainName.IsStar
				select x).ToArray<AcceptedDomain>();
			}
			return list;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0007FE74 File Offset: 0x0007E074
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain@C:OrganizationConfig")]
		public PowerShellResults<AcceptedDomain> GetManagedDomains(AcceptedDomainFilter filter, SortOptions sort)
		{
			PowerShellResults<AcceptedDomain> list = this.GetList(filter, sort);
			if (list.Failed)
			{
				return list;
			}
			AcceptedDomain[] output = (from x in list.Output
			where x.AuthenticationType != AuthenticationType.Federated
			select x).ToArray<AcceptedDomain>();
			list.Output = output;
			return list;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0007FED8 File Offset: 0x0007E0D8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain@C:OrganizationConfig")]
		public PowerShellResults<AcceptedDomain> GetAcceptedDomainsWithOutExternalRelay(AcceptedDomainFilter filter, SortOptions sort)
		{
			PowerShellResults<AcceptedDomain> list = this.GetList(filter, sort);
			if (list.Failed)
			{
				return list;
			}
			list.Output = (from x in list.Output
			where x.DomainType != AcceptedDomainType.ExternalRelay
			select x).ToArray<AcceptedDomain>();
			return list;
		}

		// Token: 0x04002071 RID: 8305
		private const string Noun = "AcceptedDomain";

		// Token: 0x04002072 RID: 8306
		internal const string GetCmdlet = "Get-AcceptedDomain";

		// Token: 0x04002073 RID: 8307
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x04002074 RID: 8308
		private const string GetListRole = "Get-AcceptedDomain@C:OrganizationConfig";
	}
}
