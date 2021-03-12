using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200004F RID: 79
	public class JET_SNDBATTACHED : JET_RECOVERYCONTROL
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B35B File Offset: 0x0000955B
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000B363 File Offset: 0x00009563
		public string wszDbPath { get; internal set; }

		// Token: 0x06000479 RID: 1145 RVA: 0x0000B36C File Offset: 0x0000956C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNDBATTACHED({0})", new object[]
			{
				this.wszDbPath
			});
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000B399 File Offset: 0x00009599
		internal void SetFromNativeSndbattached(ref NATIVE_SNDBATTACHED native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			this.wszDbPath = Marshal.PtrToStringUni(native.wszDbPath);
		}
	}
}
