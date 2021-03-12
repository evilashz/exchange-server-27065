using System;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000221 RID: 545
	internal interface IReportProgress
	{
		// Token: 0x060012FD RID: 4861
		void ReportProgress(int workProcessed, int totalWork, string statusText, string errorText);
	}
}
