using System;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000047 RID: 71
	internal enum WICComponentType
	{
		// Token: 0x0400010F RID: 271
		WICDecoder = 1,
		// Token: 0x04000110 RID: 272
		WICEncoder,
		// Token: 0x04000111 RID: 273
		WICPixelFormatConverter = 4,
		// Token: 0x04000112 RID: 274
		WICMetadataReader = 8,
		// Token: 0x04000113 RID: 275
		WICMetadataWriter = 16,
		// Token: 0x04000114 RID: 276
		WICPixelFormat = 32,
		// Token: 0x04000115 RID: 277
		WICAllComponents = 63,
		// Token: 0x04000116 RID: 278
		WICCOMPONENTTYPE_FORCE_DWORD = 2147483647
	}
}
