using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BD RID: 957
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAttachmentDataProvidersRequest
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00076AF5 File Offset: 0x00074CF5
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x00076AFD File Offset: 0x00074CFD
		[DataMember]
		public int ClientVersion { get; set; }
	}
}
