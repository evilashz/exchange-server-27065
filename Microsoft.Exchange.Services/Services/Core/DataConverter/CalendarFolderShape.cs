using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000199 RID: 409
	internal class CalendarFolderShape : Shape
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00036AD8 File Offset: 0x00034CD8
		static CalendarFolderShape()
		{
			CalendarFolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			CalendarFolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
			CalendarFolderShape.defaultProperties.Add(BaseFolderSchema.TotalCount);
			CalendarFolderShape.defaultProperties.Add(BaseFolderSchema.ChildFolderCount);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00036B2B File Offset: 0x00034D2B
		private CalendarFolderShape() : base(Schema.CalendarFolder, CalendarFolderSchema.GetSchema(), new BaseFolderShape(), CalendarFolderShape.defaultProperties)
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00036B47 File Offset: 0x00034D47
		internal static CalendarFolderShape CreateShape()
		{
			return new CalendarFolderShape();
		}

		// Token: 0x04000828 RID: 2088
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
