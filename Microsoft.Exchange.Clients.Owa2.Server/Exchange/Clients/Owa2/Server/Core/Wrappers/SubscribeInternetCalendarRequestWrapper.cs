using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C4 RID: 708
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeInternetCalendarRequestWrapper
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0005434C File Offset: 0x0005254C
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x00054354 File Offset: 0x00052554
		[DataMember(Name = "request")]
		public SubscribeInternetCalendarRequest Request { get; set; }
	}
}
