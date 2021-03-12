using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB4 RID: 2996
	[Serializable]
	public abstract class SinglePropertyMatchesPredicate : MatchesPatternsPredicate
	{
		// Token: 0x060070E6 RID: 28902 RVA: 0x001CD9C3 File Offset: 0x001CBBC3
		protected SinglePropertyMatchesPredicate(string internalPropertyName, bool isLegacyRegex)
		{
			base.UseLegacyRegex = isLegacyRegex;
			this.internalPropertyName = internalPropertyName;
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x001CD9DC File Offset: 0x001CBBDC
		internal static TransportRulePredicate CreateFromInternalCondition<T>(Condition condition, string internalPropertyName) where T : SinglePropertyMatchesPredicate, new()
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if ((!predicateCondition.Name.Equals("matches") && !predicateCondition.Name.Equals("matchesRegex")) || !predicateCondition.Property.Name.Equals(internalPropertyName))
			{
				return null;
			}
			bool useLegacyRegex = !predicateCondition.Name.Equals("matchesRegex");
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
			T t = Activator.CreateInstance<T>();
			t.UseLegacyRegex = useLegacyRegex;
			t.patterns = array;
			return t;
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x001CDAE4 File Offset: 0x001CBCE4
		internal virtual Property CreateProperty()
		{
			return TransportRuleParser.Instance.CreateProperty(this.internalPropertyName);
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x001CDAF8 File Offset: 0x001CBCF8
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Pattern pattern in this.patterns)
			{
				shortList.Add(pattern.ToString());
			}
			string name = base.UseLegacyRegex ? "matches" : "matchesRegex";
			return TransportRuleParser.Instance.CreatePredicate(name, this.CreateProperty(), shortList);
		}

		// Token: 0x04003A28 RID: 14888
		private readonly string internalPropertyName;
	}
}
