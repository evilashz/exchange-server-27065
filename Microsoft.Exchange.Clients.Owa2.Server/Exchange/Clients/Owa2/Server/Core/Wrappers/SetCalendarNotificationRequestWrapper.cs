using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B2 RID: 690
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarNotificationRequestWrapper
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00054168 File Offset: 0x00052368
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00054170 File Offset: 0x00052370
		[DataMember(Name = "request")]
		public SetCalendarNotificationRequest Request { get; set; }
	}
}
