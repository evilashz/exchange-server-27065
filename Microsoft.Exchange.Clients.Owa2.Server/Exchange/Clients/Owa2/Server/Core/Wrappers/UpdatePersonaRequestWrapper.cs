using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002D0 RID: 720
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdatePersonaRequestWrapper
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x000544BC File Offset: 0x000526BC
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x000544C4 File Offset: 0x000526C4
		[DataMember(Name = "request")]
		public UpdatePersonaJsonRequest Request { get; set; }
	}
}
