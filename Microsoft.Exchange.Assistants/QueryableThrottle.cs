using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B5 RID: 181
	internal class QueryableThrottle : QueryableObjectImplBase<QueryableThrottleObjectSchema>
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001B31E File Offset: 0x0001951E
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001B330 File Offset: 0x00019530
		public string ThrottleName
		{
			get
			{
				return (string)this[QueryableThrottleObjectSchema.ThrottleName];
			}
			set
			{
				this[QueryableThrottleObjectSchema.ThrottleName] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001B33E File Offset: 0x0001953E
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001B350 File Offset: 0x00019550
		public int CurrentThrottle
		{
			get
			{
				return (int)this[QueryableThrottleObjectSchema.CurrentThrottle];
			}
			set
			{
				this[QueryableThrottleObjectSchema.CurrentThrottle] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001B363 File Offset: 0x00019563
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001B375 File Offset: 0x00019575
		public int ActiveWorkItems
		{
			get
			{
				return (int)this[QueryableThrottleObjectSchema.ActiveWorkItems];
			}
			set
			{
				this[QueryableThrottleObjectSchema.ActiveWorkItems] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001B388 File Offset: 0x00019588
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001B39A File Offset: 0x0001959A
		public bool OverThrottle
		{
			get
			{
				return (bool)this[QueryableThrottleObjectSchema.OverThrottle];
			}
			set
			{
				this[QueryableThrottleObjectSchema.OverThrottle] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001B3AD File Offset: 0x000195AD
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001B3BF File Offset: 0x000195BF
		public int PendingWorkItemsOnBase
		{
			get
			{
				return (int)this[QueryableThrottleObjectSchema.PendingWorkItemsOnBase];
			}
			set
			{
				this[QueryableThrottleObjectSchema.PendingWorkItemsOnBase] = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001B3D2 File Offset: 0x000195D2
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001B3E4 File Offset: 0x000195E4
		public int QueueLength
		{
			get
			{
				return (int)this[QueryableThrottleObjectSchema.QueueLength];
			}
			set
			{
				this[QueryableThrottleObjectSchema.QueueLength] = value;
			}
		}
	}
}
