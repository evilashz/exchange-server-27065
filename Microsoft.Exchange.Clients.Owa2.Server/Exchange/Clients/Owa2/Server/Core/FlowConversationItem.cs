using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000212 RID: 530
	[DataContract]
	[Serializable]
	public class FlowConversationItem
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00048F96 File Offset: 0x00047196
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00048F9E File Offset: 0x0004719E
		[DataMember]
		public string FlowConversationId { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00048FA7 File Offset: 0x000471A7
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00048FAF File Offset: 0x000471AF
		[DataMember]
		public string SenderPhotoEmailAddress { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00048FB8 File Offset: 0x000471B8
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00048FC0 File Offset: 0x000471C0
		[DataMember]
		public string Preview { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00048FC9 File Offset: 0x000471C9
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00048FD1 File Offset: 0x000471D1
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00048FDA File Offset: 0x000471DA
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00048FE2 File Offset: 0x000471E2
		[DataMember]
		public int UnReadCount { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00048FEB File Offset: 0x000471EB
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00048FF3 File Offset: 0x000471F3
		[DataMember]
		public string ReceivedTimeUtc { get; set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00048FFC File Offset: 0x000471FC
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00049004 File Offset: 0x00047204
		[DataMember]
		public ItemId LastItemId { get; set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0004900D File Offset: 0x0004720D
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00049015 File Offset: 0x00047215
		[DataMember]
		public EmailAddressWrapper[] Participants { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004901E File Offset: 0x0004721E
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00049026 File Offset: 0x00047226
		[DataMember]
		public int TotalCount { get; set; }
	}
}
