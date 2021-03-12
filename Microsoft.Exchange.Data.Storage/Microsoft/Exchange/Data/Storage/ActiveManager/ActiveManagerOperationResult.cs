using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000305 RID: 773
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActiveManagerOperationResult
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x0008DAE8 File Offset: 0x0008BCE8
		public ActiveManagerOperationResult(bool succeeded, Exception ex)
		{
			this.exception = ex;
			if (succeeded)
			{
				this.errorCode = ActiveManagerOperationResultCode.Success;
				return;
			}
			if (this.Exception is DatabaseNotFoundException)
			{
				this.errorCode = ActiveManagerOperationResultCode.TransientError;
				return;
			}
			if (this.Exception is StorageTransientException)
			{
				this.errorCode = ActiveManagerOperationResultCode.TransientError;
				return;
			}
			if (this.Exception is ObjectNotFoundException)
			{
				this.errorCode = ActiveManagerOperationResultCode.PermanentError;
				return;
			}
			if (this.Exception is StoragePermanentException)
			{
				this.errorCode = ActiveManagerOperationResultCode.PermanentError;
				return;
			}
			if (this.Exception is ServerForDatabaseNotFoundException)
			{
				this.errorCode = ActiveManagerOperationResultCode.ServerForDatabaseNotFoundException;
				return;
			}
			this.errorCode = ActiveManagerOperationResultCode.PermanentError;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0008DB7D File Offset: 0x0008BD7D
		public bool Succeeded
		{
			get
			{
				return this.errorCode == ActiveManagerOperationResultCode.Success;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0008DB88 File Offset: 0x0008BD88
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0008DB90 File Offset: 0x0008BD90
		public ActiveManagerOperationResultCode ResultCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04001469 RID: 5225
		private ActiveManagerOperationResultCode errorCode;

		// Token: 0x0400146A RID: 5226
		private Exception exception;
	}
}
