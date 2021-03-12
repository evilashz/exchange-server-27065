using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000197 RID: 407
	internal class BaseFolderShape : Shape
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00036A01 File Offset: 0x00034C01
		static BaseFolderShape()
		{
			BaseFolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			BaseFolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00036A2B File Offset: 0x00034C2B
		public BaseFolderShape() : base(Schema.Folder, BaseFolderSchema.GetSchema(), null, BaseFolderShape.defaultProperties)
		{
		}

		// Token: 0x04000824 RID: 2084
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
