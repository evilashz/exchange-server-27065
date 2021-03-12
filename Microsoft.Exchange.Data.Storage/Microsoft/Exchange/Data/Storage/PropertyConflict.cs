using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE1 RID: 2785
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyConflict
	{
		// Token: 0x06006504 RID: 25860 RVA: 0x001ACBD6 File Offset: 0x001AADD6
		public PropertyConflict(PropertyDefinition propertyDefinition, object originalValue, object clientValue, object serverValue, object resolvedValue, bool resolvable)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException();
			}
			this.PropertyDefinition = propertyDefinition;
			this.OriginalValue = originalValue;
			this.ClientValue = clientValue;
			this.ServerValue = serverValue;
			this.ResolvedValue = resolvedValue;
			this.ConflictResolvable = resolvable;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001ACC14 File Offset: 0x001AAE14
		public override string ToString()
		{
			return string.Format("Property = {0}, values = <{1}, {2}, {3}>, ResolvedValue = {4}, ConflictResolvable = {5}.", new object[]
			{
				this.PropertyDefinition,
				this.OriginalValue,
				this.ClientValue,
				this.ServerValue,
				this.ResolvedValue,
				this.ConflictResolvable
			});
		}

		// Token: 0x0400398F RID: 14735
		public readonly PropertyDefinition PropertyDefinition;

		// Token: 0x04003990 RID: 14736
		public readonly bool ConflictResolvable;

		// Token: 0x04003991 RID: 14737
		public readonly object OriginalValue;

		// Token: 0x04003992 RID: 14738
		public readonly object ServerValue;

		// Token: 0x04003993 RID: 14739
		public readonly object ClientValue;

		// Token: 0x04003994 RID: 14740
		public readonly object ResolvedValue;
	}
}
