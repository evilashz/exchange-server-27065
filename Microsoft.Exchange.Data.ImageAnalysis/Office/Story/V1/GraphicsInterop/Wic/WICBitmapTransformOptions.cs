using System;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000045 RID: 69
	[Flags]
	internal enum WICBitmapTransformOptions
	{
		// Token: 0x04000103 RID: 259
		WICBitmapTransformRotate0 = 0,
		// Token: 0x04000104 RID: 260
		WICBitmapTransformRotate90 = 1,
		// Token: 0x04000105 RID: 261
		WICBitmapTransformRotate180 = 2,
		// Token: 0x04000106 RID: 262
		WICBitmapTransformRotate270 = 3,
		// Token: 0x04000107 RID: 263
		WICBitmapTransformFlipHorizontal = 8,
		// Token: 0x04000108 RID: 264
		WICBitmapTransformFlipVertical = 16,
		// Token: 0x04000109 RID: 265
		WICBITMAPTRANSFORMOPTIONS_FORCE_DWORD = 2147483647
	}
}
