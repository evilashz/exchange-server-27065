using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200004D RID: 77
	public class JET_SNCORRUPTEDPAGE
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000B275 File Offset: 0x00009475
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000B27D File Offset: 0x0000947D
		public string wszDatabase { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000B286 File Offset: 0x00009486
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000B28E File Offset: 0x0000948E
		public JET_DBID dbid { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000B297 File Offset: 0x00009497
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000B29F File Offset: 0x0000949F
		public JET_DBINFOMISC dbinfomisc { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000B2A8 File Offset: 0x000094A8
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public int pageNumber { get; internal set; }

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B2BC File Offset: 0x000094BC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNCORRUPTEDPAGE({0})", new object[]
			{
				this.pageNumber
			});
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B2F0 File Offset: 0x000094F0
		internal void SetFromNativeSncorruptedpage(ref NATIVE_SNCORRUPTEDPAGE native)
		{
			this.wszDatabase = Marshal.PtrToStringUni(native.wszDatabase);
			this.dbid = new JET_DBID
			{
				Value = native.dbid
			};
			this.dbinfomisc = new JET_DBINFOMISC();
			this.dbinfomisc.SetFromNativeDbinfoMisc(ref native.dbinfomisc);
			this.pageNumber = checked((int)native.pageNumber);
		}
	}
}
