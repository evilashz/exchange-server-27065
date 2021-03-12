using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029E RID: 670
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NotifyAppWipeRequestWrapper
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x00053EEC File Offset: 0x000520EC
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x00053EF4 File Offset: 0x000520F4
		[DataMember(Name = "wipeReason")]
		public DataWipeReason WipeReason { get; set; }
	}
}
