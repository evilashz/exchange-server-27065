using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000236 RID: 566
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x00071564 File Offset: 0x0006F764
		public void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00071573 File Offset: 0x0006F773
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			((ICollection)this).CopyTo(rules, index);
		}

		// Token: 0x170003CA RID: 970
		public AuthorizationRule this[int index]
		{
			get
			{
				return base.InnerList[index] as AuthorizationRule;
			}
		}
	}
}
