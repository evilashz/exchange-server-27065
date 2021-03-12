using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000005 RID: 5
	internal class HaltAction : Microsoft.Exchange.MessagingPolicies.Rules.Action
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002235 File Offset: 0x00000435
		public HaltAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000223E File Offset: 0x0000043E
		public override string Name
		{
			get
			{
				return "Halt";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002245 File Offset: 0x00000445
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			return ExecutionControl.SkipAll;
		}
	}
}
