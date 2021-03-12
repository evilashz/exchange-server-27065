using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200003B RID: 59
	internal class ADOperationResult
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00012D10 File Offset: 0x00010F10
		public ADOperationResult(ADOperationErrorCode errorCode, Exception e)
		{
			this.errorCode = errorCode;
			this.exception = e;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00012D26 File Offset: 0x00010F26
		public bool Succeeded
		{
			get
			{
				return this.errorCode == ADOperationErrorCode.Success;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00012D31 File Offset: 0x00010F31
		public ADOperationErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00012D39 File Offset: 0x00010F39
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040000F6 RID: 246
		private ADOperationErrorCode errorCode;

		// Token: 0x040000F7 RID: 247
		private Exception exception;

		// Token: 0x040000F8 RID: 248
		public static ADOperationResult Success = new ADOperationResult(ADOperationErrorCode.Success, null);
	}
}
