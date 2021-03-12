using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026C RID: 620
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreatePersonaRequestWrapper
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x000538FA File Offset: 0x00051AFA
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00053902 File Offset: 0x00051B02
		[DataMember(Name = "request")]
		public CreatePersonaJsonRequest Request { get; set; }
	}
}
