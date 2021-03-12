using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000020 RID: 32
	internal interface IBrokerDatabaseData
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C1 RID: 193
		string Name { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194
		Guid DatabaseGuid { get; }
	}
}
