using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C8E RID: 3214
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityBufferDescriptor
	{
		// Token: 0x060046E7 RID: 18151 RVA: 0x000BEA07 File Offset: 0x000BCC07
		public SecurityBufferDescriptor(int count)
		{
			this.Count = count;
		}

		// Token: 0x04003BF7 RID: 15351
		public readonly int Version;

		// Token: 0x04003BF8 RID: 15352
		public readonly int Count;

		// Token: 0x04003BF9 RID: 15353
		public unsafe void* UnmanagedPointer;
	}
}
