using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000052 RID: 82
	internal class CacheFileCookieSerializer
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00009C6C File Offset: 0x00007E6C
		internal static LinkedList<CacheFileCookie> ReadCookieList(FileStream fs)
		{
			return CacheFileCookieSerializer.ReadCookieList(fs, -1L, -1);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009C78 File Offset: 0x00007E78
		internal static CacheFileCookie ReadCookie(FileStream fs, long offset)
		{
			LinkedList<CacheFileCookie> linkedList = CacheFileCookieSerializer.ReadCookieList(fs, offset, 1);
			if (linkedList.First == null)
			{
				return null;
			}
			return linkedList.First.Value;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009CA4 File Offset: 0x00007EA4
		internal static CacheFileCookie ReadLastCookie(FileStream fs)
		{
			LinkedList<CacheFileCookie> linkedList = CacheFileCookieSerializer.ReadCookieList(fs, -1L, -1);
			if (linkedList.Last == null)
			{
				return null;
			}
			return linkedList.Last.Value;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009CD0 File Offset: 0x00007ED0
		internal static LinkedList<CacheFileCookie> ReadCookieList(FileStream fs, long offset, int pageSize)
		{
			LinkedList<CacheFileCookie> linkedList = new LinkedList<CacheFileCookie>();
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			CacheFileCookieSerializer.SetOffsetAndPageSize(fs, binaryFormatter, offset, ref pageSize);
			for (int i = 0; i < pageSize; i++)
			{
				long position = fs.Position;
				if (position == fs.Length)
				{
					break;
				}
				CacheFileCookie cacheFileCookie = (CacheFileCookie)binaryFormatter.Deserialize(fs);
				cacheFileCookie.NextCookieOffset = fs.Position;
				linkedList.AddLast(cacheFileCookie);
			}
			return linkedList;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009D38 File Offset: 0x00007F38
		internal static void ReadNextCookies(FileStream fs, long offset, int pageSize, LinkedList<CacheFileCookie> cookieList, int capacity)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			CacheFileCookieSerializer.SetOffsetAndPageSize(fs, binaryFormatter, offset, ref pageSize);
			for (int i = 0; i < pageSize; i++)
			{
				long position = fs.Position;
				if (position == fs.Length)
				{
					return;
				}
				CacheFileCookie cacheFileCookie = (CacheFileCookie)binaryFormatter.Deserialize(fs);
				cacheFileCookie.NextCookieOffset = fs.Position;
				cookieList.AddLast(cacheFileCookie);
				if (cookieList.Count > capacity)
				{
					cookieList.RemoveFirst();
				}
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009DA4 File Offset: 0x00007FA4
		internal static void ReadPreCookies(FileStream fs, long offset, int pageSize, LinkedList<CacheFileCookie> cookieList, int capacity)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			CacheFileCookieSerializer.SetOffsetAndPageSize(fs, binaryFormatter, offset, ref pageSize);
			for (int i = 0; i < pageSize; i++)
			{
				long position = fs.Position;
				if (position == fs.Length)
				{
					return;
				}
				CacheFileCookie cacheFileCookie = (CacheFileCookie)binaryFormatter.Deserialize(fs);
				cacheFileCookie.NextCookieOffset = fs.Position;
				cookieList.AddFirst(cacheFileCookie);
				if (cookieList.Count > capacity)
				{
					cookieList.RemoveLast();
				}
				if (cacheFileCookie.PreCookieOffset < 0L)
				{
					return;
				}
				fs.Seek(cacheFileCookie.PreCookieOffset, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009E2C File Offset: 0x0000802C
		internal static void WriteCookieToFile(FileStream cookieFileStream, CacheFileCookie cookie, CacheFileCookie preCookie)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			if (cookieFileStream.Length == 0L)
			{
				binaryFormatter.Serialize(cookieFileStream, 0);
			}
			cookie.PreCookieOffset = preCookie.CookieOffset;
			cookie.CookieOffset = cookieFileStream.Position;
			binaryFormatter.Serialize(cookieFileStream, cookie);
			cookieFileStream.SetLength(cookieFileStream.Position);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009E84 File Offset: 0x00008084
		internal static void StampCookieCount(FileStream cookieFileStream, int count)
		{
			cookieFileStream.Seek(0L, SeekOrigin.Begin);
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			binaryFormatter.Serialize(cookieFileStream, count);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009EB0 File Offset: 0x000080B0
		internal static void SetOffsetAndPageSize(FileStream fs, BinaryFormatter formatter, long offset, ref int pageSize)
		{
			if (offset < 0L)
			{
				fs.Seek(0L, SeekOrigin.Begin);
				int num = (int)formatter.Deserialize(fs);
				if (pageSize < 0)
				{
					pageSize = num;
					return;
				}
			}
			else
			{
				fs.Seek(offset, SeekOrigin.Begin);
			}
		}
	}
}
