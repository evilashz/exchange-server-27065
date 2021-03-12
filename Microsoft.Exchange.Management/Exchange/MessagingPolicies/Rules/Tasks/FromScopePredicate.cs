using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD0 RID: 3024
	[Serializable]
	public class FromScopePredicate : TransportRulePredicate, IEquatable<FromScopePredicate>
	{
		// Token: 0x060071D7 RID: 29143 RVA: 0x001CFBCF File Offset: 0x001CDDCF
		public override int GetHashCode()
		{
			return this.Scope.GetHashCode();
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x001CFBE1 File Offset: 0x001CDDE1
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as FromScopePredicate)));
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x001CFC1A File Offset: 0x001CDE1A
		public bool Equals(FromScopePredicate other)
		{
			return this.Scope.Equals(other.Scope);
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x060071DA RID: 29146 RVA: 0x001CFC37 File Offset: 0x001CDE37
		// (set) Token: 0x060071DB RID: 29147 RVA: 0x001CFC3F File Offset: 0x001CDE3F
		[LocDisplayName(RulesTasksStrings.IDs.FromScopeDisplayName)]
		[ExceptionParameterName("ExceptIfFromScope")]
		[LocDescription(RulesTasksStrings.IDs.FromScopeDescription)]
		[ConditionParameterName("FromScope")]
		public FromUserScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x060071DC RID: 29148 RVA: 0x001CFC48 File Offset: 0x001CDE48
		[LocDisplayName(RulesTasksStrings.IDs.SubTypeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.SubTypeDescription)]
		public override IEnumerable<RuleSubType> RuleSubTypes
		{
			get
			{
				return new RuleSubType[]
				{
					RuleSubType.None,
					RuleSubType.Dlp
				};
			}
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x001CFC68 File Offset: 0x001CDE68
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			bool flag = false;
			if (condition.ConditionType == ConditionType.Not)
			{
				condition = ((NotCondition)condition).SubCondition;
				flag = true;
			}
			if (condition.ConditionType != ConditionType.And)
			{
				return null;
			}
			AndCondition andCondition = (AndCondition)condition;
			if (andCondition.SubConditions.Count != 2 || andCondition.SubConditions[0].ConditionType != ConditionType.Not || andCondition.SubConditions[1].ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			NotCondition notCondition = (NotCondition)andCondition.SubConditions[0];
			if (notCondition.SubCondition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)notCondition.SubCondition;
			PredicateCondition predicateCondition2 = (PredicateCondition)andCondition.SubConditions[1];
			if (!predicateCondition.Name.Equals("is") || !predicateCondition.Property.Name.Equals("Message.Auth") || predicateCondition.Value.RawValues.Count != 1 || !predicateCondition.Value.RawValues[0].Equals("<>"))
			{
				return null;
			}
			if (!predicateCondition2.Name.Equals("isInternal") || !predicateCondition2.Property.Name.Equals("Message.From") || predicateCondition2.Value.RawValues.Count != 0)
			{
				return null;
			}
			FromScopePredicate fromScopePredicate = new FromScopePredicate();
			if (flag)
			{
				fromScopePredicate.Scope = FromUserScope.NotInOrganization;
			}
			else
			{
				fromScopePredicate.Scope = FromUserScope.InOrganization;
			}
			return fromScopePredicate;
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x001CFDD2 File Offset: 0x001CDFD2
		internal override void Reset()
		{
			this.scope = FromUserScope.InOrganization;
			base.Reset();
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x001CFDE1 File Offset: 0x001CDFE1
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionFromScope(LocalizedDescriptionAttribute.FromEnum(typeof(FromUserScope), this.Scope));
			}
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x001CFE08 File Offset: 0x001CE008
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>
			{
				"<>"
			};
			PredicateCondition subCondition = TransportRuleParser.Instance.CreatePredicate("is", TransportRuleParser.Instance.CreateProperty("Message.Auth"), valueEntries);
			valueEntries = new ShortList<string>();
			NotCondition item = new NotCondition(subCondition);
			PredicateCondition item2 = TransportRuleParser.Instance.CreatePredicate("isInternal", TransportRuleParser.Instance.CreateProperty("Message.From"), valueEntries);
			AndCondition andCondition = new AndCondition();
			andCondition.SubConditions.Add(item);
			andCondition.SubConditions.Add(item2);
			if (this.Scope == FromUserScope.NotInOrganization)
			{
				return new NotCondition(andCondition);
			}
			return andCondition;
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x001CFEAC File Offset: 0x001CE0AC
		internal override string GetPredicateParameters()
		{
			return Enum.GetName(typeof(FromUserScope), this.Scope);
		}

		// Token: 0x04003A4D RID: 14925
		private FromUserScope scope;
	}
}
