using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x02000078 RID: 120
	internal abstract class SimpleTable<TItem, TClusteredIndexKey, TSegment> : ITable<TItem>, ITable
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		public SimpleTable(IIndexDescriptor<TItem, TClusteredIndexKey> primaryKeyDescriptor)
		{
			int maxRunningTasks = Settings.MaxRunningTasks;
			this.dictionary = new ConcurrentDictionary<TClusteredIndexKey, TSegment>(maxRunningTasks, 1024, SimpleTable<TItem, TClusteredIndexKey, TSegment>.keyComparer);
			this.indexes = new Dictionary<Type, IIndex<TSegment>>();
			this.primaryIndexDescriptor = primaryKeyDescriptor;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		public IEnumerable<TItem> GetItems<TKey>(IIndexDescriptor<TItem, TKey> indexDescriptor)
		{
			Func<KeyValuePair<TClusteredIndexKey, TSegment>, TSegment> func = null;
			IEnumerable<TItem> query;
			IIndex<TSegment> index;
			if (indexDescriptor is IIndexDescriptor<TItem, TClusteredIndexKey>)
			{
				IIndexDescriptor<TItem, TClusteredIndexKey> indexDescriptor2 = indexDescriptor as IIndexDescriptor<TItem, TClusteredIndexKey>;
				TSegment segment;
				if (this.dictionary.TryGetValue(indexDescriptor2.Key, out segment))
				{
					query = this.GetItemsFromSegment<TClusteredIndexKey>(segment, indexDescriptor2);
				}
				else
				{
					query = Enumerable.Empty<TItem>();
				}
			}
			else if (this.indexes.TryGetValue(indexDescriptor.GetType(), out index))
			{
				SimpleIndex<TSegment, TKey> simpleIndex = (SimpleIndex<TSegment, TKey>)index;
				IEnumerable<TSegment> items = simpleIndex.GetItems(indexDescriptor.Key);
				query = this.GetItemsFromSegments<TKey>(items, indexDescriptor);
			}
			else
			{
				IEnumerable<KeyValuePair<TClusteredIndexKey, TSegment>> source = this.dictionary;
				if (func == null)
				{
					func = ((KeyValuePair<TClusteredIndexKey, TSegment> pair) => pair.Value);
				}
				IEnumerable<TSegment> segments = source.Select(func);
				query = this.GetItemsFromSegments<TKey>(segments, indexDescriptor);
			}
			return indexDescriptor.ApplyIndexRestriction(SimpleTable<TItem, TClusteredIndexKey, TSegment>.dataAccess.AsDataAccessQuery<TItem>(query));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public void Insert(TItem item, TracingContext traceContext)
		{
			TClusteredIndexKey key = this.primaryIndexDescriptor.GetKeyValues(item).First<TClusteredIndexKey>();
			TSegment orAdd = this.dictionary.GetOrAdd(key, (TClusteredIndexKey k) => this.CreateSegment(item));
			bool flag = this.AddToSegment(orAdd, item);
			if (flag && this.indexes != null)
			{
				foreach (Type key2 in this.indexes.Keys)
				{
					this.indexes[key2].Add(orAdd, traceContext);
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001C5C0 File Offset: 0x0001A7C0
		public void AddIndex<TKey>(IIndexDescriptor<TItem, TKey> indexDescriptor)
		{
			if (this.dictionary.Count != 0)
			{
				throw new NotSupportedException("Indexes cannot be added once rows have been added to the table");
			}
			this.indexes[indexDescriptor.GetType()] = new SimpleIndex<TSegment, TKey>((TSegment segment) => indexDescriptor.GetKeyValues(this.GetItemsFromSegment<TKey>(segment, indexDescriptor).First<TItem>()));
		}

		// Token: 0x060006BD RID: 1725
		protected abstract IEnumerable<TItem> GetItemsFromSegment<TKey>(TSegment segment, IIndexDescriptor<TItem, TKey> indexDescriptor);

		// Token: 0x060006BE RID: 1726
		protected abstract IEnumerable<TItem> GetItemsFromSegments<TKey>(IEnumerable<TSegment> segments, IIndexDescriptor<TItem, TKey> indexDescriptor);

		// Token: 0x060006BF RID: 1727
		protected abstract TSegment CreateSegment(TItem item);

		// Token: 0x060006C0 RID: 1728
		protected abstract bool AddToSegment(TSegment segment, TItem item);

		// Token: 0x04000448 RID: 1096
		private const int DictionarySize = 1024;

		// Token: 0x04000449 RID: 1097
		private static LocalDataAccess dataAccess = new LocalDataAccess();

		// Token: 0x0400044A RID: 1098
		private static KeyComparer<TClusteredIndexKey> keyComparer = new KeyComparer<TClusteredIndexKey>();

		// Token: 0x0400044B RID: 1099
		private ConcurrentDictionary<TClusteredIndexKey, TSegment> dictionary;

		// Token: 0x0400044C RID: 1100
		private Dictionary<Type, IIndex<TSegment>> indexes;

		// Token: 0x0400044D RID: 1101
		private IIndexDescriptor<TItem, TClusteredIndexKey> primaryIndexDescriptor;
	}
}
