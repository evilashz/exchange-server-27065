using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002B4 RID: 692
	public class JET_RECORDLIST
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00018B38 File Offset: 0x00016D38
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00018B40 File Offset: 0x00016D40
		public JET_TABLEID tableid { get; internal set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00018B49 File Offset: 0x00016D49
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00018B51 File Offset: 0x00016D51
		public int cRecords { get; internal set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00018B5A File Offset: 0x00016D5A
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00018B62 File Offset: 0x00016D62
		public JET_COLUMNID columnidBookmark { get; internal set; }

		// Token: 0x06000C5C RID: 3164 RVA: 0x00018B6C File Offset: 0x00016D6C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RECORDLIST(0x{0:x},{1} records)", new object[]
			{
				this.tableid,
				this.cRecords
			});
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00018BAC File Offset: 0x00016DAC
		internal void SetFromNativeRecordlist(NATIVE_RECORDLIST value)
		{
			this.tableid = new JET_TABLEID
			{
				Value = value.tableid
			};
			this.cRecords = checked((int)value.cRecords);
			this.columnidBookmark = new JET_COLUMNID
			{
				Value = value.columnidBookmark
			};
		}
	}
}
