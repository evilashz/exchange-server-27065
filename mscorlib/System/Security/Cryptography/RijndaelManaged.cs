using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000289 RID: 649
	[ComVisible(true)]
	public sealed class RijndaelManaged : Rijndael
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x0007DDF2 File Offset: 0x0007BFF2
		public RijndaelManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0007DE18 File Offset: 0x0007C018
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Encrypt);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x0007DE2F File Offset: 0x0007C02F
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Decrypt);
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0007DE46 File Offset: 0x0007C046
		public override void GenerateKey()
		{
			this.KeyValue = Utils.GenerateRandom(this.KeySizeValue / 8);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0007DE5B File Offset: 0x0007C05B
		public override void GenerateIV()
		{
			this.IVValue = Utils.GenerateRandom(this.BlockSizeValue / 8);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0007DE70 File Offset: 0x0007C070
		private ICryptoTransform NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, RijndaelManagedTransformMode encryptMode)
		{
			if (rgbKey == null)
			{
				rgbKey = Utils.GenerateRandom(this.KeySizeValue / 8);
			}
			if (rgbIV == null)
			{
				rgbIV = Utils.GenerateRandom(this.BlockSizeValue / 8);
			}
			return new RijndaelManagedTransform(rgbKey, mode, rgbIV, this.BlockSizeValue, feedbackSize, this.PaddingValue, encryptMode);
		}
	}
}
