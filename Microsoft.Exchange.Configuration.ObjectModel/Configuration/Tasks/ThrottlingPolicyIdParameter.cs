using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public class ThrottlingPolicyIdParameter : ADIdParameter, IIdentityParameter
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002B3C3 File Offset: 0x000295C3
		public ThrottlingPolicyIdParameter()
		{
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002B3CB File Offset: 0x000295CB
		public ThrottlingPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002B3D4 File Offset: 0x000295D4
		public ThrottlingPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002B3DD File Offset: 0x000295DD
		public ThrottlingPolicyIdParameter(ThrottlingPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002B3EB File Offset: 0x000295EB
		public ThrottlingPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002B3F4 File Offset: 0x000295F4
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002B3F7 File Offset: 0x000295F7
		public static ThrottlingPolicyIdParameter Parse(string identity)
		{
			return new ThrottlingPolicyIdParameter(identity);
		}
	}
}
