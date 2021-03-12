using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE7 RID: 3047
	[Serializable]
	public class SenderAttributeMatchesPredicate : MatchesPatternsPredicate, IEquatable<SenderAttributeMatchesPredicate>
	{
		// Token: 0x060072D4 RID: 29396 RVA: 0x001D39F0 File Offset: 0x001D1BF0
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x001D39FD File Offset: 0x001D1BFD
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SenderAttributeMatchesPredicate)));
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x001D3A36 File Offset: 0x001D1C36
		public bool Equals(SenderAttributeMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x001D3A5B File Offset: 0x001D1C5B
		// (set) Token: 0x060072D8 RID: 29400 RVA: 0x001D3A63 File Offset: 0x001D1C63
		[ConditionParameterName("SenderADAttributeMatchesPatterns")]
		[ExceptionParameterName("ExceptIfSenderADAttributeMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
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

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x060072D9 RID: 29401 RVA: 0x001D3A6C File Offset: 0x001D1C6C
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSenderAttributeMatches);
			}
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x001D3A7A File Offset: 0x001D1C7A
		public SenderAttributeMatchesPredicate() : this(true)
		{
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x001D3A83 File Offset: 0x001D1C83
		public SenderAttributeMatchesPredicate(bool useLegacyRegex)
		{
			base.UseLegacyRegex = useLegacyRegex;
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x001D3A94 File Offset: 0x001D1C94
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("senderAttributeMatches") && !predicateCondition.Name.Equals("senderAttributeMatchesRegex"))
			{
				return null;
			}
			bool useLegacyRegex = !predicateCondition.Name.Equals("senderAttributeMatchesRegex");
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
			return new SenderAttributeMatchesPredicate(useLegacyRegex)
			{
				Patterns = array
			};
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x001D3B70 File Offset: 0x001D1D70
		internal override void Reset()
		{
			this.patterns = null;
			base.Reset();
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x001D3B80 File Offset: 0x001D1D80
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

		// Token: 0x060072DF RID: 29407 RVA: 0x001D3BB8 File Offset: 0x001D1DB8
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Pattern pattern in this.patterns)
			{
				shortList.Add(pattern.ToString());
			}
			string name = base.UseLegacyRegex ? "senderAttributeMatches" : "senderAttributeMatchesRegex";
			return TransportRuleParser.Instance.CreatePredicate(name, null, shortList);
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x001D3C2C File Offset: 0x001D1E2C
		internal override void SuppressPiiData()
		{
			this.Patterns = Utils.RedactNameValuePairPatterns(this.Patterns);
		}
	}
}
