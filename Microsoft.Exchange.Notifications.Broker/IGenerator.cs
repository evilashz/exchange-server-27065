using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000B RID: 11
	internal interface IGenerator
	{
		// Token: 0x0600005B RID: 91
		void Subscribe(BrokerSubscription brokerSubscription);

		// Token: 0x0600005C RID: 92
		void CreateMapiNotificationHandler(BrokerSubscription brokerSubscription);

		// Token: 0x0600005D RID: 93
		void Unsubscribe(BrokerSubscription brokerSubscription);

		// Token: 0x0600005E RID: 94
		bool MailboxIsHostedLocally(Guid mailboxGuid);

		// Token: 0x0600005F RID: 95
		bool MailboxIsHostedLocally(Guid databaseGuid, Guid mailboxGuid);
	}
}
