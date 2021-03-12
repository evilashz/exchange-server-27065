using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200009F RID: 159
	internal class DateTimeStringComparer : IComparer<string>
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00012C3C File Offset: 0x00010E3C
		public int Compare(string x, string y)
		{
			return Convert.ToDateTime(x).CompareTo(Convert.ToDateTime(y));
		}
	}
}
