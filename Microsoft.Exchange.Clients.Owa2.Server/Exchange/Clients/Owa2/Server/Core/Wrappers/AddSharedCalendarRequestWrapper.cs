using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000261 RID: 609
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddSharedCalendarRequestWrapper
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0005373D File Offset: 0x0005193D
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00053745 File Offset: 0x00051945
		[DataMember(Name = "request")]
		public AddSharedCalendarRequest Request { get; set; }
	}
}
