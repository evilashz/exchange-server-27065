using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000274 RID: 628
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteContactFolderRequestWrapper
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000539E4 File Offset: 0x00051BE4
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x000539EC File Offset: 0x00051BEC
		[DataMember(Name = "folderId")]
		public FolderId FolderId { get; set; }
	}
}
