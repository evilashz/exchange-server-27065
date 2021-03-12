using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A8 RID: 680
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RenameTaskFolderRequestWrapper
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0005403B File Offset: 0x0005223B
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x00054043 File Offset: 0x00052243
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005404C File Offset: 0x0005224C
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x00054054 File Offset: 0x00052254
		[DataMember(Name = "newTaskFolderName")]
		public string NewTaskFolderName { get; set; }
	}
}
