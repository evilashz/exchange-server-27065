using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C93 RID: 3219
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct SspiHandle
	{
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x000BEC4A File Offset: 0x000BCE4A
		public bool IsZero
		{
			get
			{
				return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
			}
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000BEC70 File Offset: 0x000BCE70
		public override string ToString()
		{
			return this.HandleHi.ToString("x") + ":" + this.HandleLo.ToString("x");
		}

		// Token: 0x04003C19 RID: 15385
		private IntPtr HandleHi;

		// Token: 0x04003C1A RID: 15386
		private IntPtr HandleLo;
	}
}
