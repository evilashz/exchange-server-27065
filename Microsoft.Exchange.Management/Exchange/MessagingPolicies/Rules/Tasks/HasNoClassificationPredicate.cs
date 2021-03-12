using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD2 RID: 3026
	[ExceptionParameterName("ExceptIfHasNoClassification")]
	[ConditionParameterName("HasNoClassification")]
	[Serializable]
	public class HasNoClassificationPredicate : TransportRulePredicate, IEquatable<HasNoClassificationPredicate>
	{
		// Token: 0x060071EF RID: 29167 RVA: 0x001D00DA File Offset: 0x001CE2DA
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x001D00DD File Offset: 0x001CE2DD
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as HasNoClassificationPredicate)));
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x001D0116 File Offset: 0x001CE316
		public bool Equals(HasNoClassificationPredicate other)
		{
			return true;
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x060071F2 RID: 29170 RVA: 0x001D0119 File Offset: 0x001CE319
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionHasNoClassification;
			}
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x001D0128 File Offset: 0x001CE328
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!(predicateCondition.Property is HeaderProperty))
			{
				return null;
			}
			if (!predicateCondition.Name.Equals("notExists") || !predicateCondition.Property.Name.Equals("X-MS-Exchange-Organization-Classification"))
			{
				return null;
			}
			return new HasNoClassificationPredicate();
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x001D0188 File Offset: 0x001CE388
		internal override Condition ToInternalCondition()
		{
			Property property = TransportRuleParser.Instance.CreateProperty("Message.Headers:X-MS-Exchange-Organization-Classification");
			ShortList<string> valueEntries = new ShortList<string>();
			return TransportRuleParser.Instance.CreatePredicate("notExists", property, valueEntries);
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x001D01BE File Offset: 0x001CE3BE
		internal override string GetPredicateParameters()
		{
			return "$true";
		}
	}
}
