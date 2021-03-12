using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028C RID: 652
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaModernGroupMembershipRequestWrapper
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00053CB3 File Offset: 0x00051EB3
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x00053CBB File Offset: 0x00051EBB
		[DataMember(Name = "request")]
		public GetPersonaModernGroupMembershipJsonRequest Request { get; set; }
	}
}
