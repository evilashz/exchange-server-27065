using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C5 RID: 453
	internal class SearchFolderShape : Shape
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x000404E4 File Offset: 0x0003E6E4
		static SearchFolderShape()
		{
			SearchFolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			SearchFolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
			SearchFolderShape.defaultProperties.Add(SearchFolderSchema.UnreadCount);
			SearchFolderShape.defaultProperties.Add(BaseFolderSchema.TotalCount);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00040537 File Offset: 0x0003E737
		private SearchFolderShape() : base(Schema.SearchFolder, SearchFolderSchema.GetSchema(), new BaseFolderShape(), SearchFolderShape.defaultProperties)
		{
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00040553 File Offset: 0x0003E753
		internal static SearchFolderShape CreateShape()
		{
			return new SearchFolderShape();
		}

		// Token: 0x04000A1E RID: 2590
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
