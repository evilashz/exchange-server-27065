using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B1 RID: 1713
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EqualsToComparisionQuery : ComparisionQuery<object>
	{
		// Token: 0x0600454F RID: 17743 RVA: 0x00127270 File Offset: 0x00125470
		internal EqualsToComparisionQuery(int index, object propValue) : base(index, propValue)
		{
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x0012727A File Offset: 0x0012547A
		public override bool IsMatch(object[] row)
		{
			return object.Equals(this.PropValue, row[this.Index]);
		}
	}
}
