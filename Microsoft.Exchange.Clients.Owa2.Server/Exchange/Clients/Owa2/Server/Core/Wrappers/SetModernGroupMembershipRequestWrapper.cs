using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BB RID: 699
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetModernGroupMembershipRequestWrapper
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00054249 File Offset: 0x00052449
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x00054251 File Offset: 0x00052451
		[DataMember(Name = "request")]
		public SetModernGroupMembershipJsonRequest Request { get; set; }
	}
}
