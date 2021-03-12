using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000149 RID: 329
	internal class FolderIdProperty : FolderIdPropertyBase
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0002C368 File Offset: 0x0002A568
		protected FolderIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002C371 File Offset: 0x0002A571
		public static FolderIdProperty CreateCommand(CommandContext commandContext)
		{
			return new FolderIdProperty(commandContext);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002C379 File Offset: 0x0002A579
		public override void ToServiceObject()
		{
			if (!base.TryCheckAndConvertToPublicFolderIdFromStoreObject(CommandOptions.ConvertFolderIdToPublicFolderId))
			{
				base.ToServiceObject();
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002C38A File Offset: 0x0002A58A
		public override void ToServiceObjectForPropertyBag()
		{
			if (!base.TryCheckAndConvertToPublicFolderIdFromPropertyBag(CommandOptions.ConvertFolderIdToPublicFolderId, CoreFolderSchema.Id))
			{
				base.ToServiceObjectForPropertyBag();
			}
		}
	}
}
