using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8B RID: 2955
	[ActionParameterName("RemoveOME")]
	[Serializable]
	public class RemoveOMEAction : TransportRuleAction, IEquatable<RemoveOMEAction>
	{
		// Token: 0x06006F5C RID: 28508 RVA: 0x001C8E27 File Offset: 0x001C7027
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x001C8E2A File Offset: 0x001C702A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || !(base.GetType() != right.GetType()));
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x001C8E58 File Offset: 0x001C7058
		public bool Equals(RemoveOMEAction other)
		{
			return this.Equals(other);
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x001C8E61 File Offset: 0x001C7061
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRemoveOME;
			}
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x001C8E6D File Offset: 0x001C706D
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RemoveOME")
			{
				return null;
			}
			return new RemoveOMEAction();
		}

		// Token: 0x06006F61 RID: 28513 RVA: 0x001C8E88 File Offset: 0x001C7088
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-E4eDecryptMessage"),
				new Value("true")
			};
			return TransportRuleParser.Instance.CreateAction("RemoveOME", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x001C8ED3 File Offset: 0x001C70D3
		internal override string GetActionParameters()
		{
			return "$true";
		}
	}
}
