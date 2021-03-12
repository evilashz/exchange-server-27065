using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	internal class BitMaskFilter : GenericBitMaskFilter
	{
		// Token: 0x060001BD RID: 445 RVA: 0x000076B6 File Offset: 0x000058B6
		public BitMaskFilter(PropertyDefinition property, ulong mask, bool isNonZero) : base(property, mask)
		{
			this.isNonZero = isNonZero;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000076C7 File Offset: 0x000058C7
		public bool IsNonZero
		{
			get
			{
				return this.isNonZero;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000076CF File Offset: 0x000058CF
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new BitMaskFilter(property, base.Mask, this.isNonZero);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000076EC File Offset: 0x000058EC
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(BitMask(");
			sb.Append(base.Property.Name);
			sb.Append(")=");
			sb.Append(base.Mask);
			sb.Append(",");
			sb.Append(this.isNonZero ? "NonZero" : "Zero");
			sb.Append(")");
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007764 File Offset: 0x00005964
		public override bool Equals(object obj)
		{
			BitMaskFilter bitMaskFilter = obj as BitMaskFilter;
			return bitMaskFilter != null && this.isNonZero == bitMaskFilter.isNonZero && base.Equals(obj);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007792 File Offset: 0x00005992
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (this.isNonZero ? int.MaxValue : 0);
		}

		// Token: 0x04000096 RID: 150
		private readonly bool isNonZero;
	}
}
