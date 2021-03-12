using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct AnnotatedPropertyValue
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00011335 File Offset: 0x0000F535
		internal AnnotatedPropertyValue(PropertyTag propertyTag, PropertyValue propertyValue, NamedProperty namedProperty)
		{
			this.AnnotatedPropertyTag = new AnnotatedPropertyTag(propertyTag, namedProperty);
			this.PropertyValue = propertyValue;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001134B File Offset: 0x0000F54B
		public NamedProperty NamedProperty
		{
			get
			{
				return this.AnnotatedPropertyTag.NamedProperty;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00011358 File Offset: 0x0000F558
		public PropertyTag PropertyTag
		{
			get
			{
				return this.AnnotatedPropertyTag.PropertyTag;
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00011368 File Offset: 0x0000F568
		public override string ToString()
		{
			return string.Format("{0}={1}{2}", this.AnnotatedPropertyTag, this.PropertyValue.IsError ? "(error)" : string.Empty, this.PropertyValue.Value);
		}

		// Token: 0x04000333 RID: 819
		public readonly PropertyValue PropertyValue;

		// Token: 0x04000334 RID: 820
		public readonly AnnotatedPropertyTag AnnotatedPropertyTag;
	}
}
