using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BEE RID: 3054
	[Serializable]
	public class SentToScopePredicate : BifurcationInfoPredicate, IEquatable<SentToScopePredicate>
	{
		// Token: 0x0600732D RID: 29485 RVA: 0x001D4B82 File Offset: 0x001D2D82
		public override int GetHashCode()
		{
			return this.Scope.GetHashCode();
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x001D4B94 File Offset: 0x001D2D94
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SentToScopePredicate)));
		}

		// Token: 0x0600732F RID: 29487 RVA: 0x001D4BCD File Offset: 0x001D2DCD
		public bool Equals(SentToScopePredicate other)
		{
			return this.Scope.Equals(other.Scope);
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x06007330 RID: 29488 RVA: 0x001D4BEA File Offset: 0x001D2DEA
		// (set) Token: 0x06007331 RID: 29489 RVA: 0x001D4BF2 File Offset: 0x001D2DF2
		[ConditionParameterName("SentToScope")]
		[ExceptionParameterName("ExceptIfSentToScope")]
		[LocDescription(RulesTasksStrings.IDs.ToScopeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.ToScopeDisplayName)]
		public ToUserScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x06007332 RID: 29490 RVA: 0x001D4BFB File Offset: 0x001D2DFB
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSentToScope(LocalizedDescriptionAttribute.FromEnum(typeof(ToUserScope), this.Scope));
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x06007333 RID: 29491 RVA: 0x001D4C24 File Offset: 0x001D2E24
		[LocDescription(RulesTasksStrings.IDs.SubTypeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SubTypeDisplayName)]
		public override IEnumerable<RuleSubType> RuleSubTypes
		{
			get
			{
				return new RuleSubType[]
				{
					RuleSubType.None,
					RuleSubType.Dlp
				};
			}
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x001D4C44 File Offset: 0x001D2E44
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(bifInfo.ManagementRelationship))
			{
				return null;
			}
			SentToScopePredicate sentToScopePredicate = new SentToScopePredicate();
			if (bifInfo.InternalRecipients && !bifInfo.ExternalRecipients && !bifInfo.ExternalPartnerRecipients && !bifInfo.ExternalNonPartnerRecipients)
			{
				sentToScopePredicate.Scope = ToUserScope.InOrganization;
				return sentToScopePredicate;
			}
			if (!bifInfo.InternalRecipients && bifInfo.ExternalRecipients && !bifInfo.ExternalPartnerRecipients && !bifInfo.ExternalNonPartnerRecipients)
			{
				sentToScopePredicate.Scope = ToUserScope.NotInOrganization;
				return sentToScopePredicate;
			}
			if (!bifInfo.InternalRecipients && !bifInfo.ExternalRecipients && bifInfo.ExternalPartnerRecipients && !bifInfo.ExternalNonPartnerRecipients)
			{
				sentToScopePredicate.Scope = ToUserScope.ExternalPartner;
				return sentToScopePredicate;
			}
			if (!bifInfo.InternalRecipients && !bifInfo.ExternalRecipients && !bifInfo.ExternalPartnerRecipients && bifInfo.ExternalNonPartnerRecipients)
			{
				sentToScopePredicate.Scope = ToUserScope.ExternalNonPartner;
				return sentToScopePredicate;
			}
			return null;
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x001D4DEE File Offset: 0x001D2FEE
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x001D4DF1 File Offset: 0x001D2FF1
		internal override void Reset()
		{
			this.scope = ToUserScope.InOrganization;
			base.Reset();
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x001D4E00 File Offset: 0x001D3000
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			switch (this.scope)
			{
			case ToUserScope.InOrganization:
				ruleBifurcationInfo.InternalRecipients = true;
				break;
			case ToUserScope.NotInOrganization:
				ruleBifurcationInfo.ExternalRecipients = true;
				break;
			case ToUserScope.ExternalPartner:
				ruleBifurcationInfo.ExternalPartnerRecipients = true;
				break;
			case ToUserScope.ExternalNonPartner:
				ruleBifurcationInfo.ExternalNonPartnerRecipients = true;
				break;
			default:
				return null;
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x001D4E5C File Offset: 0x001D305C
		internal override string GetPredicateParameters()
		{
			return Enum.GetName(typeof(ToUserScope), this.Scope);
		}

		// Token: 0x04003A91 RID: 14993
		private ToUserScope scope;
	}
}
