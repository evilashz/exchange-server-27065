using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8D RID: 2957
	[ActionParameterName("Disconnect")]
	[Serializable]
	public class DisconnectAction : TransportRuleAction, IEquatable<DisconnectAction>
	{
		// Token: 0x06006F6C RID: 28524 RVA: 0x001C8F69 File Offset: 0x001C7169
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006F6D RID: 28525 RVA: 0x001C8F6C File Offset: 0x001C716C
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x001C8F9A File Offset: 0x001C719A
		public bool Equals(DisconnectAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06006F6F RID: 28527 RVA: 0x001C8FA3 File Offset: 0x001C71A3
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionDisconnect;
			}
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x001C8FAF File Offset: 0x001C71AF
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "Disconnect")
			{
				return null;
			}
			return new DisconnectAction();
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x001C8FCA File Offset: 0x001C71CA
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("Disconnect", Utils.GetActionName(this));
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x001C8FE1 File Offset: 0x001C71E1
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
