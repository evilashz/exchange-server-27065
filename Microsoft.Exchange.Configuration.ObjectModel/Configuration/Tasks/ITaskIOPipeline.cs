using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000030 RID: 48
	public interface ITaskIOPipeline
	{
		// Token: 0x060001F8 RID: 504
		bool WriteVerbose(LocalizedString input, out LocalizedString output);

		// Token: 0x060001F9 RID: 505
		bool WriteDebug(LocalizedString input, out LocalizedString output);

		// Token: 0x060001FA RID: 506
		bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output);

		// Token: 0x060001FB RID: 507
		bool WriteError(TaskErrorInfo input, out TaskErrorInfo output);

		// Token: 0x060001FC RID: 508
		bool WriteObject(object input, out object output);

		// Token: 0x060001FD RID: 509
		bool WriteProgress(ExProgressRecord input, out ExProgressRecord output);

		// Token: 0x060001FE RID: 510
		bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output);

		// Token: 0x060001FF RID: 511
		bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output);
	}
}
