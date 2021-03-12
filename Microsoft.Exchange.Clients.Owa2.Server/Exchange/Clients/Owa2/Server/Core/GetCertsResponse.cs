using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C2 RID: 962
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCertsResponse
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00076BAE File Offset: 0x00074DAE
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x00076BB6 File Offset: 0x00074DB6
		[DataMember]
		public EmailAddressWrapper[][] InvalidRecipients { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00076BBF File Offset: 0x00074DBF
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x00076BC7 File Offset: 0x00074DC7
		[DataMember]
		public string[][] ValidRecipients { get; set; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00076BD0 File Offset: 0x00074DD0
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x00076BD8 File Offset: 0x00074DD8
		[DataMember]
		public string[] CertsRawData { get; set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x00076BE1 File Offset: 0x00074DE1
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x00076BE9 File Offset: 0x00074DE9
		[DataMember]
		public GetCertsErrorStatus ErrorStatus { get; set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00076BF2 File Offset: 0x00074DF2
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x00076BFA File Offset: 0x00074DFA
		[DataMember]
		public string[] ErrorDetails { get; set; }
	}
}
