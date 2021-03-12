using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000036 RID: 54
	internal sealed class BitMaskAndFilter : GenericBitMaskFilter
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x000077AB File Offset: 0x000059AB
		public BitMaskAndFilter(PropertyDefinition property, ulong mask) : base(property, mask)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000077B5 File Offset: 0x000059B5
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new BitMaskAndFilter(property, base.Mask);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000077CC File Offset: 0x000059CC
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(BitwiseAnd(");
			sb.Append(base.Property.Name);
			sb.Append(",");
			sb.Append(base.Mask);
			sb.Append("))");
		}
	}
}
