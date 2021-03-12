using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBF RID: 3007
	[Serializable]
	public class AttachmentMatchesPatternsPredicate : MatchesPatternsPredicate, IEquatable<AttachmentMatchesPatternsPredicate>
	{
		// Token: 0x06007146 RID: 28998 RVA: 0x001CE571 File Offset: 0x001CC771
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007147 RID: 28999 RVA: 0x001CE57E File Offset: 0x001CC77E
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentMatchesPatternsPredicate)));
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x001CE5B7 File Offset: 0x001CC7B7
		public bool Equals(AttachmentMatchesPatternsPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x06007149 RID: 29001 RVA: 0x001CE5DC File Offset: 0x001CC7DC
		// (set) Token: 0x0600714A RID: 29002 RVA: 0x001CE5E4 File Offset: 0x001CC7E4
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[ConditionParameterName("AttachmentMatchesPatterns")]
		[ExceptionParameterName("ExceptIfAttachmentMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
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

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x0600714B RID: 29003 RVA: 0x001CE5ED File Offset: 0x001CC7ED
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAttachmentMatchesPatterns);
			}
		}

		// Token: 0x0600714C RID: 29004 RVA: 0x001CE5FB File Offset: 0x001CC7FB
		public AttachmentMatchesPatternsPredicate() : this(true)
		{
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x001CE604 File Offset: 0x001CC804
		public AttachmentMatchesPatternsPredicate(bool useLegacyRegex)
		{
			base.UseLegacyRegex = useLegacyRegex;
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x001CE614 File Offset: 0x001CC814
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentMatchesPatterns") && !predicateCondition.Name.Equals("attachmentMatchesRegexPatterns"))
			{
				return null;
			}
			bool useLegacyRegex = !predicateCondition.Name.Equals("attachmentMatchesRegexPatterns");
			Pattern[] array = new Pattern[predicateCondition.Value.RawValues.Count];
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				try
				{
					array[i] = new Pattern(predicateCondition.Value.RawValues[i], useLegacyRegex, false);
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			return new AttachmentMatchesPatternsPredicate(useLegacyRegex)
			{
				Patterns = array
			};
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x001CE6F0 File Offset: 0x001CC8F0
		protected override void ValidateRead(List<ValidationError> errors)
		{
			IEnumerable<ValidationError> enumerable = PatternValidator.ValidatePatterns(this.Patterns, base.Name, false);
			if (enumerable != null)
			{
				errors.AddRange(enumerable);
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x001CE724 File Offset: 0x001CC924
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Pattern pattern in this.Patterns)
			{
				shortList.Add(pattern.ToString());
			}
			string name = base.UseLegacyRegex ? "attachmentMatchesPatterns" : "attachmentMatchesRegexPatterns";
			return TransportRuleParser.Instance.CreatePredicate(name, null, shortList);
		}
	}
}
