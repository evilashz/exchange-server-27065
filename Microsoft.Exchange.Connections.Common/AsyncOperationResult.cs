using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncOperationResult<TData>
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020DC File Offset: 0x000002DC
		public AsyncOperationResult(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EB File Offset: 0x000002EB
		public AsyncOperationResult(TData data)
		{
			this.data = data;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FA File Offset: 0x000002FA
		public AsyncOperationResult(TData data, Exception exception)
		{
			this.data = data;
			this.exception = exception;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002117 File Offset: 0x00000317
		public static Exception CanceledException { get; private set; } = new OperationCanceledException();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000211F File Offset: 0x0000031F
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002127 File Offset: 0x00000327
		public TData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212F File Offset: 0x0000032F
		public bool IsCanceled
		{
			get
			{
				return this.exception is OperationCanceledException;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000213F File Offset: 0x0000033F
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000214A File Offset: 0x0000034A
		public bool IsRetryable
		{
			get
			{
				return this.exception is TransientException;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000215A File Offset: 0x0000035A
		public override string ToString()
		{
			if (this.IsCanceled)
			{
				return "Canceled";
			}
			if (this.IsSucceeded)
			{
				return "Success";
			}
			return this.exception.ToString();
		}

		// Token: 0x04000001 RID: 1
		private readonly TData data;

		// Token: 0x04000002 RID: 2
		private readonly Exception exception;
	}
}
