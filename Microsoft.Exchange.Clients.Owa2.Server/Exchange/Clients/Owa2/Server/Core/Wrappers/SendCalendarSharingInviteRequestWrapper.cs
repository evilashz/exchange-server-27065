using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AC RID: 684
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendCalendarSharingInviteRequestWrapper
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000540B0 File Offset: 0x000522B0
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x000540B8 File Offset: 0x000522B8
		[DataMember(Name = "request")]
		public CalendarShareInviteRequest Request { get; set; }
	}
}
