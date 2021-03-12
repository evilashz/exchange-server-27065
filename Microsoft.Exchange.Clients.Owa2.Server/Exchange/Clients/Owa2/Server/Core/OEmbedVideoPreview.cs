using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000418 RID: 1048
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OEmbedVideoPreview : LinkPreview
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000807A0 File Offset: 0x0007E9A0
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x000807A8 File Offset: 0x0007E9A8
		[DataMember]
		public string EmbeddedHtml { get; set; }
	}
}
