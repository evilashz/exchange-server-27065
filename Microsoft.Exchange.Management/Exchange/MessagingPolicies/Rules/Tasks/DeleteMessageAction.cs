using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8C RID: 2956
	[ActionParameterName("DeleteMessage")]
	[Serializable]
	public class DeleteMessageAction : TransportRuleAction, IEquatable<DeleteMessageAction>
	{
		// Token: 0x06006F64 RID: 28516 RVA: 0x001C8EE2 File Offset: 0x001C70E2
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x001C8EE5 File Offset: 0x001C70E5
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x001C8F13 File Offset: 0x001C7113
		public bool Equals(DeleteMessageAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x06006F67 RID: 28519 RVA: 0x001C8F1C File Offset: 0x001C711C
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionDeleteMessage;
			}
		}

		// Token: 0x06006F68 RID: 28520 RVA: 0x001C8F28 File Offset: 0x001C7128
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "DeleteMessage")
			{
				return null;
			}
			return new DeleteMessageAction();
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x001C8F43 File Offset: 0x001C7143
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("DeleteMessage", Utils.GetActionName(this));
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x001C8F5A File Offset: 0x001C715A
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
