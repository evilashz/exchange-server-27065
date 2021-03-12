using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationEqualityFilter
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		internal MigrationEqualityFilter(PropertyDefinition definition, object value)
		{
			this.Property = definition;
			this.Value = value;
			this.Filter = new ComparisonFilter(ComparisonOperator.Equal, definition, value);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		internal MigrationEqualityFilter(PropertyDefinition definition, string value, bool ignoreCase)
		{
			this.Property = definition;
			this.Value = value;
			MatchFlags matchFlags = ignoreCase ? MatchFlags.IgnoreCase : MatchFlags.Default;
			this.Filter = new TextFilter(definition, value, MatchOptions.FullString, matchFlags);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010030 File Offset: 0x0000E230
		internal MigrationEqualityFilter(PropertyDefinition definition, object value, ComparisonOperator op)
		{
			this.Property = definition;
			this.Value = value;
			this.Filter = new ComparisonFilter(op, definition, value);
		}

		// Token: 0x04000156 RID: 342
		internal readonly PropertyDefinition Property;

		// Token: 0x04000157 RID: 343
		internal readonly object Value;

		// Token: 0x04000158 RID: 344
		internal readonly QueryFilter Filter;
	}
}
