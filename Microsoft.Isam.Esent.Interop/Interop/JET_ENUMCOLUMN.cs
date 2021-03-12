using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000282 RID: 642
	public class JET_ENUMCOLUMN
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00016EAD File Offset: 0x000150AD
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00016EB5 File Offset: 0x000150B5
		public JET_COLUMNID columnid { get; internal set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00016EBE File Offset: 0x000150BE
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00016EC6 File Offset: 0x000150C6
		public JET_wrn err { get; internal set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00016ECF File Offset: 0x000150CF
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x00016ED7 File Offset: 0x000150D7
		public int cEnumColumnValue { get; internal set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00016EE0 File Offset: 0x000150E0
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00016EE8 File Offset: 0x000150E8
		public JET_ENUMCOLUMNVALUE[] rgEnumColumnValue { get; internal set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00016EF1 File Offset: 0x000150F1
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00016EF9 File Offset: 0x000150F9
		public int cbData { get; internal set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00016F02 File Offset: 0x00015102
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00016F0A File Offset: 0x0001510A
		public IntPtr pvData { get; internal set; }

		// Token: 0x06000B53 RID: 2899 RVA: 0x00016F14 File Offset: 0x00015114
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_ENUMCOLUMN(0x{0:x})", new object[]
			{
				this.columnid
			});
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00016F48 File Offset: 0x00015148
		internal void SetFromNativeEnumColumn(NATIVE_ENUMCOLUMN value)
		{
			this.columnid = new JET_COLUMNID
			{
				Value = value.columnid
			};
			this.err = (JET_wrn)value.err;
			checked
			{
				if (JET_wrn.ColumnSingleValue == this.err)
				{
					this.cbData = (int)value.cbData;
					this.pvData = value.pvData;
					return;
				}
				this.cEnumColumnValue = (int)value.cEnumColumnValue;
				this.rgEnumColumnValue = null;
			}
		}
	}
}
