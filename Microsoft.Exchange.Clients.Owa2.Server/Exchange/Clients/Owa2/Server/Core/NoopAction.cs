using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000224 RID: 548
	internal sealed class NoopAction : Microsoft.Exchange.MessagingPolicies.Rules.Action
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x0004A12C File Offset: 0x0004832C
		public NoopAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0004A135 File Offset: 0x00048335
		public override string Name
		{
			get
			{
				return "Noop";
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0004A13C File Offset: 0x0004833C
		public override void ValidateArguments(ShortList<Argument> inputArguments)
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004A13E File Offset: 0x0004833E
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			return ExecutionControl.Execute;
		}
	}
}
