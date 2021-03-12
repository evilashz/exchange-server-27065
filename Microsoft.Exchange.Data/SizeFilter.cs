using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal class SizeFilter : QueryFilter
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00007FAF File Offset: 0x000061AF
		public SizeFilter(ComparisonOperator comparisonOperator, PropertyDefinition property, uint propertySize)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.comparisonOperator = comparisonOperator;
			this.property = property;
			this.propertySize = propertySize;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007FDC File Offset: 0x000061DC
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.property.ToString());
			sb.Append(" ");
			sb.Append(this.comparisonOperator.ToString());
			sb.Append(" ");
			sb.Append(this.propertySize);
			sb.Append(")");
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000804F File Offset: 0x0000624F
		public ComparisonOperator ComparisonOperator
		{
			get
			{
				return this.comparisonOperator;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008057 File Offset: 0x00006257
		public PropertyDefinition Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000805F File Offset: 0x0000625F
		public uint PropertySize
		{
			get
			{
				return this.propertySize;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008068 File Offset: 0x00006268
		internal override IEnumerable<PropertyDefinition> FilterProperties()
		{
			return new List<PropertyDefinition>(1)
			{
				this.Property
			};
		}

		// Token: 0x0400009F RID: 159
		private readonly ComparisonOperator comparisonOperator;

		// Token: 0x040000A0 RID: 160
		private readonly PropertyDefinition property;

		// Token: 0x040000A1 RID: 161
		private readonly uint propertySize;
	}
}
