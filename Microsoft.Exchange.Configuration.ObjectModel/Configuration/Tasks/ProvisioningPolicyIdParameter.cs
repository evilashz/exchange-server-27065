using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public class ProvisioningPolicyIdParameter : ADIdParameter
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002402E File Offset: 0x0002222E
		public ProvisioningPolicyIdParameter()
		{
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00024036 File Offset: 0x00022236
		public ProvisioningPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002403F File Offset: 0x0002223F
		public ProvisioningPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00024048 File Offset: 0x00022248
		public ProvisioningPolicyIdParameter(ADProvisioningPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00024056 File Offset: 0x00022256
		public ProvisioningPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002405F File Offset: 0x0002225F
		public static ProvisioningPolicyIdParameter Parse(string identity)
		{
			return new ProvisioningPolicyIdParameter(identity);
		}
	}
}
