using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A6 RID: 166
	internal static class SpamRuleBlobUtils
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x0001281C File Offset: 0x00010A1C
		public static string CompressData(string data)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(gzipStream, data);
				}
				@string = Encoding.Default.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001288C File Offset: 0x00010A8C
		public static string DecompressData(string data)
		{
			byte[] bytes = Encoding.Default.GetBytes(data);
			string result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				int num = bytes.Length;
				int i = num;
				int num2 = 0;
				while (i > 0)
				{
					int num3 = memoryStream.Read(bytes, num2, i);
					if (num3 == 0)
					{
						break;
					}
					num2 += num3;
					i -= num3;
				}
				memoryStream.Position = 0L;
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					result = (string)binaryFormatter.Deserialize(gzipStream);
				}
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00012938 File Offset: 0x00010B38
		public static string[] GetProcessorIds(string compressedProcessorIds)
		{
			return SpamRuleBlobUtils.DecompressData(compressedProcessorIds).Split(new char[]
			{
				','
			});
		}
	}
}
