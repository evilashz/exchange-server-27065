using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA7 RID: 2983
	[Serializable]
	public class ADAttributeComparisonPredicate : BifurcationInfoPredicate, IEquatable<ADAttributeComparisonPredicate>
	{
		// Token: 0x0600707E RID: 28798 RVA: 0x001CC9A9 File Offset: 0x001CABA9
		public override int GetHashCode()
		{
			return this.ADAttribute.GetHashCode() ^ this.Evaluation.GetHashCode();
		}

		// Token: 0x0600707F RID: 28799 RVA: 0x001CC9CC File Offset: 0x001CABCC
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ADAttributeComparisonPredicate)));
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x001CCA05 File Offset: 0x001CAC05
		public bool Equals(ADAttributeComparisonPredicate other)
		{
			return this.ADAttribute.Equals(other.ADAttribute) && this.Evaluation.Equals(other.Evaluation);
		}

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x06007081 RID: 28801 RVA: 0x001CCA41 File Offset: 0x001CAC41
		// (set) Token: 0x06007082 RID: 28802 RVA: 0x001CCA49 File Offset: 0x001CAC49
		[ExceptionParameterName("ExceptIfADComparisonAttribute")]
		[LocDisplayName(RulesTasksStrings.IDs.ADAttributeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ADAttributeDescription)]
		[ConditionParameterName("ADComparisonAttribute")]
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

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x06007083 RID: 28803 RVA: 0x001CCA52 File Offset: 0x001CAC52
		// (set) Token: 0x06007084 RID: 28804 RVA: 0x001CCA5A File Offset: 0x001CAC5A
		[ExceptionParameterName("ExceptIfADComparisonOperator")]
		[ConditionParameterName("ADComparisonOperator")]
		[LocDescription(RulesTasksStrings.IDs.EvaluationDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.EvaluationDisplayName)]
		public Evaluation Evaluation
		{
			get
			{
				return this.evaluation;
			}
			set
			{
				this.evaluation = value;
			}
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x06007085 RID: 28805 RVA: 0x001CCA63 File Offset: 0x001CAC63
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionADAttributeComparison(LocalizedDescriptionAttribute.FromEnum(typeof(ADAttribute), this.ADAttribute), LocalizedDescriptionAttribute.FromEnum(typeof(Evaluation), this.Evaluation));
			}
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x001CCAA3 File Offset: 0x001CACA3
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x001CCAA8 File Offset: 0x001CACA8
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count == 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			ADAttributeComparisonPredicate adattributeComparisonPredicate = new ADAttributeComparisonPredicate
			{
				ADAttribute = (ADAttribute)Enum.Parse(typeof(ADAttribute), bifInfo.ADAttributes[0])
			};
			if (bifInfo.CheckADAttributeEquality)
			{
				adattributeComparisonPredicate.Evaluation = Evaluation.Equal;
			}
			else
			{
				adattributeComparisonPredicate.Evaluation = Evaluation.NotEqual;
			}
			return adattributeComparisonPredicate;
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x001CCC10 File Offset: 0x001CAE10
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			return new RuleBifurcationInfo
			{
				ADAttributes = 
				{
					this.ADAttribute.ToString()
				},
				CheckADAttributeEquality = (this.Evaluation == Evaluation.Equal)
			};
		}

		// Token: 0x06007089 RID: 28809 RVA: 0x001CCC54 File Offset: 0x001CAE54
		internal override string ToCmdletParameter(bool isException = false)
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				isException ? "ExceptIfADComparisonAttribute" : "ADComparisonAttribute",
				Enum.GetName(typeof(ADAttribute), this.ADAttribute),
				isException ? "ExceptIfADComparisonOperator" : "ADComparisonOperator",
				Enum.GetName(typeof(Evaluation), this.Evaluation)
			});
		}

		// Token: 0x04003A11 RID: 14865
		private ADAttribute adAttribute;

		// Token: 0x04003A12 RID: 14866
		private Evaluation evaluation;
	}
}
