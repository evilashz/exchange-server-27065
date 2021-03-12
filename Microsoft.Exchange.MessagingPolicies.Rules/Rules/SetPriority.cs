using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000086 RID: 134
	internal class SetPriority : TransportAction
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0001586F File Offset: 0x00013A6F
		public SetPriority(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00015878 File Offset: 0x00013A78
		public override string Name
		{
			get
			{
				return "SetPriority";
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001587F File Offset: 0x00013A7F
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			return ExecutionControl.Execute;
		}
	}
}
