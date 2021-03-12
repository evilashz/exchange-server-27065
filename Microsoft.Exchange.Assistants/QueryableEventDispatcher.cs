using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AA RID: 170
	internal class QueryableEventDispatcher : QueryableObjectImplBase<QueryableEventDispatcherObjectSchema>
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001A5A5 File Offset: 0x000187A5
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001A5B7 File Offset: 0x000187B7
		public string AssistantName
		{
			get
			{
				return (string)this[QueryableEventDispatcherObjectSchema.AssistantName];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.AssistantName] = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001A5C5 File Offset: 0x000187C5
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001A5D7 File Offset: 0x000187D7
		public Guid AssistantGuid
		{
			get
			{
				return (Guid)this[QueryableEventDispatcherObjectSchema.AssistantGuid];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.AssistantGuid] = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001A5EA File Offset: 0x000187EA
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001A5FC File Offset: 0x000187FC
		public long CommittedWatermark
		{
			get
			{
				return (long)this[QueryableEventDispatcherObjectSchema.CommittedWatermark];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.CommittedWatermark] = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001A60F File Offset: 0x0001880F
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001A621 File Offset: 0x00018821
		public long HighestEventQueued
		{
			get
			{
				return (long)this[QueryableEventDispatcherObjectSchema.HighestEventQueued];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.HighestEventQueued] = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001A634 File Offset: 0x00018834
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001A646 File Offset: 0x00018846
		public long RecoveryEventCounter
		{
			get
			{
				return (long)this[QueryableEventDispatcherObjectSchema.RecoveryEventCounter];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.RecoveryEventCounter] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001A659 File Offset: 0x00018859
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001A66B File Offset: 0x0001886B
		public bool IsInRetry
		{
			get
			{
				return (bool)this[QueryableEventDispatcherObjectSchema.IsInRetry];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.IsInRetry] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001A67E File Offset: 0x0001887E
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001A690 File Offset: 0x00018890
		public int PendingQueueLength
		{
			get
			{
				return (int)this[QueryableEventDispatcherObjectSchema.PendingQueueLength];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.PendingQueueLength] = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001A6A3 File Offset: 0x000188A3
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001A6B5 File Offset: 0x000188B5
		public int ActiveQueueLength
		{
			get
			{
				return (int)this[QueryableEventDispatcherObjectSchema.ActiveQueueLength];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.ActiveQueueLength] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001A6C8 File Offset: 0x000188C8
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001A6DA File Offset: 0x000188DA
		public int PendingWorkers
		{
			get
			{
				return (int)this[QueryableEventDispatcherObjectSchema.PendingWorkers];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.PendingWorkers] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001A6ED File Offset: 0x000188ED
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001A6FF File Offset: 0x000188FF
		public int ActiveWorkers
		{
			get
			{
				return (int)this[QueryableEventDispatcherObjectSchema.ActiveWorkers];
			}
			set
			{
				this[QueryableEventDispatcherObjectSchema.ActiveWorkers] = value;
			}
		}
	}
}
