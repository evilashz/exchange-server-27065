using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000051 RID: 81
	public class JET_SNDBDETACHED : JET_RECOVERYCONTROL
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000B3C0 File Offset: 0x000095C0
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public string wszDbPath { get; internal set; }

		// Token: 0x0600047E RID: 1150 RVA: 0x0000B3D4 File Offset: 0x000095D4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNDBDETACHED({0})", new object[]
			{
				this.wszDbPath
			});
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000B401 File Offset: 0x00009601
		internal void SetFromNativeSndbdetached(ref NATIVE_SNDBDETACHED native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			this.wszDbPath = Marshal.PtrToStringUni(native.wszDbPath);
		}
	}
}
