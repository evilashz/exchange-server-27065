using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	public class InterceptorRuleIdParameter : ADIdParameter
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00022BBE File Offset: 0x00020DBE
		public InterceptorRuleIdParameter()
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00022BC6 File Offset: 0x00020DC6
		public InterceptorRuleIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00022BCF File Offset: 0x00020DCF
		public InterceptorRuleIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00022BD8 File Offset: 0x00020DD8
		public InterceptorRuleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00022BE1 File Offset: 0x00020DE1
		public static explicit operator string(InterceptorRuleIdParameter interceptorRuleIdParameter)
		{
			return interceptorRuleIdParameter.ToString();
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00022BE9 File Offset: 0x00020DE9
		public static InterceptorRuleIdParameter Parse(string identity)
		{
			return new InterceptorRuleIdParameter(identity);
		}
	}
}
