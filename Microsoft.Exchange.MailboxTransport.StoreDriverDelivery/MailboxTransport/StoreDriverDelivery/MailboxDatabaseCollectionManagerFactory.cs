using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200002E RID: 46
	internal class MailboxDatabaseCollectionManagerFactory
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000B83E File Offset: 0x00009A3E
		public static IMailboxDatabaseCollectionManager Create()
		{
			if (MailboxDatabaseCollectionManagerFactory.InstanceBuilder != null)
			{
				return MailboxDatabaseCollectionManagerFactory.InstanceBuilder();
			}
			return new MailboxDatabaseCollectionManager();
		}

		// Token: 0x04000101 RID: 257
		public static MailboxDatabaseCollectionManagerFactory.MailboxDatabaseCollectionManagerBuilder InstanceBuilder;

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x0600023E RID: 574
		public delegate IMailboxDatabaseCollectionManager MailboxDatabaseCollectionManagerBuilder();
	}
}
