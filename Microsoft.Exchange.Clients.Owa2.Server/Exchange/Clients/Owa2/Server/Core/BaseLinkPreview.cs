using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000414 RID: 1044
	[KnownType(typeof(OEmbedVideoPreview))]
	[KnownType(typeof(YouTubeLinkPreview))]
	[KnownType(typeof(LinkPreview))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseLinkPreview
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0008062C File Offset: 0x0007E82C
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x00080634 File Offset: 0x0007E834
		[DataMember]
		public string Id { get; set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0008063D File Offset: 0x0007E83D
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x00080645 File Offset: 0x0007E845
		[DataMember]
		public string Url { get; set; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0008064E File Offset: 0x0007E84E
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x00080656 File Offset: 0x0007E856
		[DataMember]
		public long RequestStartTimeMilliseconds { get; set; }
	}
}
