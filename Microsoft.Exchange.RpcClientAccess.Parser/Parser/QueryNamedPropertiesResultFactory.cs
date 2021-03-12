using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010E RID: 270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryNamedPropertiesResultFactory : StandardResultFactory
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001040C File Offset: 0x0000E60C
		internal QueryNamedPropertiesResultFactory() : base(RopId.QueryNamedProperties)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00010416 File Offset: 0x0000E616
		public RopResult CreateSuccessfulResult(PropertyId[] propertyIds, NamedProperty[] namedProperties)
		{
			return new SuccessfulQueryNamedPropertiesResult(propertyIds, namedProperties);
		}
	}
}
