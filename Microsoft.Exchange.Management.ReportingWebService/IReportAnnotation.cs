using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000023 RID: 35
	internal interface IReportAnnotation
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B9 RID: 185
		string ReportTitle { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186
		IEnumerable<string> Xaxis { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BB RID: 187
		IEnumerable<string> Yaxis { get; }
	}
}
