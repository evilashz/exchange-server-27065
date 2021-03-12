using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000094 RID: 148
	public abstract class ColumnValueOfStruct<T> : ColumnValue where T : struct, IEquatable<T>
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00010193 File Offset: 0x0000E393
		public override object ValueAsObject
		{
			get
			{
				return BoxedValueCache<T>.GetBoxedValue(this.Value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000101A0 File Offset: 0x0000E3A0
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000101A8 File Offset: 0x0000E3A8
		public T? Value { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public override int Length
		{
			get
			{
				if (this.Value == null)
				{
					return 0;
				}
				return this.Size;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000101DC File Offset: 0x0000E3DC
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000101FD File Offset: 0x0000E3FD
		protected void CheckDataCount(int count)
		{
			if (this.Size != count)
			{
				throw new EsentInvalidColumnException();
			}
		}
	}
}
