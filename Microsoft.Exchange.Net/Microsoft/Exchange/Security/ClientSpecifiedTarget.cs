using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C87 RID: 3207
	internal class ClientSpecifiedTarget
	{
		// Token: 0x060046DC RID: 18140 RVA: 0x000BE720 File Offset: 0x000BC920
		internal unsafe ClientSpecifiedTarget(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				using (SafeContextBuffer safeContextBuffer = new SafeContextBuffer(Marshal.ReadIntPtr(ptr2, 0)))
				{
					this.target = Marshal.PtrToStringUni(safeContextBuffer.DangerousGetHandle());
				}
			}
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x000BE790 File Offset: 0x000BC990
		public override string ToString()
		{
			return this.target;
		}

		// Token: 0x04003BC0 RID: 15296
		private readonly string target;
	}
}
