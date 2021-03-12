using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA0 RID: 2976
	[Serializable]
	public class SetSclAction : TransportRuleAction, IEquatable<SetSclAction>
	{
		// Token: 0x06007047 RID: 28743 RVA: 0x001CB5C8 File Offset: 0x001C97C8
		public override int GetHashCode()
		{
			return this.SclValue.GetHashCode();
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x001CB5E9 File Offset: 0x001C97E9
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SetSclAction)));
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x001CB624 File Offset: 0x001C9824
		public bool Equals(SetSclAction other)
		{
			return this.SclValue.Equals(other.SclValue);
		}

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x0600704A RID: 28746 RVA: 0x001CB645 File Offset: 0x001C9845
		// (set) Token: 0x0600704B RID: 28747 RVA: 0x001CB64D File Offset: 0x001C984D
		[LocDisplayName(RulesTasksStrings.IDs.SclValueDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.SclValueDescription)]
		[ActionParameterName("SetSCL")]
		public SclValue SclValue
		{
			get
			{
				return this.sclValue;
			}
			set
			{
				this.sclValue = value;
			}
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x0600704C RID: 28748 RVA: 0x001CB658 File Offset: 0x001C9858
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSetScl(this.SclValue.ToString());
			}
		}

		// Token: 0x0600704D RID: 28749 RVA: 0x001CB684 File Offset: 0x001C9884
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "SetHeader" || TransportRuleAction.GetStringValue(action.Arguments[0]) != "X-MS-Exchange-Organization-SCL")
			{
				return null;
			}
			int input;
			if (!int.TryParse(TransportRuleAction.GetStringValue(action.Arguments[1]), out input))
			{
				return null;
			}
			SetSclAction setSclAction = new SetSclAction();
			try
			{
				setSclAction.SclValue = new SclValue(input);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return setSclAction;
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x001CB70C File Offset: 0x001C990C
		internal override void Reset()
		{
			this.sclValue = new SclValue(0);
			base.Reset();
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x001CB720 File Offset: 0x001C9920
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-SCL"),
				new Value(this.SclValue.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("SetHeader", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x001CB77C File Offset: 0x001C997C
		internal override string GetActionParameters()
		{
			return this.SclValue.ToString();
		}

		// Token: 0x040039DB RID: 14811
		private SclValue sclValue;
	}
}
