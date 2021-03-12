using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C4 RID: 708
	public class JET_SETCOLUMN : IContentEquatable<JET_SETCOLUMN>, IDeepCloneable<JET_SETCOLUMN>
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00019AE6 File Offset: 0x00017CE6
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00019AEE File Offset: 0x00017CEE
		public JET_COLUMNID columnid { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00019AF7 File Offset: 0x00017CF7
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00019AFF File Offset: 0x00017CFF
		public byte[] pvData { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00019B08 File Offset: 0x00017D08
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00019B10 File Offset: 0x00017D10
		public int ibData { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00019B19 File Offset: 0x00017D19
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00019B21 File Offset: 0x00017D21
		public int cbData { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00019B2A File Offset: 0x00017D2A
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00019B32 File Offset: 0x00017D32
		public SetColumnGrbit grbit { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00019B3B File Offset: 0x00017D3B
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00019B43 File Offset: 0x00017D43
		public int ibLongValue { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00019B4C File Offset: 0x00017D4C
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00019B54 File Offset: 0x00017D54
		public int itagSequence { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00019B5D File Offset: 0x00017D5D
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00019B65 File Offset: 0x00017D65
		public JET_wrn err { get; internal set; }

		// Token: 0x06000CDE RID: 3294 RVA: 0x00019B70 File Offset: 0x00017D70
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SETCOLUMN(0x{0:x},{1},ibLongValue={2},itagSequence={3})", new object[]
			{
				this.columnid.Value,
				Util.DumpBytes(this.pvData, this.ibData, this.cbData),
				this.ibLongValue,
				this.itagSequence
			});
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public bool ContentEquals(JET_SETCOLUMN other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckDataSize();
			other.CheckDataSize();
			return this.columnid == other.columnid && this.ibData == other.ibData && this.cbData == other.cbData && this.grbit == other.grbit && this.ibLongValue == other.ibLongValue && this.itagSequence == other.itagSequence && this.err == other.err && Util.ArrayEqual(this.pvData, other.pvData, this.ibData, this.cbData);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00019C84 File Offset: 0x00017E84
		public JET_SETCOLUMN DeepClone()
		{
			JET_SETCOLUMN jet_SETCOLUMN = (JET_SETCOLUMN)base.MemberwiseClone();
			if (this.pvData != null)
			{
				jet_SETCOLUMN.pvData = new byte[this.pvData.Length];
				Array.Copy(this.pvData, jet_SETCOLUMN.pvData, this.cbData);
			}
			return jet_SETCOLUMN;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00019CD0 File Offset: 0x00017ED0
		internal void CheckDataSize()
		{
			if (this.cbData < 0)
			{
				throw new ArgumentOutOfRangeException("cbData", "data length cannot be negative");
			}
			if (this.ibData < 0)
			{
				throw new ArgumentOutOfRangeException("ibData", "data offset cannot be negative");
			}
			if (this.ibData != 0 && (this.pvData == null || this.ibData >= this.pvData.Length))
			{
				throw new ArgumentOutOfRangeException("ibData", this.ibData, "cannot be greater than the length of the pvData");
			}
			if ((this.pvData == null && this.cbData != 0) || (this.pvData != null && this.cbData > this.pvData.Length - this.ibData))
			{
				throw new ArgumentOutOfRangeException("cbData", this.cbData, "cannot be greater than the length of the pvData");
			}
			if (this.itagSequence < 0)
			{
				throw new ArgumentOutOfRangeException("itagSequence", this.itagSequence, "cannot be negative");
			}
			if (this.ibLongValue < 0)
			{
				throw new ArgumentOutOfRangeException("ibLongValue", this.ibLongValue, "cannot be negative");
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00019DDC File Offset: 0x00017FDC
		internal NATIVE_SETCOLUMN GetNativeSetcolumn()
		{
			return checked(new NATIVE_SETCOLUMN
			{
				columnid = this.columnid.Value,
				cbData = (uint)this.cbData,
				grbit = (uint)this.grbit,
				ibLongValue = (uint)this.ibLongValue,
				itagSequence = (uint)this.itagSequence
			});
		}
	}
}
