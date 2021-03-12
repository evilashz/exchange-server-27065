using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B4 RID: 1716
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GreaterThenOrEqualToComparisionQuery : ComparisionQuery<IComparable>
	{
		// Token: 0x06004555 RID: 17749 RVA: 0x001272D3 File Offset: 0x001254D3
		internal GreaterThenOrEqualToComparisionQuery(int index, IComparable propValue) : base(index, propValue)
		{
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x001272DD File Offset: 0x001254DD
		public override bool IsMatch(object[] row)
		{
			return Utils.CompareValues(this.PropValue, row[this.Index]) <= 0;
		}
	}
}
