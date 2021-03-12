using System;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000075 RID: 117
	public abstract class DarWorkloadHost
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000B0B0 File Offset: 0x000092B0
		public virtual DarTaskExecutionCommand ShouldContinue(DarTask task, out string additionalInformation)
		{
			additionalInformation = null;
			return DarTaskExecutionCommand.ContinueExecution;
		}
	}
}
