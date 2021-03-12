using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class RecipientEnforcementProvisioningPolicyIdParameter : ProvisioningPolicyIdParameter
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x00024067 File Offset: 0x00022267
		public RecipientEnforcementProvisioningPolicyIdParameter()
		{
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002406F File Offset: 0x0002226F
		public RecipientEnforcementProvisioningPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00024078 File Offset: 0x00022278
		public RecipientEnforcementProvisioningPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00024081 File Offset: 0x00022281
		public RecipientEnforcementProvisioningPolicyIdParameter(ADProvisioningPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002408F File Offset: 0x0002228F
		public RecipientEnforcementProvisioningPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00024098 File Offset: 0x00022298
		public new static ProvisioningPolicyIdParameter Parse(string identity)
		{
			return new RecipientEnforcementProvisioningPolicyIdParameter(identity);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000240A0 File Offset: 0x000222A0
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
