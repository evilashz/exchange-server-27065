using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE1 RID: 3041
	[Serializable]
	public class RecipientAttributeContainsPredicate : BifurcationInfoContainsWordsPredicate, IEquatable<RecipientAttributeContainsPredicate>
	{
		// Token: 0x0600728C RID: 29324 RVA: 0x001D277B File Offset: 0x001D097B
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x001D2788 File Offset: 0x001D0988
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientAttributeContainsPredicate)));
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x001D27C1 File Offset: 0x001D09C1
		public bool Equals(RecipientAttributeContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x0600728F RID: 29327 RVA: 0x001D27E6 File Offset: 0x001D09E6
		// (set) Token: 0x06007290 RID: 29328 RVA: 0x001D27EE File Offset: 0x001D09EE
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("RecipientADAttributeContainsWords")]
		[ExceptionParameterName("ExceptIfRecipientADAttributeContainsWords")]
		public override Word[] Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x001D27F7 File Offset: 0x001D09F7
		internal override BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRecipientAttributeContains);
			}
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x001D2808 File Offset: 0x001D0A08
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Words == null || this.Words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			Word[] words = this.Words;
			int i = 0;
			while (i < words.Length)
			{
				Word word = words[i];
				string value = word.Value;
				int index;
				if (!string.IsNullOrEmpty(value) && !Utils.CheckIsUnicodeStringWellFormed(value, out index))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)value[index]), base.Name));
				}
				else
				{
					int num = value.IndexOf(':');
					if (num >= 0)
					{
						string text = value.Substring(0, num).Trim().ToLowerInvariant();
						if (TransportUtils.GetDisclaimerMacroLookupTable().ContainsKey(text))
						{
							i++;
							continue;
						}
						errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidMacroName(text), base.Name));
					}
					else
					{
						errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.MacroNameNotSpecified(value), base.Name));
					}
				}
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007293 RID: 29331 RVA: 0x001D2910 File Offset: 0x001D0B10
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x001D2914 File Offset: 0x001D0B14
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count == 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (bifInfo.InternalRecipients || bifInfo.ExternalRecipients || bifInfo.ExternalPartnerRecipients || bifInfo.ExternalNonPartnerRecipients)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(bifInfo.ManagementRelationship))
			{
				return null;
			}
			RecipientAttributeContainsPredicate recipientAttributeContainsPredicate = new RecipientAttributeContainsPredicate();
			Word[] array = new Word[bifInfo.RecipientAttributeContains.Count];
			for (int i = 0; i < bifInfo.RecipientAttributeContains.Count; i++)
			{
				try
				{
					array[i] = new Word(bifInfo.RecipientAttributeContains[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			recipientAttributeContainsPredicate.Words = array;
			return recipientAttributeContainsPredicate;
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x001D2AA0 File Offset: 0x001D0CA0
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Word word in this.words)
			{
				ruleBifurcationInfo.RecipientAttributeContains.Add(word.ToString());
				ruleBifurcationInfo.Patterns.Add(word.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x001D2B09 File Offset: 0x001D0D09
		internal override void SuppressPiiData()
		{
			this.Words = Utils.RedactNameValuePairWords(this.Words);
		}
	}
}
