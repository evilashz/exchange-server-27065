using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A4 RID: 676
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveModernGroupRequestWrapper
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00053FB5 File Offset: 0x000521B5
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x00053FBD File Offset: 0x000521BD
		[DataMember(Name = "request")]
		public RemoveModernGroupRequest Request { get; set; }
	}
}
