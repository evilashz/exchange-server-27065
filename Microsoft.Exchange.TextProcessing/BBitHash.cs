using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000007 RID: 7
	internal class BBitHash : IDisposable
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public void Dispose()
		{
			this.hashAlgorithm.Dispose();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public void BBitHashShingle(string shingle, ulong[] minimumHashes)
		{
			if (string.IsNullOrEmpty(shingle))
			{
				throw new ArgumentException(Strings.InvalidShingle(shingle));
			}
			ulong termValue;
			ulong termValue2;
			this.ComputeShingleHash(shingle, out termValue, out termValue2);
			int num = minimumHashes.Length / 2;
			for (int i = 0; i < num; i++)
			{
				minimumHashes[i] = Math.Min(this.GenerateTermHash(termValue, i), minimumHashes[i]);
				minimumHashes[i + num] = Math.Min(this.GenerateTermHash(termValue2, i), minimumHashes[i + num]);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D48 File Offset: 0x00000F48
		private void ComputeShingleHash(string input, out ulong upperBits, out ulong lowerBits)
		{
			byte[] value = this.hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(input));
			lowerBits = BitConverter.ToUInt64(value, 0);
			upperBits = BitConverter.ToUInt64(value, 8);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D80 File Offset: 0x00000F80
		private ulong GenerateTermHash(ulong termValue, int hashSeed)
		{
			ulong num = HashSeeds.PrimeNumbers[hashSeed];
			ulong num2 = HashSeeds.LittlePrimeNumbers[hashSeed];
			num = num * (num2 * (termValue >> 32) >> 5 | num2 * (termValue >> 32) << 59) + (termValue >> 32);
			ulong num3 = termValue & (ulong)-1;
			return num * (num2 * num3 >> 5) | (num2 * num3 << 59) + num3;
		}

		// Token: 0x04000024 RID: 36
		private HashAlgorithm hashAlgorithm = MessageDigestForNonCryptographicPurposes.Create();
	}
}
