using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9E RID: 2974
	[Serializable]
	public class SetAuditSeverityAction : TransportRuleAction, IEquatable<SetAuditSeverityAction>
	{
		// Token: 0x0600702E RID: 28718 RVA: 0x001CB0F3 File Offset: 0x001C92F3
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.SeverityLevel))
			{
				return this.SeverityLevel.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x001CB10F File Offset: 0x001C930F
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SetAuditSeverityAction)));
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x001CB148 File Offset: 0x001C9348
		public bool Equals(SetAuditSeverityAction other)
		{
			if (string.IsNullOrEmpty(this.SeverityLevel))
			{
				return string.IsNullOrEmpty(other.SeverityLevel);
			}
			return this.SeverityLevel.Equals(other.SeverityLevel);
		}

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x06007031 RID: 28721 RVA: 0x001CB174 File Offset: 0x001C9374
		// (set) Token: 0x06007032 RID: 28722 RVA: 0x001CB17C File Offset: 0x001C937C
		[LocDescription(RulesTasksStrings.IDs.SetAuditSeverityDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SetAuditSeverityDisplayName)]
		[ActionParameterName("SetAuditSeverity")]
		public string SeverityLevel { get; set; }

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x06007033 RID: 28723 RVA: 0x001CB188 File Offset: 0x001C9388
		internal override string Description
		{
			get
			{
				string severityLevel = string.Empty;
				if (!string.IsNullOrEmpty(this.SeverityLevel))
				{
					severityLevel = LocalizedDescriptionAttribute.FromEnum(typeof(AuditSeverityLevel), Enum.Parse(typeof(AuditSeverityLevel), this.SeverityLevel, false));
				}
				return RulesTasksStrings.RuleDescriptionSetAuditSeverity(severityLevel);
			}
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x001CB1DC File Offset: 0x001C93DC
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "AuditSeverityLevel")
			{
				return null;
			}
			SetAuditSeverityAction setAuditSeverityAction = new SetAuditSeverityAction();
			TransportRuleAction result;
			try
			{
				setAuditSeverityAction.SeverityLevel = TransportRuleAction.GetStringValue(action.Arguments[0]);
				result = setAuditSeverityAction;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x001CB234 File Offset: 0x001C9434
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(this.SeverityLevel)
			};
			return TransportRuleParser.Instance.CreateAction("AuditSeverityLevel", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x001CB270 File Offset: 0x001C9470
		internal override string GetActionParameters()
		{
			return Utils.QuoteCmdletParameter(this.SeverityLevel);
		}
	}
}
