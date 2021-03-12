using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000057 RID: 87
	public class JET_SNOPENCHECKPOINT : JET_RECOVERYCONTROL
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000B5CC File Offset: 0x000097CC
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public string wszCheckpoint { get; private set; }

		// Token: 0x06000497 RID: 1175 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNOPENCHECKPOINT({0})", new object[]
			{
				this.wszCheckpoint
			});
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000B60D File Offset: 0x0000980D
		internal void SetFromNativeSnopencheckpoint(ref NATIVE_SNOPENCHECKPOINT native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			this.wszCheckpoint = Marshal.PtrToStringUni(native.wszCheckpoint);
		}
	}
}
