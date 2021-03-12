using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B6 RID: 694
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetFolderMruConfigurationRequestWrapper
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x000541CC File Offset: 0x000523CC
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x000541D4 File Offset: 0x000523D4
		[DataMember(Name = "folderMruConfiguration")]
		public TargetFolderMruConfiguration FolderMruConfiguration { get; set; }
	}
}
