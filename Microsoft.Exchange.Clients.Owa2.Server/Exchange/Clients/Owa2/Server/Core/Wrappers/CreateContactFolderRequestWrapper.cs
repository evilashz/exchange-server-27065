using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000268 RID: 616
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateContactFolderRequestWrapper
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00053852 File Offset: 0x00051A52
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0005385A File Offset: 0x00051A5A
		[DataMember(Name = "parentFolderId")]
		public BaseFolderId ParentFolderId { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00053863 File Offset: 0x00051A63
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0005386B File Offset: 0x00051A6B
		[DataMember(Name = "displayName")]
		public string DisplayName { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00053874 File Offset: 0x00051A74
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0005387C File Offset: 0x00051A7C
		[DataMember(Name = "priority")]
		public int Priority { get; set; }
	}
}
