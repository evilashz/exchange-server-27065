using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200086E RID: 2158
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ItemCategoryList : ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x0600513B RID: 20795 RVA: 0x00152329 File Offset: 0x00150529
		public ItemCategoryList(IItem item)
		{
			this.item = item;
			this.initialValue = this.GetNativeValue();
			Array.Sort<string>(this.initialValue, Category.NameComparer);
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x00152354 File Offset: 0x00150554
		public int Count
		{
			get
			{
				return this.GetNativeValue().Length;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x0600513D RID: 20797 RVA: 0x0015235E File Offset: 0x0015055E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x00152364 File Offset: 0x00150564
		public void AddRange(ICollection<string> categoryNames)
		{
			if (categoryNames == null)
			{
				throw new ArgumentNullException("categoryNames");
			}
			HashSet<string> hashSet = new HashSet<string>(this);
			foreach (string text in categoryNames)
			{
				if (text == null)
				{
					throw new ArgumentNullException("categoryNames");
				}
				if (hashSet.Contains(text))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Category {0} already exists on the item", new object[]
					{
						text
					}), "categoryName");
				}
				hashSet.Add(text);
			}
			this.SetNativeValue(Util.MergeArrays<string>(new ICollection<string>[]
			{
				categoryNames,
				this.GetNativeValue()
			}));
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x00152424 File Offset: 0x00150624
		public void AddRange(params string[] categoryNames)
		{
			this.AddRange((ICollection<string>)categoryNames);
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x00152434 File Offset: 0x00150634
		public void Add(string categoryName)
		{
			this.AddRange(new string[]
			{
				categoryName
			});
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x00152453 File Offset: 0x00150653
		public void Clear()
		{
			this.SetNativeValue(Array<string>.Empty);
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x00152460 File Offset: 0x00150660
		public bool Contains(string categoryName)
		{
			return -1 != ItemCategoryList.IndexOf(this.GetNativeValue(), categoryName);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00152474 File Offset: 0x00150674
		public void CopyTo(string[] array, int arrayIndex)
		{
			this.GetNativeValue().CopyTo(array, arrayIndex);
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x00152484 File Offset: 0x00150684
		public bool Remove(string item)
		{
			string[] nativeValue = this.GetNativeValue();
			int num = ItemCategoryList.IndexOf(nativeValue, item);
			if (num != -1)
			{
				this.SetNativeValue(Util.RemoveArrayElements<string>(nativeValue, new int[]
				{
					num
				}));
				return true;
			}
			return false;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x001524BF File Offset: 0x001506BF
		public IEnumerator<string> GetEnumerator()
		{
			return ((IList<string>)this.GetNativeValue()).GetEnumerator();
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x001524D1 File Offset: 0x001506D1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetNativeValue().GetEnumerator();
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x001524DE File Offset: 0x001506DE
		internal IEnumerable<string> GetNewCategories()
		{
			if (this.initialValue.Length == 0)
			{
				return this.InternalGetNewCategories();
			}
			return this;
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x001524F4 File Offset: 0x001506F4
		private static int IndexOf(string[] categories, string categoryName)
		{
			for (int i = 0; i < categories.Length; i++)
			{
				if (Category.NameComparer.Compare(categories[i], categoryName) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x00152522 File Offset: 0x00150722
		private string[] GetNativeValue()
		{
			return (string[])this.item.GetValueOrDefault<string[]>(InternalSchema.Categories, Array<string>.Empty).Clone();
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x001526E8 File Offset: 0x001508E8
		private IEnumerable<string> InternalGetNewCategories()
		{
			foreach (string assignedCategory in this)
			{
				if (Array.BinarySearch<string>(this.initialValue, assignedCategory, Category.NameComparer) == -1)
				{
					yield return assignedCategory;
				}
			}
			yield break;
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x00152705 File Offset: 0x00150905
		private void SetNativeValue(string[] newValue)
		{
			this.item[InternalSchema.Categories] = newValue;
		}

		// Token: 0x04002C4E RID: 11342
		private readonly IItem item;

		// Token: 0x04002C4F RID: 11343
		private readonly string[] initialValue;
	}
}
