using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200003C RID: 60
	internal static class ServiceStrings
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000CC80 File Offset: 0x0000AE80
		static ServiceStrings()
		{
			ServiceStrings.stringIDs.Add(2385039751U, "NotificationBrokerMailboxChangeAssistantName");
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public static LocalizedString InvalidBrokerSubscriptionException(string json)
		{
			return new LocalizedString("InvalidBrokerSubscriptionException", ServiceStrings.ResourceManager, new object[]
			{
				json
			});
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public static LocalizedString NotificationBrokerMailboxChangeAssistantName
		{
			get
			{
				return new LocalizedString("NotificationBrokerMailboxChangeAssistantName", ServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public static LocalizedString WrongMailboxForSubscriptionException(Guid subscriptionMailboxGuid, Guid mailboxDataGuid)
		{
			return new LocalizedString("WrongMailboxForSubscriptionException", ServiceStrings.ResourceManager, new object[]
			{
				subscriptionMailboxGuid,
				mailboxDataGuid
			});
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public static LocalizedString BrokerMailboxNotFoundException(Guid mailboxGuid)
		{
			return new LocalizedString("BrokerMailboxNotFoundException", ServiceStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public static LocalizedString MailboxNotFoundInDatabaseExceptionError(Guid mailbox, Guid database)
		{
			return new LocalizedString("MailboxNotFoundInDatabaseExceptionError", ServiceStrings.ResourceManager, new object[]
			{
				mailbox,
				database
			});
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public static LocalizedString SessionLockTimeoutException(Guid mailboxGuid, string cultureName)
		{
			return new LocalizedString("SessionLockTimeoutException", ServiceStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				cultureName
			});
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public static LocalizedString GetLocalizedString(ServiceStrings.IDs key)
		{
			return new LocalizedString(ServiceStrings.stringIDs[(uint)key], ServiceStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000115 RID: 277
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000116 RID: 278
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Notifications.Broker.Strings", typeof(ServiceStrings).GetTypeInfo().Assembly);

		// Token: 0x0200003D RID: 61
		public enum IDs : uint
		{
			// Token: 0x04000118 RID: 280
			NotificationBrokerMailboxChangeAssistantName = 2385039751U
		}

		// Token: 0x0200003E RID: 62
		private enum ParamIDs
		{
			// Token: 0x0400011A RID: 282
			InvalidBrokerSubscriptionException,
			// Token: 0x0400011B RID: 283
			WrongMailboxForSubscriptionException,
			// Token: 0x0400011C RID: 284
			BrokerMailboxNotFoundException,
			// Token: 0x0400011D RID: 285
			MailboxNotFoundInDatabaseExceptionError,
			// Token: 0x0400011E RID: 286
			SessionLockTimeoutException
		}
	}
}
