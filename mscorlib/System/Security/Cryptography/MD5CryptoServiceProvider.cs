using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000271 RID: 625
	[ComVisible(true)]
	public sealed class MD5CryptoServiceProvider : MD5
	{
		// Token: 0x06002223 RID: 8739 RVA: 0x0007890D File Offset: 0x00076B0D
		[SecuritySafeCritical]
		public MD5CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00078948 File Offset: 0x00076B48
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00078971 File Offset: 0x00076B71
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000789A8 File Offset: 0x00076BA8
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000789B8 File Offset: 0x00076BB8
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000C64 RID: 3172
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
