using System;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001FC RID: 508
	public interface ISettingsContext
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600119E RID: 4510
		Guid? ServerGuid { get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600119F RID: 4511
		string ServerName { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060011A0 RID: 4512
		ServerVersion ServerVersion { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060011A1 RID: 4513
		string ServerRole { get; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060011A2 RID: 4514
		string ProcessName { get; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060011A3 RID: 4515
		Guid? DagOrServerGuid { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060011A4 RID: 4516
		Guid? DatabaseGuid { get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060011A5 RID: 4517
		string DatabaseName { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060011A6 RID: 4518
		ServerVersion DatabaseVersion { get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060011A7 RID: 4519
		string OrganizationName { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060011A8 RID: 4520
		ExchangeObjectVersion OrganizationVersion { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060011A9 RID: 4521
		Guid? MailboxGuid { get; }

		// Token: 0x060011AA RID: 4522
		string GetGenericProperty(string propertyName);
	}
}
