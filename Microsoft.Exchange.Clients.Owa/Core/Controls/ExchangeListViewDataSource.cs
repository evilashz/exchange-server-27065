using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C6 RID: 710
	public abstract class ExchangeListViewDataSource
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x0009E21C File Offset: 0x0009C41C
		protected ExchangeListViewDataSource(Hashtable properties)
		{
			this.propertyMap = new Dictionary<PropertyDefinition, int>(properties.Count);
			IDictionaryEnumerator enumerator = properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				PropertyDefinition key = (PropertyDefinition)enumerator.Key;
				this.propertyMap[key] = 0;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001B9E RID: 7070
		public abstract int TotalCount { get; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0009E287 File Offset: 0x0009C487
		public virtual int TotalItemCount
		{
			get
			{
				return this.TotalCount;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0009E28F File Offset: 0x0009C48F
		protected virtual bool IsPreviousItemLoaded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0009E292 File Offset: 0x0009C492
		internal virtual QueryResult QueryResult
		{
			get
			{
				throw new NotImplementedException("Not implemented by default. Implement in the derived class, if needed");
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0009E2A0 File Offset: 0x0009C4A0
		internal PropertyDefinition[] GetRequestedProperties()
		{
			Dictionary<PropertyDefinition, int> dictionary = null;
			return this.GetRequestedProperties(false, ref dictionary);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0009E2B8 File Offset: 0x0009C4B8
		internal PropertyDefinition[] GetRequestedProperties(bool getPropertyMap, ref Dictionary<PropertyDefinition, int> outPropertyMap)
		{
			PropertyDefinition[] array = new PropertyDefinition[this.propertyMap.Count];
			int num = 0;
			using (Dictionary<PropertyDefinition, int>.Enumerator enumerator = this.propertyMap.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyDefinition[] array2 = array;
					int num2 = num;
					KeyValuePair<PropertyDefinition, int> keyValuePair = enumerator.Current;
					array2[num2] = keyValuePair.Key;
					num++;
				}
			}
			for (int i = 0; i < num; i++)
			{
				this.propertyMap[array[i]] = i;
			}
			if (getPropertyMap)
			{
				outPropertyMap = new Dictionary<PropertyDefinition, int>(this.propertyMap);
			}
			return array;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0009E354 File Offset: 0x0009C554
		protected int PropertyIndex(PropertyDefinition propertyDefinition)
		{
			return this.propertyMap[propertyDefinition];
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0009E364 File Offset: 0x0009C564
		public virtual T GetItemProperty<T>(PropertyDefinition propertyDefinition) where T : class
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (!this.propertyMap.ContainsKey(propertyDefinition))
			{
				return default(T);
			}
			int num = this.IsPreviousItemLoaded ? (this.currentItem + 1) : this.currentItem;
			int num2 = this.propertyMap[propertyDefinition];
			return this.items[num][num2] as T;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009E3D4 File Offset: 0x0009C5D4
		public virtual T GetItemProperty<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (!this.propertyMap.ContainsKey(propertyDefinition))
			{
				return defaultValue;
			}
			int num = this.IsPreviousItemLoaded ? (this.currentItem + 1) : this.currentItem;
			int num2 = this.propertyMap[propertyDefinition];
			object obj = this.items[num][num2];
			if (obj == null || !(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0009E443 File Offset: 0x0009C643
		public int GetPropertyIndex(PropertyDefinition propertyDefinition)
		{
			return this.propertyMap[propertyDefinition];
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0009E451 File Offset: 0x0009C651
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0009E459 File Offset: 0x0009C659
		public int StartRange
		{
			get
			{
				return this.startRange;
			}
			protected set
			{
				this.startRange = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0009E462 File Offset: 0x0009C662
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x0009E46A File Offset: 0x0009C66A
		public int EndRange
		{
			get
			{
				return this.endRange;
			}
			protected set
			{
				this.endRange = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0009E473 File Offset: 0x0009C673
		public int RangeCount
		{
			get
			{
				if (this.endRange < this.startRange || this.startRange == -2147483648 || this.endRange == -2147483648)
				{
					return 0;
				}
				return this.endRange - this.startRange + 1;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0009E4AE File Offset: 0x0009C6AE
		public virtual int CurrentItem
		{
			get
			{
				return this.currentItem;
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0009E4B6 File Offset: 0x0009C6B6
		public virtual bool MoveNext()
		{
			this.currentItem++;
			return this.currentItem < this.RangeCount;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
		public virtual void MoveToItem(int itemIndex)
		{
			if (itemIndex < -1 || (!this.IsPreviousItemLoaded && itemIndex < 0) || this.RangeCount <= itemIndex)
			{
				throw new ArgumentException("itemIndex=" + itemIndex.ToString(CultureInfo.CurrentCulture) + " is out of range.");
			}
			this.currentItem = itemIndex;
		}

		// Token: 0x17000755 RID: 1877
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x0009E522 File Offset: 0x0009C722
		protected object[][] Items
		{
			set
			{
				this.items = value;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0009E52B File Offset: 0x0009C72B
		protected void SetIndexer(int index)
		{
			this.currentItem = index;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0009E534 File Offset: 0x0009C734
		public virtual object GetCurrentItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001405 RID: 5125
		private Dictionary<PropertyDefinition, int> propertyMap;

		// Token: 0x04001406 RID: 5126
		private object[][] items;

		// Token: 0x04001407 RID: 5127
		private int startRange = int.MinValue;

		// Token: 0x04001408 RID: 5128
		private int endRange = int.MinValue;

		// Token: 0x04001409 RID: 5129
		private int currentItem = -1;
	}
}
