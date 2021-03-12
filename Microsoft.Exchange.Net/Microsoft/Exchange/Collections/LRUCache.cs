using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200003F RID: 63
	public class LRUCache<TKey, TValue> where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000875C File Offset: 0x0000695C
		public LRUCache(int capacity, Func<TKey, TValue> loadItem) : this(capacity, loadItem, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008780 File Offset: 0x00006980
		public LRUCache(int capacity, Func<TKey, TValue> loadItem, double? maxRatio, Action getCounter, Action missCounter, Func<bool> forceEvictCondition, Func<bool> forceEvictPredicate, Action<TValue> elementEvictCallback)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (loadItem == null)
			{
				throw new ArgumentNullException("loadItem");
			}
			this.capacity = capacity;
			this.loadItem = loadItem;
			this.mapping = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(capacity);
			this.evictionList = new LinkedList<KeyValuePair<TKey, TValue>>();
			if (maxRatio != null)
			{
				double? num = maxRatio;
				if (num.GetValueOrDefault() > 0.0 || num == null)
				{
					double? num2 = maxRatio;
					if (num2.GetValueOrDefault() < 1.0 || num2 == null)
					{
						goto IL_9E;
					}
				}
				throw new ArgumentException("maxRatio has to be null or (0,1)");
			}
			IL_9E:
			this.maxRatio = maxRatio;
			this.getCounter = (getCounter ?? LRUCache<TKey, TValue>.ActionDefault);
			this.missCounter = (missCounter ?? LRUCache<TKey, TValue>.ActionDefault);
			this.forceEvictCondition = (forceEvictCondition ?? LRUCache<TKey, TValue>.PredicateDefault);
			this.forceEvictPredicate = (forceEvictPredicate ?? LRUCache<TKey, TValue>.PredicateDefault);
			this.elementEvictCallback = (elementEvictCallback ?? LRUCache<TKey, TValue>.ActionTValueDefault);
			this.lockObject = new ReaderWriterLockSlim();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008894 File Offset: 0x00006A94
		public void Reset()
		{
			try
			{
				this.lockObject.EnterWriteLock();
				if (this.elementEvictCallback != LRUCache<TKey, TValue>.ActionTValueDefault)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this.evictionList)
					{
						this.elementEvictCallback(keyValuePair.Value);
					}
				}
				this.mapping.Clear();
				this.evictionList.Clear();
			}
			finally
			{
				try
				{
					this.lockObject.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008950 File Offset: 0x00006B50
		public void UpdateCapacity(int capacity, Action action)
		{
			try
			{
				this.lockObject.EnterWriteLock();
				this.capacity = capacity;
				if (action != null)
				{
					action();
				}
			}
			finally
			{
				try
				{
					this.lockObject.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000089A8 File Offset: 0x00006BA8
		public TValue Get(TKey key)
		{
			bool flag;
			return this.Get(key, out flag);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000089C0 File Offset: 0x00006BC0
		public TValue Get(TKey key, out bool elementEvicted)
		{
			elementEvicted = false;
			this.getCounter();
			TValue result;
			try
			{
				this.lockObject.EnterWriteLock();
				TValue tvalue;
				if (this.TryLoadFromCache(key, out tvalue))
				{
					result = tvalue;
				}
				else
				{
					this.missCounter();
					TValue value = this.loadItem(key);
					result = this.AddNewItem(key, value, ref elementEvicted);
				}
			}
			finally
			{
				try
				{
					this.lockObject.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008A48 File Offset: 0x00006C48
		protected virtual bool TryLoadFromCache(TKey key, out TValue value)
		{
			LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode;
			if (this.mapping.TryGetValue(key, out linkedListNode))
			{
				this.evictionList.Remove(linkedListNode);
				this.evictionList.AddFirst(linkedListNode);
				value = linkedListNode.Value.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008A9C File Offset: 0x00006C9C
		protected virtual TValue AddNewItem(TKey key, TValue value, ref bool elementEvicted)
		{
			if (this.evictionList.Count >= this.capacity || this.forceEvictCondition())
			{
				double num = (double)this.capacity;
				double? num2 = this.maxRatio;
				int num3 = (int)(num * ((num2 != null) ? num2.GetValueOrDefault() : 1.0));
				while (this.evictionList.Count > 0 && (this.evictionList.Count >= num3 || this.forceEvictPredicate()))
				{
					this.elementEvictCallback(this.evictionList.Last.Value.Value);
					this.mapping.Remove(this.evictionList.Last.Value.Key);
					this.evictionList.RemoveLast();
					elementEvicted = true;
				}
			}
			KeyValuePair<TKey, TValue> value2 = new KeyValuePair<TKey, TValue>(key, value);
			LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode;
			if (this.mapping.TryGetValue(key, out linkedListNode))
			{
				linkedListNode.Value = value2;
				this.evictionList.Remove(linkedListNode);
			}
			else
			{
				linkedListNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(value2);
				this.mapping.Add(linkedListNode.Value.Key, linkedListNode);
			}
			this.evictionList.AddFirst(linkedListNode);
			return value;
		}

		// Token: 0x0400011C RID: 284
		private static readonly Action ActionDefault = delegate()
		{
		};

		// Token: 0x0400011D RID: 285
		private static readonly Func<bool> PredicateDefault = () => false;

		// Token: 0x0400011E RID: 286
		private static readonly Action<TValue> ActionTValueDefault = delegate(TValue v)
		{
		};

		// Token: 0x0400011F RID: 287
		private readonly Func<TKey, TValue> loadItem;

		// Token: 0x04000120 RID: 288
		private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> mapping;

		// Token: 0x04000121 RID: 289
		private readonly LinkedList<KeyValuePair<TKey, TValue>> evictionList;

		// Token: 0x04000122 RID: 290
		private readonly double? maxRatio;

		// Token: 0x04000123 RID: 291
		private readonly Action getCounter;

		// Token: 0x04000124 RID: 292
		private readonly Action missCounter;

		// Token: 0x04000125 RID: 293
		private readonly Func<bool> forceEvictCondition;

		// Token: 0x04000126 RID: 294
		private readonly Func<bool> forceEvictPredicate;

		// Token: 0x04000127 RID: 295
		private readonly Action<TValue> elementEvictCallback;

		// Token: 0x04000128 RID: 296
		private readonly ReaderWriterLockSlim lockObject;

		// Token: 0x04000129 RID: 297
		private int capacity;
	}
}
