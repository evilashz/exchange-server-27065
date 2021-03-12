using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x0200048C RID: 1164
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_KeyedCollectionDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		// Token: 0x060038F3 RID: 14579 RVA: 0x000D8DB0 File Offset: 0x000D6FB0
		[__DynamicallyInvokable]
		protected KeyedCollection() : this(null, 0)
		{
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000D8DBA File Offset: 0x000D6FBA
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer) : this(comparer, 0)
		{
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000D8DC4 File Offset: 0x000D6FC4
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000D8DFB File Offset: 0x000D6FFB
		[__DynamicallyInvokable]
		public IEqualityComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170008B5 RID: 2229
		[__DynamicallyInvokable]
		public TItem this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				if (this.dict != null)
				{
					return this.dict[key];
				}
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return titem;
					}
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TItem);
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000D8E98 File Offset: 0x000D7098
		[__DynamicallyInvokable]
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			if (key != null)
			{
				foreach (TItem item in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(item), key))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000D8F28 File Offset: 0x000D7128
		private bool ContainsItem(TItem item)
		{
			TKey keyForItem;
			if (this.dict == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return base.Items.Contains(item);
			}
			TItem x;
			bool flag = this.dict.TryGetValue(keyForItem, out x);
			return flag && EqualityComparer<TItem>.Default.Equals(x, item);
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000D8F7C File Offset: 0x000D717C
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key) && base.Remove(this.dict[key]);
			}
			if (key != null)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (this.comparer.Equals(this.GetKeyForItem(base.Items[i]), key))
					{
						this.RemoveItem(i);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000D900A File Offset: 0x000D720A
		[__DynamicallyInvokable]
		protected IDictionary<TKey, TItem> Dictionary
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dict;
			}
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000D9014 File Offset: 0x000D7214
		[__DynamicallyInvokable]
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.ContainsItem(item))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000D9067 File Offset: 0x000D7267
		[__DynamicallyInvokable]
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		// Token: 0x060038FE RID: 14590
		[__DynamicallyInvokable]
		protected abstract TKey GetKeyForItem(TItem item);

		// Token: 0x060038FF RID: 14591 RVA: 0x000D908C File Offset: 0x000D728C
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000D90BC File Offset: 0x000D72BC
		[__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(base.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000D90F4 File Offset: 0x000D72F4
		[__DynamicallyInvokable]
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000D9174 File Offset: 0x000D7374
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			this.keyCount++;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000D91D8 File Offset: 0x000D73D8
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in base.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000D924C File Offset: 0x000D744C
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x04001880 RID: 6272
		private const int defaultThreshold = 0;

		// Token: 0x04001881 RID: 6273
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04001882 RID: 6274
		private Dictionary<TKey, TItem> dict;

		// Token: 0x04001883 RID: 6275
		private int keyCount;

		// Token: 0x04001884 RID: 6276
		private int threshold;
	}
}
