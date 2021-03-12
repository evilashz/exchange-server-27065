using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000015 RID: 21
	internal sealed class SafeMsiHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000033D8 File Offset: 0x000015D8
		internal SafeMsiHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000033E8 File Offset: 0x000015E8
		private SafeMsiHandle() : base(true)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000033F1 File Offset: 0x000015F1
		protected override bool ReleaseHandle()
		{
			return SafeMsiHandle.CloseHandle(this.handle) == 0U;
		}

		// Token: 0x0600005D RID: 93
		[DllImport("msi", EntryPoint = "MsiCloseHandle")]
		private static extern uint CloseHandle(IntPtr any);
	}
}
