using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200014E RID: 334
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AndPropertyFilter : IPropertyFilter
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00011296 File Offset: 0x0000F496
		internal AndPropertyFilter(IEnumerable<IPropertyFilter> propertyFilters)
		{
			Util.ThrowOnNullArgument(propertyFilters, "propertyFilters");
			this.propertyFilters = propertyFilters;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public bool IncludeProperty(PropertyTag propertyTag)
		{
			foreach (IPropertyFilter propertyFilter in this.propertyFilters)
			{
				if (!propertyFilter.IncludeProperty(propertyTag))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000330 RID: 816
		private readonly IEnumerable<IPropertyFilter> propertyFilters;
	}
}
