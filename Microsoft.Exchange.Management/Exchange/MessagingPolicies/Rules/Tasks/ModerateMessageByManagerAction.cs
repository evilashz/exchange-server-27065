using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B92 RID: 2962
	[ActionParameterName("ModerateMessageByManager")]
	[Serializable]
	public class ModerateMessageByManagerAction : TransportRuleAction, IEquatable<ModerateMessageByManagerAction>
	{
		// Token: 0x06006FAC RID: 28588 RVA: 0x001C9A76 File Offset: 0x001C7C76
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x001C9A79 File Offset: 0x001C7C79
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006FAE RID: 28590 RVA: 0x001C9AA7 File Offset: 0x001C7CA7
		public bool Equals(ModerateMessageByManagerAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x001C9AB0 File Offset: 0x001C7CB0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionModerateMessageByManager;
			}
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x001C9ABC File Offset: 0x001C7CBC
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "ModerateMessageByManager")
			{
				return null;
			}
			return new ModerateMessageByManagerAction();
		}

		// Token: 0x06006FB1 RID: 28593 RVA: 0x001C9AD7 File Offset: 0x001C7CD7
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("ModerateMessageByManager", Utils.GetActionName(this));
		}

		// Token: 0x06006FB2 RID: 28594 RVA: 0x001C9AEE File Offset: 0x001C7CEE
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
