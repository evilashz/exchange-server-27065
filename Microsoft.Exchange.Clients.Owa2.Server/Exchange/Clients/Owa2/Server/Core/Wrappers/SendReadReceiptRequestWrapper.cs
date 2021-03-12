using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AF RID: 687
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendReadReceiptRequestWrapper
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000540FB File Offset: 0x000522FB
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00054103 File Offset: 0x00052303
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}
