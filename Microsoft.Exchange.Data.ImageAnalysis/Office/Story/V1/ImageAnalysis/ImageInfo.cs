using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	internal class ImageInfo
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002A7D File Offset: 0x00000C7D
		public ImageInfo(ImageSource imageSource)
		{
			if (imageSource == null)
			{
				throw new ArgumentNullException("imageSource");
			}
			this.imageSource = imageSource;
			this.Width = this.imageSource.Width;
			this.Height = this.imageSource.Height;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ABC File Offset: 0x00000CBC
		protected ImageInfo()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public ImageSource ImageSource
		{
			get
			{
				return this.imageSource;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002ACC File Offset: 0x00000CCC
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public float Width { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002ADD File Offset: 0x00000CDD
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public float Height { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002AEE File Offset: 0x00000CEE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public SalientObjectAnalysis SalientObjectAnalysis { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002AFF File Offset: 0x00000CFF
		public bool IsLocked
		{
			get
			{
				return null == this.imageSource;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B0C File Offset: 0x00000D0C
		public static ImageInfo FromBase64(string base64)
		{
			if (base64 == null)
			{
				throw new ArgumentNullException("base64");
			}
			ImageInfo result;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(base64)))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					result = (ImageInfo)binaryFormatter.Deserialize(gzipStream);
				}
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B84 File Offset: 0x00000D84
		public void Lock()
		{
			this.imageSource = null;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B90 File Offset: 0x00000D90
		public string ToBase64()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.Serialize(gzipStream, this);
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BFC File Offset: 0x00000DFC
		internal SalientObjectAnalysis PerformSalientObjectAnalysis()
		{
			this.CheckNotLocked();
			return new SalientObjectAnalysis(this.imageSource);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C0F File Offset: 0x00000E0F
		private void CheckNotLocked()
		{
			if (this.IsLocked)
			{
				throw new InvalidOperationException("ImageInfo is locked and no further analysis is possible.");
			}
		}

		// Token: 0x04000022 RID: 34
		[NonSerialized]
		private ImageSource imageSource;
	}
}
