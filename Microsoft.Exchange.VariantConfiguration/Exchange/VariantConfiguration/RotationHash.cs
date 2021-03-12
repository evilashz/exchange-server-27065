using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000139 RID: 313
	public static class RotationHash
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x000238F8 File Offset: 0x00021AF8
		public static int ComputeHash(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException("input");
			}
			input = input.ToLowerInvariant();
			byte[] array = Encoding.UTF8.GetBytes(input);
			lock (RotationHash.staticLock)
			{
				array = RotationHash.Sha256.ComputeHash(array);
			}
			uint num = 0U;
			for (int i = 0; i < 4; i++)
			{
				num <<= 8;
				num |= (uint)array[i];
			}
			return (int)(num % 1000U);
		}

		// Token: 0x040004AB RID: 1195
		public const int HashCount = 1000;

		// Token: 0x040004AC RID: 1196
		private const int IntByteCount = 4;

		// Token: 0x040004AD RID: 1197
		private static readonly HashAlgorithm Sha256 = new SHA256CryptoServiceProvider();

		// Token: 0x040004AE RID: 1198
		private static object staticLock = new object();
	}
}
