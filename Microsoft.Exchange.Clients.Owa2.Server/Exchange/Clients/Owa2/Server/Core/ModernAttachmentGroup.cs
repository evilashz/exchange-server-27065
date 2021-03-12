using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003EB RID: 1003
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernAttachmentGroup
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000789C3 File Offset: 0x00076BC3
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000789CB File Offset: 0x00076BCB
		[DataMember(Order = 1)]
		public int AttachmentsReturned { get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000789D4 File Offset: 0x00076BD4
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x000789DC File Offset: 0x00076BDC
		[DataMember(Order = 1)]
		public int ItemsProcessed { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x000789E5 File Offset: 0x00076BE5
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x000789ED File Offset: 0x00076BED
		[DataMember(Order = 1)]
		public int ItemsOffsetNext { get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000789F6 File Offset: 0x00076BF6
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x000789FE File Offset: 0x00076BFE
		[DataMember(Order = 1)]
		public int ItemsTotal { get; set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00078A07 File Offset: 0x00076C07
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x00078A0F File Offset: 0x00076C0F
		[DataMember(Order = 1)]
		public bool RetrievedLastItem { get; set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00078A18 File Offset: 0x00076C18
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x00078A20 File Offset: 0x00076C20
		[DataMember(Order = 2)]
		public string[] Path { get; set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00078A29 File Offset: 0x00076C29
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x00078A31 File Offset: 0x00076C31
		[DataMember(Order = 3)]
		public ModernAttachment[] AttachmentGroup { get; set; }
	}
}
