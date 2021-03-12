using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B46 RID: 2886
	internal sealed class SafeSecureHGlobalHandle : SafeHGlobalHandleBase
	{
		// Token: 0x06003E26 RID: 15910 RVA: 0x000A26F3 File Offset: 0x000A08F3
		private SafeSecureHGlobalHandle(IntPtr handle, int length) : base(handle)
		{
			this.length = length;
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000A2703 File Offset: 0x000A0903
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000A270C File Offset: 0x000A090C
		public static SafeSecureHGlobalHandle AllocHGlobal(int size)
		{
			SafeSecureHGlobalHandle safeSecureHGlobalHandle = new SafeSecureHGlobalHandle(Marshal.AllocHGlobal(size), size);
			safeSecureHGlobalHandle.ZeroMemory();
			return safeSecureHGlobalHandle;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000A2730 File Offset: 0x000A0930
		public static SafeSecureHGlobalHandle CopyToHGlobal(byte[] bytes)
		{
			bool flag = false;
			SafeSecureHGlobalHandle safeSecureHGlobalHandle = SafeSecureHGlobalHandle.AllocHGlobal(bytes.Length);
			SafeSecureHGlobalHandle result;
			try
			{
				Marshal.Copy(bytes, 0, safeSecureHGlobalHandle.DangerousGetHandle(), bytes.Length);
				flag = true;
				result = safeSecureHGlobalHandle;
			}
			finally
			{
				if (!flag)
				{
					safeSecureHGlobalHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000A277C File Offset: 0x000A097C
		public static SafeSecureHGlobalHandle DecryptAndAllocHGlobal(SecureString secureString)
		{
			int num = secureString.Length * 2;
			return new SafeSecureHGlobalHandle(Marshal.SecureStringToGlobalAllocUnicode(secureString), num);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000A279E File Offset: 0x000A099E
		public static SafeSecureHGlobalHandle Assign(IntPtr handle, int size)
		{
			return new SafeSecureHGlobalHandle(handle, size);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000A27A7 File Offset: 0x000A09A7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			this.ZeroMemory();
			return base.ReleaseHandle();
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000A27B5 File Offset: 0x000A09B5
		private void ZeroMemory()
		{
			if (!this.IsInvalid && this.length != 0)
			{
				NativeMethods.ZeroMemory(this.handle, (uint)this.length);
			}
		}

		// Token: 0x040035F1 RID: 13809
		private readonly int length;
	}
}
