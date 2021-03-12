using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000207 RID: 519
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ContactFolderResponse
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00048C83 File Offset: 0x00046E83
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00048C8B File Offset: 0x00046E8B
		[DataMember]
		public string ResponseCode { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00048C94 File Offset: 0x00046E94
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00048C9C File Offset: 0x00046E9C
		[DataMember]
		public FolderId FolderId { get; set; }
	}
}
