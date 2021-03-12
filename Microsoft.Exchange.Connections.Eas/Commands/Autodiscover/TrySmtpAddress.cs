using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TrySmtpAddress : AutodiscoverStep
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000420F File Offset: 0x0000240F
		internal TrySmtpAddress(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.TryRemovingDomainPrefix)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004219 File Offset: 0x00002419
		public override Step ExecuteStep(StepContext stepContext)
		{
			stepContext.ProbeStack.Push(base.EasConnectionSettings.EasEndpointSettings.Domain);
			return base.ExecuteStep(stepContext);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000423D File Offset: 0x0000243D
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.ExistingEndpoint;
		}
	}
}
