using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BF1 RID: 3057
	[Serializable]
	public class SubjectOrBodyContainsPredicate : ContainsWordsPredicate, IEquatable<SubjectOrBodyContainsPredicate>
	{
		// Token: 0x0600734B RID: 29515 RVA: 0x001D4FD2 File Offset: 0x001D31D2
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x001D4FDF File Offset: 0x001D31DF
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SubjectOrBodyContainsPredicate)));
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x001D5018 File Offset: 0x001D3218
		public bool Equals(SubjectOrBodyContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x0600734E RID: 29518 RVA: 0x001D503D File Offset: 0x001D323D
		// (set) Token: 0x0600734F RID: 29519 RVA: 0x001D5045 File Offset: 0x001D3245
		[ExceptionParameterName("ExceptIfSubjectOrBodyContainsWords")]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("SubjectOrBodyContainsWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public override Word[] Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x06007350 RID: 29520 RVA: 0x001D504E File Offset: 0x001D324E
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSubjectOrBodyContains);
			}
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x001D505C File Offset: 0x001D325C
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
			if (!predicateCondition.Name.Equals("contains") || !predicateCondition2.Name.Equals("contains") || !predicateCondition.Property.Name.Equals("Message.Subject") || !predicateCondition2.Property.Name.Equals("Message.Body"))
			{
				return null;
			}
			if (predicateCondition.Value.RawValues.Count != predicateCondition2.Value.RawValues.Count)
			{
				return null;
			}
			Word[] array = new Word[predicateCondition.Value.RawValues.Count];
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				if (predicateCondition.Value.RawValues[i] != predicateCondition2.Value.RawValues[i])
				{
					return null;
				}
				try
				{
					array[i] = new Word(predicateCondition.Value.RawValues[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			return new SubjectOrBodyContainsPredicate
			{
				Words = array
			};
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x001D5208 File Offset: 0x001D3408
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Word word in this.Words)
			{
				shortList.Add(word.ToString());
			}
			PredicateCondition item = TransportRuleParser.Instance.CreatePredicate("contains", TransportRuleParser.Instance.CreateProperty("Message.Subject"), shortList);
			PredicateCondition item2 = TransportRuleParser.Instance.CreatePredicate("contains", TransportRuleParser.Instance.CreateProperty("Message.Body"), shortList);
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
