using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BDF RID: 3039
	[Serializable]
	public class RecipientAddressContainsWordsPredicate : BifurcationInfoContainsWordsPredicate, IEquatable<RecipientAddressContainsWordsPredicate>
	{
		// Token: 0x06007276 RID: 29302 RVA: 0x001D216A File Offset: 0x001D036A
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x001D2177 File Offset: 0x001D0377
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientAddressContainsWordsPredicate)));
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x001D21B0 File Offset: 0x001D03B0
		public bool Equals(RecipientAddressContainsWordsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x06007279 RID: 29305 RVA: 0x001D21D5 File Offset: 0x001D03D5
		// (set) Token: 0x0600727A RID: 29306 RVA: 0x001D21DD File Offset: 0x001D03DD
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[ExceptionParameterName("ExceptIfRecipientAddressContainsWords")]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("RecipientAddressContainsWords")]
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

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x0600727B RID: 29307 RVA: 0x001D21E6 File Offset: 0x001D03E6
		internal override BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRecipientAddressContains);
			}
		}

		// Token: 0x0600727C RID: 29308 RVA: 0x001D21F4 File Offset: 0x001D03F4
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Words == null || this.Words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			foreach (Word word in this.Words)
			{
				string value = word.Value;
				int index;
				if (!string.IsNullOrEmpty(value) && !Utils.CheckIsUnicodeStringWellFormed(value, out index))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)value[index]), base.Name));
					return;
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x001D2291 File Offset: 0x001D0491
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x001D2294 File Offset: 0x001D0494
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count == 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			RecipientAddressContainsWordsPredicate recipientAddressContainsWordsPredicate = new RecipientAddressContainsWordsPredicate();
			Word[] array = new Word[bifInfo.RecipientAddressContainsWords.Count];
			for (int i = 0; i < bifInfo.RecipientAddressContainsWords.Count; i++)
			{
				try
				{
					array[i] = new Word(bifInfo.RecipientAddressContainsWords[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			recipientAddressContainsWordsPredicate.Words = array;
			return recipientAddressContainsWordsPredicate;
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x001D2420 File Offset: 0x001D0620
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Word word in this.Words)
			{
				ruleBifurcationInfo.RecipientAddressContainsWords.Add(word.ToString());
				ruleBifurcationInfo.Patterns.Add(word.ToString());
			}
			return ruleBifurcationInfo;
		}
	}
}
