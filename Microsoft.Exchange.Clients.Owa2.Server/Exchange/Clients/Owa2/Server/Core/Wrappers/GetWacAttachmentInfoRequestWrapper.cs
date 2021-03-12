using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000294 RID: 660
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWacAttachmentInfoRequestWrapper
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00053D8C File Offset: 0x00051F8C
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x00053D94 File Offset: 0x00051F94
		[DataMember(Name = "attachmentId")]
		public string AttachmentId { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00053D9D File Offset: 0x00051F9D
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x00053DA5 File Offset: 0x00051FA5
		[DataMember(Name = "isEdit")]
		public bool IsEdit { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00053DAE File Offset: 0x00051FAE
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x00053DB6 File Offset: 0x00051FB6
		[DataMember(Name = "draftId")]
		public string DraftId { get; set; }
	}
}
