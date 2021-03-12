using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	internal class ExistsFilter : SinglePropertyFilter
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00007E5B File Offset: 0x0000605B
		public ExistsFilter(PropertyDefinition property) : base(property)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007E64 File Offset: 0x00006064
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new ExistsFilter(property);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007E73 File Offset: 0x00006073
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(Exists(");
			sb.Append(base.Property.Name);
			sb.Append("))");
		}
	}
}
