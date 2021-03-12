using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000176 RID: 374
	internal interface IReportBytes
	{
		// Token: 0x06001001 RID: 4097
		void ReportBytesRead(int count);

		// Token: 0x06001002 RID: 4098
		void ReportBytesWritten(int count);
	}
}
