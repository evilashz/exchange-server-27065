using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200010F RID: 271
	internal sealed class ParentFolderIdProperty : FolderIdPropertyBase
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00026459 File Offset: 0x00024659
		private ParentFolderIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00026462 File Offset: 0x00024662
		protected override StoreId GetIdFromObject(StoreObject storeObject)
		{
			if (storeObject.Id != null && storeObject.Id.ObjectId.ProviderLevelItemId.Length > 0)
			{
				return storeObject.ParentId;
			}
			return null;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00026489 File Offset: 0x00024689
		public static ParentFolderIdProperty CreateCommand(CommandContext commandContext)
		{
			return new ParentFolderIdProperty(commandContext);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00026491 File Offset: 0x00024691
		public override void ToServiceObject()
		{
			if (!base.TryCheckAndConvertToPublicFolderIdFromStoreObject(CommandOptions.ConvertParentFolderIdToPublicFolderId))
			{
				base.ToServiceObject();
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000264A2 File Offset: 0x000246A2
		public override void ToServiceObjectForPropertyBag()
		{
			if (!base.TryCheckAndConvertToPublicFolderIdFromPropertyBag(CommandOptions.ConvertParentFolderIdToPublicFolderId, StoreObjectSchema.ParentItemId))
			{
				base.ToServiceObjectForPropertyBag();
			}
		}
	}
}
