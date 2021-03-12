using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE9 RID: 3049
	[Serializable]
	public class SenderInRecipientListPredicate : BifurcationInfoPredicate, IEquatable<SenderInRecipientListPredicate>
	{
		// Token: 0x060072F2 RID: 29426 RVA: 0x001D3ECE File Offset: 0x001D20CE
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Lists);
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x001D3EDB File Offset: 0x001D20DB
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SenderInRecipientListPredicate)));
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x001D3F14 File Offset: 0x001D2114
		public bool Equals(SenderInRecipientListPredicate other)
		{
			if (this.Lists == null)
			{
				return null == other.Lists;
			}
			return this.Lists.SequenceEqual(other.Lists);
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x060072F5 RID: 29429 RVA: 0x001D3F39 File Offset: 0x001D2139
		// (set) Token: 0x060072F6 RID: 29430 RVA: 0x001D3F41 File Offset: 0x001D2141
		[ExceptionParameterName("ExceptIfSenderInRecipientList")]
		[ConditionParameterName("SenderInRecipientList")]
		[LocDisplayName(RulesTasksStrings.IDs.ListsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ListsDescription)]
		public Word[] Lists
		{
			get
			{
				return this.lists;
			}
			set
			{
				this.lists = value;
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x060072F7 RID: 29431 RVA: 0x001D3F4A File Offset: 0x001D214A
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSenderInRecipientList(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Lists), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x001D3F76 File Offset: 0x001D2176
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Lists == null || this.Lists.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x001D3FA8 File Offset: 0x001D21A8
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x001D3FAC File Offset: 0x001D21AC
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count == 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			SenderInRecipientListPredicate senderInRecipientListPredicate = new SenderInRecipientListPredicate();
			Word[] array = new Word[bifInfo.SenderInRecipientList.Count];
			for (int i = 0; i < bifInfo.SenderInRecipientList.Count; i++)
			{
				array[i] = new Word(bifInfo.SenderInRecipientList[i]);
			}
			senderInRecipientListPredicate.Lists = array;
			return senderInRecipientListPredicate;
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x001D4120 File Offset: 0x001D2320
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Word word in this.lists)
			{
				ruleBifurcationInfo.SenderInRecipientList.Add(word.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x001D4185 File Offset: 0x001D2385
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Lists
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x001D41BE File Offset: 0x001D23BE
		internal override void SuppressPiiData()
		{
			this.Lists = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SenderInRecipientList, this.Lists);
		}

		// Token: 0x04003A83 RID: 14979
		private Word[] lists;
	}
}
