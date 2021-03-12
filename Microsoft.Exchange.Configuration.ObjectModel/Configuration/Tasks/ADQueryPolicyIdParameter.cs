using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public class ADQueryPolicyIdParameter : ADIdParameter
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001E22A File Offset: 0x0001C42A
		public ADQueryPolicyIdParameter()
		{
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001E232 File Offset: 0x0001C432
		public ADQueryPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001E23B File Offset: 0x0001C43B
		public ADQueryPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001E244 File Offset: 0x0001C444
		public ADQueryPolicyIdParameter(ADQueryPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001E252 File Offset: 0x0001C452
		public ADQueryPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001E25B File Offset: 0x0001C45B
		public static ADQueryPolicyIdParameter Parse(string identity)
		{
			return new ADQueryPolicyIdParameter(identity);
		}
	}
}
