using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000A1 RID: 161
	internal class SenderAttributeContainsPredicate : TextMatchingPredicate
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00016DC4 File Offset: 0x00014FC4
		public SenderAttributeContainsPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00016DCF File Offset: 0x00014FCF
		public override string Name
		{
			get
			{
				return "senderAttributeContains";
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00016DD6 File Offset: 0x00014FD6
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00016DDD File Offset: 0x00014FDD
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(typeof(string[]), entries);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016E1C File Offset: 0x0001501C
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			context.PredicateName = this.Name;
			object value = base.Value.GetValue(context);
			List<string> attributes = new List<string>();
			string text = value as string;
			if (text != null)
			{
				attributes.Add(text);
			}
			else
			{
				attributes = (List<string>)value;
			}
			if (!attributes.Any<string>())
			{
				return false;
			}
			IEnumerable<string> source = (IEnumerable<string>)MessageProperty.GetMessageFromValue(context);
			return source.Any((string fromAddress) => TransportUtils.UserAttributeContainsWords(context, fromAddress, attributes.ToArray(), this.Name));
		}
	}
}
