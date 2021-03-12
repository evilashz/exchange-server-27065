using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B2 RID: 1714
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotEqualsToComparisionQuery : ComparisionQuery<object>
	{
		// Token: 0x06004551 RID: 17745 RVA: 0x0012728F File Offset: 0x0012548F
		internal NotEqualsToComparisionQuery(int index, object propValue) : base(index, propValue)
		{
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00127299 File Offset: 0x00125499
		public override bool IsMatch(object[] row)
		{
			return !object.Equals(this.PropValue, row[this.Index]);
		}
	}
}
