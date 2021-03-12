using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE0 RID: 3040
	[Serializable]
	public class RecipientAddressMatchesPatternsPredicate : BifurcationInfoMatchesPatternsPredicate, IEquatable<RecipientAddressMatchesPatternsPredicate>
	{
		// Token: 0x06007281 RID: 29313 RVA: 0x001D2491 File Offset: 0x001D0691
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x001D249E File Offset: 0x001D069E
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientAddressMatchesPatternsPredicate)));
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x001D24D7 File Offset: 0x001D06D7
		public bool Equals(RecipientAddressMatchesPatternsPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x06007284 RID: 29316 RVA: 0x001D24FC File Offset: 0x001D06FC
		// (set) Token: 0x06007285 RID: 29317 RVA: 0x001D2504 File Offset: 0x001D0704
		[ExceptionParameterName("ExceptIfRecipientAddressMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[ConditionParameterName("RecipientAddressMatchesPatterns")]
		public override Pattern[] Patterns
		{
			get
			{
				return this.patterns;
			}
			set
			{
				this.patterns = value;
			}
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x06007286 RID: 29318 RVA: 0x001D250D File Offset: 0x001D070D
		internal override BifurcationInfoMatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new BifurcationInfoMatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRecipientAddressMatches);
			}
		}

		// Token: 0x06007287 RID: 29319 RVA: 0x001D251B File Offset: 0x001D071B
		public RecipientAddressMatchesPatternsPredicate() : this(false)
		{
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x001D2524 File Offset: 0x001D0724
		public RecipientAddressMatchesPatternsPredicate(bool useLegacyRegex)
		{
			base.UseLegacyRegex = useLegacyRegex;
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x001D2533 File Offset: 0x001D0733
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x001D2538 File Offset: 0x001D0738
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || (bifInfo.RecipientMatchesPatterns.Count == 0 && bifInfo.RecipientMatchesRegexPatterns.Count == 0) || bifInfo.RecipientAttributeContains.Count > 0 || bifInfo.RecipientAttributeMatches.Count > 0 || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			bool flag = bifInfo.RecipientMatchesPatterns.Count > 0;
			RecipientAddressMatchesPatternsPredicate recipientAddressMatchesPatternsPredicate = new RecipientAddressMatchesPatternsPredicate(flag);
			List<string> list = flag ? bifInfo.RecipientMatchesPatterns : bifInfo.RecipientMatchesRegexPatterns;
			Pattern[] array = new Pattern[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				try
				{
					array[i] = new Pattern(list[i], flag, false);
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			recipientAddressMatchesPatternsPredicate.Patterns = array;
			return recipientAddressMatchesPatternsPredicate;
		}

		// Token: 0x0600728B RID: 29323 RVA: 0x001D26F0 File Offset: 0x001D08F0
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Pattern pattern in this.Patterns)
			{
				if (base.UseLegacyRegex)
				{
					ruleBifurcationInfo.RecipientMatchesPatterns.Add(pattern.ToString());
				}
				else
				{
					ruleBifurcationInfo.RecipientMatchesRegexPatterns.Add(pattern.ToString());
				}
				ruleBifurcationInfo.Patterns.Add(pattern.ToString());
			}
			return ruleBifurcationInfo;
		}
	}
}
