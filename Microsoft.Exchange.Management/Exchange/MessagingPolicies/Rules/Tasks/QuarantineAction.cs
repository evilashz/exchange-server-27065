using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B97 RID: 2967
	[ActionParameterName("Quarantine")]
	[Serializable]
	public class QuarantineAction : TransportRuleAction, IEquatable<QuarantineAction>
	{
		// Token: 0x06006FDF RID: 28639 RVA: 0x001CA57B File Offset: 0x001C877B
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x001CA57E File Offset: 0x001C877E
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006FE1 RID: 28641 RVA: 0x001CA5AC File Offset: 0x001C87AC
		public bool Equals(QuarantineAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x001CA5B5 File Offset: 0x001C87B5
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionQuarantine;
			}
		}

		// Token: 0x06006FE3 RID: 28643 RVA: 0x001CA5C4 File Offset: 0x001C87C4
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "Quarantine")
			{
				return null;
			}
			return new QuarantineAction();
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x001CA5EC File Offset: 0x001C87EC
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("Quarantine", Utils.GetActionName(this));
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x001CA603 File Offset: 0x001C8803
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
