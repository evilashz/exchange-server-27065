using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB5 RID: 2997
	[Serializable]
	public class AnyOfRecipientAddressMatchesPredicate : SinglePropertyMatchesPredicate, IEquatable<AnyOfRecipientAddressMatchesPredicate>
	{
		// Token: 0x060070EA RID: 28906 RVA: 0x001CDB71 File Offset: 0x001CBD71
		public AnyOfRecipientAddressMatchesPredicate() : this(true)
		{
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x001CDB7A File Offset: 0x001CBD7A
		public AnyOfRecipientAddressMatchesPredicate(bool useLegacyRegex) : base("Message.EnvelopeRecipients", useLegacyRegex)
		{
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x001CDB88 File Offset: 0x001CBD88
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x001CDB95 File Offset: 0x001CBD95
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AnyOfRecipientAddressMatchesPredicate)));
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x001CDBCE File Offset: 0x001CBDCE
		public bool Equals(AnyOfRecipientAddressMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x060070EF RID: 28911 RVA: 0x001CDBF3 File Offset: 0x001CBDF3
		// (set) Token: 0x060070F0 RID: 28912 RVA: 0x001CDBFB File Offset: 0x001CBDFB
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[ExceptionParameterName("ExceptIfAnyOfRecipientAddressMatchesPatterns")]
		[ConditionParameterName("AnyOfRecipientAddressMatchesPatterns")]
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

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x060070F1 RID: 28913 RVA: 0x001CDC04 File Offset: 0x001CBE04
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAnyOfRecipientAddressMatches);
			}
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x001CDC12 File Offset: 0x001CBE12
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyMatchesPredicate.CreateFromInternalCondition<AnyOfRecipientAddressMatchesPredicate>(condition, "Message.EnvelopeRecipients");
		}

		// Token: 0x04003A29 RID: 14889
		private const string InternalPropertyName = "Message.EnvelopeRecipients";
	}
}
