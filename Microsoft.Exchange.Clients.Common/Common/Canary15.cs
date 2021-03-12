using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000008 RID: 8
	internal class Canary15
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000042D8 File Offset: 0x000024D8
		public Canary15(string logonUniqueKey)
		{
			byte[] userContextIdBinary = Guid.NewGuid().ToByteArray();
			byte[] bytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
			long keyIndex;
			int segmentIndex;
			byte[] hashBinary = Canary15DataManager.ComputeHash(userContextIdBinary, bytes, logonUniqueKey, out keyIndex, out segmentIndex);
			this.Init(userContextIdBinary, bytes, logonUniqueKey, hashBinary, Canary15.FormatLogData(keyIndex, segmentIndex));
			this.IsRenewed = true;
			this.IsAboutToExpire = false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000433C File Offset: 0x0000253C
		private Canary15(byte[] userContextIdBinary, byte[] timeStampBinary, string logonUniqueKey, byte[] hashBinary, string logData)
		{
			this.Init(userContextIdBinary, timeStampBinary, logonUniqueKey, hashBinary, logData);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00004351 File Offset: 0x00002551
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00004359 File Offset: 0x00002559
		public string UserContextId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00004362 File Offset: 0x00002562
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000436A File Offset: 0x0000256A
		public string LogonUniqueKey { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00004373 File Offset: 0x00002573
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000437B File Offset: 0x0000257B
		internal bool IsRenewed { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00004384 File Offset: 0x00002584
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000438C File Offset: 0x0000258C
		internal bool IsAboutToExpire { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00004395 File Offset: 0x00002595
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000439D File Offset: 0x0000259D
		internal DateTime CreationTime { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000043A6 File Offset: 0x000025A6
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000043AE File Offset: 0x000025AE
		internal string LogData { get; private set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000043B8 File Offset: 0x000025B8
		public static Canary15 RestoreCanary15(string canaryString, string logonUniqueKey)
		{
			byte[] userContextIdBinary;
			byte[] array;
			byte[] array2;
			if (Canary15.ParseCanary15(canaryString, out userContextIdBinary, out array, out array2))
			{
				if (Canary15.IsExpired(array))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(5L, "Canary is expired, timeStampBinary={0}", Canary15.GetHexString(array));
					return null;
				}
				long keyIndex;
				int segmentIndex;
				byte[] array3 = Canary15DataManager.ComputeHash(userContextIdBinary, array, logonUniqueKey, out keyIndex, out segmentIndex);
				if (Canary15.AreEqual(array3, array2))
				{
					return new Canary15(userContextIdBinary, array, logonUniqueKey, array2, Canary15.FormatLogData(keyIndex, segmentIndex));
				}
				ExTraceGlobals.CoreTracer.TraceDebug<string, string>(5L, "testHashBinary={0}!=hashBinary={1}", Canary15.GetHexString(array3), Canary15.GetHexString(array2));
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string, string>(5L, "RestoreCanary failed, logonUniqueKey={0}, canaryString={1}", logonUniqueKey, canaryString);
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00004450 File Offset: 0x00002650
		public static bool ValidateCanary15(string canaryString, string logonUniqueKey)
		{
			byte[] userContextIdBinary;
			byte[] array;
			byte[] array2;
			if (!Canary15.ParseCanary15(canaryString, out userContextIdBinary, out array, out array2))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(10L, "ValidateCanary failed, canaryString={0}", canaryString);
				return false;
			}
			if (Canary15.IsExpired(array))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(10L, "Canary is expired, timeStampBinary={0}", Canary15.GetHexString(array));
				return false;
			}
			long num;
			int num2;
			byte[] array3 = Canary15DataManager.ComputeHash(userContextIdBinary, array, logonUniqueKey, out num, out num2);
			if (Canary15.AreEqual(array2, array3))
			{
				return true;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string, string>(10L, "testHashBinary={0}!=hashBinary={1}", Canary15.GetHexString(array3), Canary15.GetHexString(array2));
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000044DC File Offset: 0x000026DC
		public override string ToString()
		{
			return this.canaryString;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000044E4 File Offset: 0x000026E4
		private static string GetHexString(byte[] bytes)
		{
			if (!ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return null;
			}
			if (bytes == null)
			{
				return "NULL_BYTE_ARRAY";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000453C File Offset: 0x0000273C
		private static bool IsExpired(byte[] timeStampBinary)
		{
			long num = BitConverter.ToInt64(timeStampBinary, 0);
			long ticks = DateTime.UtcNow.Ticks;
			if (num + 864000000000L < ticks)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<long, long, long>(3L, "timeStamp{0}+timeStampLifetime{1} < utcNow={2}", num, 864000000000L, ticks);
				return true;
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000458C File Offset: 0x0000278C
		private static bool IsNearExpiration(byte[] timeStampBinary)
		{
			long num = BitConverter.ToInt64(timeStampBinary, 0);
			long ticks = DateTime.UtcNow.Ticks;
			if (num + 36000000000L < ticks)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<long, long, long>(3L, "timeStamp{0}+timeStampHalfLifetime{1} < utcNow={2}", num, 36000000000L, ticks);
				return true;
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000045DC File Offset: 0x000027DC
		private static string Encode(byte[] canaryBinary)
		{
			int num = (canaryBinary.Length + 2 - (canaryBinary.Length + 2) % 3) / 3 * 4;
			char[] array = new char[num];
			int num2 = Convert.ToBase64CharArray(canaryBinary, 0, canaryBinary.Length, array, 0);
			for (int i = 0; i < num2; i++)
			{
				char c = array[i];
				if (c != '+')
				{
					if (c != '/')
					{
						if (c == '=')
						{
							array[i] = '.';
						}
					}
					else
					{
						array[i] = '_';
					}
				}
				else
				{
					array[i] = '-';
				}
			}
			return new string(array);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00004650 File Offset: 0x00002850
		private static byte[] Decode(string canaryString)
		{
			char[] array = canaryString.ToCharArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				switch (c)
				{
				case '-':
					array[i] = '+';
					break;
				case '.':
					array[i] = '=';
					break;
				default:
					if (c == '_')
					{
						array[i] = '/';
					}
					break;
				}
			}
			return Convert.FromBase64CharArray(array, 0, num);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000046AC File Offset: 0x000028AC
		private static bool ParseCanary15(string canaryString, out byte[] userContextIdBinary, out byte[] timeStampBinary, out byte[] hashBinary)
		{
			userContextIdBinary = null;
			timeStampBinary = null;
			hashBinary = null;
			if (canaryString == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(4L, "Canary string is null");
				return false;
			}
			if (canaryString.Length != 76)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<int>(4L, "canaryString.length={0}", canaryString.Length);
				return false;
			}
			byte[] array;
			try
			{
				array = Canary15.Decode(canaryString);
			}
			catch (FormatException ex)
			{
				if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(4L, "Format Exception {0}", ex.ToString());
				}
				return false;
			}
			if (array.Length != 56)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<int, int>(4L, "canaryBinary.Length={0}!=CanaryBinaryLength={1}", array.Length, 56);
				return false;
			}
			userContextIdBinary = new byte[16];
			timeStampBinary = new byte[8];
			hashBinary = new byte[32];
			Array.Copy(array, 0, userContextIdBinary, 0, 16);
			Array.Copy(array, 16, timeStampBinary, 0, 8);
			Array.Copy(array, 24, hashBinary, 0, 32);
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000047A0 File Offset: 0x000029A0
		private static bool AreEqual(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000047D8 File Offset: 0x000029D8
		private static string FormatLogData(long keyIndex, int segmentIndex)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				keyIndex,
				segmentIndex
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00004810 File Offset: 0x00002A10
		private void Init(byte[] userContextIdBinary, byte[] timeStampBinary, string logonUniqueKey, byte[] hashBinary, string logData)
		{
			byte[] array = new byte[userContextIdBinary.Length + timeStampBinary.Length + hashBinary.Length];
			userContextIdBinary.CopyTo(array, 0);
			timeStampBinary.CopyTo(array, userContextIdBinary.Length);
			hashBinary.CopyTo(array, userContextIdBinary.Length + timeStampBinary.Length);
			this.UserContextId = new Guid(userContextIdBinary).ToString("N");
			this.LogonUniqueKey = logonUniqueKey;
			this.canaryString = Canary15.Encode(array);
			long ticks = BitConverter.ToInt64(timeStampBinary, 0);
			this.CreationTime = new DateTime(ticks, DateTimeKind.Utc);
			this.LogData = logData;
			this.IsRenewed = false;
			this.IsAboutToExpire = Canary15.IsNearExpiration(timeStampBinary);
		}

		// Token: 0x040001B2 RID: 434
		public const int CanaryStringLength = 76;

		// Token: 0x040001B3 RID: 435
		private const int UserContextIdLength = 16;

		// Token: 0x040001B4 RID: 436
		private const int TimeStampLength = 8;

		// Token: 0x040001B5 RID: 437
		private const int HashLength = 32;

		// Token: 0x040001B6 RID: 438
		private const int CanaryBinaryLength = 56;

		// Token: 0x040001B7 RID: 439
		private const long TimeStampHalfLifetime = 36000000000L;

		// Token: 0x040001B8 RID: 440
		private const long TimeStampLifetime = 864000000000L;

		// Token: 0x040001B9 RID: 441
		private string canaryString;
	}
}
