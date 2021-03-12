using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026A RID: 618
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateModernGroupRequestWrapper
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x000538B7 File Offset: 0x00051AB7
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x000538BF File Offset: 0x00051ABF
		[DataMember(Name = "request")]
		public CreateModernGroupRequest Request { get; set; }
	}
}
