using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA9 RID: 2985
	[Serializable]
	public class ADAttributeMatchesTextPredicate : BifurcationInfoPredicate, IEquatable<ADAttributeMatchesTextPredicate>
	{
		// Token: 0x0600708B RID: 28811 RVA: 0x001CCCD9 File Offset: 0x001CAED9
		public override int GetHashCode()
		{
			return this.EvaluatedUser.GetHashCode() ^ (string.IsNullOrEmpty(this.AttributeValue) ? 0 : this.AttributeValue.GetHashCode()) ^ this.ADAttribute.GetHashCode();
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x001CCD18 File Offset: 0x001CAF18
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ADAttributeMatchesTextPredicate)));
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x001CCD54 File Offset: 0x001CAF54
		public bool Equals(ADAttributeMatchesTextPredicate other)
		{
			if (string.IsNullOrEmpty(this.AttributeValue))
			{
				return this.EvaluatedUser.Equals(other.EvaluatedUser) && string.IsNullOrEmpty(other.AttributeValue) && this.ADAttribute.Equals(other.ADAttribute);
			}
			return this.EvaluatedUser.Equals(other.EvaluatedUser) && this.AttributeValue.Equals(other.AttributeValue) && this.ADAttribute.Equals(other.ADAttribute);
		}

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x0600708E RID: 28814 RVA: 0x001CCE03 File Offset: 0x001CB003
		// (set) Token: 0x0600708F RID: 28815 RVA: 0x001CCE0B File Offset: 0x001CB00B
		[LocDisplayName(RulesTasksStrings.IDs.EvaluatedUserDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.EvaluatedUserDescription)]
		public EvaluatedUser EvaluatedUser
		{
			get
			{
				return this.evaluatedUser;
			}
			set
			{
				this.evaluatedUser = value;
			}
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x06007090 RID: 28816 RVA: 0x001CCE14 File Offset: 0x001CB014
		// (set) Token: 0x06007091 RID: 28817 RVA: 0x001CCE1C File Offset: 0x001CB01C
		[LocDisplayName(RulesTasksStrings.IDs.AttributeValueDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.AttributeValueDescription)]
		public string AttributeValue
		{
			get
			{
				return this.attributeValue;
			}
			set
			{
				this.attributeValue = value;
			}
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x06007092 RID: 28818 RVA: 0x001CCE25 File Offset: 0x001CB025
		// (set) Token: 0x06007093 RID: 28819 RVA: 0x001CCE2D File Offset: 0x001CB02D
		[LocDescription(RulesTasksStrings.IDs.ADAttributeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.ADAttributeDisplayName)]
		public ADAttribute ADAttribute
		{
			get
			{
				return this.adAttribute;
			}
			set
			{
				this.adAttribute = value;
			}
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x06007094 RID: 28820 RVA: 0x001CCE36 File Offset: 0x001CB036
		// (set) Token: 0x06007095 RID: 28821 RVA: 0x001CCE3E File Offset: 0x001CB03E
		[LocDescription(RulesTasksStrings.IDs.ADAttributeEvaluationTypeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.ADAttributeEvaluationTypeDisplayName)]
		public ADAttributeEvaluationType ADAttributeEvaluationType
		{
			get
			{
				return this.adAttributeEvaluationType;
			}
			set
			{
				this.adAttributeEvaluationType = value;
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x06007096 RID: 28822 RVA: 0x001CCE48 File Offset: 0x001CB048
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionADAttributeMatchesText(LocalizedDescriptionAttribute.FromEnum(typeof(EvaluatedUser), this.EvaluatedUser), LocalizedDescriptionAttribute.FromEnum(typeof(ADAttribute), this.ADAttribute), LocalizedDescriptionAttribute.FromEnum(typeof(ADAttributeEvaluationType), this.ADAttributeEvaluationType), this.AttributeValue);
			}
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x001CCEB3 File Offset: 0x001CB0B3
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007098 RID: 28824 RVA: 0x001CCEB8 File Offset: 0x001CB0B8
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count == 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			ADAttributeMatchesTextPredicate adattributeMatchesTextPredicate = new ADAttributeMatchesTextPredicate
			{
				ADAttribute = (ADAttribute)Enum.Parse(typeof(ADAttribute), bifInfo.ADAttributesForTextMatch[0]),
				AttributeValue = bifInfo.ADAttributeValue
			};
			if (bifInfo.CheckADAttributeEquality)
			{
				adattributeMatchesTextPredicate.ADAttributeEvaluationType = ADAttributeEvaluationType.Equals;
			}
			else
			{
				adattributeMatchesTextPredicate.ADAttributeEvaluationType = ADAttributeEvaluationType.Contains;
			}
			if (bifInfo.IsSenderEvaluation)
			{
				adattributeMatchesTextPredicate.EvaluatedUser = EvaluatedUser.Sender;
			}
			else
			{
				adattributeMatchesTextPredicate.EvaluatedUser = EvaluatedUser.Recipient;
			}
			return adattributeMatchesTextPredicate;
		}

		// Token: 0x06007099 RID: 28825 RVA: 0x001CD044 File Offset: 0x001CB244
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			return new RuleBifurcationInfo
			{
				ADAttributesForTextMatch = 
				{
					this.ADAttribute.ToString()
				},
				CheckADAttributeEquality = (this.ADAttributeEvaluationType == ADAttributeEvaluationType.Equals),
				IsSenderEvaluation = (this.EvaluatedUser == EvaluatedUser.Sender),
				ADAttributeValue = this.AttributeValue
			};
		}

		// Token: 0x04003A16 RID: 14870
		private ADAttribute adAttribute;

		// Token: 0x04003A17 RID: 14871
		private string attributeValue;

		// Token: 0x04003A18 RID: 14872
		private EvaluatedUser evaluatedUser;

		// Token: 0x04003A19 RID: 14873
		private ADAttributeEvaluationType adAttributeEvaluationType;
	}
}
