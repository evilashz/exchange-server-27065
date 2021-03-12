using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200011A RID: 282
	internal class ReplayRequest : IReplayRequest, IDisposable
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x0002EA88 File Offset: 0x0002CC88
		public ReplayRequest(MessagingDatabase database, ReplayRequestStorage storage)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			this.database = database;
			this.Storage = storage;
			if (this.State != ResubmitRequestState.Completed)
			{
				this.database.SuspendDataCleanup(this.StartTime, this.EndTime);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0002EAFF File Offset: 0x0002CCFF
		public long TotalReplayedMessages
		{
			get
			{
				return this.totalReplayedMessages;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002EB07 File Offset: 0x0002CD07
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0002EB14 File Offset: 0x0002CD14
		public long ContinuationToken
		{
			get
			{
				return this.Storage.ContinuationToken;
			}
			private set
			{
				this.Storage.ContinuationToken = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002EB22 File Offset: 0x0002CD22
		public DateTime DateCreated
		{
			get
			{
				return this.Storage.DateCreated;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002EB2F File Offset: 0x0002CD2F
		public Destination Destination
		{
			get
			{
				return this.Storage.Destination;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0002EB49 File Offset: 0x0002CD49
		public string DiagnosticInformation
		{
			get
			{
				return this.Storage.DiagnosticInformation;
			}
			set
			{
				this.Storage.DiagnosticInformation = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002EB57 File Offset: 0x0002CD57
		public DateTime EndTime
		{
			get
			{
				return this.Storage.EndTime;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002EB64 File Offset: 0x0002CD64
		public long RequestId
		{
			get
			{
				return this.Storage.RequestId;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002EB71 File Offset: 0x0002CD71
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0002EB7E File Offset: 0x0002CD7E
		public long PrimaryRequestId
		{
			get
			{
				return this.Storage.PrimaryRequestId;
			}
			set
			{
				this.Storage.PrimaryRequestId = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002EB8C File Offset: 0x0002CD8C
		public DateTime StartTime
		{
			get
			{
				return this.Storage.StartTime;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002EB99 File Offset: 0x0002CD99
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
		public ResubmitRequestState State
		{
			get
			{
				return this.Storage.State;
			}
			set
			{
				if (this.Storage.State == value)
				{
					return;
				}
				if (this.Storage.State == ResubmitRequestState.Completed)
				{
					this.database.SuspendDataCleanup(this.StartTime, this.EndTime);
				}
				if (value == ResubmitRequestState.Completed)
				{
					this.database.ResumeDataCleanup(this.StartTime, this.EndTime);
				}
				this.Storage.State = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002EC0E File Offset: 0x0002CE0E
		public int OutstandingMailItemCount
		{
			get
			{
				return this.outstandingMailItemCount;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002EC16 File Offset: 0x0002CE16
		public DateTime LastResubmittedMessageOrginalDeliveryTime
		{
			get
			{
				return this.lastResubmittedMessageOrginalDeliveryTime;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002EC1E File Offset: 0x0002CE1E
		public Guid CorrelationId
		{
			get
			{
				return this.Storage.CorrelationId;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002EC2B File Offset: 0x0002CE2B
		public bool IsTestRequest
		{
			get
			{
				return this.Storage.IsTestRequest;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002F1A0 File Offset: 0x0002D3A0
		public IEnumerable<TransportMailItem> GetMessagesForRedelivery(int count)
		{
			int returnedCount = 0;
			string conditions = null;
			if (this.Destination.Type == Destination.DestinationType.Conditional)
			{
				try
				{
					conditions = this.Destination.ToString();
				}
				catch (Exception)
				{
					conditions = null;
				}
			}
			if (this.bookmarks == null)
			{
				if (this.Destination.Type == Destination.DestinationType.Conditional)
				{
					this.bookmarks = new ReplayRequest.ResumableCollection<Tuple<MessagingGeneration, IGrouping<int, int>>>(this.database.GetConditionalDeliveredBookmarks(this.StartTime, this.EndTime, this.Destination, this.ContinuationToken, conditions));
				}
				else
				{
					this.bookmarks = new ReplayRequest.ResumableCollection<Tuple<MessagingGeneration, IGrouping<int, int>>>(this.database.GetDeliveredBookmarks(this.Destination, this.StartTime, this.EndTime, this.ContinuationToken));
				}
				this.requestStopwatch.Reset();
			}
			this.requestStopwatch.Start();
			Stopwatch stopwatch = Stopwatch.StartNew();
			long continuationTokenFromSearch = this.ContinuationToken;
			foreach (MailItemAndRecipients mailItemAndRecipients in this.database.GetDeliveredMessages(this.bookmarks.ToIEnumerable(new int?(count)), ref continuationTokenFromSearch, conditions))
			{
				MailItemAndRecipients mailItemAndRecipients2 = mailItemAndRecipients;
				TransportMailItem transportMailItem = TransportMailItem.NewMailItem(mailItemAndRecipients2.MailItem, LatencyComponent.Replay);
				MailItemAndRecipients mailItemAndRecipients3 = mailItemAndRecipients;
				foreach (IMailRecipientStorage mailRecipientStorage in mailItemAndRecipients3.Recipients)
				{
					transportMailItem.AddRecipient(mailRecipientStorage);
					this.lastResubmittedMessageOrginalDeliveryTime = mailRecipientStorage.DeliveryTime.GetValueOrDefault();
				}
				this.ContinuationToken = transportMailItem.MsgId;
				returnedCount++;
				this.perfCounters.ReplayedItemCount.Increment();
				this.perfCounters.ReplayedItemAverageLatency.IncrementBy(stopwatch.ElapsedTicks);
				this.perfCounters.ReplayedItemAverageLatencyBase.Increment();
				this.requestStopwatch.Stop();
				stopwatch.Stop();
				yield return transportMailItem;
				this.requestStopwatch.Start();
				stopwatch.Restart();
			}
			if (returnedCount == 0)
			{
				this.ContinuationToken = continuationTokenFromSearch;
			}
			this.totalReplayedMessages += (long)returnedCount;
			this.requestStopwatch.Stop();
			if (this.bookmarks.Finished)
			{
				this.State = ResubmitRequestState.Completed;
				ExTraceGlobals.StorageTracer.TracePerformance(this.RequestId, "Request for {0}:{1} ({2}-{3}) has returned {4} messages and took {5} on db code", new object[]
				{
					this.Destination.Type,
					this.Destination,
					this.StartTime,
					this.EndTime,
					this.TotalReplayedMessages,
					this.requestStopwatch.Elapsed
				});
			}
			yield break;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
		public void AddMailItemReference()
		{
			Interlocked.Increment(ref this.outstandingMailItemCount);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002F1D2 File Offset: 0x0002D3D2
		public void ReleaseMailItemReference()
		{
			if (Interlocked.Decrement(ref this.outstandingMailItemCount) < 0)
			{
				throw new InvalidOperationException("Outstanding count cannot be negative");
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002F1ED File Offset: 0x0002D3ED
		public virtual void Commit()
		{
			this.Storage.Commit();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002F1FA File Offset: 0x0002D3FA
		public void Materialize(Transaction transaction)
		{
			this.Storage.Materialize(transaction);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002F208 File Offset: 0x0002D408
		public void Delete()
		{
			this.State = ResubmitRequestState.Completed;
			this.Storage.MarkToDelete();
			this.Commit();
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002F222 File Offset: 0x0002D422
		public void Dispose()
		{
			if (this.bookmarks != null)
			{
				this.bookmarks.Dispose();
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002F238 File Offset: 0x0002D438
		public override string ToString()
		{
			return string.Format("From:{0} To:{1} Destination:{2} State:{3}", new object[]
			{
				this.StartTime,
				this.EndTime,
				this.Destination,
				this.State
			});
		}

		// Token: 0x0400057D RID: 1405
		protected readonly ReplayRequestStorage Storage;

		// Token: 0x0400057E RID: 1406
		private readonly Stopwatch requestStopwatch = new Stopwatch();

		// Token: 0x0400057F RID: 1407
		private readonly MessagingDatabase database;

		// Token: 0x04000580 RID: 1408
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x04000581 RID: 1409
		private ReplayRequest.ResumableCollection<Tuple<MessagingGeneration, IGrouping<int, int>>> bookmarks;

		// Token: 0x04000582 RID: 1410
		private long totalReplayedMessages;

		// Token: 0x04000583 RID: 1411
		private int outstandingMailItemCount;

		// Token: 0x04000584 RID: 1412
		private DateTime lastResubmittedMessageOrginalDeliveryTime;

		// Token: 0x0200011B RID: 283
		internal class ResumableCollection<T> : IDisposable
		{
			// Token: 0x1700038F RID: 911
			// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002F28A File Offset: 0x0002D48A
			public bool Finished
			{
				get
				{
					return this.finished;
				}
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002F292 File Offset: 0x0002D492
			public IEnumerator<T> Enumerator
			{
				get
				{
					return this.enumerator;
				}
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x0002F29A File Offset: 0x0002D49A
			public ResumableCollection(IEnumerable<T> collection)
			{
				this.enumerator = collection.GetEnumerator();
				this.finished = false;
			}

			// Token: 0x06000CEB RID: 3307 RVA: 0x0002F40C File Offset: 0x0002D60C
			public IEnumerable<T> ToIEnumerable(int? take = null)
			{
				int returnedCount = 0;
				while (take == null || returnedCount < take)
				{
					if (!this.enumerator.MoveNext())
					{
						this.finished = true;
						break;
					}
					yield return this.enumerator.Current;
					returnedCount++;
				}
				yield break;
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x0002F430 File Offset: 0x0002D630
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x04000585 RID: 1413
			private readonly IEnumerator<T> enumerator;

			// Token: 0x04000586 RID: 1414
			private bool finished;
		}
	}
}
