using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal class OrFilter : CompositeFilter
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00006EFB File Offset: 0x000050FB
		public OrFilter(params QueryFilter[] filters) : this(false, filters)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006F05 File Offset: 0x00005105
		public OrFilter(bool ignoreWhenVerifyingMaxDepth, params QueryFilter[] filters) : base(ignoreWhenVerifyingMaxDepth, filters)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006F0F File Offset: 0x0000510F
		public OrFilter(bool ignoreWhenVerifyingMaxDepth, bool isAtomic, params QueryFilter[] filters) : base(ignoreWhenVerifyingMaxDepth, filters)
		{
			base.IsAtomic = isAtomic;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006F20 File Offset: 0x00005120
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(|(");
			foreach (QueryFilter queryFilter in base.Filters)
			{
				queryFilter.ToString(sb);
			}
			sb.Append("))");
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006F8C File Offset: 0x0000518C
		public override QueryFilter CloneWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			return new OrFilter(true, base.CloneFiltersWithPropertyReplacement(propertyMap));
		}
	}
}
