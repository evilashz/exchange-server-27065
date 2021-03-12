using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B5 RID: 1717
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LessThenComparisionQuery : ComparisionQuery<IComparable>
	{
		// Token: 0x06004557 RID: 17751 RVA: 0x001272F8 File Offset: 0x001254F8
		internal LessThenComparisionQuery(int index, IComparable propValue) : base(index, propValue)
		{
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00127302 File Offset: 0x00125502
		public override bool IsMatch(object[] row)
		{
			return Utils.CompareValues(this.PropValue, row[this.Index]) > 0;
		}
	}
}
