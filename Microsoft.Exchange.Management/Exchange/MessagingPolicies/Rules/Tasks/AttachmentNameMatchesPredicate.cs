using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC0 RID: 3008
	[Serializable]
	public class AttachmentNameMatchesPredicate : SinglePropertyMatchesPredicate, IEquatable<AttachmentNameMatchesPredicate>
	{
		// Token: 0x06007151 RID: 29009 RVA: 0x001CE798 File Offset: 0x001CC998
		public AttachmentNameMatchesPredicate() : this(true)
		{
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x001CE7A1 File Offset: 0x001CC9A1
		public AttachmentNameMatchesPredicate(bool useLegacyRegex) : base("Message.AttachmentNames", useLegacyRegex)
		{
		}

		// Token: 0x06007153 RID: 29011 RVA: 0x001CE7AF File Offset: 0x001CC9AF
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x001CE7BC File Offset: 0x001CC9BC
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentNameMatchesPredicate)));
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x001CE7F5 File Offset: 0x001CC9F5
		public bool Equals(AttachmentNameMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x06007156 RID: 29014 RVA: 0x001CE81A File Offset: 0x001CCA1A
		// (set) Token: 0x06007157 RID: 29015 RVA: 0x001CE822 File Offset: 0x001CCA22
		[ExceptionParameterName("ExceptIfAttachmentNameMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[ConditionParameterName("AttachmentNameMatchesPatterns")]
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

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x06007158 RID: 29016 RVA: 0x001CE82B File Offset: 0x001CCA2B
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAttachmentNameMatches);
			}
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x001CE839 File Offset: 0x001CCA39
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SinglePropertyMatchesPredicate.CreateFromInternalCondition<AttachmentNameMatchesPredicate>(condition, "Message.AttachmentNames");
		}

		// Token: 0x04003A36 RID: 14902
		private const string InternalPropertyName = "Message.AttachmentNames";
	}
}
