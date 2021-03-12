using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029D RID: 669
	public class JET_INDEXRANGE : IContentEquatable<JET_INDEXRANGE>, IDeepCloneable<JET_INDEXRANGE>
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x00017F60 File Offset: 0x00016160
		public JET_INDEXRANGE()
		{
			this.grbit = IndexRangeGrbit.RecordInIndex;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00017F6F File Offset: 0x0001616F
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00017F77 File Offset: 0x00016177
		public JET_TABLEID tableid { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00017F80 File Offset: 0x00016180
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00017F88 File Offset: 0x00016188
		public IndexRangeGrbit grbit { get; set; }

		// Token: 0x06000BDD RID: 3037 RVA: 0x00017F91 File Offset: 0x00016191
		public JET_INDEXRANGE DeepClone()
		{
			return (JET_INDEXRANGE)base.MemberwiseClone();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00017FA0 File Offset: 0x000161A0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEXRANGE(0x{0:x},{1})", new object[]
			{
				this.tableid.Value,
				this.grbit
			});
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00017FE5 File Offset: 0x000161E5
		public bool ContentEquals(JET_INDEXRANGE other)
		{
			return other != null && this.tableid == other.tableid && this.grbit == other.grbit;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00018010 File Offset: 0x00016210
		internal NATIVE_INDEXRANGE GetNativeIndexRange()
		{
			return new NATIVE_INDEXRANGE
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_INDEXRANGE)),
				tableid = this.tableid.Value,
				grbit = (uint)this.grbit
			};
		}
	}
}
