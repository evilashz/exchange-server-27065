using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RetryableWorkItem : DisposeTrackableBase
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000605E File Offset: 0x0000425E
		protected RetryableWorkItem(int initialRetryInMilliseconds, int retryBackoffFactor, int maximumNumberOfAttempts)
		{
			this.initialRetryInMilliseconds = initialRetryInMilliseconds;
			this.retryBackoffFactor = retryBackoffFactor;
			this.maximumNumberOfAttempts = maximumNumberOfAttempts;
			this.ResetRetryState();
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00006081 File Offset: 0x00004281
		public bool IsMaximumNumberOfAttemptsReached
		{
			get
			{
				base.CheckDisposed();
				return this.numberOfAttempts >= this.maximumNumberOfAttempts;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000609A File Offset: 0x0000429A
		public int CurrentRetryCount
		{
			get
			{
				base.CheckDisposed();
				return this.numberOfAttempts - 1;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000060AA File Offset: 0x000042AA
		public string TypeFullName
		{
			get
			{
				base.CheckDisposed();
				if (this.typeFullName == null)
				{
					this.typeFullName = base.GetType().FullName;
				}
				return this.typeFullName;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000060D4 File Offset: 0x000042D4
		public int UpdateRetryStateOnRetry()
		{
			base.CheckDisposed();
			this.numberOfAttempts++;
			int result = this.nextRetryWait;
			this.nextRetryWait *= this.retryBackoffFactor;
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006110 File Offset: 0x00004310
		public void MaxOutRetryCount()
		{
			base.CheckDisposed();
			this.maximumNumberOfAttempts = this.numberOfAttempts;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006124 File Offset: 0x00004324
		protected void ResetRetryState()
		{
			base.CheckDisposed();
			this.numberOfAttempts = 1;
			this.nextRetryWait = this.initialRetryInMilliseconds;
		}

		// Token: 0x0400006C RID: 108
		private readonly int initialRetryInMilliseconds;

		// Token: 0x0400006D RID: 109
		private readonly int retryBackoffFactor;

		// Token: 0x0400006E RID: 110
		private int maximumNumberOfAttempts;

		// Token: 0x0400006F RID: 111
		private int numberOfAttempts;

		// Token: 0x04000070 RID: 112
		private int nextRetryWait;

		// Token: 0x04000071 RID: 113
		private string typeFullName;
	}
}
