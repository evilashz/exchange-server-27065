using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000287 RID: 647
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMimeRequestWrapper
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00053C36 File Offset: 0x00051E36
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x00053C3E File Offset: 0x00051E3E
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}
