using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncOperationResult<TData>
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x000086FC File Offset: 0x000068FC
		public AsyncOperationResult(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000870B File Offset: 0x0000690B
		public AsyncOperationResult(TData data)
		{
			this.data = data;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000871A File Offset: 0x0000691A
		public AsyncOperationResult(TData data, Exception exception)
		{
			this.data = data;
			this.exception = exception;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008730 File Offset: 0x00006930
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008738 File Offset: 0x00006938
		public TData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008740 File Offset: 0x00006940
		public bool IsCanceled
		{
			get
			{
				return this.exception is OperationCanceledException;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008750 File Offset: 0x00006950
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000875B File Offset: 0x0000695B
		public bool IsRetryable
		{
			get
			{
				return this.exception is TransientException;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000876B File Offset: 0x0000696B
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

		// Token: 0x040000F5 RID: 245
		public static readonly Exception CanceledException = new OperationCanceledException();

		// Token: 0x040000F6 RID: 246
		private readonly TData data;

		// Token: 0x040000F7 RID: 247
		private readonly Exception exception;
	}
}
