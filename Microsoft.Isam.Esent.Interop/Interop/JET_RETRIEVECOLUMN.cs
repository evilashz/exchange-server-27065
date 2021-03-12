using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BD RID: 701
	public class JET_RETRIEVECOLUMN
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001955C File Offset: 0x0001775C
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00019564 File Offset: 0x00017764
		public JET_COLUMNID columnid { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0001956D File Offset: 0x0001776D
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00019575 File Offset: 0x00017775
		public byte[] pvData { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0001957E File Offset: 0x0001777E
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00019586 File Offset: 0x00017786
		public int ibData { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0001958F File Offset: 0x0001778F
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00019597 File Offset: 0x00017797
		public int cbData { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000195A0 File Offset: 0x000177A0
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x000195A8 File Offset: 0x000177A8
		public int cbActual { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000195B1 File Offset: 0x000177B1
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x000195B9 File Offset: 0x000177B9
		public RetrieveColumnGrbit grbit { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000195C2 File Offset: 0x000177C2
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x000195CA File Offset: 0x000177CA
		public int ibLongValue { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000195D3 File Offset: 0x000177D3
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x000195DB File Offset: 0x000177DB
		public int itagSequence { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000195E4 File Offset: 0x000177E4
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x000195EC File Offset: 0x000177EC
		public JET_COLUMNID columnidNextTagged { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x000195F5 File Offset: 0x000177F5
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x000195FD File Offset: 0x000177FD
		public JET_wrn err { get; private set; }

		// Token: 0x06000CAE RID: 3246 RVA: 0x00019608 File Offset: 0x00017808
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RETRIEVECOLUMN(0x{0:x})", new object[]
			{
				this.columnid
			});
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0001963C File Offset: 0x0001783C
		internal void CheckDataSize()
		{
			if (this.cbData < 0)
			{
				throw new ArgumentOutOfRangeException("cbData", this.cbData, "data length cannot be negative");
			}
			if (this.ibData < 0)
			{
				throw new ArgumentOutOfRangeException("ibData", this.cbData, "data offset cannot be negative");
			}
			if (this.ibData != 0 && (this.pvData == null || this.ibData >= this.pvData.Length))
			{
				throw new ArgumentOutOfRangeException("ibData", this.ibData, "cannot be greater than the length of the pvData buffer");
			}
			if ((this.pvData == null && this.cbData != 0) || (this.pvData != null && this.cbData > this.pvData.Length - this.ibData))
			{
				throw new ArgumentOutOfRangeException("cbData", this.cbData, "cannot be greater than the length of the pvData buffer");
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00019718 File Offset: 0x00017918
		internal void GetNativeRetrievecolumn(ref NATIVE_RETRIEVECOLUMN retrievecolumn)
		{
			retrievecolumn.columnid = this.columnid.Value;
			retrievecolumn.cbData = (uint)this.cbData;
			retrievecolumn.grbit = (uint)this.grbit;
			checked
			{
				retrievecolumn.ibLongValue = (uint)this.ibLongValue;
				retrievecolumn.itagSequence = (uint)this.itagSequence;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00019768 File Offset: 0x00017968
		internal void UpdateFromNativeRetrievecolumn(ref NATIVE_RETRIEVECOLUMN native)
		{
			checked
			{
				this.cbActual = (int)native.cbActual;
				this.columnidNextTagged = new JET_COLUMNID
				{
					Value = native.columnidNextTagged
				};
				this.itagSequence = (int)native.itagSequence;
				this.err = (JET_wrn)native.err;
			}
		}
	}
}
