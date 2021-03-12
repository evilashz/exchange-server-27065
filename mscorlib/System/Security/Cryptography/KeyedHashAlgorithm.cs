using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026D RID: 621
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		// Token: 0x06002203 RID: 8707 RVA: 0x0007835C File Offset: 0x0007655C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0007838B File Offset: 0x0007658B
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x0007839D File Offset: 0x0007659D
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000783C8 File Offset: 0x000765C8
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000783D4 File Offset: 0x000765D4
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04000C59 RID: 3161
		protected byte[] KeyValue;
	}
}
