using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct AnnotatedPropertyTag
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00011308 File Offset: 0x0000F508
		internal AnnotatedPropertyTag(PropertyTag propertyTag, NamedProperty namedProperty)
		{
			this.PropertyTag = propertyTag;
			this.NamedProperty = namedProperty;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00011318 File Offset: 0x0000F518
		public override string ToString()
		{
			return string.Format("{0}/{1}", this.PropertyTag, this.NamedProperty);
		}

		// Token: 0x04000331 RID: 817
		public readonly NamedProperty NamedProperty;

		// Token: 0x04000332 RID: 818
		public readonly PropertyTag PropertyTag;
	}
}
