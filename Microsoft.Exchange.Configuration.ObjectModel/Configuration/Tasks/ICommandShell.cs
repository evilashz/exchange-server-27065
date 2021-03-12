using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200002F RID: 47
	internal interface ICommandShell
	{
		// Token: 0x060001EB RID: 491
		void WriteObject(object sendToPipeline);

		// Token: 0x060001EC RID: 492
		void WriteError(LocalizedException exception, ExchangeErrorCategory category, object target);

		// Token: 0x060001ED RID: 493
		void WriteError(LocalizedException exception, ExchangeErrorCategory category, object target, bool reThrow);

		// Token: 0x060001EE RID: 494
		void ThrowTerminatingError(LocalizedException exception, ExchangeErrorCategory category, object target);

		// Token: 0x060001EF RID: 495
		void WriteVerbose(LocalizedString message);

		// Token: 0x060001F0 RID: 496
		void WriteDebug(LocalizedString message);

		// Token: 0x060001F1 RID: 497
		void WriteWarning(LocalizedString message);

		// Token: 0x060001F2 RID: 498
		void WriteProgress(ExProgressRecord record);

		// Token: 0x060001F3 RID: 499
		bool ShouldContinue(LocalizedString promptMessage);

		// Token: 0x060001F4 RID: 500
		bool ShouldProcess(LocalizedString promptMessage);

		// Token: 0x060001F5 RID: 501
		void SetShouldExit(int exitCode);

		// Token: 0x060001F6 RID: 502
		bool TryGetVariableValue<T>(string name, out T value);

		// Token: 0x060001F7 RID: 503
		void PrependTaskIOPipelineHandler(ITaskIOPipeline pipeline);
	}
}
