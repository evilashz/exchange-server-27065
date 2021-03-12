using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD7 RID: 3031
	[Serializable]
	public class ManagementRelationshipPredicate : BifurcationInfoPredicate, IEquatable<ManagementRelationshipPredicate>
	{
		// Token: 0x06007221 RID: 29217 RVA: 0x001D0871 File Offset: 0x001CEA71
		public override int GetHashCode()
		{
			return this.ManagementRelationship.GetHashCode();
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x001D0883 File Offset: 0x001CEA83
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ManagementRelationshipPredicate)));
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x001D08BC File Offset: 0x001CEABC
		public bool Equals(ManagementRelationshipPredicate other)
		{
			return this.ManagementRelationship.Equals(other.ManagementRelationship);
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x06007224 RID: 29220 RVA: 0x001D08D9 File Offset: 0x001CEAD9
		// (set) Token: 0x06007225 RID: 29221 RVA: 0x001D08E1 File Offset: 0x001CEAE1
		[LocDisplayName(RulesTasksStrings.IDs.ManagementRelationshipDisplayName)]
		[ConditionParameterName("SenderManagementRelationship")]
		[ExceptionParameterName("ExceptIfSenderManagementRelationship")]
		[LocDescription(RulesTasksStrings.IDs.ManagementRelationshipDescription)]
		public ManagementRelationship ManagementRelationship
		{
			get
			{
				return this.managementRelationship;
			}
			set
			{
				this.managementRelationship = value;
			}
		}

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06007226 RID: 29222 RVA: 0x001D08EA File Offset: 0x001CEAEA
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionManagementRelationship(LocalizedDescriptionAttribute.FromEnum(typeof(ManagementRelationship), this.ManagementRelationship));
			}
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x001D0910 File Offset: 0x001CEB10
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x06007228 RID: 29224 RVA: 0x001D0914 File Offset: 0x001CEB14
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
			{
				return null;
			}
			if (bifInfo.InternalRecipients || bifInfo.ExternalRecipients || bifInfo.ExternalPartnerRecipients || bifInfo.ExternalNonPartnerRecipients)
			{
				return null;
			}
			if (string.IsNullOrEmpty(bifInfo.ManagementRelationship))
			{
				return null;
			}
			ManagementRelationshipPredicate managementRelationshipPredicate = new ManagementRelationshipPredicate();
			if (string.Equals(bifInfo.ManagementRelationship, ManagementRelationship.Manager.ToString(), StringComparison.InvariantCultureIgnoreCase))
			{
				managementRelationshipPredicate.ManagementRelationship = ManagementRelationship.Manager;
			}
			else
			{
				managementRelationshipPredicate.ManagementRelationship = ManagementRelationship.DirectReport;
			}
			return managementRelationshipPredicate;
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x001D0A68 File Offset: 0x001CEC68
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			return new RuleBifurcationInfo
			{
				ManagementRelationship = this.ManagementRelationship.ToString()
			};
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x001D0A95 File Offset: 0x001CEC95
		internal override string GetPredicateParameters()
		{
			return Enum.GetName(typeof(ManagementRelationship), this.ManagementRelationship);
		}

		// Token: 0x04003A57 RID: 14935
		private ManagementRelationship managementRelationship;
	}
}
