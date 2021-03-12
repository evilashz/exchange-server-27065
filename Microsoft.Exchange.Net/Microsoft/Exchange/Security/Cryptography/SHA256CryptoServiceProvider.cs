using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000AE3 RID: 2787
	[ComVisible(false)]
	internal sealed class SHA256CryptoServiceProvider : SHA256
	{
		// Token: 0x06003BD8 RID: 15320 RVA: 0x0009A270 File Offset: 0x00098470
		public SHA256CryptoServiceProvider()
		{
			this.handle = SafeHashHandle.Create(HashUtilities.StaticAESHandle, CapiNativeMethods.AlgorithmId.Sha256);
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x0009A28D File Offset: 0x0009848D
		public override void Initialize()
		{
			if (this.handle != null && !this.handle.IsClosed)
			{
				this.handle.Dispose();
			}
			this.handle = SafeHashHandle.Create(HashUtilities.StaticAESHandle, CapiNativeMethods.AlgorithmId.Sha256);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x0009A2C4 File Offset: 0x000984C4
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.handle.HashData(array, ibStart, cbSize);
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x0009A2D4 File Offset: 0x000984D4
		protected override byte[] HashFinal()
		{
			return this.handle.HashFinal();
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x0009A2E1 File Offset: 0x000984E1
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.handle != null && !this.handle.IsClosed)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x040034B0 RID: 13488
		private SafeHashHandle handle;
	}
}
