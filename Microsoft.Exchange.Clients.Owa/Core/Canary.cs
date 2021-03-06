using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000E6 RID: 230
	internal class Canary
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x0003A52C File Offset: 0x0003872C
		static Canary()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 78, ".cctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Canary.cs");
			byte[] array = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().ObjectGuid.ToByteArray();
			byte[] array2 = topologyConfigurationSession.GetDatabasesContainerId().ObjectGuid.ToByteArray();
			Canary.adObjectIdsBinary = new byte[array.Length + array2.Length];
			array.CopyTo(Canary.adObjectIdsBinary, 0);
			array2.CopyTo(Canary.adObjectIdsBinary, array.Length);
			if (ExTraceGlobals.UserContextTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				using (SHA256Cng sha256Cng = new SHA256Cng())
				{
					byte[] bytes = sha256Cng.ComputeHash(Canary.adObjectIdsBinary);
					ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(2L, "{0}.Canary(): adObjectIdsBinaryHash={1}", "Owa.Core.Canary", Canary.GetHexString(bytes));
					sha256Cng.Clear();
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003A60C File Offset: 0x0003880C
		public Canary(Guid userContextId, string logonUniqueKey) : this(userContextId.ToByteArray(), BitConverter.GetBytes(DateTime.UtcNow.Ticks + 864000000000L), logonUniqueKey)
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003A644 File Offset: 0x00038844
		private Canary(byte[] userContextIdBinary, byte[] timeStampBinary, string logonUniqueKey)
		{
			byte[] array = Canary.ComputeHash(userContextIdBinary, timeStampBinary, logonUniqueKey);
			byte[] array2 = new byte[userContextIdBinary.Length + timeStampBinary.Length + array.Length];
			userContextIdBinary.CopyTo(array2, 0);
			timeStampBinary.CopyTo(array2, userContextIdBinary.Length);
			array.CopyTo(array2, userContextIdBinary.Length + timeStampBinary.Length);
			this.UserContextIdGuid = new Guid(userContextIdBinary);
			this.LogonUniqueKey = logonUniqueKey;
			this.canaryString = Canary.Encode(array2);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003A6B0 File Offset: 0x000388B0
		internal Canary CloneRenewed()
		{
			return new Canary(this.UserContextIdGuid, this.LogonUniqueKey);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0003A6C4 File Offset: 0x000388C4
		public string UserContextId
		{
			get
			{
				return this.UserContextIdGuid.ToString("N");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0003A6E4 File Offset: 0x000388E4
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0003A6EC File Offset: 0x000388EC
		public Guid UserContextIdGuid { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0003A6F5 File Offset: 0x000388F5
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0003A6FD File Offset: 0x000388FD
		public string LogonUniqueKey { get; private set; }

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003A708 File Offset: 0x00038908
		public static Canary RestoreCanary(string canaryString, string logonUniqueKey)
		{
			byte[] userContextIdBinary;
			byte[] timeStampBinary;
			byte[] array;
			if (Canary.ParseCanary(canaryString, out userContextIdBinary, out timeStampBinary, out array) && Canary.ValidateCanary(canaryString, logonUniqueKey))
			{
				return new Canary(userContextIdBinary, timeStampBinary, logonUniqueKey);
			}
			ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(5L, "{0}.RestoreCanary failed: logonUniqueKey={1}, canaryString={2}", "Owa.Core.Canary", logonUniqueKey, canaryString);
			return null;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0003A74E File Offset: 0x0003894E
		public bool ValidateCanary(string canaryString)
		{
			return Canary.ValidateCanary(canaryString, this.LogonUniqueKey);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0003A75C File Offset: 0x0003895C
		public static bool ValidateCanary(string canaryString, string logonUniqueKey)
		{
			byte[] userContextIdBinary;
			byte[] array;
			byte[] array2;
			if (!Canary.ParseCanary(canaryString, out userContextIdBinary, out array, out array2))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(10L, "{0}.{1}: Parse failed, canaryString={2}", "Owa.Core.Canary", "ValidateCanary", canaryString);
				return false;
			}
			if (Canary.IsExpired(array))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(10L, "{0}.{1}: IsExpired=true, timeStampBinary={2}", "Owa.Core.Canary", "ValidateCanary", Canary.GetHexString(array));
				return false;
			}
			byte[] array3 = Canary.ComputeHash(userContextIdBinary, array, logonUniqueKey);
			if (Canary.AreEqual(array2, array3))
			{
				return true;
			}
			ExTraceGlobals.UserContextTracer.TraceDebug(10L, "{0}.{1}: hashBinary is invalid, testHashBinary={2}!=hashBinary={3}", new object[]
			{
				"Owa.Core.Canary",
				"ValidateCanary",
				Canary.GetHexString(array3),
				Canary.GetHexString(array2)
			});
			return false;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003A819 File Offset: 0x00038A19
		public override string ToString()
		{
			return this.canaryString;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0003A824 File Offset: 0x00038A24
		private static string GetHexString(byte[] bytes)
		{
			if (!ExTraceGlobals.UserContextTracer.IsTraceEnabled(TraceType.DebugTrace))
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

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003A87C File Offset: 0x00038A7C
		private static byte[] ComputeHash(byte[] userContextIdBinary, byte[] timeStampBinary, string logonUniqueKey)
		{
			int num = 0;
			byte[] bytes = new UnicodeEncoding().GetBytes(logonUniqueKey);
			byte[] array = new byte[userContextIdBinary.Length + timeStampBinary.Length + bytes.Length + Canary.adObjectIdsBinary.Length];
			userContextIdBinary.CopyTo(array, num);
			num += userContextIdBinary.Length;
			timeStampBinary.CopyTo(array, num);
			num += timeStampBinary.Length;
			bytes.CopyTo(array, num);
			num += bytes.Length;
			Canary.adObjectIdsBinary.CopyTo(array, num);
			byte[] result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				result = sha256Cng.ComputeHash(array);
				sha256Cng.Clear();
			}
			return result;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003A91C File Offset: 0x00038B1C
		private static bool IsExpired(byte[] timeStampBinary)
		{
			long num = BitConverter.ToInt64(timeStampBinary, 0);
			long ticks = DateTime.UtcNow.Ticks;
			if (num < ticks)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, long, long>(3L, "{0}.IsExpired()=false, timeStamp={1}<utcNow={2}", "Owa.Core.Canary", num, ticks);
				return true;
			}
			return false;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0003A960 File Offset: 0x00038B60
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

		// Token: 0x060007BB RID: 1979 RVA: 0x0003A9D4 File Offset: 0x00038BD4
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

		// Token: 0x060007BC RID: 1980 RVA: 0x0003AA30 File Offset: 0x00038C30
		private static bool ParseCanary(string canaryString, out byte[] userContextIdBinary, out byte[] timeStampBinary, out byte[] hashBinary)
		{
			userContextIdBinary = null;
			timeStampBinary = null;
			hashBinary = null;
			if (string.IsNullOrEmpty(canaryString) || canaryString.Length != 76)
			{
				if (canaryString == null)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(4L, "{0}.{1}: canaryString is null", "Owa.Core.Canary", "ParseCanary");
				}
				else
				{
					ExTraceGlobals.UserContextTracer.TraceDebug(4L, "{0}.{1}: canaryString.length={2}, CanaryStringLength={3}", new object[]
					{
						"Owa.Core.Canary",
						"ParseCanary",
						canaryString.Length,
						76
					});
				}
				return false;
			}
			byte[] array;
			try
			{
				array = Canary.Decode(canaryString);
			}
			catch (FormatException ex)
			{
				if (ExTraceGlobals.UserContextTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(4L, "{0}.{1}: Format Exception: {2}", "Owa.Core.Canary", "ParseCanary", ex.ToString());
				}
				return false;
			}
			if (array.Length != 56)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug(4L, "{0}.{1}: canaryBinary.Length={2}!=CanaryBinaryLength={3}", new object[]
				{
					"Owa.Core.Canary",
					"ParseCanary",
					array.Length,
					56
				});
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

		// Token: 0x060007BD RID: 1981 RVA: 0x0003AB9C File Offset: 0x00038D9C
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

		// Token: 0x04000576 RID: 1398
		public const int CanaryStringLength = 76;

		// Token: 0x04000577 RID: 1399
		private const string ThisClassName = "Owa.Core.Canary";

		// Token: 0x04000578 RID: 1400
		private const int UserContextIdLength = 16;

		// Token: 0x04000579 RID: 1401
		private const int TimeStampLength = 8;

		// Token: 0x0400057A RID: 1402
		private const int HashLength = 32;

		// Token: 0x0400057B RID: 1403
		private const int CanaryBinaryLength = 56;

		// Token: 0x0400057C RID: 1404
		private const long TimeStampLifetime = 864000000000L;

		// Token: 0x0400057D RID: 1405
		private readonly string canaryString;

		// Token: 0x0400057E RID: 1406
		private static byte[] adObjectIdsBinary;
	}
}
