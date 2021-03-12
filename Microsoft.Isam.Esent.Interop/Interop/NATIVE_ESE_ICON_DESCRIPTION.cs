using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200000C RID: 12
	internal struct NATIVE_ESE_ICON_DESCRIPTION
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000027A2 File Offset: 0x000009A2
		public void FreeNativeIconDescription()
		{
			Marshal.FreeHGlobal(this.pvData);
			this.pvData = IntPtr.Zero;
		}

		// Token: 0x04000046 RID: 70
		public uint ulSize;

		// Token: 0x04000047 RID: 71
		public IntPtr pvData;
	}
}
