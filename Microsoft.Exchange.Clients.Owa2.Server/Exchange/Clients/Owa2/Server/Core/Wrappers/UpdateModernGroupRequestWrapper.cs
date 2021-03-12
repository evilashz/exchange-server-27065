using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CF RID: 719
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateModernGroupRequestWrapper
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x000544A3 File Offset: 0x000526A3
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x000544AB File Offset: 0x000526AB
		[DataMember(Name = "request")]
		public UpdateModernGroupRequest Request { get; set; }
	}
}
