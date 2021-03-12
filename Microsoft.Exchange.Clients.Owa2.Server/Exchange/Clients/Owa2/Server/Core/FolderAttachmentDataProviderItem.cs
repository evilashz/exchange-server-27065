using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A4 RID: 932
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FolderAttachmentDataProviderItem : AttachmentDataProviderItem
	{
		// Token: 0x06001DDD RID: 7645 RVA: 0x00076394 File Offset: 0x00074594
		public FolderAttachmentDataProviderItem()
		{
			base.Type = AttachmentDataProviderItemType.Folder;
		}
	}
}
