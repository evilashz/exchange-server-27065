using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA2 RID: 2978
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsRecipientStringCondition : StringCondition
	{
		// Token: 0x06006ABA RID: 27322 RVA: 0x001C798C File Offset: 0x001C5B8C
		private ContainsRecipientStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsRecipientStringCondition, rule, text)
		{
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x001C7998 File Offset: 0x001C5B98
		public static ContainsRecipientStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsRecipientStringCondition(rule, text);
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06006ABC RID: 27324 RVA: 0x001C79C1 File Offset: 0x001C5BC1
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x001C79C4 File Offset: 0x001C5BC4
		internal override Restriction BuildRestriction()
		{
			byte[][] array = new byte[base.Text.Length][];
			for (int i = 0; i < base.Text.Length; i++)
			{
				string s = base.Text[i].ToUpperInvariant();
				array[i] = CTSGlobals.AsciiEncoding.GetBytes(s);
			}
			Restriction restriction = Condition.CreateORSearchKeyContentRestriction(array, PropTag.SearchKey, ContentFlags.SubString);
			return new Restriction.RecipientRestriction(restriction);
		}
	}
}
