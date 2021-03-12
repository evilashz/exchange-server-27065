using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE3 RID: 3043
	[Serializable]
	public class RecipientDomainIsPredicate : BifurcationInfoContainsWordsPredicate, IEquatable<RecipientDomainIsPredicate>
	{
		// Token: 0x060072A5 RID: 29349 RVA: 0x001D2E5A File Offset: 0x001D105A
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x001D2E67 File Offset: 0x001D1067
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientDomainIsPredicate)));
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x001D2EA0 File Offset: 0x001D10A0
		public bool Equals(RecipientDomainIsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x060072A8 RID: 29352 RVA: 0x001D2EC5 File Offset: 0x001D10C5
		// (set) Token: 0x060072A9 RID: 29353 RVA: 0x001D2ECD File Offset: 0x001D10CD
		[ConditionParameterName("RecipientDomainIs")]
		[ExceptionParameterName("ExceptIfRecipientDomainIs")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
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

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x060072AA RID: 29354 RVA: 0x001D2ED6 File Offset: 0x001D10D6
		internal override BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRecipientDomainIs);
			}
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x001D2EE4 File Offset: 0x001D10E4
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

		// Token: 0x060072AC RID: 29356 RVA: 0x001D2F81 File Offset: 0x001D1181
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x001D2F84 File Offset: 0x001D1184
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count == 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			RecipientDomainIsPredicate recipientDomainIsPredicate = new RecipientDomainIsPredicate();
			Word[] array = new Word[bifInfo.RecipientDomainIs.Count];
			for (int i = 0; i < bifInfo.RecipientDomainIs.Count; i++)
			{
				try
				{
					array[i] = new Word(bifInfo.RecipientDomainIs[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			recipientDomainIsPredicate.Words = array;
			return recipientDomainIsPredicate;
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x001D3110 File Offset: 0x001D1310
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Word word in this.Words)
			{
				ruleBifurcationInfo.RecipientDomainIs.Add(word.ToString());
			}
			return ruleBifurcationInfo;
		}
	}
}
