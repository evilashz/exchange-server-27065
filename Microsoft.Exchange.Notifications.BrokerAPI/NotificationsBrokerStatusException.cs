using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class NotificationsBrokerStatusException : NotificationsBrokerPermanentException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002F3D File Offset: 0x0000113D
		public NotificationsBrokerStatusException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F46 File Offset: 0x00001146
		public NotificationsBrokerStatusException(BrokerStatus brokerStatus) : base(NotificationsBrokerStatusException.LocalizedStringFromBrokerStatus(brokerStatus))
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F54 File Offset: 0x00001154
		private static LocalizedString LocalizedStringFromBrokerStatus(BrokerStatus brokerStatus)
		{
			switch (brokerStatus)
			{
			default:
				return ClientAPIStrings.BrokerStatus_UnknownError;
			case BrokerStatus.Cancelled:
				return ClientAPIStrings.BrokerStatus_Cancelled;
			}
		}
	}
}
