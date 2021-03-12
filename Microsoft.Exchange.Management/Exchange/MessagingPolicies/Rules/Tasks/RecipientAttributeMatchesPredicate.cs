using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE2 RID: 3042
	[Serializable]
	public class RecipientAttributeMatchesPredicate : BifurcationInfoMatchesPatternsPredicate, IEquatable<RecipientAttributeMatchesPredicate>
	{
		// Token: 0x06007298 RID: 29336 RVA: 0x001D2B24 File Offset: 0x001D0D24
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x001D2B31 File Offset: 0x001D0D31
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RecipientAttributeMatchesPredicate)));
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x001D2B6A File Offset: 0x001D0D6A
		public bool Equals(RecipientAttributeMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x0600729B RID: 29339 RVA: 0x001D2B8F File Offset: 0x001D0D8F
		// (set) Token: 0x0600729C RID: 29340 RVA: 0x001D2B97 File Offset: 0x001D0D97
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[ExceptionParameterName("ExceptIfRecipientADAttributeMatchesPatterns")]
		[ConditionParameterName("RecipientADAttributeMatchesPatterns")]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
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

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x0600729D RID: 29341 RVA: 0x001D2BA0 File Offset: 0x001D0DA0
		internal override BifurcationInfoMatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new BifurcationInfoMatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRecipientAttributeMatches);
			}
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x001D2BAE File Offset: 0x001D0DAE
		public RecipientAttributeMatchesPredicate() : this(false)
		{
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x001D2BB7 File Offset: 0x001D0DB7
		internal RecipientAttributeMatchesPredicate(bool useLegacyRegex)
		{
			base.UseLegacyRegex = useLegacyRegex;
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x001D2BC8 File Offset: 0x001D0DC8
		protected override void ValidateRead(List<ValidationError> errors)
		{
			IEnumerable<ValidationError> enumerable = PatternValidator.ValidateAdAttributePatterns(this.Patterns, base.Name, base.UseLegacyRegex);
			if (enumerable != null)
			{
				errors.AddRange(enumerable);
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x001D2BFF File Offset: 0x001D0DFF
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return null;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x001D2C04 File Offset: 0x001D0E04
		internal static TransportRulePredicate CreatePredicateFromBifInfo(RuleBifurcationInfo bifInfo)
		{
			if (bifInfo.ADAttributesForTextMatch.Count > 0 || bifInfo.ADAttributes.Count > 0 || bifInfo.Managers.Count > 0 || bifInfo.Recipients.Count > 0 || bifInfo.Lists.Count > 0 || bifInfo.FromRecipients.Count > 0 || bifInfo.FromLists.Count > 0 || bifInfo.Partners.Count > 0 || bifInfo.RecipientAddressContainsWords.Count > 0 || bifInfo.RecipientDomainIs.Count > 0 || bifInfo.RecipientMatchesPatterns.Count > 0 || bifInfo.RecipientAttributeContains.Count > 0 || (bifInfo.RecipientAttributeMatches.Count == 0 && bifInfo.RecipientAttributeMatchesRegex.Count == 0) || bifInfo.SenderInRecipientList.Count > 0 || bifInfo.RecipientInSenderList.Count > 0)
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
			bool flag = bifInfo.RecipientAttributeMatches.Count > 0;
			RecipientAttributeMatchesPredicate recipientAttributeMatchesPredicate = new RecipientAttributeMatchesPredicate(flag);
			List<string> list = flag ? bifInfo.RecipientAttributeMatches : bifInfo.RecipientAttributeMatchesRegex;
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
			recipientAttributeMatchesPredicate.patterns = array;
			return recipientAttributeMatchesPredicate;
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x001D2DBC File Offset: 0x001D0FBC
		internal override RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo)
		{
			additionalBifurcationInfo = null;
			RuleBifurcationInfo ruleBifurcationInfo = new RuleBifurcationInfo();
			foreach (Pattern pattern in this.patterns)
			{
				if (base.UseLegacyRegex)
				{
					ruleBifurcationInfo.RecipientAttributeMatches.Add(pattern.ToString());
				}
				else
				{
					ruleBifurcationInfo.RecipientAttributeMatchesRegex.Add(pattern.ToString());
				}
				ruleBifurcationInfo.Patterns.Add(pattern.ToString());
			}
			return ruleBifurcationInfo;
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x001D2E47 File Offset: 0x001D1047
		internal override void SuppressPiiData()
		{
			this.Patterns = Utils.RedactNameValuePairPatterns(this.Patterns);
		}
	}
}
