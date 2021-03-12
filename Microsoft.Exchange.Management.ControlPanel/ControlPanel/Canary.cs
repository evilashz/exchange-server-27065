using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000546 RID: 1350
	public class Canary
	{
		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000BF89C File Offset: 0x000BDA9C
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000BF8A4 File Offset: 0x000BDAA4
		public string UserContextId { get; private set; }

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000BF8AD File Offset: 0x000BDAAD
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x000BF8B5 File Offset: 0x000BDAB5
		public string LogonUniqueKey { get; private set; }

		// Token: 0x06003F80 RID: 16256 RVA: 0x000BF8C0 File Offset: 0x000BDAC0
		static Canary()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 585, ".cctor", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Utilities\\Canary.cs");
			byte[] array = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().ObjectGuid.ToByteArray();
			byte[] array2 = topologyConfigurationSession.GetDatabasesContainerId().ObjectGuid.ToByteArray();
			Canary.adObjectIdsBinary = new byte[array.Length + array2.Length];
			array.CopyTo(Canary.adObjectIdsBinary, 0);
			array2.CopyTo(Canary.adObjectIdsBinary, array.Length);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000BF944 File Offset: 0x000BDB44
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

		// Token: 0x06003F82 RID: 16258 RVA: 0x000BF9E4 File Offset: 0x000BDBE4
		private static bool IsExpired(byte[] timeStampBinary)
		{
			long num = BitConverter.ToInt64(timeStampBinary, 0);
			return num < DateTime.UtcNow.Ticks;
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000BFA0C File Offset: 0x000BDC0C
		private Canary(byte[] userContextIdBinary, byte[] timeStampBinary, string logonUniqueKey)
		{
			byte[] array = Canary.ComputeHash(userContextIdBinary, timeStampBinary, logonUniqueKey);
			byte[] array2 = new byte[userContextIdBinary.Length + timeStampBinary.Length + array.Length];
			userContextIdBinary.CopyTo(array2, 0);
			timeStampBinary.CopyTo(array2, userContextIdBinary.Length);
			array.CopyTo(array2, userContextIdBinary.Length + timeStampBinary.Length);
			this.UserContextId = new Guid(userContextIdBinary).ToString("N");
			this.LogonUniqueKey = logonUniqueKey;
			this.canaryString = Canary.Encode(array2);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000BFA88 File Offset: 0x000BDC88
		public Canary(Guid userContextId, string logonUniqueKey) : this(userContextId.ToByteArray(), BitConverter.GetBytes(DateTime.UtcNow.Ticks + 1728000000000L), logonUniqueKey)
		{
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000BFAC0 File Offset: 0x000BDCC0
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

		// Token: 0x06003F86 RID: 16262 RVA: 0x000BFB34 File Offset: 0x000BDD34
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

		// Token: 0x06003F87 RID: 16263 RVA: 0x000BFB90 File Offset: 0x000BDD90
		private static bool ParseCanary(string canaryString, out byte[] userContextIdBinary, out byte[] timeStampBinary, out byte[] hashBinary)
		{
			userContextIdBinary = null;
			timeStampBinary = null;
			hashBinary = null;
			if (string.IsNullOrEmpty(canaryString) || canaryString.Length != 76)
			{
				return false;
			}
			byte[] array;
			try
			{
				array = Canary.Decode(canaryString);
			}
			catch (FormatException)
			{
				return false;
			}
			if (array.Length != 56)
			{
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

		// Token: 0x06003F88 RID: 16264 RVA: 0x000BFC24 File Offset: 0x000BDE24
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

		// Token: 0x06003F89 RID: 16265 RVA: 0x000BFC5C File Offset: 0x000BDE5C
		public static Canary RestoreCanary(string canaryString, string logonUniqueKey)
		{
			byte[] userContextIdBinary;
			byte[] timeStampBinary;
			byte[] b;
			if (Canary.ParseCanary(canaryString, out userContextIdBinary, out timeStampBinary, out b))
			{
				if (Canary.IsExpired(timeStampBinary))
				{
					return null;
				}
				byte[] a = Canary.ComputeHash(userContextIdBinary, timeStampBinary, logonUniqueKey);
				if (Canary.AreEqual(a, b))
				{
					return new Canary(userContextIdBinary, timeStampBinary, logonUniqueKey);
				}
			}
			return null;
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000BFCA0 File Offset: 0x000BDEA0
		public bool ValidateCanary(string canaryString)
		{
			byte[] userContextIdBinary;
			byte[] timeStampBinary;
			byte[] a;
			if (!Canary.ParseCanary(canaryString, out userContextIdBinary, out timeStampBinary, out a))
			{
				return false;
			}
			if (Canary.IsExpired(timeStampBinary))
			{
				return false;
			}
			byte[] b = Canary.ComputeHash(userContextIdBinary, timeStampBinary, this.LogonUniqueKey);
			return Canary.AreEqual(a, b);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000BFCDC File Offset: 0x000BDEDC
		public override string ToString()
		{
			return this.canaryString;
		}

		// Token: 0x04002922 RID: 10530
		private const int UserContextIdLength = 16;

		// Token: 0x04002923 RID: 10531
		private const int TimeStampLength = 8;

		// Token: 0x04002924 RID: 10532
		private const int HashLength = 32;

		// Token: 0x04002925 RID: 10533
		private const int CanaryBinaryLength = 56;

		// Token: 0x04002926 RID: 10534
		private const long TimeStampLifetime = 1728000000000L;

		// Token: 0x04002927 RID: 10535
		public const int CanaryStringLength = 76;

		// Token: 0x04002928 RID: 10536
		private readonly string canaryString;

		// Token: 0x04002929 RID: 10537
		private static byte[] adObjectIdsBinary;
	}
}
