using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000761 RID: 1889
	internal sealed class EventQueueOverflowException : ServicePermanentException
	{
		// Token: 0x06003873 RID: 14451 RVA: 0x000C7713 File Offset: 0x000C5913
		public EventQueueOverflowException() : base(CoreResources.IDs.ErrorMissedNotificationEvents)
		{
			this.Data[StreamingConnection.IsNonFatalSubscriptionExceptionKey] = bool.TrueString;
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000C773A File Offset: 0x000C593A
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
