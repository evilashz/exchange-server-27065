using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000415 RID: 1045
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetLinkPreviewRequest
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00080667 File Offset: 0x0007E867
		// (set) Token: 0x060023AA RID: 9130 RVA: 0x0008066F File Offset: 0x0007E86F
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x00080678 File Offset: 0x0007E878
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x00080680 File Offset: 0x0007E880
		[DataMember]
		public string Url { get; set; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00080689 File Offset: 0x0007E889
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x00080691 File Offset: 0x0007E891
		[DataMember]
		public long RequestStartTimeMilliseconds { get; set; }

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x0008069A File Offset: 0x0007E89A
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x000806A2 File Offset: 0x0007E8A2
		[DataMember]
		public bool Autoplay { get; set; }
	}
}
