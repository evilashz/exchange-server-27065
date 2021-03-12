using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB0 RID: 2992
	[Serializable]
	public abstract class SinglePropertyContainsPredicate : ContainsWordsPredicate
	{
		// Token: 0x060070C9 RID: 28873 RVA: 0x001CD5D6 File Offset: 0x001CB7D6
		protected SinglePropertyContainsPredicate(string internalPropertyName)
		{
			this.internalPropertyName = internalPropertyName;
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x001CD5E8 File Offset: 0x001CB7E8
		internal static TransportRulePredicate CreateFromInternalCondition<T>(Condition condition, string internalPropertyName) where T : SinglePropertyContainsPredicate, new()
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("contains") || !predicateCondition.Property.Name.Equals(internalPropertyName))
			{
				return null;
			}
			Word[] array = new Word[predicateCondition.Value.RawValues.Count];
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				try
				{
					array[i] = new Word(predicateCondition.Value.RawValues[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			T t = Activator.CreateInstance<T>();
			t.Words = array;
			return t;
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x001CD6B8 File Offset: 0x001CB8B8
		internal virtual Property CreateProperty()
		{
			return TransportRuleParser.Instance.CreateProperty(this.internalPropertyName);
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x001CD6CC File Offset: 0x001CB8CC
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Word word in this.Words)
			{
				shortList.Add(word.ToString());
			}
			return TransportRuleParser.Instance.CreatePredicate("contains", this.CreateProperty(), shortList);
		}

		// Token: 0x04003A23 RID: 14883
		private readonly string internalPropertyName;
	}
}
