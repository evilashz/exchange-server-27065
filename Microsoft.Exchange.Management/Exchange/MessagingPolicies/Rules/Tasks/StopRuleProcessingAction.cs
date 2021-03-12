using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA2 RID: 2978
	[ActionParameterName("StopRuleProcessing")]
	[Serializable]
	public class StopRuleProcessingAction : TransportRuleAction, IEquatable<StopRuleProcessingAction>
	{
		// Token: 0x06007062 RID: 28770 RVA: 0x001CBAE4 File Offset: 0x001C9CE4
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06007063 RID: 28771 RVA: 0x001CBAE7 File Offset: 0x001C9CE7
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x001CBB15 File Offset: 0x001C9D15
		public bool Equals(StopRuleProcessingAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x06007065 RID: 28773 RVA: 0x001CBB1E File Offset: 0x001C9D1E
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionStopRuleProcessing;
			}
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x001CBB2A File Offset: 0x001C9D2A
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "Halt")
			{
				return null;
			}
			return new StopRuleProcessingAction();
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x001CBB45 File Offset: 0x001C9D45
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("Halt", null);
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x001CBB57 File Offset: 0x001C9D57
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
