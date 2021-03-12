using System;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000059 RID: 89
	public static class StringUtil
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00008A28 File Offset: 0x00006C28
		public static string Unwrap(string value)
		{
			if (value.Length < 2)
			{
				return value;
			}
			int index = value.Length - 1;
			char c = value[index];
			char c2 = value[0];
			if (c2 == '\'' && c == '\'')
			{
				return StringUtil.WithoutFirstAndLastCharacters(value);
			}
			if (c2 == '"' && c == '"')
			{
				return StringUtil.WithoutFirstAndLastCharacters(value);
			}
			if (c2 == '<' && c == '>')
			{
				return StringUtil.WithoutFirstAndLastCharacters(value);
			}
			return value;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008A90 File Offset: 0x00006C90
		public static byte[] PackString(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return null;
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream))
				{
					streamWriter.Write(data);
					streamWriter.Flush();
					array = memoryStream.ToArray();
				}
			}
			if (array.Length == 0)
			{
				return null;
			}
			return array;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008B04 File Offset: 0x00006D04
		public static string UnpackString(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			string result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008B60 File Offset: 0x00006D60
		public static byte[] DecompressBytes(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress, true))
					{
						byte[] array = new byte[4096];
						for (;;)
						{
							int num = gzipStream.Read(array, 0, array.Length);
							if (num == 0)
							{
								break;
							}
							memoryStream2.Write(array, 0, num);
						}
						result = memoryStream2.ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008C08 File Offset: 0x00006E08
		public static byte[] CompressBytes(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return data;
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(data, 0, data.Length);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008C78 File Offset: 0x00006E78
		public static string DecompressString(byte[] data)
		{
			return StringUtil.UnpackString(StringUtil.DecompressBytes(data));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008C85 File Offset: 0x00006E85
		public static byte[] CompressString(string data)
		{
			return StringUtil.CompressBytes(StringUtil.PackString(data));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008C92 File Offset: 0x00006E92
		private static string WithoutFirstAndLastCharacters(string value)
		{
			return value.Substring(1, value.Length - 2);
		}
	}
}
