using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C8 RID: 200
	public abstract class PredicateCondition : Condition
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000F57B File Offset: 0x0000D77B
		protected PredicateCondition()
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F58C File Offset: 0x0000D78C
		protected PredicateCondition(Property property, List<string> entries)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (entries != null)
			{
				if (!entries.Any((string entry) => entry == null))
				{
					this.property = property;
					this.value = (this.BuildValue(entries) ?? Value.Empty);
					return;
				}
			}
			throw new ArgumentNullException("entries");
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000F604 File Offset: 0x0000D804
		protected PredicateCondition(Property property, List<List<KeyValuePair<string, string>>> entries)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (entries != null)
			{
				if (!entries.Any((List<KeyValuePair<string, string>> entry) => entry == null))
				{
					this.property = property;
					this.value = this.BuildValue(entries);
					return;
				}
			}
			throw new ArgumentNullException("entries");
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000F66C File Offset: 0x0000D86C
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Predicate;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000F66F File Offset: 0x0000D86F
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0000F677 File Offset: 0x0000D877
		public Property Property
		{
			get
			{
				return this.property;
			}
			protected set
			{
				this.property = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000F680 File Offset: 0x0000D880
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000F688 File Offset: 0x0000D888
		public Value Value
		{
			get
			{
				return this.value;
			}
			protected set
			{
				this.value = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000517 RID: 1303
		public abstract string Name { get; }

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F691 File Offset: 0x0000D891
		protected virtual Value BuildValue(List<string> entries)
		{
			return Value.CreateValue((this.property == null) ? typeof(string) : this.property.Type, entries);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		protected virtual Value BuildValue(List<List<KeyValuePair<string, string>>> entries)
		{
			return Value.CreateValue(entries);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		protected int CompareComparablePropertyAndValue(PolicyEvaluationContext context)
		{
			if (!this.Property.IsComparableTo(this.Value))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' is in inconsitent state due to unknown property '{1}'", context.CurrentRule.Name, this.property.Name));
			}
			object obj = this.Property.GetValue(context);
			object obj2 = this.Value.GetValue(context);
			if (!(obj2 is IComparable))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' contains an invalid property '{1}'", context.CurrentRule.Name, this.Property.Name));
			}
			return ((IComparable)obj).CompareTo(obj2);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000F75C File Offset: 0x0000D95C
		protected bool CompareEquatablePropertyAndValue(PolicyEvaluationContext context)
		{
			if (!this.Property.IsEquatableTo(this.Value))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' is in inconsitent state due to unknown property '{1}'", context.CurrentRule.Name, this.property.Name));
			}
			object obj = this.Property.GetValue(context);
			object obj2 = this.Value.GetValue(context);
			if (obj == null && obj2 == null)
			{
				return true;
			}
			if (obj == null || obj2 == null)
			{
				return false;
			}
			if (!Argument.IsTypeEquatable(obj2.GetType()))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' contains an invalid property '{1}'", context.CurrentRule.Name, this.Property.Name));
			}
			return obj.Equals(obj2);
		}

		// Token: 0x04000315 RID: 789
		private Property property;

		// Token: 0x04000316 RID: 790
		private Value value;
	}
}
