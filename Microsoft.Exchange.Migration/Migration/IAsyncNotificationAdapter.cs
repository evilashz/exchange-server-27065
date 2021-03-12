using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000002 RID: 2
	internal interface IAsyncNotificationAdapter
	{
		// Token: 0x06000001 RID: 1
		Guid? CreateNotification(IMigrationDataProvider dataProvider, MigrationJob job);

		// Token: 0x06000002 RID: 2
		void UpdateNotification(IMigrationDataProvider dataProvider, MigrationJob job);

		// Token: 0x06000003 RID: 3
		void RemoveNotification(IMigrationDataProvider dataProvider, MigrationJob job);
	}
}
