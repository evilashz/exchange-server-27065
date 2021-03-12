using System;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Compliance
{
	// Token: 0x02000004 RID: 4
	public abstract class HMACForNonCryptographicPurposes : HMAC
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000386C File Offset: 0x00001A6C
		public HMACForNonCryptographicPurposes(byte[] key, HashAlgorithm hashAlgorithm)
		{
			this.hashAlgorithm = hashAlgorithm;
			byte[] array;
			if (key.Length > 64)
			{
				array = new byte[64];
				using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
				{
					byte[] array2 = messageDigestForNonCryptographicPurposes.ComputeHash(key);
					Buffer.BlockCopy(array2, 0, array, 0, array2.Length);
					goto IL_61;
				}
			}
			if (key.Length == 64)
			{
				array = key;
			}
			else
			{
				array = new byte[64];
				Buffer.BlockCopy(key, 0, array, 0, key.Length);
			}
			IL_61:
			this.keyInnerPad = new byte[64];
			this.keyOuterPad = new byte[64];
			for (int i = 0; i < 64; i++)
			{
				this.keyInnerPad[i] = (array[i] ^ 54);
				this.keyOuterPad[i] = (array[i] ^ 92);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00003930 File Offset: 0x00001B30
		public override byte[] Hash
		{
			get
			{
				if (this.HashValue == null)
				{
					throw new CryptographicUnexpectedOperationException("Hash must be finalized before the hash value is retrieved.");
				}
				return this.HashValue;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000394C File Offset: 0x00001B4C
		public new byte[] ComputeHash(byte[] message)
		{
			byte[] right = this.hashAlgorithm.ComputeHash(HMACForNonCryptographicPurposes.Concatenate(this.keyInnerPad, message));
			this.HashValue = this.hashAlgorithm.ComputeHash(HMACForNonCryptographicPurposes.Concatenate(this.keyOuterPad, right));
			return this.HashValue;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003994 File Offset: 0x00001B94
		public new byte[] ComputeHash(Stream stream)
		{
			long length = stream.Length;
			byte[] array = new byte[length];
			stream.Read(array, 0, (int)length);
			return this.ComputeHash(array);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000039C4 File Offset: 0x00001BC4
		private static byte[] Concatenate(byte[] left, byte[] right)
		{
			byte[] array = new byte[left.Length + right.Length];
			Buffer.BlockCopy(left, 0, array, 0, left.Length);
			Buffer.BlockCopy(right, 0, array, left.Length, right.Length);
			return array;
		}

		// Token: 0x04000009 RID: 9
		private readonly HashAlgorithm hashAlgorithm;

		// Token: 0x0400000A RID: 10
		private readonly byte[] keyInnerPad;

		// Token: 0x0400000B RID: 11
		private readonly byte[] keyOuterPad;
	}
}
