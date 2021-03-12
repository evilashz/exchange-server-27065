using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000288 RID: 648
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		// Token: 0x06002313 RID: 8979 RVA: 0x0007DD59 File Offset: 0x0007BF59
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x0007DD99 File Offset: 0x0007BF99
		public new static Rijndael Create()
		{
			return Rijndael.Create("System.Security.Cryptography.Rijndael");
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0007DDA5 File Offset: 0x0007BFA5
		public new static Rijndael Create(string algName)
		{
			return (Rijndael)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04000CBD RID: 3261
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x04000CBE RID: 3262
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
