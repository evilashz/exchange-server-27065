using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029B RID: 667
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupMembershipRequestMessageDetailsRequestWrapper
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00053E6E File Offset: 0x0005206E
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x00053E76 File Offset: 0x00052076
		[DataMember(Name = "request")]
		public ModernGroupMembershipRequestMessageDetailsRequest Request { get; set; }
	}
}
