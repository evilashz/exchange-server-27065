using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000295 RID: 661
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWacIframeUrlRequestWrapper
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00053DC7 File Offset: 0x00051FC7
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x00053DCF File Offset: 0x00051FCF
		[DataMember(Name = "attachmentId")]
		public string AttachmentId { get; set; }
	}
}
