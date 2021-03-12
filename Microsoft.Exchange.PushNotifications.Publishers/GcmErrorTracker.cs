using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200009D RID: 157
	internal class GcmErrorTracker : ErrorTracker<GcmErrorType>
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x000127B4 File Offset: 0x000109B4
		public GcmErrorTracker(int backOffTimeInSeconds) : base(GcmErrorTracker.ErrorWeightTable, 100, backOffTimeInSeconds, 0)
		{
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000127C5 File Offset: 0x000109C5
		public override ExDateTime BackOffEndTime
		{
			get
			{
				if (!(this.RetryAfter > base.BackOffEndTime))
				{
					return base.BackOffEndTime;
				}
				return this.RetryAfter;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000127E7 File Offset: 0x000109E7
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x000127EF File Offset: 0x000109EF
		private ExDateTime RetryAfter { get; set; }

		// Token: 0x06000584 RID: 1412 RVA: 0x000127F8 File Offset: 0x000109F8
		public virtual void SetRetryAfter(ExDateTime retryAfter)
		{
			this.RetryAfter = retryAfter;
		}

		// Token: 0x040002B0 RID: 688
		private const int MaxErrorWeight = 100;

		// Token: 0x040002B1 RID: 689
		private static readonly Dictionary<GcmErrorType, int> ErrorWeightTable = new Dictionary<GcmErrorType, int>
		{
			{
				GcmErrorType.Unknown,
				1
			},
			{
				GcmErrorType.Transport,
				40
			}
		};
	}
}
