using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027D RID: 637
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarSharingRecipientInfoRequestWrapper
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00053AD6 File Offset: 0x00051CD6
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00053ADE File Offset: 0x00051CDE
		[DataMember(Name = "request")]
		public GetCalendarSharingRecipientInfoRequest Request { get; set; }
	}
}
