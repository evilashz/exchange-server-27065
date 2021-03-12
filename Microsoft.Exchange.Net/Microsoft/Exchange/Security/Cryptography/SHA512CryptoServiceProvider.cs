using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000AE4 RID: 2788
	[ComVisible(false)]
	internal sealed class SHA512CryptoServiceProvider : SHA512
	{
		// Token: 0x06003BDD RID: 15325 RVA: 0x0009A30D File Offset: 0x0009850D
		public SHA512CryptoServiceProvider()
		{
			this.handle = SafeHashHandle.Create(HashUtilities.StaticAESHandle, CapiNativeMethods.AlgorithmId.Sha512);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x0009A32A File Offset: 0x0009852A
		public override void Initialize()
		{
			if (this.handle != null && !this.handle.IsClosed)
			{
				this.handle.Dispose();
			}
			this.handle = SafeHashHandle.Create(HashUtilities.StaticAESHandle, CapiNativeMethods.AlgorithmId.Sha512);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x0009A361 File Offset: 0x00098561
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.handle.HashData(array, ibStart, cbSize);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x0009A371 File Offset: 0x00098571
		protected override byte[] HashFinal()
		{
			return this.handle.HashFinal();
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x0009A37E File Offset: 0x0009857E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.handle != null && !this.handle.IsClosed)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x040034B1 RID: 13489
		private SafeHashHandle handle;
	}
}
