using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000293 RID: 659
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityInternalRequestWrapper
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00053D73 File Offset: 0x00051F73
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00053D7B File Offset: 0x00051F7B
		[DataMember(Name = "request")]
		public GetUserAvailabilityInternalJsonRequest Request { get; set; }
	}
}
