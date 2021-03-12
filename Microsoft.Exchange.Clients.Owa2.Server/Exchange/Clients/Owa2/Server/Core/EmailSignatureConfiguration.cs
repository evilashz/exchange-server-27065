using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E9 RID: 489
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmailSignatureConfiguration
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00042148 File Offset: 0x00040348
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00042150 File Offset: 0x00040350
		[DataMember]
		public bool AutoAddSignature { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00042159 File Offset: 0x00040359
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00042161 File Offset: 0x00040361
		[DataMember]
		public string SignatureText { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004216A File Offset: 0x0004036A
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00042172 File Offset: 0x00040372
		[DataMember]
		public bool UseDesktopSignature { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0004217B File Offset: 0x0004037B
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x00042183 File Offset: 0x00040383
		[DataMember]
		public string DesktopSignatureText { get; set; }
	}
}
