using System;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000139 RID: 313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatacenterServerAuthentication
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x000354F3 File Offset: 0x000336F3
		public DatacenterServerAuthentication()
		{
			this.randomNumberGenerator = RandomNumberGenerator.Create();
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00035506 File Offset: 0x00033706
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0003550E File Offset: 0x0003370E
		public byte[] CurrentSecretKey
		{
			get
			{
				return this.currentSecretKey;
			}
			set
			{
				this.currentSecretKey = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00035517 File Offset: 0x00033717
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0003551F File Offset: 0x0003371F
		internal byte[] CurrentIVKey
		{
			get
			{
				return this.currentIVKey;
			}
			set
			{
				this.currentIVKey = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00035528 File Offset: 0x00033728
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00035530 File Offset: 0x00033730
		public byte[] PreviousSecretKey
		{
			get
			{
				return this.previousSecretKey;
			}
			set
			{
				this.previousSecretKey = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00035539 File Offset: 0x00033739
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00035541 File Offset: 0x00033741
		internal byte[] PreviousIVKey
		{
			get
			{
				return this.previousIVKey;
			}
			set
			{
				this.previousIVKey = value;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003554A File Offset: 0x0003374A
		public bool TrySetCurrentAndPreviousSecretKeys(string currentKeyBase64, string previousKeyBase64)
		{
			return this.TrySetCurrentAndPreviousSecretKeys(currentKeyBase64, previousKeyBase64, null, null);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00035558 File Offset: 0x00033758
		public bool TrySetCurrentAndPreviousSecretKeys(string currentKeyBase64, string previousKeyBase64, string currentIVBase64, string previousIVBase64)
		{
			if (string.IsNullOrEmpty(currentKeyBase64))
			{
				return false;
			}
			if (previousKeyBase64 != null && previousKeyBase64.Length == 0)
			{
				return false;
			}
			bool result;
			try
			{
				this.currentSecretKey = Convert.FromBase64String(currentKeyBase64);
				if (!string.IsNullOrEmpty(currentIVBase64))
				{
					this.currentIVKey = Convert.FromBase64String(currentIVBase64);
				}
				if (string.IsNullOrEmpty(previousKeyBase64))
				{
					this.previousSecretKey = null;
					this.previousIVKey = null;
				}
				else
				{
					this.previousSecretKey = Convert.FromBase64String(previousKeyBase64);
					if (!string.IsNullOrEmpty(previousIVBase64))
					{
						this.previousIVKey = Convert.FromBase64String(previousIVBase64);
					}
				}
				result = true;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000355F0 File Offset: 0x000337F0
		public string GetAuthenticationString()
		{
			if (this.currentSecretKey == null)
			{
				throw new InvalidOperationException("Current secret key was not set");
			}
			byte[] randomBytes = this.GetRandomBytes(128);
			long utcTicks = ExDateTime.Now.UtcTicks;
			return this.GenerateCustomAuthenticationString(randomBytes, utcTicks);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00035634 File Offset: 0x00033834
		public string GenerateCustomAuthenticationString(byte[] nonce, long timestamp)
		{
			if (nonce == null)
			{
				throw new ArgumentNullException("nonce");
			}
			if (this.currentSecretKey == null)
			{
				throw new InvalidOperationException("Current secret key was not set");
			}
			byte[] hashedValue = this.GetHashedValue(this.currentSecretKey, nonce, 0, nonce.Length, timestamp);
			byte[] array = new byte[1 + nonce.Length + hashedValue.Length + 8];
			array[0] = 0;
			Array.Copy(nonce, 0, array, 1, nonce.Length);
			Array.Copy(hashedValue, 0, array, 1 + nonce.Length, hashedValue.Length);
			DatacenterServerAuthentication.SetBlobTailToLong(array, timestamp);
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000356B4 File Offset: 0x000338B4
		public bool ValidateAuthenticationString(string authenticationString)
		{
			if (this.currentSecretKey == null)
			{
				throw new InvalidOperationException("Current secret key was not set");
			}
			if (string.IsNullOrEmpty(authenticationString))
			{
				return false;
			}
			byte[] array = null;
			try
			{
				array = Convert.FromBase64String(authenticationString);
			}
			catch (FormatException)
			{
				return false;
			}
			if (array.Length < 9)
			{
				return false;
			}
			if (array[0] != 0)
			{
				return false;
			}
			long longFromBlobTail = DatacenterServerAuthentication.GetLongFromBlobTail(array);
			if (ExDateTime.Now.UtcTicks - (long)((ulong)-1294967296) > longFromBlobTail)
			{
				return false;
			}
			bool result = false;
			byte[] hashedValue = this.GetHashedValue(this.currentSecretKey, array, 1, 16, longFromBlobTail);
			if (array.Length != 17 + hashedValue.Length + 8)
			{
				return false;
			}
			if (DatacenterServerAuthentication.HashesMatch(hashedValue, array, 17))
			{
				result = true;
			}
			else if (this.previousSecretKey != null)
			{
				hashedValue = this.GetHashedValue(this.previousSecretKey, array, 1, 16, longFromBlobTail);
				if (array.Length == 17 + hashedValue.Length + 8)
				{
					result = DatacenterServerAuthentication.HashesMatch(hashedValue, array, 17);
				}
			}
			return result;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000357A0 File Offset: 0x000339A0
		private static void SetBlobTailToLong(byte[] blob, long value)
		{
			int num = blob.Length;
			blob[num - 8] = (byte)(value & 255L);
			blob[num - 7] = (byte)(value >> 8 & 255L);
			blob[num - 6] = (byte)(value >> 16 & 255L);
			blob[num - 5] = (byte)(value >> 24 & 255L);
			blob[num - 4] = (byte)(value >> 32 & 255L);
			blob[num - 3] = (byte)(value >> 40 & 255L);
			blob[num - 2] = (byte)(value >> 48 & 255L);
			blob[num - 1] = (byte)(value >> 56 & 255L);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00035838 File Offset: 0x00033A38
		private static long GetLongFromBlobTail(byte[] blob)
		{
			int num = blob.Length;
			long num2 = 0L;
			num2 |= (long)((ulong)blob[num - 8]);
			num2 |= (long)((long)((ulong)blob[num - 7]) << 8);
			num2 |= (long)((long)((ulong)blob[num - 6]) << 16);
			num2 |= (long)((long)((ulong)blob[num - 5]) << 24);
			num2 |= (long)((long)((ulong)blob[num - 4]) << 32);
			num2 |= (long)((long)((ulong)blob[num - 3]) << 40);
			num2 |= (long)((long)((ulong)blob[num - 2]) << 48);
			return num2 | (long)((long)((ulong)blob[num - 1]) << 56);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000358AC File Offset: 0x00033AAC
		private static bool HashesMatch(byte[] internalHash, byte[] arrayContainingExternalHash, int externalHashOffset)
		{
			for (int i = 0; i < internalHash.Length; i++)
			{
				if (internalHash[i] != arrayContainingExternalHash[externalHashOffset + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000358D4 File Offset: 0x00033AD4
		private byte[] GetRandomBytes(int bits)
		{
			byte[] array = new byte[bits / 8];
			this.randomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000358F8 File Offset: 0x00033AF8
		private byte[] GetHashedValue(byte[] secretKey, byte[] arrayContainingNonce, int nonceStartOffset, int nonceByteLength, long timestamp)
		{
			byte[] array = new byte[secretKey.Length + nonceByteLength + 8];
			Array.Copy(secretKey, array, secretKey.Length);
			Array.Copy(arrayContainingNonce, nonceStartOffset, array, secretKey.Length, nonceByteLength);
			DatacenterServerAuthentication.SetBlobTailToLong(array, timestamp);
			byte[] result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				result = sha256Cng.ComputeHash(array);
			}
			return result;
		}

		// Token: 0x040006D9 RID: 1753
		public const int NonceLength = 128;

		// Token: 0x040006DA RID: 1754
		public const long MaxAuthStringLifetimeTicks = 3000000000L;

		// Token: 0x040006DB RID: 1755
		private const int AuthenticationBlobVersion = 0;

		// Token: 0x040006DC RID: 1756
		private readonly RandomNumberGenerator randomNumberGenerator;

		// Token: 0x040006DD RID: 1757
		private byte[] currentSecretKey;

		// Token: 0x040006DE RID: 1758
		private byte[] currentIVKey;

		// Token: 0x040006DF RID: 1759
		private byte[] previousIVKey;

		// Token: 0x040006E0 RID: 1760
		private byte[] previousSecretKey;
	}
}
