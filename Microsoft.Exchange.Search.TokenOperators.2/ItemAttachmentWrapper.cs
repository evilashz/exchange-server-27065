using System;
using System.IO;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000019 RID: 25
	internal class ItemAttachmentWrapper : BufferPoolAttachmentWrapper
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000515E File Offset: 0x0000335E
		public ItemAttachmentWrapper(Stream itemAttachmentContent)
		{
			Util.ThrowOnNullArgument(itemAttachmentContent, "itemAttachmentConent");
			base.InitAttachmentStream(itemAttachmentContent);
		}
	}
}
