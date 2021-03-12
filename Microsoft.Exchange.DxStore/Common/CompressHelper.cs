using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200007F RID: 127
	public static class CompressHelper
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		public static byte[] Zip(string str)
		{
			if (str == null)
			{
				str = string.Empty;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Compress))
					{
						memoryStream.CopyTo(gzipStream);
					}
					result = memoryStream2.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00010D90 File Offset: 0x0000EF90
		public static string Unzip(byte[] bytes)
		{
			string result = string.Empty;
			if (bytes != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(bytes))
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
						{
							gzipStream.CopyTo(memoryStream2);
						}
						result = Encoding.UTF8.GetString(memoryStream2.ToArray());
					}
				}
			}
			return result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00010E20 File Offset: 0x0000F020
		public static string ZipToBase64String(string str)
		{
			return Convert.ToBase64String(CompressHelper.Zip(str));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00010E3C File Offset: 0x0000F03C
		public static string UnzipFromBase64String(string base64Str)
		{
			return CompressHelper.Unzip(Convert.FromBase64String(base64Str));
		}
	}
}
