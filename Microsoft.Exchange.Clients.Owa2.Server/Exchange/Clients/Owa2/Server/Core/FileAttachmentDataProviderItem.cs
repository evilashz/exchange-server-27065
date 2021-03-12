using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A5 RID: 933
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FileAttachmentDataProviderItem : AttachmentDataProviderItem
	{
		// Token: 0x06001DDE RID: 7646 RVA: 0x000763A3 File Offset: 0x000745A3
		public FileAttachmentDataProviderItem()
		{
			base.Type = AttachmentDataProviderItemType.File;
		}
	}
}
