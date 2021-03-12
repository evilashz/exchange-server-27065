using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B5 RID: 693
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarSharingPermissionsRequestWrapper
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x000541B3 File Offset: 0x000523B3
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x000541BB File Offset: 0x000523BB
		[DataMember(Name = "request")]
		public SetCalendarSharingPermissionsRequest Request { get; set; }
	}
}
