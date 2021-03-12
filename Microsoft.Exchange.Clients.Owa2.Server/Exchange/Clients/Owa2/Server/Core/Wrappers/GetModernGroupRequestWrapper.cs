using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000289 RID: 649
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupRequestWrapper
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00053C68 File Offset: 0x00051E68
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x00053C70 File Offset: 0x00051E70
		[DataMember(Name = "request")]
		public GetModernGroupJsonRequest Request { get; set; }
	}
}
