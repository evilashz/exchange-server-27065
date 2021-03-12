using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AD RID: 429
	internal class FolderShape : Shape
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0003A32C File Offset: 0x0003852C
		static FolderShape()
		{
			FolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			FolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
			FolderShape.defaultProperties.Add(FolderSchema.UnreadCount);
			FolderShape.defaultProperties.Add(BaseFolderSchema.TotalCount);
			FolderShape.defaultProperties.Add(BaseFolderSchema.ChildFolderCount);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003A38E File Offset: 0x0003858E
		private FolderShape() : base(Schema.Folder, FolderSchema.GetSchema(), new BaseFolderShape(), FolderShape.defaultProperties)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003A3AA File Offset: 0x000385AA
		internal static FolderShape CreateShape()
		{
			return new FolderShape();
		}

		// Token: 0x040008FE RID: 2302
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
