using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExcludingPropertyFilter : IPropertyFilter
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x000113B4 File Offset: 0x0000F5B4
		internal ExcludingPropertyFilter(PropertyTag[] propertiesToExclude)
		{
			this.propertiesToExclude = propertiesToExclude;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000113C3 File Offset: 0x0000F5C3
		internal ExcludingPropertyFilter(ICollection<PropertyTag> propertiesToExclude)
		{
			this.propertiesToExclude = propertiesToExclude;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000113D2 File Offset: 0x0000F5D2
		public bool IncludeProperty(PropertyTag propertyTag)
		{
			return !this.propertiesToExclude.Contains(propertyTag);
		}

		// Token: 0x04000335 RID: 821
		private readonly ICollection<PropertyTag> propertiesToExclude;
	}
}
