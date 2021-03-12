using System;
using System.Globalization;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000292 RID: 658
	public class JET_INDEX_COLUMN
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00017400 File Offset: 0x00015600
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00017408 File Offset: 0x00015608
		public JET_COLUMNID columnid { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00017411 File Offset: 0x00015611
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x00017419 File Offset: 0x00015619
		public JetRelop relop { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00017422 File Offset: 0x00015622
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0001742A File Offset: 0x0001562A
		public byte[] pvData { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00017433 File Offset: 0x00015633
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0001743B File Offset: 0x0001563B
		public JetIndexColumnGrbit grbit { get; set; }

		// Token: 0x06000B83 RID: 2947 RVA: 0x00017444 File Offset: 0x00015644
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEX_COLUMN(0x{0:x})", new object[]
			{
				this.columnid
			});
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00017478 File Offset: 0x00015678
		internal NATIVE_INDEX_COLUMN GetNativeIndexColumn(ref GCHandleCollection handles)
		{
			NATIVE_INDEX_COLUMN result = default(NATIVE_INDEX_COLUMN);
			result.columnid = this.columnid.Value;
			result.relop = (uint)this.relop;
			result.grbit = (uint)this.grbit;
			if (this.pvData != null)
			{
				result.pvData = handles.Add(this.pvData);
				result.cbData = (uint)this.pvData.Length;
			}
			return result;
		}
	}
}
