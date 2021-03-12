using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000417 RID: 1047
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LinkPreview : BaseLinkPreview
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00080754 File Offset: 0x0007E954
		// (set) Token: 0x060023C6 RID: 9158 RVA: 0x0008075C File Offset: 0x0007E95C
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00080765 File Offset: 0x0007E965
		// (set) Token: 0x060023C8 RID: 9160 RVA: 0x0008076D File Offset: 0x0007E96D
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x00080776 File Offset: 0x0007E976
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x0008077E File Offset: 0x0007E97E
		[DataMember]
		public string ImageUrl { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00080787 File Offset: 0x0007E987
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x0008078F File Offset: 0x0007E98F
		[DataMember]
		public bool IsVideo { get; set; }
	}
}
