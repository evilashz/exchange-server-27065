using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B6 RID: 1718
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LessThenOrEqualToComparisionQuery : ComparisionQuery<IComparable>
	{
		// Token: 0x06004559 RID: 17753 RVA: 0x0012731A File Offset: 0x0012551A
		internal LessThenOrEqualToComparisionQuery(int index, IComparable propValue) : base(index, propValue)
		{
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00127324 File Offset: 0x00125524
		public override bool IsMatch(object[] row)
		{
			return Utils.CompareValues(this.PropValue, row[this.Index]) >= 0;
		}
	}
}
