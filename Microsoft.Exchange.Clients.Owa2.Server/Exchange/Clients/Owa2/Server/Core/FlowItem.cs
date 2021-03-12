using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000213 RID: 531
	[DataContract]
	[Serializable]
	public class FlowItem
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00049037 File Offset: 0x00047237
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0004903F File Offset: 0x0004723F
		[DataMember]
		public string ItemBody { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00049048 File Offset: 0x00047248
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00049050 File Offset: 0x00047250
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00049059 File Offset: 0x00047259
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00049061 File Offset: 0x00047261
		[DataMember]
		public string ReceivedTimeUtc { get; set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0004906A File Offset: 0x0004726A
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00049072 File Offset: 0x00047272
		[DataMember]
		public EmailAddressWrapper Sender { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004907B File Offset: 0x0004727B
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00049083 File Offset: 0x00047283
		[DataMember]
		public bool IsRead { get; set; }
	}
}
