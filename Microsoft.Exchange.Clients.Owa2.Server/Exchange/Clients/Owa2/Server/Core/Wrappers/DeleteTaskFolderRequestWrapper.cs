using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000277 RID: 631
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteTaskFolderRequestWrapper
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00053A40 File Offset: 0x00051C40
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00053A48 File Offset: 0x00051C48
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}
