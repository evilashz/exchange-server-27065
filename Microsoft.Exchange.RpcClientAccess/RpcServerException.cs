using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcServerException : Exception
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000090DF File Offset: 0x000072DF
		internal RpcServerException(string message, RpcErrorCode storeError) : base(message)
		{
			this.storeError = storeError;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000090EF File Offset: 0x000072EF
		internal RpcServerException(string message, RpcErrorCode storeError, Exception innerException) : base(message, innerException)
		{
			this.storeError = storeError;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009100 File Offset: 0x00007300
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00009108 File Offset: 0x00007308
		public bool DropConnection
		{
			get
			{
				return this.dropConnection;
			}
			set
			{
				this.dropConnection = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009111 File Offset: 0x00007311
		public override string Message
		{
			get
			{
				return string.Format("{0} (StoreError={1})", base.Message, this.storeError);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000912E File Offset: 0x0000732E
		internal RpcErrorCode StoreError
		{
			get
			{
				return this.storeError;
			}
		}

		// Token: 0x040001F3 RID: 499
		private readonly RpcErrorCode storeError;

		// Token: 0x040001F4 RID: 500
		private bool dropConnection;
	}
}
