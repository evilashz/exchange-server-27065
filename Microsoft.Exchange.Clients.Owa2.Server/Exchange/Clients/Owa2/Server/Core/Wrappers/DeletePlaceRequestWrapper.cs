using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000276 RID: 630
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeletePlaceRequestWrapper
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00053A27 File Offset: 0x00051C27
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00053A2F File Offset: 0x00051C2F
		[DataMember(Name = "request")]
		public DeletePlaceRequest Request { get; set; }
	}
}
