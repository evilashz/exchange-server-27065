using System;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000A RID: 10
	public class ContainsPredicate : TextMatchingPredicate
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002C0C File Offset: 0x00000E0C
		public ContainsPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C17 File Offset: 0x00000E17
		public override string Name
		{
			get
			{
				return "contains";
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C20 File Offset: 0x00000E20
		public override Version MinimumVersion
		{
			get
			{
				if (string.CompareOrdinal(base.Property.Name, "Message.SenderDomain") == 0)
				{
					return ContainsPredicate.SenderDomainContainsWordsVersion;
				}
				if (string.CompareOrdinal(base.Property.Name, "Message.ContentCharacterSets") == 0)
				{
					return ContainsPredicate.ContentCharacterSetContainsWordsVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C70 File Offset: 0x00000E70
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			Value result;
			try
			{
				MultiMatcher multiMatcher = new MultiMatcher();
				IMatch matcher = creationContext.MatchFactory.CreateSingleExecutionTermSet(entries);
				multiMatcher.Add(matcher);
				result = Value.CreateValue(multiMatcher, entries);
			}
			catch (ArgumentException innerException)
			{
				throw new RulesValidationException(TextMatchingStrings.KeywordInternalParsingError, innerException);
			}
			return result;
		}

		// Token: 0x04000018 RID: 24
		public static readonly Version SenderDomainContainsWordsVersion = new Version("15.00.0005.00");

		// Token: 0x04000019 RID: 25
		public static readonly Version ContentCharacterSetContainsWordsVersion = new Version("15.00.0005.01");
	}
}
