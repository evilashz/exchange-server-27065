using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000288 RID: 648
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernAttachmentsRequestWrapper
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00053C4F File Offset: 0x00051E4F
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x00053C57 File Offset: 0x00051E57
		[DataMember(Name = "request")]
		public GetModernAttachmentsRequest Request { get; set; }
	}
}
