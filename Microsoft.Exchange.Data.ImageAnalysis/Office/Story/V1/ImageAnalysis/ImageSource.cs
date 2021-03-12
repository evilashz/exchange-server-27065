using System;
using System.Globalization;
using System.IO;
using Microsoft.Office.Story.V1.GraphicsInterop;
using Microsoft.Office.Story.V1.GraphicsInterop.Wic;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x0200000E RID: 14
	internal class ImageSource
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002C24 File Offset: 0x00000E24
		public ImageSource(Stream stream) : this(ImageSource.ReadToEnd(stream))
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C34 File Offset: 0x00000E34
		public ImageSource(byte[] buffer)
		{
			if (buffer == null || buffer.Length == 0)
			{
				throw new ArgumentException("Image buffer does not contain a valid image to load.", "buffer");
			}
			this.buffer = buffer;
			IWICImagingFactory iwicimagingFactory = WicUtility.CreateFactory();
			IWICBitmapFrameDecode iwicbitmapFrameDecode = iwicimagingFactory.Load(this.ImageStream);
			int num;
			int num2;
			iwicbitmapFrameDecode.GetSize(out num, out num2);
			this.Width = (float)num;
			this.Height = (float)num2;
			IWICMetadataQueryReader iwicmetadataQueryReader = null;
			try
			{
				iwicbitmapFrameDecode.GetMetadataQueryReader(out iwicmetadataQueryReader);
				string s;
				DateTime value;
				if (iwicmetadataQueryReader.TryGetMetadataProperty("/app1/ifd/exif/subifd:{uint=36867}", out s) && DateTime.TryParseExact(s, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out value))
				{
					this.DateTaken = new DateTime?(value);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				ushort value2;
				if (iwicmetadataQueryReader.TryGetMetadataProperty("/app1/ifd/{ushort=274}", out value2))
				{
					this.Orientation = ImageSource.TransformOptionsFromUshort(value2);
				}
				else
				{
					this.Orientation = WICBitmapTransformOptions.WICBitmapTransformRotate0;
				}
			}
			catch (Exception)
			{
			}
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicmetadataQueryReader);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicbitmapFrameDecode);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicimagingFactory);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D2C File Offset: 0x00000F2C
		public Stream ImageStream
		{
			get
			{
				return new MemoryStream(this.buffer, false);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D3A File Offset: 0x00000F3A
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002D42 File Offset: 0x00000F42
		public float Height { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D4B File Offset: 0x00000F4B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002D53 File Offset: 0x00000F53
		public float Width { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002D64 File Offset: 0x00000F64
		public Uri Source { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002D75 File Offset: 0x00000F75
		public DateTime? DateTaken { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002D86 File Offset: 0x00000F86
		public WICBitmapTransformOptions Orientation { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002D8F File Offset: 0x00000F8F
		public byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D98 File Offset: 0x00000F98
		private static byte[] ReadToEnd(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private static WICBitmapTransformOptions TransformOptionsFromUshort(ushort value)
		{
			WICBitmapTransformOptions result = WICBitmapTransformOptions.WICBitmapTransformRotate0;
			switch (value)
			{
			case 2:
				result = WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal;
				break;
			case 3:
				result = WICBitmapTransformOptions.WICBitmapTransformRotate180;
				break;
			case 4:
				result = WICBitmapTransformOptions.WICBitmapTransformFlipVertical;
				break;
			case 5:
				result = (WICBitmapTransformOptions.WICBitmapTransformRotate90 | WICBitmapTransformOptions.WICBitmapTransformRotate180 | WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal);
				break;
			case 6:
				result = WICBitmapTransformOptions.WICBitmapTransformRotate90;
				break;
			case 7:
				result = (WICBitmapTransformOptions.WICBitmapTransformRotate90 | WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal);
				break;
			case 8:
				result = WICBitmapTransformOptions.WICBitmapTransformRotate270;
				break;
			}
			return result;
		}

		// Token: 0x0400002B RID: 43
		private const string DateTakenMetadataPath = "/app1/ifd/exif/subifd:{uint=36867}";

		// Token: 0x0400002C RID: 44
		private const string DateFormat = "yyyy:MM:dd HH:mm:ss";

		// Token: 0x0400002D RID: 45
		private const string OrientationMetadataPath = "/app1/ifd/{ushort=274}";

		// Token: 0x0400002E RID: 46
		private readonly byte[] buffer;
	}
}
