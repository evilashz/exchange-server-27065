using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000247 RID: 583
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x060020C6 RID: 8390 RVA: 0x000729E8 File Offset: 0x00070BE8
		public RNGCryptoServiceProvider() : this(null)
		{
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000729F1 File Offset: 0x00070BF1
		public RNGCryptoServiceProvider(string str) : this(null)
		{
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000729FA File Offset: 0x00070BFA
		public RNGCryptoServiceProvider(byte[] rgb) : this(null)
		{
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00072A03 File Offset: 0x00070C03
		[SecuritySafeCritical]
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			if (cspParams != null)
			{
				this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
				this.m_ownsHandle = true;
				return;
			}
			this.m_safeProvHandle = Utils.StaticProvHandle;
			this.m_ownsHandle = false;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00072A34 File Offset: 0x00070C34
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.m_ownsHandle)
			{
				this.m_safeProvHandle.Dispose();
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00072A53 File Offset: 0x00070C53
		[SecuritySafeCritical]
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetBytes(this.m_safeProvHandle, data, data.Length);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00072A72 File Offset: 0x00070C72
		[SecuritySafeCritical]
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetNonZeroBytes(this.m_safeProvHandle, data, data.Length);
		}

		// Token: 0x060020CD RID: 8397
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x060020CE RID: 8398
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetNonZeroBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x04000BE4 RID: 3044
		[SecurityCritical]
		private SafeProvHandle m_safeProvHandle;

		// Token: 0x04000BE5 RID: 3045
		private bool m_ownsHandle;
	}
}
