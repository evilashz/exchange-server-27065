using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000008 RID: 8
	public abstract class PredicateCondition : Condition
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000278C File Offset: 0x0000098C
		protected PredicateCondition()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002794 File Offset: 0x00000994
		protected PredicateCondition(Property property, ShortList<string> entries, RulesCreationContext context)
		{
			this.property = property;
			this.value = this.BuildValue(entries, context);
			if (this.value == null)
			{
				this.value = Value.Empty;
			}
			this.UpdateSize(context);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027CB File Offset: 0x000009CB
		protected PredicateCondition(Property property, ShortList<ShortList<KeyValuePair<string, string>>> entries, RulesCreationContext creationContext)
		{
			this.property = property;
			this.value = this.BuildValue(entries, creationContext);
			this.UpdateSize(creationContext);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027F0 File Offset: 0x000009F0
		protected void UpdateSize(RulesCreationContext creationContext)
		{
			creationContext.ConditionAndActionSize += 18;
			if (this.property != null)
			{
				creationContext.ConditionAndActionSize += this.property.GetEstimatedSize();
			}
			if (this.value != null)
			{
				creationContext.ConditionAndActionSize += this.value.GetEstimatedSize();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000284C File Offset: 0x00000A4C
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Predicate;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000284F File Offset: 0x00000A4F
		public Property Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002857 File Offset: 0x00000A57
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51
		public abstract string Name { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000285F File Offset: 0x00000A5F
		protected virtual Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(this.property.Type, entries);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002872 File Offset: 0x00000A72
		protected virtual Value BuildValue(ShortList<ShortList<KeyValuePair<string, string>>> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000287C File Offset: 0x00000A7C
		protected int ComparePropertyAndValue(RulesEvaluationContext context)
		{
			if (!this.property.IsNumerical || !this.Value.IsNumerical)
			{
				throw new InvalidOperationException(RulesStrings.InvalidPropertyType);
			}
			object obj = this.property.GetValue(context);
			object obj2 = this.Value.GetValue(context);
			IComparable comparable = obj as IComparable;
			if (comparable == null)
			{
				throw new RuleInvalidOperationException(RulesStrings.InvalidPropertyForRule(this.property.Name, context.CurrentRule.Name));
			}
			context.Trace("Comparing '{0}' with '{1}'", new object[]
			{
				obj.ToString(),
				(obj2 != null) ? obj2.ToString() : "null"
			});
			return comparable.CompareTo(obj2);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000293C File Offset: 0x00000B3C
		protected void UpdateEvaluationHistory(RulesEvaluationContext context, bool isMatch, List<string> matches, int supplementalInfo = 0)
		{
			if (context != null)
			{
				List<string> list = matches ?? new List<string>
				{
					this.Value.GetValue(context).ToString()
				};
				context.RulesEvaluationHistory.AddPredicateEvaluationResult(context, base.GetType(), isMatch, list, supplementalInfo);
				context.Trace("Condition '{0}' evaluated as '{1}'. {2}", new object[]
				{
					base.GetType().ToString(),
					isMatch ? "Match" : "Not Match",
					isMatch ? string.Join(",", list) : string.Empty
				});
			}
		}

		// Token: 0x04000015 RID: 21
		protected Property property;

		// Token: 0x04000016 RID: 22
		protected Value value;
	}
}
