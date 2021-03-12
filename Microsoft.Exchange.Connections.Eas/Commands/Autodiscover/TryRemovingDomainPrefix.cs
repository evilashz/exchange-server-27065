using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TryRemovingDomainPrefix : AutodiscoverStep
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000041B0 File Offset: 0x000023B0
		internal TryRemovingDomainPrefix(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.TryAddingAutodiscoverPrefix)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000041BC File Offset: 0x000023BC
		public override Step ExecuteStep(StepContext stepContext)
		{
			string mostRecentDomain;
			if (base.TryStrippingPrefixFromDomain(stepContext.EasConnectionSettings.EasEndpointSettings.Domain, out mostRecentDomain))
			{
				stepContext.EasConnectionSettings.EasEndpointSettings.MostRecentDomain = mostRecentDomain;
				return Step.TrySmtpAddress;
			}
			return base.NextStepOnFailure;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000041FC File Offset: 0x000023FC
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.ExistingEndpoint;
		}
	}
}
