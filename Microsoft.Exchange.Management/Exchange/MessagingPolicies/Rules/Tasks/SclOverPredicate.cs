using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE5 RID: 3045
	[Serializable]
	public class SclOverPredicate : TransportRulePredicate, IEquatable<SclOverPredicate>
	{
		// Token: 0x060072BE RID: 29374 RVA: 0x001D347C File Offset: 0x001D167C
		public override int GetHashCode()
		{
			return this.SclValue.GetHashCode();
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x001D349D File Offset: 0x001D169D
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SclOverPredicate)));
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x001D34D8 File Offset: 0x001D16D8
		public bool Equals(SclOverPredicate other)
		{
			return this.SclValue.Equals(other.SclValue);
		}

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x060072C1 RID: 29377 RVA: 0x001D34F9 File Offset: 0x001D16F9
		// (set) Token: 0x060072C2 RID: 29378 RVA: 0x001D3501 File Offset: 0x001D1701
		[ExceptionParameterName("ExceptIfSCLOver")]
		[LocDisplayName(RulesTasksStrings.IDs.SclValueDisplayName)]
		[ConditionParameterName("SCLOver")]
		[LocDescription(RulesTasksStrings.IDs.SclValueDescription)]
		public SclValue SclValue
		{
			get
			{
				return this.sclValue;
			}
			set
			{
				this.sclValue = value;
			}
		}

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x060072C3 RID: 29379 RVA: 0x001D350C File Offset: 0x001D170C
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSclOver(this.SclValue.ToString());
			}
		}

		// Token: 0x060072C4 RID: 29380 RVA: 0x001D3538 File Offset: 0x001D1738
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.And)
			{
				return null;
			}
			AndCondition andCondition = (AndCondition)condition;
			if (andCondition.SubConditions.Count != 2 || andCondition.SubConditions[0].ConditionType != ConditionType.Predicate || andCondition.SubConditions[1].ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)andCondition.SubConditions[0];
			PredicateCondition predicateCondition2 = (PredicateCondition)andCondition.SubConditions[1];
			if (!predicateCondition.Name.Equals("exists") || !predicateCondition.Property.Name.Equals("Message.SclValue"))
			{
				return null;
			}
			if (!predicateCondition2.Name.Equals("greaterThanOrEqual") || !predicateCondition2.Property.Name.Equals("Message.SclValue") || predicateCondition2.Value.RawValues.Count != 1)
			{
				return null;
			}
			int input;
			if (!int.TryParse(predicateCondition2.Value.RawValues[0], out input))
			{
				return null;
			}
			SclOverPredicate sclOverPredicate = new SclOverPredicate();
			try
			{
				sclOverPredicate.SclValue = new SclValue(input);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return sclOverPredicate;
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x001D3668 File Offset: 0x001D1868
		internal override void Reset()
		{
			this.sclValue = new SclValue(0);
			base.Reset();
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x001D367C File Offset: 0x001D187C
		internal override Condition ToInternalCondition()
		{
			PredicateCondition item = TransportRuleParser.Instance.CreatePredicate("exists", TransportRuleParser.Instance.CreateProperty("Message.SclValue"), new ShortList<string>());
			PredicateCondition item2 = TransportRuleParser.Instance.CreatePredicate("greaterThanOrEqual", TransportRuleParser.Instance.CreateProperty("Message.SclValue"), new ShortList<string>
			{
				this.SclValue.ToString()
			});
			return new AndCondition
			{
				SubConditions = 
				{
					item,
					item2
				}
			};
		}

		// Token: 0x060072C7 RID: 29383 RVA: 0x001D3710 File Offset: 0x001D1910
		internal override string GetPredicateParameters()
		{
			return this.SclValue.ToString();
		}

		// Token: 0x04003A7C RID: 14972
		private SclValue sclValue;
	}
}
