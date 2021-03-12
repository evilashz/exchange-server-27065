using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal class ConfigObjectCollection : BaseConfigCollection, ICloneable
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000048CC File Offset: 0x00002ACC
		public ConfigObjectCollection()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048D4 File Offset: 0x00002AD4
		public ConfigObjectCollection(ConfigObject[] configObjectArray) : base(configObjectArray)
		{
		}

		// Token: 0x17000032 RID: 50
		public ConfigObject this[int index]
		{
			get
			{
				return (ConfigObject)base.List[index];
			}
			set
			{
				base.Replace(index, value);
			}
		}

		// Token: 0x17000033 RID: 51
		public ConfigObject this[string identity]
		{
			get
			{
				int num = base.IndexOfIdentity(identity);
				if (-1 != num)
				{
					return this[num];
				}
				return null;
			}
			set
			{
				base.Replace(base.IndexOfIdentity(identity), value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000492E File Offset: 0x00002B2E
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004936 File Offset: 0x00002B36
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
			set
			{
				this.totalCount = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000493F File Offset: 0x00002B3F
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004947 File Offset: 0x00002B47
		public int PageOffset
		{
			get
			{
				return this.pageOffset;
			}
			set
			{
				this.pageOffset = value;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004950 File Offset: 0x00002B50
		public object Clone()
		{
			ConfigObjectCollection configObjectCollection = new ConfigObjectCollection();
			foreach (object obj in base.List)
			{
				ConfigObject value = (ConfigObject)obj;
				configObjectCollection.List.Add(value);
			}
			configObjectCollection.IsReadOnly = this.IsReadOnly;
			configObjectCollection.TotalCount = this.TotalCount;
			configObjectCollection.PageOffset = this.PageOffset;
			return configObjectCollection;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000049DC File Offset: 0x00002BDC
		public virtual void Sort(string sortProperty, ListSortDirection direction)
		{
			this.SortWithComparer(new ConfigObjectComparer(sortProperty, direction));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000049EB File Offset: 0x00002BEB
		public virtual void Sort(ListSortDescription sort)
		{
			this.SortWithComparer(new ConfigObjectComparer(sort));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000049F9 File Offset: 0x00002BF9
		public virtual void Sort(ListSortDescriptionCollection sorts)
		{
			this.SortWithComparer(new ConfigObjectComparer(sorts));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004A08 File Offset: 0x00002C08
		private void SortWithComparer(ConfigObjectComparer comparer)
		{
			ConfigObject[] array = new ConfigObject[base.Count];
			this.CopyTo(array, 0);
			Array.Sort(array, comparer);
			base.Clear();
			this.AddRange(array);
		}

		// Token: 0x0400004A RID: 74
		private int totalCount;

		// Token: 0x0400004B RID: 75
		private int pageOffset;
	}
}
