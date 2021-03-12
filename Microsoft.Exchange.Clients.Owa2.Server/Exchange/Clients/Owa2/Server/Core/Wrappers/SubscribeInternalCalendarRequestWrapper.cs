using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C3 RID: 707
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeInternalCalendarRequestWrapper
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00054333 File Offset: 0x00052533
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x0005433B File Offset: 0x0005253B
		[DataMember(Name = "request")]
		public SubscribeInternalCalendarRequest Request { get; set; }
	}
}
