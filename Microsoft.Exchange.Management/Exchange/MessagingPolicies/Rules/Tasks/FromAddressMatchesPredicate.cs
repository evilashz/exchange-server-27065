using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BCC RID: 3020
	[Serializable]
	public class FromAddressMatchesPredicate : SinglePropertyMatchesPredicate, IEquatable<FromAddressMatchesPredicate>
	{
		// Token: 0x060071BC RID: 29116 RVA: 0x001CF988 File Offset: 0x001CDB88
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x001CF995 File Offset: 0x001CDB95
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as FromAddressMatchesPredicate)));
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x001CF9CE File Offset: 0x001CDBCE
		public bool Equals(FromAddressMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x060071BF RID: 29119 RVA: 0x001CF9F3 File Offset: 0x001CDBF3
		// (set) Token: 0x060071C0 RID: 29120 RVA: 0x001CF9FB File Offset: 0x001CDBFB
		[ExceptionParameterName("ExceptIfFromAddressMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[ConditionParameterName("FromAddressMatchesPatterns")]
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

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x060071C1 RID: 29121 RVA: 0x001CFA04 File Offset: 0x001CDC04
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionFromAddressMatches);
			}
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x001CFA12 File Offset: 0x001CDC12
		public FromAddressMatchesPredicate() : this(true)
		{
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x001CFA1B File Offset: 0x001CDC1B
		public FromAddressMatchesPredicate(bool useLegacyRegex) : base("Message.From", useLegacyRegex)
		{
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x001CFA29 File Offset: 0x001CDC29
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyMatchesPredicate.CreateFromInternalCondition<FromAddressMatchesPredicate>(condition, "Message.From");
		}

		// Token: 0x04003A45 RID: 14917
		private const string InternalPropertyName = "Message.From";
	}
}
