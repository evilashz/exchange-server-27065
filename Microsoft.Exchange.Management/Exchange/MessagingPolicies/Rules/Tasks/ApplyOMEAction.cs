using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8E RID: 2958
	[ActionParameterName("ApplyOME")]
	[Serializable]
	public class ApplyOMEAction : TransportRuleAction, IEquatable<ApplyOMEAction>
	{
		// Token: 0x06006F74 RID: 28532 RVA: 0x001C8FF0 File Offset: 0x001C71F0
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006F75 RID: 28533 RVA: 0x001C8FF3 File Offset: 0x001C71F3
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006F76 RID: 28534 RVA: 0x001C9021 File Offset: 0x001C7221
		public bool Equals(ApplyOMEAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x06006F77 RID: 28535 RVA: 0x001C902A File Offset: 0x001C722A
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionApplyOME;
			}
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x001C9036 File Offset: 0x001C7236
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "ApplyOME")
			{
				return null;
			}
			return new ApplyOMEAction();
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x001C9054 File Offset: 0x001C7254
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-E4eEncryptMessage"),
				new Value("true")
			};
			return TransportRuleParser.Instance.CreateAction("ApplyOME", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006F7A RID: 28538 RVA: 0x001C909F File Offset: 0x001C729F
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
