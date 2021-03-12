using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.CommonMath;
using Microsoft.Office.Story.V1.GraphicsInterop;
using Microsoft.Office.Story.V1.GraphicsInterop.Wic;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	[Serializable]
	internal abstract class ImageBase<TPixel, TValue> where TPixel : struct, IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00005D7D File Offset: 0x00003F7D
		protected ImageBase(int width, int height) : this()
		{
			this.EnsureBuffer(width, height);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005D90 File Offset: 0x00003F90
		protected ImageBase(int width, int height, TPixel[] image) : this()
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (image.Length != width * height)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Array is of wrong size {0}. Must be {1} * {2} = {3} in size.", new object[]
				{
					image.Length,
					width,
					height,
					width * height
				}), "image");
			}
			this.Width = width;
			this.Height = height;
			this.Image = image;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005E18 File Offset: 0x00004018
		protected ImageBase(Stream stream, Guid wicImageFormat) : this()
		{
			IWICImagingFactory iwicimagingFactory = WicUtility.CreateFactory();
			IWICBitmapFrameDecode iwicbitmapFrameDecode = iwicimagingFactory.Load(stream);
			this.LoadFromWic(iwicimagingFactory, iwicbitmapFrameDecode, wicImageFormat);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicimagingFactory);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicbitmapFrameDecode);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005E4E File Offset: 0x0000404E
		protected ImageBase(IWICImagingFactory factory, IWICBitmapSource bitmapSource, Guid wicImageFormat) : this()
		{
			this.LoadFromWic(factory, bitmapSource, wicImageFormat);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005E5F File Offset: 0x0000405F
		private ImageBase()
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005E67 File Offset: 0x00004067
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005E6F File Offset: 0x0000406F
		[DataMember]
		public Vector2 Offset { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005E78 File Offset: 0x00004078
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00005E80 File Offset: 0x00004080
		[DataMember]
		public int Width { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00005E89 File Offset: 0x00004089
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00005E91 File Offset: 0x00004091
		[DataMember]
		public int Height { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005E9A File Offset: 0x0000409A
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00005EA2 File Offset: 0x000040A2
		[DataMember]
		public TPixel[] Image { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005EAC File Offset: 0x000040AC
		public int Bands
		{
			get
			{
				TPixel tpixel = default(TPixel);
				return tpixel.Bands;
			}
		}

		// Token: 0x1700003D RID: 61
		public TPixel this[int x, int y]
		{
			get
			{
				return this.Image[x + y * this.Width];
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005EE8 File Offset: 0x000040E8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} by {1}, {2} band of type {3}", new object[]
			{
				this.Width,
				this.Height,
				this.Bands,
				typeof(TValue).Name
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00005F48 File Offset: 0x00004148
		private void EnsureBuffer(int width, int height)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			if (width != this.Width || height != this.Height || this.Image.Length != this.Width * this.Height)
			{
				this.Width = width;
				this.Height = height;
				this.Image = new TPixel[width * height];
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005FB8 File Offset: 0x000041B8
		private void LoadFromWic(IWICImagingFactory factory, IWICBitmapSource bitmapSource, Guid wicImageFormat)
		{
			int width;
			int height;
			bitmapSource.GetSize(out width, out height);
			this.EnsureBuffer(width, height);
			factory.FillBlobFromBitmapSource(bitmapSource, this.Image, wicImageFormat);
		}
	}
}
