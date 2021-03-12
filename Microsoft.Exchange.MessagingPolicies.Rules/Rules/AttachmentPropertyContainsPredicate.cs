using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000095 RID: 149
	internal sealed class AttachmentPropertyContainsPredicate : TextMatchingPredicate
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00015E29 File Offset: 0x00014029
		public AttachmentPropertyContainsPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015E34 File Offset: 0x00014034
		public override string Name
		{
			get
			{
				return "attachmentPropertyContains";
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00015E3B File Offset: 0x0001403B
		public override Version MinimumVersion
		{
			get
			{
				return AttachmentPropertyContainsPredicate.AttachmentPropertyContainsBaseVersion;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00015E42 File Offset: 0x00014042
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			this.searchPropertyValues = AttachmentPropertyContainsPredicate.ParsePredicateParameters(entries);
			if (!this.searchPropertyValues.Any<KeyValuePair<string, List<string>>>())
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidAttachmentPropertyParameter(this.Name));
			}
			return Value.CreateValue(typeof(string[]), entries);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00015E80 File Offset: 0x00014080
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.searchPropertyValues)
			{
				foreach (IDictionary<string, string> dictionary in transportRulesEvaluationContext.Message.AttachmentProperties)
				{
					string text;
					if (dictionary.TryGetValue(keyValuePair.Key, out text))
					{
						bool flag = TransportUtils.IsMatchTplKeyword(text, keyValuePair.Key, keyValuePair.Value, transportRulesEvaluationContext);
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015F60 File Offset: 0x00014160
		internal static List<KeyValuePair<string, List<string>>> ParsePredicateParameters(IEnumerable<string> parameters)
		{
			List<KeyValuePair<string, List<string>>> list = new List<KeyValuePair<string, List<string>>>(parameters.Count<string>());
			foreach (string text in parameters)
			{
				int num = text.IndexOf(':');
				if (num >= 0 && num < text.Length - 1)
				{
					string text2 = text.Substring(0, num).Trim().ToLowerInvariant();
					if (!string.IsNullOrEmpty(text2))
					{
						List<string> list2 = (from w in text.Substring(num + 1).ToLowerInvariant().Split(new char[]
						{
							','
						})
						select w.Trim()).ToList<string>();
						if (!list2.Any(new Func<string, bool>(string.IsNullOrEmpty)))
						{
							list.Add(new KeyValuePair<string, List<string>>(text2, list2));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04000277 RID: 631
		internal static readonly Version AttachmentPropertyContainsBaseVersion = new Version("15.00.0014.00");

		// Token: 0x04000278 RID: 632
		private List<KeyValuePair<string, List<string>>> searchPropertyValues;
	}
}
