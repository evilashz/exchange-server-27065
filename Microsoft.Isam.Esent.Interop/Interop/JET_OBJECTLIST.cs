using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002A5 RID: 677
	public class JET_OBJECTLIST
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000185BC File Offset: 0x000167BC
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x000185C4 File Offset: 0x000167C4
		public JET_TABLEID tableid { get; internal set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000185CD File Offset: 0x000167CD
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x000185D5 File Offset: 0x000167D5
		public int cRecord { get; internal set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000185DE File Offset: 0x000167DE
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x000185E6 File Offset: 0x000167E6
		public JET_COLUMNID columnidobjectname { get; internal set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x000185EF File Offset: 0x000167EF
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x000185F7 File Offset: 0x000167F7
		public JET_COLUMNID columnidobjtyp { get; internal set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00018600 File Offset: 0x00016800
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00018608 File Offset: 0x00016808
		public JET_COLUMNID columnidgrbit { get; internal set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00018611 File Offset: 0x00016811
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00018619 File Offset: 0x00016819
		public JET_COLUMNID columnidflags { get; internal set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00018622 File Offset: 0x00016822
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0001862A File Offset: 0x0001682A
		public JET_COLUMNID columnidcRecord { get; internal set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00018633 File Offset: 0x00016833
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0001863B File Offset: 0x0001683B
		public JET_COLUMNID columnidcontainername { get; internal set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00018644 File Offset: 0x00016844
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0001864C File Offset: 0x0001684C
		public JET_COLUMNID columnidcPage { get; internal set; }

		// Token: 0x06000C21 RID: 3105 RVA: 0x00018658 File Offset: 0x00016858
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_OBJECTLIST(0x{0:x},{1} records)", new object[]
			{
				this.tableid,
				this.cRecord
			});
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00018698 File Offset: 0x00016898
		internal void SetFromNativeObjectlist(NATIVE_OBJECTLIST value)
		{
			this.tableid = new JET_TABLEID
			{
				Value = value.tableid
			};
			this.cRecord = checked((int)value.cRecord);
			this.columnidobjectname = new JET_COLUMNID
			{
				Value = value.columnidobjectname
			};
			this.columnidobjtyp = new JET_COLUMNID
			{
				Value = value.columnidobjtyp
			};
			this.columnidgrbit = new JET_COLUMNID
			{
				Value = value.columnidgrbit
			};
			this.columnidflags = new JET_COLUMNID
			{
				Value = value.columnidflags
			};
			this.columnidcRecord = new JET_COLUMNID
			{
				Value = value.columnidcRecord
			};
			this.columnidcPage = new JET_COLUMNID
			{
				Value = value.columnidcPage
			};
			this.columnidcontainername = new JET_COLUMNID
			{
				Value = value.columnidcontainername
			};
		}
	}
}
