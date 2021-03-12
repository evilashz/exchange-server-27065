using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000196 RID: 406
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IncludeAllPropertyFilter : IPropertyFilter
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0001BD96 File Offset: 0x00019F96
		private IncludeAllPropertyFilter()
		{
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001BD9E File Offset: 0x00019F9E
		public bool IncludeProperty(PropertyTag propertyTag)
		{
			return true;
		}

		// Token: 0x040003DC RID: 988
		public static readonly IPropertyFilter Instance = new IncludeAllPropertyFilter();
	}
}
