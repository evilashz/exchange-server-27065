using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class DehydrateableMailboxPolicyIdParameter : ADIdParameter, IIdentityParameter
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00022CFD File Offset: 0x00020EFD
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00022D00 File Offset: 0x00020F00
		public DehydrateableMailboxPolicyIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00022D09 File Offset: 0x00020F09
		public DehydrateableMailboxPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00022D12 File Offset: 0x00020F12
		public DehydrateableMailboxPolicyIdParameter(MailboxPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00022D20 File Offset: 0x00020F20
		public DehydrateableMailboxPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00022D29 File Offset: 0x00020F29
		public DehydrateableMailboxPolicyIdParameter()
		{
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00022D31 File Offset: 0x00020F31
		public static DehydrateableMailboxPolicyIdParameter Parse(string rawString)
		{
			return new DehydrateableMailboxPolicyIdParameter(rawString);
		}
	}
}
