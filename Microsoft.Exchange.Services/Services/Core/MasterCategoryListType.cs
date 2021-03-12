using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020005EB RID: 1515
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MasterCategoryListType
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000B28B1 File Offset: 0x000B0AB1
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000B28B9 File Offset: 0x000B0AB9
		[DataMember]
		public CategoryType[] MasterList { get; set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000B28C2 File Offset: 0x000B0AC2
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000B28CA File Offset: 0x000B0ACA
		[DataMember]
		public CategoryType[] DefaultList { get; set; }
	}
}
