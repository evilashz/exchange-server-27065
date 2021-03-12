using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028A RID: 650
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupUnseenItemsRequestWrapper
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00053C81 File Offset: 0x00051E81
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x00053C89 File Offset: 0x00051E89
		[DataMember(Name = "request")]
		public GetModernGroupUnseenItemsJsonRequest Request { get; set; }
	}
}
