using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	internal class AndFilter : CompositeFilter
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00006E6A File Offset: 0x0000506A
		public AndFilter(params QueryFilter[] filters) : this(false, filters)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006E74 File Offset: 0x00005074
		public AndFilter(bool ignoreWhenVerifyingMaxDepth, params QueryFilter[] filters) : base(ignoreWhenVerifyingMaxDepth, filters)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006E80 File Offset: 0x00005080
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(&(");
			foreach (QueryFilter queryFilter in base.Filters)
			{
				queryFilter.ToString(sb);
			}
			sb.Append("))");
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006EEC File Offset: 0x000050EC
		public override QueryFilter CloneWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			return new AndFilter(true, base.CloneFiltersWithPropertyReplacement(propertyMap));
		}
	}
}
