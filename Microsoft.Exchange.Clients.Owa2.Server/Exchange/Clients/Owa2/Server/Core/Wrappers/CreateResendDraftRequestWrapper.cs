using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026E RID: 622
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateResendDraftRequestWrapper
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0005392C File Offset: 0x00051B2C
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x00053934 File Offset: 0x00051B34
		[DataMember(Name = "ndrMessageId")]
		public string NdrMessageId { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0005393D File Offset: 0x00051B3D
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x00053945 File Offset: 0x00051B45
		[DataMember(Name = "draftsFolderId")]
		public string DraftsFolderId { get; set; }
	}
}
