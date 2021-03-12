using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000284 RID: 644
	public class JET_ENUMCOLUMNID
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00016FC5 File Offset: 0x000151C5
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00016FCD File Offset: 0x000151CD
		public JET_COLUMNID columnid { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00016FD6 File Offset: 0x000151D6
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00016FDE File Offset: 0x000151DE
		public int ctagSequence { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00016FE7 File Offset: 0x000151E7
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00016FEF File Offset: 0x000151EF
		public int[] rgtagSequence { get; set; }

		// Token: 0x06000B5C RID: 2908 RVA: 0x00016FF8 File Offset: 0x000151F8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_ENUMCOLUMNID(0x{0:x})", new object[]
			{
				this.columnid
			});
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001702C File Offset: 0x0001522C
		internal void CheckDataSize()
		{
			if (this.ctagSequence < 0)
			{
				throw new ArgumentOutOfRangeException("ctagSequence", "ctagSequence cannot be negative");
			}
			if ((this.rgtagSequence == null && this.ctagSequence != 0) || (this.rgtagSequence != null && this.ctagSequence > this.rgtagSequence.Length))
			{
				throw new ArgumentOutOfRangeException("ctagSequence", this.ctagSequence, "cannot be greater than the length of the pvData");
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00017098 File Offset: 0x00015298
		internal NATIVE_ENUMCOLUMNID GetNativeEnumColumnid()
		{
			this.CheckDataSize();
			return new NATIVE_ENUMCOLUMNID
			{
				columnid = this.columnid.Value,
				ctagSequence = checked((uint)this.ctagSequence)
			};
		}
	}
}
