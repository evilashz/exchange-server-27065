using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal class PropertyComparisonFilter : QueryFilter
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000824F File Offset: 0x0000644F
		public PropertyComparisonFilter(ComparisonOperator comparisonOperator, PropertyDefinition property1, PropertyDefinition property2)
		{
			if (property1 == null)
			{
				throw new ArgumentNullException("property1");
			}
			if (property2 == null)
			{
				throw new ArgumentNullException("property2");
			}
			this.property1 = property1;
			this.property2 = property2;
			this.comparisonOperator = comparisonOperator;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008288 File Offset: 0x00006488
		public PropertyDefinition Property1
		{
			get
			{
				return this.property1;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008290 File Offset: 0x00006490
		public PropertyDefinition Property2
		{
			get
			{
				return this.property2;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008298 File Offset: 0x00006498
		public ComparisonOperator ComparisonOperator
		{
			get
			{
				return this.comparisonOperator;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000082A0 File Offset: 0x000064A0
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.property1.Name);
			sb.Append(" ");
			sb.Append(this.comparisonOperator.ToString());
			sb.Append(" ");
			sb.Append(this.property2.Name);
			sb.Append(")");
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008318 File Offset: 0x00006518
		public override bool Equals(object obj)
		{
			PropertyComparisonFilter propertyComparisonFilter = obj as PropertyComparisonFilter;
			return propertyComparisonFilter != null && propertyComparisonFilter.GetType() == base.GetType() && this.comparisonOperator == propertyComparisonFilter.comparisonOperator && this.property1.Equals(propertyComparisonFilter.property1) && this.property2.Equals(propertyComparisonFilter.property2);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008378 File Offset: 0x00006578
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(base.GetType().GetHashCode() ^ this.comparisonOperator.GetHashCode() ^ this.property1.GetHashCode() ^ this.property2.GetHashCode());
			}
			return this.hashCode.Value;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000083DC File Offset: 0x000065DC
		internal override IEnumerable<PropertyDefinition> FilterProperties()
		{
			return new List<PropertyDefinition>(2)
			{
				this.Property1,
				this.Property2
			};
		}

		// Token: 0x040000A7 RID: 167
		private int? hashCode;

		// Token: 0x040000A8 RID: 168
		private readonly PropertyDefinition property1;

		// Token: 0x040000A9 RID: 169
		private readonly PropertyDefinition property2;

		// Token: 0x040000AA RID: 170
		private readonly ComparisonOperator comparisonOperator;
	}
}
