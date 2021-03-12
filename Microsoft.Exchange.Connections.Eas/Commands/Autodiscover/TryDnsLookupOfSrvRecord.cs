using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TryDnsLookupOfSrvRecord : AutodiscoverStep
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004062 File Offset: 0x00002262
		internal TryDnsLookupOfSrvRecord(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.Failed)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004070 File Offset: 0x00002270
		public override Step ExecuteStep(StepContext stepContext)
		{
			return base.NextStepOnFailure;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004078 File Offset: 0x00002278
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.ExistingEndpoint;
		}
	}
}
