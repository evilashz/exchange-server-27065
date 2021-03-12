using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000037 RID: 55
	internal sealed class BitMaskOrFilter : GenericBitMaskFilter
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000781C File Offset: 0x00005A1C
		public BitMaskOrFilter(PropertyDefinition property, ulong mask) : base(property, mask)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007826 File Offset: 0x00005A26
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new BitMaskOrFilter(property, base.Mask);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000783C File Offset: 0x00005A3C
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(BitwiseOr(");
			sb.Append(base.Property.Name);
			sb.Append(",");
			sb.Append(base.Mask);
			sb.Append("))");
		}
	}
}
