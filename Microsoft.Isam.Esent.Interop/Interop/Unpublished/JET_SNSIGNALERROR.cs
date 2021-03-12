using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200005D RID: 93
	public class JET_SNSIGNALERROR : JET_RECOVERYCONTROL
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000B949 File Offset: 0x00009B49
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNSIGNALERROR()", new object[0]);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000B960 File Offset: 0x00009B60
		internal void SetFromNativeSnsignalerror(ref NATIVE_SNSIGNALERROR native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
		}
	}
}
