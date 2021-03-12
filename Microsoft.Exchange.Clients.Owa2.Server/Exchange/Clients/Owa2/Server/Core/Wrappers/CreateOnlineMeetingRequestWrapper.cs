using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026B RID: 619
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateOnlineMeetingRequestWrapper
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x000538D0 File Offset: 0x00051AD0
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x000538D8 File Offset: 0x00051AD8
		[DataMember(Name = "sipUri")]
		public string SipUri { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x000538E1 File Offset: 0x00051AE1
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x000538E9 File Offset: 0x00051AE9
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}
