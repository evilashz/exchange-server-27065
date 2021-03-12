using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD7 RID: 2775
	internal class SafeChainEngineHandle : SafeHandleMinusOneIsInvalid
	{
		// Token: 0x06003B9A RID: 15258 RVA: 0x00099261 File Offset: 0x00097461
		internal SafeChainEngineHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00099271 File Offset: 0x00097471
		private SafeChainEngineHandle() : base(true)
		{
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x0009927A File Offset: 0x0009747A
		public static SafeChainEngineHandle DefaultEngine
		{
			get
			{
				return new SafeChainEngineHandle(IntPtr.Zero);
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x00099286 File Offset: 0x00097486
		public bool IsDefaultEngine
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00099298 File Offset: 0x00097498
		public static SafeChainEngineHandle Create(ChainEnginePool.ChainEngineConfig configuration)
		{
			SafeChainEngineHandle result;
			if (!CapiNativeMethods.CertCreateCertificateChainEngine(ref configuration, out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000992BC File Offset: 0x000974BC
		protected override bool ReleaseHandle()
		{
			if (!this.IsDefaultEngine)
			{
				CapiNativeMethods.CertFreeCertificateChainEngine(this.handle);
			}
			return true;
		}
	}
}
