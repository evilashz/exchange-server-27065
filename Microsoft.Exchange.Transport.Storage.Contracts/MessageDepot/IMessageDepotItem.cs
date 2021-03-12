using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000010 RID: 16
	internal interface IMessageDepotItem
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54
		object MessageObject { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000037 RID: 55
		TransportMessageId Id { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56
		MessageEnvelope MessageEnvelope { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57
		bool IsPoison { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58
		bool IsSuspended { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59
		// (set) Token: 0x0600003C RID: 60
		DateTime DeferUntil { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61
		DateTime ExpirationTime { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62
		DateTime ArrivalTime { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63
		MessageDepotItemStage Stage { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64
		// (set) Token: 0x06000041 RID: 65
		bool IsDelayDsnGenerated { get; set; }

		// Token: 0x06000042 RID: 66
		object GetProperty(string propertyName);

		// Token: 0x06000043 RID: 67
		void Dehydrate();
	}
}
