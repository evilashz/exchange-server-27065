using System;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C52 RID: 3154
	public class Md5Hasher : IDisposable
	{
		// Token: 0x060045B4 RID: 17844 RVA: 0x000B9305 File Offset: 0x000B7505
		public Md5Hasher()
		{
			this.hasherImplementation = new MessageDigestForNonCryptographicPurposes();
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x000B9318 File Offset: 0x000B7518
		public int HashSize
		{
			get
			{
				return this.hasherImplementation.HashSize;
			}
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x000B9325 File Offset: 0x000B7525
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x000B9334 File Offset: 0x000B7534
		public void Dispose(bool disposing)
		{
			if (this.hasherImplementation != null)
			{
				this.hasherImplementation.Dispose();
				this.hasherImplementation = null;
			}
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x000B9350 File Offset: 0x000B7550
		public byte[] ComputeHash(byte[] buffer)
		{
			return this.hasherImplementation.ComputeHash(buffer);
		}

		// Token: 0x04003A5B RID: 14939
		private MessageDigestForNonCryptographicPurposes hasherImplementation;
	}
}
