using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B2 RID: 434
	internal class ExceptionTools
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
		public static string GetCompressedStackTrace(Exception e)
		{
			string result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					byte[] bytes = Encoding.UTF8.GetBytes(e.ToString());
					gzipStream.Write(bytes, 0, bytes.Length);
					gzipStream.Flush();
				}
				memoryStream.Flush();
				byte[] buffer = memoryStream.GetBuffer();
				result = Convert.ToBase64String(buffer, 0, (int)memoryStream.Position, Base64FormattingOptions.None);
			}
			return result;
		}
	}
}
