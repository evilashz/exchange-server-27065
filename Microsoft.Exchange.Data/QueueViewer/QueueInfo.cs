using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000279 RID: 633
	[Serializable]
	public class QueueInfo : PagedDataObject, IConfigurable
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x00044102 File Offset: 0x00042302
		internal QueueInfo(QueueIdentity identity)
		{
			this.identity = identity;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00044114 File Offset: 0x00042314
		public QueueInfo(ExtensibleQueueInfo queueInfo)
		{
			this.identity = queueInfo.QueueIdentity;
			this.deliveryType = queueInfo.DeliveryType;
			this.nextHopConnector = queueInfo.NextHopConnector;
			this.status = queueInfo.Status;
			this.messageCount = queueInfo.MessageCount;
			this.lastError = queueInfo.LastError;
			this.lastRetryTime = queueInfo.LastRetryTime;
			this.nextRetryTime = queueInfo.NextRetryTime;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00044187 File Offset: 0x00042387
		public bool IsDeliveryQueue()
		{
			return this.identity.Type == QueueType.Delivery;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00044197 File Offset: 0x00042397
		public bool IsSubmissionQueue()
		{
			return this.identity.Type == QueueType.Submission;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000441A7 File Offset: 0x000423A7
		public bool IsPoisonQueue()
		{
			return this.identity.Type == QueueType.Poison;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000441B7 File Offset: 0x000423B7
		public bool IsShadowQueue()
		{
			return this.identity.Type == QueueType.Shadow;
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000441C7 File Offset: 0x000423C7
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x000441CF File Offset: 0x000423CF
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x000441D7 File Offset: 0x000423D7
		public DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
			internal set
			{
				this.deliveryType = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000441E0 File Offset: 0x000423E0
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x000441ED File Offset: 0x000423ED
		public string NextHopDomain
		{
			get
			{
				return this.identity.NextHopDomain;
			}
			internal set
			{
				this.identity.NextHopDomain = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000441FB File Offset: 0x000423FB
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x00044203 File Offset: 0x00042403
		public Guid NextHopConnector
		{
			get
			{
				return this.nextHopConnector;
			}
			internal set
			{
				this.nextHopConnector = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004420C File Offset: 0x0004240C
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x00044214 File Offset: 0x00042414
		public QueueStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0004421D File Offset: 0x0004241D
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00044225 File Offset: 0x00042425
		public int MessageCount
		{
			get
			{
				return this.messageCount;
			}
			internal set
			{
				this.messageCount = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0004422E File Offset: 0x0004242E
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x00044236 File Offset: 0x00042436
		public string LastError
		{
			get
			{
				return this.lastError;
			}
			internal set
			{
				this.lastError = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0004423F File Offset: 0x0004243F
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x00044247 File Offset: 0x00042447
		public DateTime? LastRetryTime
		{
			get
			{
				return this.lastRetryTime;
			}
			internal set
			{
				this.lastRetryTime = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00044250 File Offset: 0x00042450
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x00044258 File Offset: 0x00042458
		public DateTime? NextRetryTime
		{
			get
			{
				return this.nextRetryTime;
			}
			internal set
			{
				this.nextRetryTime = value;
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00044261 File Offset: 0x00042461
		public void ConvertDatesToLocalTime()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00044268 File Offset: 0x00042468
		public void ConvertDatesToUniversalTime()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004426F File Offset: 0x0004246F
		public ValidationError[] Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00044276 File Offset: 0x00042476
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0004427D File Offset: 0x0004247D
		public ObjectState ObjectState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00044284 File Offset: 0x00042484
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004428B File Offset: 0x0004248B
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000C8A RID: 3210
		private QueueIdentity identity;

		// Token: 0x04000C8B RID: 3211
		private DeliveryType deliveryType;

		// Token: 0x04000C8C RID: 3212
		private Guid nextHopConnector;

		// Token: 0x04000C8D RID: 3213
		private QueueStatus status;

		// Token: 0x04000C8E RID: 3214
		private int messageCount;

		// Token: 0x04000C8F RID: 3215
		private string lastError;

		// Token: 0x04000C90 RID: 3216
		private DateTime? lastRetryTime;

		// Token: 0x04000C91 RID: 3217
		private DateTime? nextRetryTime;
	}
}
