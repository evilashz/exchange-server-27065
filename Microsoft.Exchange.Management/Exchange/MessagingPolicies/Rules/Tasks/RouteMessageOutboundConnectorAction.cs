using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9C RID: 2972
	[Serializable]
	public class RouteMessageOutboundConnectorAction : TransportRuleAction, IEquatable<RouteMessageOutboundConnectorAction>
	{
		// Token: 0x06007019 RID: 28697 RVA: 0x001CAE7E File Offset: 0x001C907E
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.ConnectorName))
			{
				return this.ConnectorName.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x001CAE9A File Offset: 0x001C909A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RouteMessageOutboundConnectorAction)));
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x001CAED3 File Offset: 0x001C90D3
		public bool Equals(RouteMessageOutboundConnectorAction other)
		{
			if (string.IsNullOrEmpty(this.ConnectorName))
			{
				return string.IsNullOrEmpty(other.ConnectorName);
			}
			return this.ConnectorName.Equals(other.ConnectorName);
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x0600701C RID: 28700 RVA: 0x001CAEFF File Offset: 0x001C90FF
		// (set) Token: 0x0600701D RID: 28701 RVA: 0x001CAF07 File Offset: 0x001C9107
		[ActionParameterName("RouteMessageOutboundConnector")]
		[LocDisplayName(RulesTasksStrings.IDs.ConnectorNameDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ConnectorNameDescription)]
		public string ConnectorName { get; set; }

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x0600701E RID: 28702 RVA: 0x001CAF10 File Offset: 0x001C9110
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRouteMessageOutboundConnector(this.ConnectorName);
			}
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x001CAF24 File Offset: 0x001C9124
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RouteMessageOutboundConnector")
			{
				return null;
			}
			RouteMessageOutboundConnectorAction routeMessageOutboundConnectorAction = new RouteMessageOutboundConnectorAction();
			try
			{
				routeMessageOutboundConnectorAction.ConnectorName = TransportRuleAction.GetStringValue(action.Arguments[0]);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return routeMessageOutboundConnectorAction;
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x001CAF7C File Offset: 0x001C917C
		internal override void Reset()
		{
			this.ConnectorName = string.Empty;
			base.Reset();
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x001CAF90 File Offset: 0x001C9190
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.ConnectorName == string.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			int index;
			if (!Utils.CheckIsUnicodeStringWellFormed(this.ConnectorName, out index))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)this.ConnectorName[index]), base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x001CB000 File Offset: 0x001C9200
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("RouteMessageOutboundConnector", new ShortList<Argument>
			{
				new Value(this.ConnectorName)
			}, Utils.GetActionName(this));
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x001CB03A File Offset: 0x001C923A
		internal override string GetActionParameters()
		{
			return Utils.QuoteCmdletParameter(this.ConnectorName);
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x001CB047 File Offset: 0x001C9247
		internal override void SuppressPiiData()
		{
			this.ConnectorName = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.RouteMessageOutboundConnector, this.ConnectorName);
		}
	}
}
