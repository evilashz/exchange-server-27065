using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000409 RID: 1033
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class SynchronizeWacAttachmentRequest
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0007EE1F File Offset: 0x0007D01F
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x0007EE27 File Offset: 0x0007D027
		[DataMember]
		public string AttachmentId { get; set; }
	}
}
