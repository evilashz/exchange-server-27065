using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BF2 RID: 3058
	[Serializable]
	public class SubjectOrBodyMatchesPredicate : MatchesPatternsPredicate, IEquatable<SubjectOrBodyMatchesPredicate>
	{
		// Token: 0x06007354 RID: 29524 RVA: 0x001D52C3 File Offset: 0x001D34C3
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns);
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x001D52D0 File Offset: 0x001D34D0
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SubjectOrBodyMatchesPredicate)));
		}

		// Token: 0x06007356 RID: 29526 RVA: 0x001D5309 File Offset: 0x001D3509
		public bool Equals(SubjectOrBodyMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return null == other.Patterns;
			}
			return this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x06007357 RID: 29527 RVA: 0x001D532E File Offset: 0x001D352E
		// (set) Token: 0x06007358 RID: 29528 RVA: 0x001D5336 File Offset: 0x001D3536
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[ConditionParameterName("SubjectOrBodyMatchesPatterns")]
		[ExceptionParameterName("ExceptIfSubjectOrBodyMatchesPatterns")]
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

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x06007359 RID: 29529 RVA: 0x001D533F File Offset: 0x001D353F
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new MatchesPatternsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSubjectOrBodyMatches);
			}
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x001D534D File Offset: 0x001D354D
		public SubjectOrBodyMatchesPredicate() : this(true)
		{
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x001D5356 File Offset: 0x001D3556
		public SubjectOrBodyMatchesPredicate(bool useLegacyRegex)
		{
			base.UseLegacyRegex = useLegacyRegex;
		}

		// Token: 0x0600735C RID: 29532 RVA: 0x001D5368 File Offset: 0x001D3568
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Or)
			{
				return null;
			}
			OrCondition orCondition = (OrCondition)condition;
			if (orCondition.SubConditions.Count != 2)
			{
				return null;
			}
			if (orCondition.SubConditions[0].ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			if (orCondition.SubConditions[1].ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)orCondition.SubConditions[0];
			PredicateCondition predicateCondition2 = (PredicateCondition)orCondition.SubConditions[1];
			if (!predicateCondition.Property.Name.Equals("Message.Subject") || !predicateCondition2.Property.Name.Equals("Message.Body"))
			{
				return null;
			}
			bool useLegacyRegex;
			if (predicateCondition.Name.Equals("matches") && predicateCondition2.Name.Equals("matches"))
			{
				useLegacyRegex = true;
			}
			else
			{
				if (!predicateCondition.Name.Equals("matchesRegex") || !predicateCondition2.Name.Equals("matchesRegex"))
				{
					return null;
				}
				useLegacyRegex = false;
			}
			if (predicateCondition.Value.RawValues.Count != predicateCondition2.Value.RawValues.Count)
			{
				return null;
			}
			Pattern[] array = new Pattern[predicateCondition.Value.RawValues.Count];
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				if (predicateCondition.Value.RawValues[i] != predicateCondition2.Value.RawValues[i])
				{
					return null;
				}
				try
				{
					array[i] = new Pattern(predicateCondition.Value.RawValues[i], useLegacyRegex, false);
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			return new SubjectOrBodyMatchesPredicate(useLegacyRegex)
			{
				Patterns = array
			};
		}

		// Token: 0x0600735D RID: 29533 RVA: 0x001D5548 File Offset: 0x001D3748
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Pattern pattern in this.Patterns)
			{
				shortList.Add(pattern.ToString());
			}
			string name = base.UseLegacyRegex ? "matches" : "matchesRegex";
			PredicateCondition item = TransportRuleParser.Instance.CreatePredicate(name, TransportRuleParser.Instance.CreateProperty("Message.Subject"), shortList);
			PredicateCondition item2 = TransportRuleParser.Instance.CreatePredicate(name, TransportRuleParser.Instance.CreateProperty("Message.Body"), shortList);
			return new OrCondition
			{
				SubConditions = 
				{
					item,
					item2
				}
			};
		}
	}
}
