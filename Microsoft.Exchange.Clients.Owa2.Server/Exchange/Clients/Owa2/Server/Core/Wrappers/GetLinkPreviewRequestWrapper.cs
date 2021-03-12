using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000285 RID: 645
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetLinkPreviewRequestWrapper
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00053C04 File Offset: 0x00051E04
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x00053C0C File Offset: 0x00051E0C
		[DataMember(Name = "getLinkPreviewRequest")]
		public GetLinkPreviewRequest GetLinkPreviewRequest { get; set; }
	}
}
