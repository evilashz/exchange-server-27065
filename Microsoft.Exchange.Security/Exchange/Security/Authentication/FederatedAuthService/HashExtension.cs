using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000083 RID: 131
	internal static class HashExtension
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x000252D4 File Offset: 0x000234D4
		static HashExtension()
		{
			HashExtension.authSalt = new byte[8];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(HashExtension.authSalt);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00025308 File Offset: 0x00023508
		public static bool CompareHash(byte[] a, byte[] b)
		{
			return a.Length == b.Length && Win32.memcmp(a, b, (long)a.Length) == 0;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00025324 File Offset: 0x00023524
		public static byte[] GetPasswordHash(byte[] password)
		{
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] array = new byte[password.Length + HashExtension.authSalt.Length];
				try
				{
					Array.Copy(password, array, password.Length);
					Array.Copy(HashExtension.authSalt, 0, array, password.Length, HashExtension.authSalt.Length);
					sha256Cng.ComputeHash(array);
				}
				finally
				{
					Array.Clear(array, 0, array.Length);
				}
				hash = sha256Cng.Hash;
			}
			return hash;
		}

		// Token: 0x04000501 RID: 1281
		private static readonly byte[] authSalt;

		// Token: 0x04000502 RID: 1282
		public static readonly byte[] InvalidHash = new byte[0];
	}
}
