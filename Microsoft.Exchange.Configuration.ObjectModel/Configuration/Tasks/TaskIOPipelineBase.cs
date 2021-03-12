using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A9 RID: 169
	public class TaskIOPipelineBase : ITaskIOPipeline
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x00018F60 File Offset: 0x00017160
		public virtual bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00018F6A File Offset: 0x0001716A
		public virtual bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00018F74 File Offset: 0x00017174
		public virtual bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00018F7E File Offset: 0x0001717E
		public virtual bool WriteError(TaskErrorInfo input, out TaskErrorInfo output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00018F84 File Offset: 0x00017184
		public virtual bool WriteObject(object input, out object output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00018F8A File Offset: 0x0001718A
		public virtual bool WriteProgress(ExProgressRecord input, out ExProgressRecord output)
		{
			output = input;
			return true;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00018F90 File Offset: 0x00017190
		public virtual bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			output = null;
			return true;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00018F9B File Offset: 0x0001719B
		public virtual bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output)
		{
			output = null;
			return true;
		}
	}
}
