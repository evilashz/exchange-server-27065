using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BF0 RID: 3056
	[Serializable]
	public class SubjectMatchesPredicate : SinglePropertyMatchesPredicate, IEquatable<SubjectMatchesPredicate>
	{
		// Token: 0x06007342 RID: 29506 RVA: 0x001D4F24 File Offset: 0x001D3124
		public SubjectMatchesPredicate() : this(true)
		{
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x001D4F2D File Offset: 0x001D312D
		public SubjectMatchesPredicate(bool useLegacyRegex) : base("Message.Subject", useLegacyRegex)
		{
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x001D4F3B File Offset: 0x001D313B
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x001D4F48 File Offset: 0x001D3148
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SubjectMatchesPredicate)));
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x001D4F81 File Offset: 0x001D3181
		public bool Equals(SubjectMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x001D4FA6 File Offset: 0x001D31A6
		// (set) Token: 0x06007348 RID: 29512 RVA: 0x001D4FAE File Offset: 0x001D31AE
		[ExceptionParameterName("ExceptIfSubjectMatchesPatterns")]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[ConditionParameterName("SubjectMatchesPatterns")]
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

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x001D4FB7 File Offset: 0x001D31B7
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSubjectMatches);
			}
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x001D4FC5 File Offset: 0x001D31C5
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyMatchesPredicate.CreateFromInternalCondition<SubjectMatchesPredicate>(condition, "Message.Subject");
		}

		// Token: 0x04003A93 RID: 14995
		private const string InternalPropertyName = "Message.Subject";
	}
}
