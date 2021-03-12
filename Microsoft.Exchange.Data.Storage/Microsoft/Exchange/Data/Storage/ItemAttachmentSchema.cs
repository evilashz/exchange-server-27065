using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C73 RID: 3187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemAttachmentSchema : AttachmentSchema
	{
		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06007001 RID: 28673 RVA: 0x001F029C File Offset: 0x001EE49C
		public new static ItemAttachmentSchema Instance
		{
			get
			{
				if (ItemAttachmentSchema.instance == null)
				{
					ItemAttachmentSchema.instance = new ItemAttachmentSchema();
				}
				return ItemAttachmentSchema.instance;
			}
		}

		// Token: 0x04004C7F RID: 19583
		private static ItemAttachmentSchema instance;
	}
}
