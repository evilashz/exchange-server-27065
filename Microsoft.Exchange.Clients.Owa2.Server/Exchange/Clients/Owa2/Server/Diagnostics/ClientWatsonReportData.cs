using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000436 RID: 1078
	internal struct ClientWatsonReportData
	{
		// Token: 0x04001439 RID: 5177
		public string TraceComponent;

		// Token: 0x0400143A RID: 5178
		public string FunctionName;

		// Token: 0x0400143B RID: 5179
		public string PackageName;

		// Token: 0x0400143C RID: 5180
		public string ExceptionMessage;

		// Token: 0x0400143D RID: 5181
		public string ExceptionType;

		// Token: 0x0400143E RID: 5182
		public string NormalizedCallStack;

		// Token: 0x0400143F RID: 5183
		public string OriginalCallStack;

		// Token: 0x04001440 RID: 5184
		public int CallStackHash;

		// Token: 0x04001441 RID: 5185
		public bool? IsUnhandledException;
	}
}
