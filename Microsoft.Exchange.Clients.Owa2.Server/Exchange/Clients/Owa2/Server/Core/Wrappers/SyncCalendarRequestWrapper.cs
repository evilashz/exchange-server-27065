using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C8 RID: 712
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncCalendarRequestWrapper
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000543E3 File Offset: 0x000525E3
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x000543EB File Offset: 0x000525EB
		[DataMember(Name = "request")]
		public SyncCalendarParameters Request { get; set; }
	}
}
