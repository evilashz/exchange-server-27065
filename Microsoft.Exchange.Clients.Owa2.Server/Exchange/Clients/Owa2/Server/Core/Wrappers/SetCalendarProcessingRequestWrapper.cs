using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B3 RID: 691
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarProcessingRequestWrapper
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00054181 File Offset: 0x00052381
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x00054189 File Offset: 0x00052389
		[DataMember(Name = "request")]
		public SetCalendarProcessingRequest Request { get; set; }
	}
}
