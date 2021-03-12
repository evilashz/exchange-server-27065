using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029D RID: 669
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveContactFolderRequestWrapper
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00053EC2 File Offset: 0x000520C2
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x00053ECA File Offset: 0x000520CA
		[DataMember(Name = "folderId")]
		public FolderId FolderId { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00053ED3 File Offset: 0x000520D3
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x00053EDB File Offset: 0x000520DB
		[DataMember(Name = "priority")]
		public int Priority { get; set; }
	}
}
