using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001EC RID: 492
	[Flags]
	public enum ExtendedFolderFlags
	{
		// Token: 0x04000DB0 RID: 3504
		ShowUnread = 1,
		// Token: 0x04000DB1 RID: 3505
		ShowTotal = 2,
		// Token: 0x04000DB2 RID: 3506
		IsSharepointFolder = 4,
		// Token: 0x04000DB3 RID: 3507
		NoShowPolicy = 32,
		// Token: 0x04000DB4 RID: 3508
		ReadOnly = 64,
		// Token: 0x04000DB5 RID: 3509
		HasRssItems = 128,
		// Token: 0x04000DB6 RID: 3510
		WebCalFolder = 256,
		// Token: 0x04000DB7 RID: 3511
		ICalFolder = 512,
		// Token: 0x04000DB8 RID: 3512
		SharedIn = 1024,
		// Token: 0x04000DB9 RID: 3513
		SharedOut = 2048,
		// Token: 0x04000DBA RID: 3514
		PersonalShare = 8192,
		// Token: 0x04000DBB RID: 3515
		SharedViaExchange = 32768,
		// Token: 0x04000DBC RID: 3516
		SFDoNotDelete = 4194304,
		// Token: 0x04000DBD RID: 3517
		ExclusivelyBound = 33554432,
		// Token: 0x04000DBE RID: 3518
		RemoteHierarchy = 67108864,
		// Token: 0x04000DBF RID: 3519
		ExchangeConsumerShareFolder = 536870912,
		// Token: 0x04000DC0 RID: 3520
		ExchangeCrossOrgShareFolder = 1073741824,
		// Token: 0x04000DC1 RID: 3521
		ExchangePublishedCalendar = -2147483648
	}
}
