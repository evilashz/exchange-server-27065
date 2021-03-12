using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9D RID: 2973
	[ActionParameterName("RouteMessageOutboundRequireTls")]
	[Serializable]
	public class RouteMessageOutboundRequireTlsAction : TransportRuleAction, IEquatable<RouteMessageOutboundRequireTlsAction>
	{
		// Token: 0x06007026 RID: 28710 RVA: 0x001CB067 File Offset: 0x001C9267
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06007027 RID: 28711 RVA: 0x001CB06A File Offset: 0x001C926A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06007028 RID: 28712 RVA: 0x001CB098 File Offset: 0x001C9298
		public bool Equals(RouteMessageOutboundRequireTlsAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x001CB0A1 File Offset: 0x001C92A1
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRouteMessageOutboundRequireTls;
			}
		}

		// Token: 0x0600702A RID: 28714 RVA: 0x001CB0AD File Offset: 0x001C92AD
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RouteMessageOutboundRequireTls")
			{
				return null;
			}
			return new RouteMessageOutboundRequireTlsAction();
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x001CB0C8 File Offset: 0x001C92C8
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("RouteMessageOutboundRequireTls", new ShortList<Argument>(), Utils.GetActionName(this));
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x001CB0E4 File Offset: 0x001C92E4
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
