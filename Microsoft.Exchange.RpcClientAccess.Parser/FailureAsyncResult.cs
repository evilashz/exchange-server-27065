using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200014C RID: 332
	internal class FailureAsyncResult<TErrorCode> : EasyCancelableAsyncResult
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0001125B File Offset: 0x0000F45B
		public FailureAsyncResult(TErrorCode errorCode, IntPtr contextHandle, Exception exception, CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.errorCode = errorCode;
			this.contextHandle = contextHandle;
			this.exception = exception;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001127C File Offset: 0x0000F47C
		public TErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00011284 File Offset: 0x0000F484
		public IntPtr ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001128C File Offset: 0x0000F48C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00011294 File Offset: 0x0000F494
		protected override void InternalCancel()
		{
		}

		// Token: 0x0400032D RID: 813
		private readonly TErrorCode errorCode;

		// Token: 0x0400032E RID: 814
		private readonly IntPtr contextHandle;

		// Token: 0x0400032F RID: 815
		private readonly Exception exception;
	}
}
