using System;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000012 RID: 18
	public class MatchesRegexPredicate : TextMatchingPredicate
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002EBE File Offset: 0x000010BE
		public MatchesRegexPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002EC9 File Offset: 0x000010C9
		public override string Name
		{
			get
			{
				return "matchesRegex";
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002ED0 File Offset: 0x000010D0
		public override Version MinimumVersion
		{
			get
			{
				return Rule.BaseVersion15;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002ED7 File Offset: 0x000010D7
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return MatchesRegexPredicate.BuildValueFromList(entries, creationContext);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EE0 File Offset: 0x000010E0
		public static Value BuildValueFromList(ShortList<string> entries, RulesCreationContext creationContext)
		{
			Value result;
			try
			{
				MultiMatcher multiMatcher = new MultiMatcher();
				foreach (string pattern in entries)
				{
					multiMatcher.Add(creationContext.MatchFactory.CreateRegex(pattern, CaseSensitivityMode.Insensitive, MatchRegexOptions.ExplicitCaptures, MatchesRegexPredicate.RegexMatchTimeout));
				}
				result = Value.CreateValue(multiMatcher, entries);
			}
			catch (ArgumentException innerException)
			{
				throw new RulesValidationException(TextMatchingStrings.RegexInternalParsingError, innerException);
			}
			return result;
		}

		// Token: 0x04000022 RID: 34
		public static readonly TimeSpan RegexMatchTimeout = new TimeSpan(0, 0, 30);
	}
}
