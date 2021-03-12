using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200003C RID: 60
	[Guid("00000040-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICPalette
	{
		// Token: 0x060001C7 RID: 455
		void InitializePredefined([In] WICBitmapPaletteType ePaletteType, [In] int fAddTransparentColor);

		// Token: 0x060001C8 RID: 456
		void InitializeCustom([In] ref int pColors, [In] int cCount);

		// Token: 0x060001C9 RID: 457
		void InitializeFromBitmap([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISurface, [In] int cCount, [In] int fAddTransparentColor);

		// Token: 0x060001CA RID: 458
		void InitializeFromPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x060001CB RID: 459
		void GetType(out WICBitmapPaletteType pePaletteType);

		// Token: 0x060001CC RID: 460
		void GetColorCount(out int pcCount);

		// Token: 0x060001CD RID: 461
		void GetColors([In] int cCount, out int pColors, out int pcActualColors);

		// Token: 0x060001CE RID: 462
		void IsBlackWhite(out int pfIsBlackWhite);

		// Token: 0x060001CF RID: 463
		void IsGrayscale(out int pfIsGrayscale);

		// Token: 0x060001D0 RID: 464
		void HasAlpha(out int pfHasAlpha);
	}
}
