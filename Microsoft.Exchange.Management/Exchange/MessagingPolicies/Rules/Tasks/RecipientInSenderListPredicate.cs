using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE4 RID: 3044
	[Serializable]
	public class RecipientInSenderListPredicate : BifurcationInfoPredicate, IEquatable<RecipientInSenderListPredicate>
	{
		// Token: 0x060072B0 RID: 29360 RVA: 0x001D3169 File Offset: 0x001D1369
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Lists);
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x001D3176 File Offset: 0x001D1376
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientInSenderListPredicate)));
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x001D31AF File Offset: 0x001D13AF
		public bool Equals(RecipientInSenderListPredicate other)
		{
			if (this.Lists == null)
			{
				return null == other.Lists;
			}
			return this.Lists.SequenceEqual(other.Lists);
		}

		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x060072B3 RID: 29363 RVA: 0x001D31D4 File Offset: 0x001D13D4
		// (set) Token: 0x060072B4 RID: 29364 RVA: 0x001D31DC File Offset: 0x001D13DC
		[ConditionParameterName("RecipientInSenderList")]
		[ExceptionParameterName("ExceptIfRecipientInSenderList")]
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

		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x060072B5 RID: 29365 RVA: 0x001D31E5 File Offset: 0x001D13E5
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRecipientInSenderList(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Lists), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x001D3211 File Offset: 0x001D1411
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Lists == null || this.Lists.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x001D3243 File Offset: 0x001D1443
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x001D3248 File Offset: 0x001D1448
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count == 0)
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
			RecipientInSenderListPredicate recipientInSenderListPredicate = new RecipientInSenderListPredicate();
			Word[] array = new Word[bifInfo.RecipientInSenderList.Count];
			for (int i = 0; i < bifInfo.RecipientInSenderList.Count; i++)
			{
				array[i] = new Word(bifInfo.RecipientInSenderList[i]);
			}
			recipientInSenderListPredicate.Lists = array;
			return recipientInSenderListPredicate;
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x001D33BC File Offset: 0x001D15BC
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Word word in this.lists)
			{
				ruleBifurcationInfo.RecipientInSenderList.Add(word.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x001D3421 File Offset: 0x001D1621
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Lists
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x001D345A File Offset: 0x001D165A
		internal override void SuppressPiiData()
		{
			this.Lists = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SenderInRecipientList, this.Lists);
		}

		// Token: 0x04003A7A RID: 14970
		private Word[] lists;
	}
}
