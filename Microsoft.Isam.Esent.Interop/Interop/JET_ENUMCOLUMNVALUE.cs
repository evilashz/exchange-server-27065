using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000286 RID: 646
	public class JET_ENUMCOLUMNVALUE
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000170DE File Offset: 0x000152DE
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x000170E6 File Offset: 0x000152E6
		public int itagSequence { get; internal set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000170EF File Offset: 0x000152EF
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x000170F7 File Offset: 0x000152F7
		public JET_wrn err { get; internal set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00017100 File Offset: 0x00015300
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00017108 File Offset: 0x00015308
		public int cbData { get; internal set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00017111 File Offset: 0x00015311
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x00017119 File Offset: 0x00015319
		public IntPtr pvData { get; internal set; }

		// Token: 0x06000B68 RID: 2920 RVA: 0x00017124 File Offset: 0x00015324
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_ENUMCOLUMNVALUE(itagSequence = {0}, cbData = {1})", new object[]
			{
				this.itagSequence,
				this.cbData
			});
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00017164 File Offset: 0x00015364
		internal void SetFromNativeEnumColumnValue(NATIVE_ENUMCOLUMNVALUE value)
		{
			checked
			{
				this.itagSequence = (int)value.itagSequence;
				this.err = (JET_wrn)value.err;
				this.cbData = (int)value.cbData;
				this.pvData = value.pvData;
			}
		}
	}
}
