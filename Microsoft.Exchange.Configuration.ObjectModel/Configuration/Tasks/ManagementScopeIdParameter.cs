using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public class ManagementScopeIdParameter : ADIdParameter
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x0002B2CA File Offset: 0x000294CA
		public ManagementScopeIdParameter()
		{
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002B2D2 File Offset: 0x000294D2
		public ManagementScopeIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002B2DB File Offset: 0x000294DB
		public ManagementScopeIdParameter(ManagementScope managementScope) : base(managementScope.Id)
		{
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002B2E9 File Offset: 0x000294E9
		public ManagementScopeIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002B2F2 File Offset: 0x000294F2
		protected ManagementScopeIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002B2FB File Offset: 0x000294FB
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002B2FE File Offset: 0x000294FE
		public static ManagementScopeIdParameter Parse(string identity)
		{
			return new ManagementScopeIdParameter(identity);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002B308 File Offset: 0x00029508
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession)
		{
			ADObjectId orgContainerId = scSession.GetOrgContainerId();
			return orgContainerId.GetDescendantId(ManagementScope.RdnScopesContainerToOrganization);
		}
	}
}
