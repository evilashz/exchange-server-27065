using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BF3 RID: 3059
	[Serializable]
	public class WithImportancePredicate : TransportRulePredicate, IEquatable<WithImportancePredicate>
	{
		// Token: 0x0600735E RID: 29534 RVA: 0x001D560A File Offset: 0x001D380A
		public override int GetHashCode()
		{
			return this.Importance.GetHashCode();
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x001D561C File Offset: 0x001D381C
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as WithImportancePredicate)));
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x001D5655 File Offset: 0x001D3855
		public bool Equals(WithImportancePredicate other)
		{
			return this.Importance.Equals(other.Importance);
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x06007361 RID: 29537 RVA: 0x001D5672 File Offset: 0x001D3872
		// (set) Token: 0x06007362 RID: 29538 RVA: 0x001D567A File Offset: 0x001D387A
		[ExceptionParameterName("ExceptIfWithImportance")]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ImportanceDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ImportanceDescription)]
		[ConditionParameterName("WithImportance")]
		public Importance Importance
		{
			get
			{
				return this.importance;
			}
			set
			{
				this.importance = value;
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06007363 RID: 29539 RVA: 0x001D5683 File Offset: 0x001D3883
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionWithImportance(LocalizedDescriptionAttribute.FromEnum(typeof(Importance), this.Importance));
			}
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x001D56AC File Offset: 0x001D38AC
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			WithImportancePredicate withImportancePredicate = new WithImportancePredicate();
			if (WithImportancePredicate.IsImportanceCondition(condition, Importance.High))
			{
				withImportancePredicate.Importance = Importance.High;
				return withImportancePredicate;
			}
			if (WithImportancePredicate.IsImportanceCondition(condition, Importance.Low))
			{
				withImportancePredicate.Importance = Importance.Low;
				return withImportancePredicate;
			}
			if (condition.ConditionType == ConditionType.Not)
			{
				NotCondition notCondition = (NotCondition)condition;
				if (notCondition.SubCondition.ConditionType == ConditionType.Or)
				{
					OrCondition orCondition = (OrCondition)notCondition.SubCondition;
					if (orCondition.SubConditions.Count == 2 && WithImportancePredicate.IsImportanceCondition(orCondition.SubConditions[0], Importance.High) && WithImportancePredicate.IsImportanceCondition(orCondition.SubConditions[1], Importance.Low))
					{
						withImportancePredicate.Importance = Importance.Normal;
						return withImportancePredicate;
					}
				}
			}
			return null;
		}

		// Token: 0x06007365 RID: 29541 RVA: 0x001D574D File Offset: 0x001D394D
		internal override void Reset()
		{
			this.importance = Importance.Normal;
			base.Reset();
		}

		// Token: 0x06007366 RID: 29542 RVA: 0x001D575C File Offset: 0x001D395C
		private static bool IsImportanceCondition(Condition condition, Importance importance)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return false;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			return predicateCondition.Name.Equals("is") && predicateCondition.Property is HeaderProperty && predicateCondition.Property.Name.Equals("Importance") && predicateCondition.Value.RawValues.Count == 1 && predicateCondition.Value.RawValues[0].Equals(importance.ToString());
		}

		// Token: 0x06007367 RID: 29543 RVA: 0x001D57EC File Offset: 0x001D39EC
		internal override Condition ToInternalCondition()
		{
			if (this.Importance != Importance.Normal)
			{
				return WithImportancePredicate.CreateImportancePredicate(this.Importance);
			}
			PredicateCondition item = WithImportancePredicate.CreateImportancePredicate(Importance.High);
			PredicateCondition item2 = WithImportancePredicate.CreateImportancePredicate(Importance.Low);
			return new NotCondition(new OrCondition
			{
				SubConditions = 
				{
					item,
					item2
				}
			});
		}

		// Token: 0x06007368 RID: 29544 RVA: 0x001D5840 File Offset: 0x001D3A40
		private static PredicateCondition CreateImportancePredicate(Importance importance)
		{
			return TransportRuleParser.Instance.CreatePredicate("is", TransportRuleParser.Instance.CreateProperty("Message.Headers:Importance"), new ShortList<string>
			{
				importance.ToString()
			});
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x001D5883 File Offset: 0x001D3A83
		internal override string GetPredicateParameters()
		{
			return Enum.GetName(typeof(Importance), this.Importance);
		}

		// Token: 0x04003A94 RID: 14996
		private Importance importance;
	}
}
