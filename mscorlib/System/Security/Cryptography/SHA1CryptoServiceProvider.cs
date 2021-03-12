using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000290 RID: 656
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		// Token: 0x06002346 RID: 9030 RVA: 0x0007FCD9 File Offset: 0x0007DED9
		[SecuritySafeCritical]
		public SHA1CryptoServiceProvider()
		{
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0007FCF6 File Offset: 0x0007DEF6
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0007FD1F File Offset: 0x0007DF1F
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0007FD56 File Offset: 0x0007DF56
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0007FD66 File Offset: 0x0007DF66
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000CDA RID: 3290
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
