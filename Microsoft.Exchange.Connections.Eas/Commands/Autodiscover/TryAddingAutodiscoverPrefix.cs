using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TryAddingAutodiscoverPrefix : AutodiscoverStep
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000400C File Offset: 0x0000220C
		internal TryAddingAutodiscoverPrefix(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.TryUnauthenticatedGet)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004018 File Offset: 0x00002218
		public override Step ExecuteStep(StepContext stepContext)
		{
			string autodiscoverDomain = base.GetAutodiscoverDomain(base.EasConnectionSettings.EasEndpointSettings.Domain);
			stepContext.ProbeStack.Push(autodiscoverDomain);
			return base.ExecuteStep(stepContext);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000404F File Offset: 0x0000224F
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.ExistingEndpoint;
		}
	}
}
