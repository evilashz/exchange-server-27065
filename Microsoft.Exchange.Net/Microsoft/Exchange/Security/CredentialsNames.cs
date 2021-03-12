using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C95 RID: 3221
	internal struct CredentialsNames
	{
		// Token: 0x060046F3 RID: 18163 RVA: 0x000BED30 File Offset: 0x000BCF30
		internal unsafe CredentialsNames(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				using (SafeContextBuffer safeContextBuffer = new SafeContextBuffer(Marshal.ReadIntPtr(ptr2, 0)))
				{
					this.UserName = Marshal.PtrToStringUni(safeContextBuffer.DangerousGetHandle());
				}
			}
		}

		// Token: 0x04003C21 RID: 15393
		public readonly string UserName;

		// Token: 0x04003C22 RID: 15394
		public static readonly int Size = Marshal.SizeOf(typeof(IntPtr));
	}
}
