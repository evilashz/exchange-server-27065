using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D3 RID: 979
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWacIframeUrlForOneDriveRequest
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00077504 File Offset: 0x00075704
		// (set) Token: 0x06001F5F RID: 8031 RVA: 0x0007750C File Offset: 0x0007570C
		[DataMember]
		public string EndPointUrl { get; set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00077515 File Offset: 0x00075715
		// (set) Token: 0x06001F61 RID: 8033 RVA: 0x0007751D File Offset: 0x0007571D
		[DataMember]
		public string DocumentUrl { get; set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00077526 File Offset: 0x00075726
		// (set) Token: 0x06001F63 RID: 8035 RVA: 0x0007752E File Offset: 0x0007572E
		[DataMember]
		public bool IsEdit { get; set; }
	}
}
