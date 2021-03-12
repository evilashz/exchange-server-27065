using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000A3 RID: 163
	internal class SenderAttributeMatchesRegexPredicate : PredicateCondition
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00016FCE File Offset: 0x000151CE
		public SenderAttributeMatchesRegexPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00016FD9 File Offset: 0x000151D9
		public override string Name
		{
			get
			{
				return "senderAttributeMatchesRegex";
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00016FE0 File Offset: 0x000151E0
		public override Version MinimumVersion
		{
			get
			{
				return Rule.BaseVersion15;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016FE7 File Offset: 0x000151E7
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(typeof(string[]), entries);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016FFC File Offset: 0x000151FC
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Value.GetValue(transportRulesEvaluationContext);
			List<string> list = new List<string>();
			string text = value as string;
			if (text != null)
			{
				list.Add(text);
			}
			else
			{
				list = (List<string>)value;
			}
			return transportRulesEvaluationContext.MailItem.FromAddress.IsValid && list.Count != 0 && TransportUtils.UserAttributeMatchesPatterns(transportRulesEvaluationContext, transportRulesEvaluationContext.MailItem.FromAddress.ToString(), list.ToArray(), this.Name);
		}
	}
}
