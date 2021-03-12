using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000419 RID: 1049
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class YouTubeLinkPreview : LinkPreview
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000807B9 File Offset: 0x0007E9B9
		// (set) Token: 0x060023D2 RID: 9170 RVA: 0x000807C1 File Offset: 0x0007E9C1
		[DataMember]
		public string PlayerUrl { get; set; }
	}
}
