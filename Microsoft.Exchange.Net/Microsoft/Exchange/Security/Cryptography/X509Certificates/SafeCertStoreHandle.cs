using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD5 RID: 2773
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003B91 RID: 15249 RVA: 0x000991C4 File Offset: 0x000973C4
		public SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000991CD File Offset: 0x000973CD
		public SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000991DD File Offset: 0x000973DD
		public static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				return new SafeCertStoreHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000991E9 File Offset: 0x000973E9
		public static SafeCertStoreHandle Clone(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return new SafeCertStoreHandle();
			}
			return CapiNativeMethods.CertDuplicateStore(handle);
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00099204 File Offset: 0x00097404
		public static SafeCertStoreHandle Clone(X509Store store)
		{
			if (store == null || store.StoreHandle == IntPtr.Zero)
			{
				return new SafeCertStoreHandle();
			}
			return CapiNativeMethods.CertDuplicateStore(store.StoreHandle);
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x0009922C File Offset: 0x0009742C
		protected override bool ReleaseHandle()
		{
			return CapiNativeMethods.CertCloseStore(this.handle, CapiNativeMethods.CertCloseStoreFlag.CertCloseStoreCheckFlag);
		}
	}
}
