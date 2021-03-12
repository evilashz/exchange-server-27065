using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000186 RID: 390
	internal sealed class StringProperty : Property
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		public StringProperty(string name) : base(name, typeof(string))
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002C4E3 File Offset: 0x0002A6E3
		protected override object OnGetValue(RulesEvaluationContext context)
		{
			throw new NotSupportedException("Outlook Protection rules are only executed on Outlook.");
		}
	}
}
