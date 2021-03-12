using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000A2 RID: 162
	internal class SenderAttributeMatchesPredicate : TextMatchingPredicate
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00016EC6 File Offset: 0x000150C6
		public SenderAttributeMatchesPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00016ED1 File Offset: 0x000150D1
		public override string Name
		{
			get
			{
				return "senderAttributeMatches";
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00016ED8 File Offset: 0x000150D8
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016EDF File Offset: 0x000150DF
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(typeof(string[]), TransportUtils.BuildPatternListForUserAttributeMatchesPredicate(entries));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016F24 File Offset: 0x00015124
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
			return source.Any((string fromAddress) => TransportUtils.UserAttributeMatchesPatterns(context, fromAddress, attributes.ToArray(), this.Name));
		}
	}
}
