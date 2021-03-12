using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000284 RID: 644
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetInlineExploreSpContentRequestWrapper
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00053BDA File Offset: 0x00051DDA
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x00053BE2 File Offset: 0x00051DE2
		[DataMember(Name = "query")]
		public string Query { get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00053BEB File Offset: 0x00051DEB
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x00053BF3 File Offset: 0x00051DF3
		[DataMember(Name = "targetUrl")]
		public string TargetUrl { get; set; }
	}
}
