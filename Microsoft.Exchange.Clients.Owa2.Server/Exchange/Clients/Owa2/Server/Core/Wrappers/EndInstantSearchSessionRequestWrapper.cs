using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000278 RID: 632
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EndInstantSearchSessionRequestWrapper
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00053A59 File Offset: 0x00051C59
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00053A61 File Offset: 0x00051C61
		[DataMember(Name = "sessionId")]
		public string SessionId { get; set; }
	}
}
