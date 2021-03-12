using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public class RoleIdParameter : ADIdParameter
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x0002A40E File Offset: 0x0002860E
		public RoleIdParameter()
		{
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002A416 File Offset: 0x00028616
		public RoleIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002A41F File Offset: 0x0002861F
		public RoleIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002A428 File Offset: 0x00028628
		public RoleIdParameter(ExchangeRole role) : base(role.Id)
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002A436 File Offset: 0x00028636
		public RoleIdParameter(ExchangeRoleAssignmentPresentation assignment) : base(assignment.Role)
		{
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002A444 File Offset: 0x00028644
		public RoleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0002A44D File Offset: 0x0002864D
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002A450 File Offset: 0x00028650
		public static RoleIdParameter Parse(string identity)
		{
			return new RoleIdParameter(identity);
		}
	}
}
