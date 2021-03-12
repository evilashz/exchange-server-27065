using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C0 RID: 960
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCertsRequest
	{
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00076B95 File Offset: 0x00074D95
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x00076B9D File Offset: 0x00074D9D
		[DataMember]
		public EmailAddressWrapper[] Recipients { get; set; }
	}
}
