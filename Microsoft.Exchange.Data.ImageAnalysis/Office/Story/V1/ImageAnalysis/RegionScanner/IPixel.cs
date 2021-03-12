using System;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x0200004D RID: 77
	internal interface IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F8 RID: 504
		float Luminance { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F9 RID: 505
		float Intensity { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FA RID: 506
		int Bands { get; }

		// Token: 0x17000042 RID: 66
		TValue this[int band]
		{
			get;
		}

		// Token: 0x060001FC RID: 508
		int CompareByIntensity(IPixel<TValue> another);

		// Token: 0x060001FD RID: 509
		bool Equals(object other);

		// Token: 0x060001FE RID: 510
		int GetHashCode();
	}
}
