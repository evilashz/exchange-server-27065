using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007C RID: 124
	internal sealed class SafeHGlobalHandle : SafeHGlobalHandleBase
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0001184E File Offset: 0x0000FA4E
		internal SafeHGlobalHandle(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001185D File Offset: 0x0000FA5D
		private SafeHGlobalHandle()
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00011865 File Offset: 0x0000FA65
		private SafeHGlobalHandle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001186F File Offset: 0x0000FA6F
		public static SafeHGlobalHandle InvalidHandle
		{
			get
			{
				return new SafeHGlobalHandle(IntPtr.Zero, false);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001187C File Offset: 0x0000FA7C
		public static SafeHGlobalHandle AllocHGlobal(int size)
		{
			return new SafeHGlobalHandle(Marshal.AllocHGlobal(size));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001188C File Offset: 0x0000FA8C
		public static SafeHGlobalHandle CopyToHGlobal(byte[] bytes)
		{
			bool flag = false;
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(bytes.Length);
			SafeHGlobalHandle result;
			try
			{
				Marshal.Copy(bytes, 0, safeHGlobalHandle.DangerousGetHandle(), bytes.Length);
				flag = true;
				result = safeHGlobalHandle;
			}
			finally
			{
				if (!flag)
				{
					safeHGlobalHandle.Dispose();
				}
			}
			return result;
		}
	}
}
