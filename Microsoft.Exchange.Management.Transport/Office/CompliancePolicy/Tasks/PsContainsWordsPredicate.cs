using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D4 RID: 212
	public abstract class PsContainsWordsPredicate : PsComplianceRulePredicateBase
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00024990 File Offset: 0x00022B90
		protected PsContainsWordsPredicate(IEnumerable<string> words)
		{
			this.Words = new MultiValuedProperty<string>(words);
			this.Words.SetIsReadOnly(false, null);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000249C4 File Offset: 0x00022BC4
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x000249CC File Offset: 0x00022BCC
		public MultiValuedProperty<string> Words { get; protected set; }
	}
}
