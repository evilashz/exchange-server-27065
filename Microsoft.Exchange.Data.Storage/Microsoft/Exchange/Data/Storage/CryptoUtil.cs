using System;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000658 RID: 1624
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class CryptoUtil
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x0011E0C0 File Offset: 0x0011C2C0
		public static byte[] GetSha1Hash(byte[] inputBytes)
		{
			byte[] result;
			using (SHA1Cng sha1Cng = new SHA1Cng())
			{
				result = sha1Cng.ComputeHash(inputBytes);
			}
			return result;
		}
	}
}
