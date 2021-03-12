using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000035 RID: 53
	internal enum MessageProcessingSource
	{
		// Token: 0x0400008C RID: 140
		Unknown,
		// Token: 0x0400008D RID: 141
		SmtpReceive,
		// Token: 0x0400008E RID: 142
		Pickup,
		// Token: 0x0400008F RID: 143
		StoreDriverSubmit,
		// Token: 0x04000090 RID: 144
		Categorizer,
		// Token: 0x04000091 RID: 145
		Routing,
		// Token: 0x04000092 RID: 146
		SmtpSend,
		// Token: 0x04000093 RID: 147
		StoreDriverLocalDelivery,
		// Token: 0x04000094 RID: 148
		DsnGenerator,
		// Token: 0x04000095 RID: 149
		Agents,
		// Token: 0x04000096 RID: 150
		NonSmtpGateway,
		// Token: 0x04000097 RID: 151
		BootLoader,
		// Token: 0x04000098 RID: 152
		DeliveryAgent,
		// Token: 0x04000099 RID: 153
		Queue
	}
}
