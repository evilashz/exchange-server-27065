using System;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD6 RID: 2774
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RsaCapiKey : IDisposable
	{
		// Token: 0x060064B8 RID: 25784 RVA: 0x001AB4EF File Offset: 0x001A96EF
		public RsaCapiKey(CspParameters parameters)
		{
			this.m_provider = new RSACryptoServiceProvider(parameters);
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x001AB504 File Offset: 0x001A9704
		public byte[] Decrypt(byte[] cipherText, bool usePadding)
		{
			int num = this.m_provider.KeySize / 8;
			int maxDataBlockSize = this.getMaxDataBlockSize(num, usePadding);
			int num2 = cipherText.Length / num;
			byte[] array = null;
			byte[] array2 = new byte[num];
			for (int i = num2 - 1; i >= 0; i--)
			{
				Array.Copy(cipherText, i * num, array2, 0, array2.Length);
				byte[] array3 = this.m_provider.Decrypt(array2, usePadding);
				if (array == null)
				{
					array = new byte[num2 * maxDataBlockSize - (maxDataBlockSize - array3.Length)];
				}
				Array.Copy(array3, 0, array, i * maxDataBlockSize, array3.Length);
			}
			return array;
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x001AB590 File Offset: 0x001A9790
		public void Dispose()
		{
			if (this.m_provider != null)
			{
				this.m_provider.Clear();
				this.m_provider = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x001AB5B4 File Offset: 0x001A97B4
		public void Init(byte[] keyBlob)
		{
			RSAParameters parameters = default(RSAParameters);
			RsaKeyBlob rsaKeyBlob = new RsaKeyBlob(keyBlob);
			try
			{
				parameters = RsaCapiKey.CreateRsaParameters(rsaKeyBlob);
				this.m_provider.ImportParameters(parameters);
			}
			finally
			{
				RsaCapiKey.ClearRsaParameters(parameters);
				rsaKeyBlob.Dispose();
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x001AB604 File Offset: 0x001A9804
		// (set) Token: 0x060064BD RID: 25789 RVA: 0x001AB611 File Offset: 0x001A9811
		public bool PersistKeyInCryptoServiceProvider
		{
			get
			{
				return this.m_provider.PersistKeyInCsp;
			}
			set
			{
				this.m_provider.PersistKeyInCsp = value;
			}
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x001AB620 File Offset: 0x001A9820
		public byte[] SignDigestValue(byte[] digest, HashAlgorithmType hashAlgorithm)
		{
			if (this.m_provider == null)
			{
				throw new InvalidOperationException("NullArgumentPassed");
			}
			if (hashAlgorithm == null)
			{
				return this.m_provider.SignHash(digest, CryptoConfig.MapNameToOID("SHA1"));
			}
			return this.m_provider.SignHash(digest, CryptoConfig.MapNameToOID("SHA256"));
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x001AB670 File Offset: 0x001A9870
		private static void ClearRsaParameters(RSAParameters parameters)
		{
			ByteArrayUtilities.Clear(parameters.Modulus);
			ByteArrayUtilities.Clear(parameters.Exponent);
			ByteArrayUtilities.Clear(parameters.D);
			ByteArrayUtilities.Clear(parameters.DP);
			ByteArrayUtilities.Clear(parameters.DQ);
			ByteArrayUtilities.Clear(parameters.InverseQ);
			ByteArrayUtilities.Clear(parameters.P);
			ByteArrayUtilities.Clear(parameters.Q);
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x001AB6E0 File Offset: 0x001A98E0
		private static RSAParameters CreateRsaParameters(RsaKeyBlob keyBlob)
		{
			RSAParameters result = default(RSAParameters);
			result.Modulus = ByteArrayUtilities.CreateReversedArray(keyBlob.Modulus);
			result.Exponent = ByteArrayUtilities.CreateReversedArray(keyBlob.Exponent);
			if (keyBlob.IsPrivateKey)
			{
				result.D = ByteArrayUtilities.CreateReversedArray(keyBlob.D);
				result.DP = ByteArrayUtilities.CreateReversedArray(keyBlob.DP);
				result.DQ = ByteArrayUtilities.CreateReversedArray(keyBlob.DQ);
				result.InverseQ = ByteArrayUtilities.CreateReversedArray(keyBlob.InverseQ);
				result.P = ByteArrayUtilities.CreateReversedArray(keyBlob.P);
				result.Q = ByteArrayUtilities.CreateReversedArray(keyBlob.Q);
			}
			return result;
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x001AB790 File Offset: 0x001A9990
		private int getMaxDataBlockSize(int blockSize, bool fOaep)
		{
			if (blockSize < 11)
			{
				throw new CoreArgumentException("blockSize");
			}
			int result;
			if (!fOaep)
			{
				result = blockSize - 11;
			}
			else
			{
				result = (blockSize - 2) / 3;
			}
			return result;
		}

		// Token: 0x04003958 RID: 14680
		private RSACryptoServiceProvider m_provider;
	}
}
