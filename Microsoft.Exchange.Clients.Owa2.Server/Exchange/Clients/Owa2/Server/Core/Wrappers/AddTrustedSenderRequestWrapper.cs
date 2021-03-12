using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000263 RID: 611
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddTrustedSenderRequestWrapper
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00053780 File Offset: 0x00051980
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x00053788 File Offset: 0x00051988
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}
