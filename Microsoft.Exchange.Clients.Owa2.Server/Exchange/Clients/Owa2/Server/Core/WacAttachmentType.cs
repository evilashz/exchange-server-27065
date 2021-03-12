using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200041C RID: 1052
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WacAttachmentType : FileAttachmentType
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000807D2 File Offset: 0x0007E9D2
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x000807DA File Offset: 0x0007E9DA
		[DataMember]
		public string WacUrl { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000807E3 File Offset: 0x0007E9E3
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x000807EB File Offset: 0x0007E9EB
		[DataMember]
		public bool IsEdit { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000807F4 File Offset: 0x0007E9F4
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x000807FC File Offset: 0x0007E9FC
		[DataMember]
		public bool IsInDraft { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00080805 File Offset: 0x0007EA05
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x0008080D File Offset: 0x0007EA0D
		[DataMember]
		public WacAttachmentStatus Status { get; set; }
	}
}
