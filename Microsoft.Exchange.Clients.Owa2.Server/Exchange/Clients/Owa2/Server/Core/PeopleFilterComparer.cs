using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000214 RID: 532
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PeopleFilterComparer : IComparer<PeopleFilter>
	{
		// Token: 0x06001497 RID: 5271 RVA: 0x00049094 File Offset: 0x00047294
		internal PeopleFilterComparer(CultureInfo culture)
		{
			this.culture = (culture ?? CultureInfo.CurrentUICulture);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000490AC File Offset: 0x000472AC
		public int Compare(PeopleFilter x, PeopleFilter y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return 1;
			}
			if (y == null)
			{
				return -1;
			}
			int num = this.CompareSortGroups(x, y);
			if (num != 0)
			{
				return num;
			}
			return this.CompareDisplayNames(x, y);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000490E4 File Offset: 0x000472E4
		private int CompareSortGroups(PeopleFilter x, PeopleFilter y)
		{
			return Comparer<int>.Default.Compare(x.SortGroupPriority, y.SortGroupPriority);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000490FC File Offset: 0x000472FC
		private int CompareDisplayNames(PeopleFilter x, PeopleFilter y)
		{
			return string.Compare(x.DisplayName, y.DisplayName, this.culture, CompareOptions.IgnoreCase);
		}

		// Token: 0x04000B1F RID: 2847
		private readonly CultureInfo culture;
	}
}
