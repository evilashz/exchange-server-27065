using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000180 RID: 384
	internal sealed class OutlookProtectionRule : Rule
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002C03D File Offset: 0x0002A23D
		public OutlookProtectionRule(string name) : base(name)
		{
			this.userOverridable = false;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002C04D File Offset: 0x0002A24D
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0002C055 File Offset: 0x0002A255
		public bool UserOverridable
		{
			get
			{
				return this.userOverridable;
			}
			set
			{
				this.userOverridable = value;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002C060 File Offset: 0x0002A260
		private static Action FindActionByName(IEnumerable<Action> actions, string name)
		{
			if (actions == null)
			{
				return null;
			}
			foreach (Action action in actions)
			{
				if (string.Equals(action.Name, name, StringComparison.Ordinal))
				{
					return action;
				}
			}
			return null;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		private static PredicateCondition FindPredicateConditionByName(Condition root, string name)
		{
			if (root == null)
			{
				return null;
			}
			ConditionType conditionType = root.ConditionType;
			if (conditionType != ConditionType.And)
			{
				switch (conditionType)
				{
				case ConditionType.True:
					return null;
				case ConditionType.Predicate:
				{
					PredicateCondition predicateCondition = (PredicateCondition)root;
					if (string.Equals(predicateCondition.Name, name, StringComparison.Ordinal))
					{
						return predicateCondition;
					}
					return null;
				}
				}
				return null;
			}
			AndCondition andCondition = (AndCondition)root;
			foreach (Condition root2 in andCondition.SubConditions)
			{
				PredicateCondition predicateCondition2 = OutlookProtectionRule.FindPredicateConditionByName(root2, name);
				if (predicateCondition2 != null)
				{
					return predicateCondition2;
				}
			}
			return null;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002C16C File Offset: 0x0002A36C
		public RightsProtectMessageAction GetRightsProtectMessageAction()
		{
			return (RightsProtectMessageAction)this.FindActionByName("RightsProtectMessage");
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002C17E File Offset: 0x0002A37E
		public PredicateCondition GetAllInternalPredicate()
		{
			return this.FindPredicateConditionByName("allInternal");
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002C18B File Offset: 0x0002A38B
		public PredicateCondition GetRecipientIsPredicate()
		{
			return this.FindPredicateConditionByName("recipientIs");
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002C198 File Offset: 0x0002A398
		public PredicateCondition GetSenderDepartmentPredicate()
		{
			return this.FindPredicateConditionByName("is");
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002C1A5 File Offset: 0x0002A3A5
		private PredicateCondition FindPredicateConditionByName(string name)
		{
			return OutlookProtectionRule.FindPredicateConditionByName(base.Condition, name);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002C1B3 File Offset: 0x0002A3B3
		private Action FindActionByName(string name)
		{
			return OutlookProtectionRule.FindActionByName(base.Actions, name);
		}

		// Token: 0x040007F6 RID: 2038
		private bool userOverridable;
	}
}
