using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000162 RID: 354
	internal sealed class MostRecentlyUsedCategories
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x000549FA File Offset: 0x00052BFA
		public static MostRecentlyUsedCategories Create(MasterCategoryList masterCategoryList, OutlookModule outlookModule)
		{
			return new MostRecentlyUsedCategories(masterCategoryList, outlookModule);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00054A04 File Offset: 0x00052C04
		private MostRecentlyUsedCategories(MasterCategoryList masterCategoryList, OutlookModule outlookModule)
		{
			Category[] array = masterCategoryList.ToArray();
			Array.Sort<Category>(array, MasterCategoryList.CreateUsageBasedComparer(outlookModule));
			int num = 10;
			if (array.Length < 10)
			{
				num = array.Length;
			}
			this.mostRecentCategories = new Category[num];
			Array.Copy(array, this.mostRecentCategories, num);
			Array.Sort<Category>(this.mostRecentCategories, new MostRecentlyUsedCategories.CategoryNameComparer());
			int num2 = array.Length - num;
			if (0 < num2)
			{
				this.otherCategories = new Category[num2];
				Array.Copy(array, num, this.otherCategories, 0, num2);
				Array.Sort<Category>(this.otherCategories, new MostRecentlyUsedCategories.CategoryNameComparer());
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00054A96 File Offset: 0x00052C96
		public Category[] MostRecentCategories
		{
			get
			{
				return this.mostRecentCategories;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00054A9E File Offset: 0x00052C9E
		public Category[] OtherCategories
		{
			get
			{
				return this.otherCategories;
			}
		}

		// Token: 0x040008A7 RID: 2215
		private const int MaxMostRecentCategoryCount = 10;

		// Token: 0x040008A8 RID: 2216
		private Category[] mostRecentCategories;

		// Token: 0x040008A9 RID: 2217
		private Category[] otherCategories;

		// Token: 0x02000163 RID: 355
		public class CategoryNameComparer : IComparer<Category>
		{
			// Token: 0x06000C50 RID: 3152 RVA: 0x00054AA6 File Offset: 0x00052CA6
			public int Compare(Category categoryX, Category categoryY)
			{
				if (categoryX == null)
				{
					throw new ArgumentNullException("categoryX");
				}
				if (categoryY == null)
				{
					throw new ArgumentNullException("categoryY");
				}
				return categoryX.Name.CompareTo(categoryY.Name);
			}
		}
	}
}
