using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000032 RID: 50
	internal sealed class EhfTransientFailureInfo
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000F17C File Offset: 0x0000D37C
		public EhfTransientFailureInfo(Exception failureException, string operationName)
		{
			this.failureException = failureException;
			this.operationName = operationName;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000F192 File Offset: 0x0000D392
		public Exception FailureException
		{
			get
			{
				return this.failureException;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000F19A File Offset: 0x0000D39A
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x040000EA RID: 234
		private readonly Exception failureException;

		// Token: 0x040000EB RID: 235
		private readonly string operationName;
	}
}
