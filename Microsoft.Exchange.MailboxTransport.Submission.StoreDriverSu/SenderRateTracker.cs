using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000025 RID: 37
	internal class SenderRateTracker
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00009A70 File Offset: 0x00007C70
		public SenderRateTracker(TimeSpan slidingWindowLength, TimeSpan bucketLength)
		{
			this.slidingWindowLength = slidingWindowLength;
			this.bucketLength = bucketLength;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009A9C File Offset: 0x00007C9C
		public long IncrementSenderRate(Guid senderMailbox, DateTime messageCreateTime)
		{
			HistoricalSlidingTotalCounter historicalSlidingTotalCounter = null;
			try
			{
				this.readerWriterLock.EnterReadLock();
				this.dictionary.TryGetValue(senderMailbox, out historicalSlidingTotalCounter);
			}
			finally
			{
				this.readerWriterLock.ExitReadLock();
			}
			if (historicalSlidingTotalCounter == null)
			{
				try
				{
					this.readerWriterLock.EnterWriteLock();
					if (!this.dictionary.TryGetValue(senderMailbox, out historicalSlidingTotalCounter))
					{
						historicalSlidingTotalCounter = new HistoricalSlidingTotalCounter(this.slidingWindowLength, this.bucketLength, messageCreateTime);
						this.dictionary[senderMailbox] = historicalSlidingTotalCounter;
					}
				}
				finally
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			return historicalSlidingTotalCounter.AddValue(1L, messageCreateTime);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009B44 File Offset: 0x00007D44
		public void ResetSenderRate(Guid senderMailbox, DateTime messageCreateTime)
		{
			try
			{
				this.readerWriterLock.EnterWriteLock();
				this.dictionary[senderMailbox] = new HistoricalSlidingTotalCounter(this.slidingWindowLength, this.bucketLength, messageCreateTime);
			}
			finally
			{
				this.readerWriterLock.ExitWriteLock();
			}
		}

		// Token: 0x0400009F RID: 159
		private readonly Dictionary<Guid, HistoricalSlidingTotalCounter> dictionary = new Dictionary<Guid, HistoricalSlidingTotalCounter>();

		// Token: 0x040000A0 RID: 160
		private readonly ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();

		// Token: 0x040000A1 RID: 161
		private readonly TimeSpan slidingWindowLength;

		// Token: 0x040000A2 RID: 162
		private readonly TimeSpan bucketLength;
	}
}
