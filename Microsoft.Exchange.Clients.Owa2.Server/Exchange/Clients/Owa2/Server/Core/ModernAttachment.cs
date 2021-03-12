using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E7 RID: 999
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernAttachment
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000788D7 File Offset: 0x00076AD7
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x000788DF File Offset: 0x00076ADF
		[DataMember]
		public ModernAttachment.AttachmentInfo Info { get; set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000788E8 File Offset: 0x00076AE8
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x000788F0 File Offset: 0x00076AF0
		[DataMember]
		public ModernAttachment.AttachmentData Data { get; set; }

		// Token: 0x020003E8 RID: 1000
		public class AttachmentInfo
		{
			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x06002042 RID: 8258 RVA: 0x00078901 File Offset: 0x00076B01
			// (set) Token: 0x06002043 RID: 8259 RVA: 0x00078909 File Offset: 0x00076B09
			[DataMember]
			public int Index { get; set; }

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x06002044 RID: 8260 RVA: 0x00078912 File Offset: 0x00076B12
			// (set) Token: 0x06002045 RID: 8261 RVA: 0x0007891A File Offset: 0x00076B1A
			[DataMember]
			public string[] Path { get; set; }
		}

		// Token: 0x020003E9 RID: 1001
		public class AttachmentData
		{
			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x06002047 RID: 8263 RVA: 0x0007892B File Offset: 0x00076B2B
			// (set) Token: 0x06002048 RID: 8264 RVA: 0x00078933 File Offset: 0x00076B33
			[DataMember]
			public ItemType Item { get; set; }

			// Token: 0x170007C2 RID: 1986
			// (get) Token: 0x06002049 RID: 8265 RVA: 0x0007893C File Offset: 0x00076B3C
			// (set) Token: 0x0600204A RID: 8266 RVA: 0x00078944 File Offset: 0x00076B44
			[DataMember]
			public AttachmentType Attachment { get; set; }

			// Token: 0x170007C3 RID: 1987
			// (get) Token: 0x0600204B RID: 8267 RVA: 0x0007894D File Offset: 0x00076B4D
			// (set) Token: 0x0600204C RID: 8268 RVA: 0x00078955 File Offset: 0x00076B55
			[DataMember]
			public AttachmentTypeEx AttachmentEx { get; set; }
		}
	}
}
