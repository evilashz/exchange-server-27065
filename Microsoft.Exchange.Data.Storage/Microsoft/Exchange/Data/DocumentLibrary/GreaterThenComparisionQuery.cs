using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B3 RID: 1715
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GreaterThenComparisionQuery : ComparisionQuery<IComparable>
	{
		// Token: 0x06004553 RID: 17747 RVA: 0x001272B1 File Offset: 0x001254B1
		internal GreaterThenComparisionQuery(int index, IComparable propValue) : base(index, propValue)
		{
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x001272BB File Offset: 0x001254BB
		public override bool IsMatch(object[] row)
		{
			return Utils.CompareValues(this.PropValue, row[this.Index]) < 0;
		}
	}
}
