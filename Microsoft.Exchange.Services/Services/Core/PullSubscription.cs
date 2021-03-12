using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000236 RID: 566
	internal sealed class PullSubscription : SubscriptionBase
	{
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00047F58 File Offset: 0x00046158
		public PullSubscription(PullSubscriptionRequest subscriptionRequest, IdAndSession[] folderIds, Guid subscriptionOwnerObjectGuid) : base(subscriptionRequest, folderIds, subscriptionOwnerObjectGuid)
		{
			this.timeOutMinutes = (double)subscriptionRequest.Timeout;
			this.SetExpirationDateTime();
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00047F78 File Offset: 0x00046178
		private void SetExpirationDateTime()
		{
			this.expirationDateTime = ExDateTime.Now.AddMinutes(this.timeOutMinutes);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00047FA0 File Offset: 0x000461A0
		public override EwsNotificationType GetEvents(string theLastWatermarkSent)
		{
			RequestDetailsLogger.Current.AppendGenericInfo("SubscriptionType", "Pull");
			EwsNotificationType result;
			lock (this.lockObject)
			{
				if (this.isExpired)
				{
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<int>((long)this.GetHashCode(), "SubscriptionBase.GetEvents. ExpiredSubscriptionException. Hashcode: {0}.", this.GetHashCode());
					throw new ExpiredSubscriptionException();
				}
				EwsNotificationType events = base.GetEvents(theLastWatermarkSent);
				this.SetExpirationDateTime();
				result = events;
			}
			return result;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0004802C File Offset: 0x0004622C
		public override bool IsExpired
		{
			get
			{
				if (!this.isExpired)
				{
					this.isExpired = (this.expirationDateTime < ExDateTime.Now);
				}
				return this.isExpired;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00048052 File Offset: 0x00046252
		protected override int EventQueueSize
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x04000B4C RID: 2892
		private const int MaximumTimeOutMinutes = 1440;

		// Token: 0x04000B4D RID: 2893
		private const int MinimumTimeOutMinutes = 1;

		// Token: 0x04000B4E RID: 2894
		private const int PullEventQueueSize = 50;

		// Token: 0x04000B4F RID: 2895
		private bool isExpired;

		// Token: 0x04000B50 RID: 2896
		private double timeOutMinutes;

		// Token: 0x04000B51 RID: 2897
		private ExDateTime expirationDateTime;
	}
}
