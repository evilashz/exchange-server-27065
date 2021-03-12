using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027E RID: 638
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCertsRequestWrapper
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00053AEF File Offset: 0x00051CEF
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00053AF7 File Offset: 0x00051CF7
		[DataMember(Name = "request")]
		public GetCertsRequest Request { get; set; }
	}
}
