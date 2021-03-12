using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000279 RID: 633
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAggregatedAccountRequestWrapper
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00053A72 File Offset: 0x00051C72
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00053A7A File Offset: 0x00051C7A
		[DataMember(Name = "request")]
		public GetAggregatedAccountRequest Request { get; set; }
	}
}
