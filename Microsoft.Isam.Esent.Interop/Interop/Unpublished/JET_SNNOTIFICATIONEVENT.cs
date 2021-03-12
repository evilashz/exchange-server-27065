using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000055 RID: 85
	public class JET_SNNOTIFICATIONEVENT : JET_RECOVERYCONTROL
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000B565 File Offset: 0x00009765
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000B56D File Offset: 0x0000976D
		public int EventID { get; private set; }

		// Token: 0x06000492 RID: 1170 RVA: 0x0000B578 File Offset: 0x00009778
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNNOTIFICATIONEVENT({0})", new object[]
			{
				this.EventID
			});
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000B5AA File Offset: 0x000097AA
		internal void SetFromNativeSnnotificationevent(ref NATIVE_SNNOTIFICATIONEVENT native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			this.EventID = native.EventID;
		}
	}
}
