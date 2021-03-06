using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000E7 RID: 231
	internal sealed class ExtendedPropertyDictionary : DataInternalComponent, IExtendedPropertyCollection, IReadOnlyExtendedPropertyCollection, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0002101D File Offset: 0x0001F21D
		public ExtendedPropertyDictionary(DataRow dataRow, DataColumn column) : base(dataRow)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			this.column = column;
			ExtendedPropertyDictionary.InitValidTypes();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00021040 File Offset: 0x0001F240
		public ExtendedPropertyDictionary(DataRow dataRow, BlobCollection blobCollection, byte blobCollectionKey) : base(dataRow)
		{
			if (blobCollection == null)
			{
				throw new ArgumentNullException("blobCollection");
			}
			this.blobCollection = blobCollection;
			this.blobCollectionKey = blobCollectionKey;
			ExtendedPropertyDictionary.InitValidTypes();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002106A File Offset: 0x0001F26A
		public ExtendedPropertyDictionary() : base(null)
		{
			ExtendedPropertyDictionary.InitValidTypes();
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00021078 File Offset: 0x0001F278
		public int Count
		{
			get
			{
				if (this.data != null || this.pendingDeferredLoad)
				{
					return this.Data.Count;
				}
				return 0;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0002109B File Offset: 0x0001F29B
		public ICollection<string> Keys
		{
			get
			{
				return ((IDictionary<string, object>)this.Data).Keys;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000210A8 File Offset: 0x0001F2A8
		public ICollection<object> Values
		{
			get
			{
				return ((IDictionary<string, object>)this.Data).Values;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000210B5 File Offset: 0x0001F2B5
		public override bool PendingDatabaseUpdates
		{
			get
			{
				return this.Dirty;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000210BD File Offset: 0x0001F2BD
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x000210C5 File Offset: 0x0001F2C5
		public bool Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000210CE File Offset: 0x0001F2CE
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000210D6 File Offset: 0x0001F2D6
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000210DF File Offset: 0x0001F2DF
		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)this.Data).Keys;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000210EC File Offset: 0x0001F2EC
		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)this.Data).Values;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000210F9 File Offset: 0x0001F2F9
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000210FC File Offset: 0x0001F2FC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000210FF File Offset: 0x0001F2FF
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001E8 RID: 488
		object IDictionary.this[object key]
		{
			get
			{
				object result;
				this.Data.TryGetValue((string)key, out result);
				return result;
			}
			set
			{
				this[(string)key] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00021138 File Offset: 0x0001F338
		private Dictionary<string, object> Data
		{
			get
			{
				Dictionary<string, object> dictionary = this.data;
				if (this.pendingDeferredLoad || dictionary == null)
				{
					lock (this)
					{
						if (this.pendingDeferredLoad)
						{
							this.DeferredLoad();
							this.pendingDeferredLoad = false;
						}
						else if (this.data == null)
						{
							this.data = this.CreateDictionary(ExtendedPropertyDictionary.initialDictionaryCapacity);
						}
						dictionary = this.data;
					}
				}
				return dictionary;
			}
		}

		// Token: 0x170001EA RID: 490
		public object this[string key]
		{
			get
			{
				return this.Data[key];
			}
			set
			{
				this.ThrowIfReadOnlyOrDeleted();
				ExtendedPropertyDictionary.ValidateValue(value);
				ExtendedPropertyDictionary.ValidateKey(key);
				if (this.HasChanged(key, value))
				{
					bool shouldRelease = false;
					try
					{
						shouldRelease = this.AssertOwnerAndTakeOwnership();
						this.Data[key] = value;
						this.dirty = true;
					}
					finally
					{
						this.AssertOwnerAndReleaseOwnership(shouldRelease);
					}
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00021238 File Offset: 0x0001F438
		public void Serialize(Stream stream)
		{
			byte[] array = ExtendedPropertyDictionary.bufferPool.Acquire();
			try
			{
				int num = 0;
				ExtendedPropertyDictionary.initialDictionaryCapacity = this.Count;
				foreach (KeyValuePair<string, object> keyValuePair in this.Data)
				{
					TypedValue typedValue = KeySerializer.Serialize(keyValuePair.Key);
					int num2 = TransportPropertyStreamWriter.SizeOf(typedValue.Type, typedValue.Value);
					StreamPropertyType streamPropertyType = ExtendedPropertyDictionary.SerializeType(keyValuePair.Value);
					num2 += TransportPropertyStreamWriter.SizeOf(streamPropertyType, keyValuePair.Value);
					if (num2 > array.Length - num)
					{
						stream.Write(array, 0, num);
						num = 0;
						if (num2 > array.Length)
						{
							if (array.Length == 1024)
							{
								ExtendedPropertyDictionary.bufferPool.Release(array);
							}
							array = new byte[num2 * 2];
						}
					}
					TransportPropertyStreamWriter.Serialize(typedValue.Type, typedValue.Value, array, ref num);
					TransportPropertyStreamWriter.Serialize(streamPropertyType, keyValuePair.Value, array, ref num);
				}
				if (num > 0)
				{
					stream.Write(array, 0, num);
				}
			}
			finally
			{
				if (array.Length == 1024)
				{
					ExtendedPropertyDictionary.bufferPool.Release(array);
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00021378 File Offset: 0x0001F578
		public void Deserialize(Stream stream, int numberOfPropertiesToFetch, bool doNotAddPropertyIfPresent)
		{
			this.ThrowIfReadOnlyOrDeleted();
			ExtendedPropertyDictionary.Deserialize(stream, this, numberOfPropertiesToFetch, doNotAddPropertyIfPresent);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002138C File Offset: 0x0001F58C
		public void Add(string key, object value)
		{
			this.ThrowIfReadOnlyOrDeleted();
			ExtendedPropertyDictionary.ValidateValue(value);
			ExtendedPropertyDictionary.ValidateKey(key);
			bool shouldRelease = false;
			try
			{
				shouldRelease = this.AssertOwnerAndTakeOwnership();
				this.Data.Add(key, value);
				this.dirty = true;
			}
			finally
			{
				this.AssertOwnerAndReleaseOwnership(shouldRelease);
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000213E4 File Offset: 0x0001F5E4
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this.ThrowIfReadOnlyOrDeleted();
			this.Data.Add(item.Key, item.Value);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00021408 File Offset: 0x0001F608
		public void Clear()
		{
			this.ThrowIfReadOnlyOrDeleted();
			bool shouldRelease = false;
			try
			{
				shouldRelease = this.AssertOwnerAndTakeOwnership();
				if (this.pendingDeferredLoad)
				{
					this.pendingDeferredLoad = false;
					this.ClearData();
					this.dirty = true;
				}
				else if (this.data != null)
				{
					this.ClearData();
					this.dirty = true;
				}
			}
			finally
			{
				this.AssertOwnerAndReleaseOwnership(shouldRelease);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00021478 File Offset: 0x0001F678
		public bool ContainsKey(string key)
		{
			return this.Data.ContainsKey(key);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00021486 File Offset: 0x0001F686
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this.Data).Contains(item);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00021494 File Offset: 0x0001F694
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, object>>)this.Data).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000214A3 File Offset: 0x0001F6A3
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this.Data).GetEnumerator();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000214B0 File Offset: 0x0001F6B0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.Data).GetEnumerator();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000214C0 File Offset: 0x0001F6C0
		public bool Remove(string key)
		{
			this.ThrowIfReadOnlyOrDeleted();
			bool shouldRelease = false;
			bool flag;
			try
			{
				shouldRelease = this.AssertOwnerAndTakeOwnership();
				flag = this.Data.Remove(key);
				if (flag)
				{
					this.dirty = true;
				}
			}
			finally
			{
				this.AssertOwnerAndReleaseOwnership(shouldRelease);
			}
			return flag;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00021510 File Offset: 0x0001F710
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this.Remove(item.Key);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002151F File Offset: 0x0001F71F
		public bool TryGetValue(string key, out object value)
		{
			return this.Data.TryGetValue(key, out value);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00021530 File Offset: 0x0001F730
		public bool TryGetValue<T>(string key, out T value)
		{
			Type typeFromHandle = typeof(T);
			StreamPropertyType streamPropertyType;
			if (ExtendedPropertyDictionary.validTypes.TryGetValue(typeFromHandle, out streamPropertyType))
			{
				if ((short)(streamPropertyType & StreamPropertyType.Array) != 0 || (short)(streamPropertyType & StreamPropertyType.List) != 0)
				{
					throw new InvalidOperationException("Array or list property requested using TryGetValue<T>(); should use TryGetListValue<ItemT>() instead");
				}
			}
			else
			{
				if (typeFromHandle.Equals(typeof(object)))
				{
					throw new InvalidOperationException("Using SystemObject as type parameter is not allowed; should use strong types instead");
				}
				streamPropertyType = StreamPropertyType.Null;
			}
			object obj;
			if (this.TryGetValue(key, out obj) && (obj is T || (default(T) == null && obj == null)))
			{
				if (obj != null)
				{
					if (streamPropertyType == StreamPropertyType.Null)
					{
						StreamPropertyType streamPropertyType2 = ExtendedPropertyDictionary.validTypes[obj.GetType()];
						if ((short)(streamPropertyType2 & StreamPropertyType.Array) != 0 || (short)(streamPropertyType2 & StreamPropertyType.List) != 0)
						{
							throw new InvalidOperationException("Array or list property requested using TryGetValue<interface<T>>(); should use TryGetListValue<ItemT>() instead");
						}
						if (streamPropertyType2 == StreamPropertyType.IPEndPoint)
						{
							streamPropertyType = StreamPropertyType.IPEndPoint;
						}
					}
					if (streamPropertyType == StreamPropertyType.IPEndPoint)
					{
						IPEndPoint ipendPoint = (IPEndPoint)obj;
						obj = new IPEndPoint(ipendPoint.Address, ipendPoint.Port);
					}
				}
				value = (T)((object)obj);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00021638 File Offset: 0x0001F838
		public bool TryGetListValue<ItemT>(string key, out ReadOnlyCollection<ItemT> value)
		{
			value = null;
			object obj;
			if (this.TryGetValue(key, out obj) && (obj == null || obj is IList<ItemT>))
			{
				if (obj != null)
				{
					value = new ReadOnlyCollection<ItemT>((IList<ItemT>)obj);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00021674 File Offset: 0x0001F874
		public T GetValue<T>(string name, T defaultValue)
		{
			T result;
			if (!this.TryGetValue<T>(name, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002168F File Offset: 0x0001F88F
		public void SetValue<T>(string name, T value)
		{
			this[name] = value;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002169E File Offset: 0x0001F89E
		public bool Contains(string name)
		{
			return this.ContainsKey(name);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000216A7 File Offset: 0x0001F8A7
		public override void LoadFromParentRow(DataTableCursor cursor)
		{
			this.ThrowIfReadOnlyOrDeleted();
			this.pendingDeferredLoad = true;
			this.ClearData();
			this.dirty = false;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public override void SaveToParentRow(DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			if (this.PendingDatabaseUpdates)
			{
				using (Stream stream = (this.blobCollection == null) ? this.column.OpenImmediateWriter(cursor, base.DataRow, false, 1) : this.blobCollection.OpenWriter(this.blobCollectionKey, cursor, false, false, null))
				{
					stream.SetLength(0L);
					if (this.data != null && this.data.Count > 0)
					{
						bool shouldRelease = false;
						try
						{
							shouldRelease = this.AssertOwnerAndTakeOwnership();
							this.Serialize(stream);
						}
						finally
						{
							this.AssertOwnerAndReleaseOwnership(shouldRelease);
						}
						base.DataRow.PerfCounters.ExtendedPropertyBytesWritten.IncrementBy(stream.Length);
					}
					base.DataRow.PerfCounters.ExtendedPropertyWrites.Increment();
				}
				this.dirty = false;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000217B4 File Offset: 0x0001F9B4
		public override void CloneFrom(IDataObjectComponent other)
		{
			this.ThrowIfReadOnlyOrDeleted();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ExtendedPropertyDictionary extendedPropertyDictionary = (ExtendedPropertyDictionary)other;
			this.PrepareEmptyData(extendedPropertyDictionary.Count);
			foreach (KeyValuePair<string, object> keyValuePair in extendedPropertyDictionary.Data)
			{
				object obj = keyValuePair.Value;
				StreamPropertyType streamPropertyType = (obj != null) ? ExtendedPropertyDictionary.validTypes[obj.GetType()] : StreamPropertyType.Null;
				StreamPropertyType streamPropertyType2 = streamPropertyType;
				if (streamPropertyType2 != StreamPropertyType.IPEndPoint)
				{
					switch (streamPropertyType2)
					{
					case StreamPropertyType.Bool | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array:
					case StreamPropertyType.SByte | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.Array:
					case StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array:
					case StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array:
					case StreamPropertyType.DateTime | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.Array:
					case StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array:
					case StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array:
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array:
						obj = ((Array)obj).Clone();
						break;
					case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array:
					{
						IPEndPoint[] array = (IPEndPoint[])obj;
						IPEndPoint[] array2 = new IPEndPoint[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = new IPEndPoint(array[i].Address, array[i].Port);
						}
						obj = array2;
						break;
					}
					case StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array:
						break;
					default:
						switch (streamPropertyType2)
						{
						case StreamPropertyType.Bool | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<bool>(obj as IEnumerable<bool>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<byte>(obj as IEnumerable<byte>);
							break;
						case StreamPropertyType.SByte | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<sbyte>(obj as IEnumerable<sbyte>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<short>(obj as IEnumerable<short>);
							break;
						case StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<ushort>(obj as IEnumerable<ushort>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<int>(obj as IEnumerable<int>);
							break;
						case StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<uint>(obj as IEnumerable<uint>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<long>(obj as IEnumerable<long>);
							break;
						case StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<ulong>(obj as IEnumerable<ulong>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<float>(obj as IEnumerable<float>);
							break;
						case StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<double>(obj as IEnumerable<double>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<decimal>(obj as IEnumerable<decimal>);
							break;
						case StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<char>(obj as IEnumerable<char>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<string>(obj as IEnumerable<string>);
							break;
						case StreamPropertyType.DateTime | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<DateTime>(obj as IEnumerable<DateTime>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<Guid>(obj as IEnumerable<Guid>);
							break;
						case StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<IPAddress>(obj as IEnumerable<IPAddress>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List:
						{
							List<IPEndPoint> list = (List<IPEndPoint>)obj;
							List<IPEndPoint> list2 = new List<IPEndPoint>(list.Count);
							for (int j = 0; j < list.Count; j++)
							{
								list2[j] = new IPEndPoint(list[j].Address, list[j].Port);
							}
							obj = list2;
							break;
						}
						case StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<RoutingAddress>(obj as IEnumerable<RoutingAddress>);
							break;
						case StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List:
						case StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List:
							obj = ExtendedPropertyDictionary.CloneList<ADObjectId>(obj as IEnumerable<ADObjectId>);
							break;
						}
						break;
					}
				}
				else
				{
					IPEndPoint ipendPoint = (IPEndPoint)obj;
					obj = new IPEndPoint(ipendPoint.Address, ipendPoint.Port);
				}
				this.data.Add(keyValuePair.Key, obj);
			}
			this.dirty = extendedPropertyDictionary.dirty;
			this.pendingDeferredLoad = false;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00021B70 File Offset: 0x0001FD70
		public override string ToString()
		{
			int count = this.Count;
			if (count == 0)
			{
				return "{}";
			}
			StringBuilder stringBuilder = new StringBuilder(250 * count);
			int num = 0;
			stringBuilder.Append('{');
			foreach (KeyValuePair<string, object> keyValuePair in this.data)
			{
				if (num++ > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append('=');
				stringBuilder.Append(keyValuePair.Value.ToString());
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021C34 File Offset: 0x0001FE34
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((string)key);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00021C44 File Offset: 0x0001FE44
		void IDictionary.Add(object key, object value)
		{
			string text = key as string;
			if (text == null)
			{
				throw new ArgumentException("The key must be of type string.", "key");
			}
			this.Add(text, value);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021C73 File Offset: 0x0001FE73
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IDictionary)this.Data).GetEnumerator();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021C80 File Offset: 0x0001FE80
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00021C8F File Offset: 0x0001FE8F
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.Data).CopyTo(array, index);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		public override void MinimizeMemory()
		{
			if (!Monitor.TryEnter(this))
			{
				return;
			}
			try
			{
				if (this.data != null && !this.dirty)
				{
					if (this.data.Count == 0)
					{
						this.data = null;
					}
					else
					{
						this.pendingDeferredLoad = true;
						this.ClearData();
					}
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00021D0C File Offset: 0x0001FF0C
		private static void InitValidTypes()
		{
			if (ExtendedPropertyDictionary.validTypes == null)
			{
				Dictionary<Type, StreamPropertyType> dictionary = new Dictionary<Type, StreamPropertyType>(ExtendedPropertyDictionary.SupportedTypes.Length);
				foreach (TypeEntry typeEntry in ExtendedPropertyDictionary.SupportedTypes)
				{
					dictionary.Add(typeEntry.Type, typeEntry.Identifier);
				}
				Interlocked.CompareExchange<Dictionary<Type, StreamPropertyType>>(ref ExtendedPropertyDictionary.validTypes, dictionary, null);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00021D70 File Offset: 0x0001FF70
		private static void Deserialize(Stream stream, IDictionary<string, object> dst, int numberOfPropertiesToFetch, bool doNotAddPropertyIfPresent)
		{
			TransportPropertyStreamReader transportPropertyStreamReader = new TransportPropertyStreamReader(stream);
			int num = 0;
			KeyValuePair<string, object> keyValuePair;
			while (num != numberOfPropertiesToFetch && transportPropertyStreamReader.Read(out keyValuePair))
			{
				num++;
				if ((!doNotAddPropertyIfPresent || !dst.ContainsKey(keyValuePair.Key)) && !string.IsNullOrEmpty(keyValuePair.Key))
				{
					if (dst.ContainsKey(keyValuePair.Key))
					{
						dst[keyValuePair.Key] = keyValuePair.Value;
					}
					else
					{
						dst.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021DF4 File Offset: 0x0001FFF4
		private static List<T> CloneList<T>(IEnumerable<T> source)
		{
			return new List<T>(source);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00021E0C File Offset: 0x0002000C
		private static void ValidateValue(object value)
		{
			if (value == null)
			{
				return;
			}
			Type type = value.GetType();
			if (ExtendedPropertyDictionary.validTypes.ContainsKey(type))
			{
				return;
			}
			throw new ArgumentOutOfRangeException("value", value, "value is of a type that is not assignable to the dictionary.");
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021E42 File Offset: 0x00020042
		private static void ValidateKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length > 255)
			{
				throw new ArgumentException(Strings.KeyLength, "key");
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00021E7C File Offset: 0x0002007C
		private static StreamPropertyType SerializeType(object value)
		{
			if (value == null)
			{
				return StreamPropertyType.Null;
			}
			StreamPropertyType result;
			if (ExtendedPropertyDictionary.validTypes.TryGetValue(value.GetType(), out result))
			{
				return result;
			}
			throw new InvalidOperationException("Unexpected type");
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021EB0 File Offset: 0x000200B0
		private bool HasChanged(string key, object value)
		{
			object obj;
			return !this.TryGetValue(key, out obj) || ((obj != null || value != null) && (obj == null || !obj.Equals(value)));
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00021EE0 File Offset: 0x000200E0
		private void DeferredLoad()
		{
			bool shouldRelease = false;
			try
			{
				shouldRelease = this.AssertOwnerAndTakeOwnership();
				using (DataTableCursor cursor = base.DataRow.Table.GetCursor())
				{
					using (cursor.BeginTransaction())
					{
						base.DataRow.SeekCurrent(cursor);
						using (Stream stream = (this.blobCollection == null) ? this.column.OpenImmediateReader(cursor, base.DataRow, 1) : this.blobCollection.OpenReader(this.blobCollectionKey, cursor, false))
						{
							if (stream.Length > 0L)
							{
								byte[] array = new byte[stream.Length];
								stream.Read(array, 0, (int)stream.Length);
								using (MemoryStream memoryStream = new MemoryStream(array, false))
								{
									this.PrepareEmptyData(ExtendedPropertyDictionary.initialDictionaryCapacity);
									ExtendedPropertyDictionary.Deserialize(memoryStream, this.data, int.MaxValue, false);
								}
								base.DataRow.PerfCounters.ExtendedPropertyReads.Increment();
								base.DataRow.PerfCounters.ExtendedPropertyBytesRead.IncrementBy((long)array.Length);
							}
							else
							{
								this.PrepareEmptyData(ExtendedPropertyDictionary.initialDictionaryCapacity);
							}
							this.dirty = false;
						}
					}
				}
			}
			finally
			{
				this.AssertOwnerAndReleaseOwnership(shouldRelease);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000220A0 File Offset: 0x000202A0
		private bool AssertOwnerAndTakeOwnership()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			int num = Interlocked.Exchange(ref this.ownerThread, managedThreadId);
			if (num == 0)
			{
				return true;
			}
			if (num != managedThreadId)
			{
				throw new ExtendedPropertyDictionary.InstrumentationException("Concurrent access detected", this, num);
			}
			return false;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000220DC File Offset: 0x000202DC
		private void AssertOwnerAndReleaseOwnership(bool shouldRelease)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			int num;
			if (shouldRelease)
			{
				num = Interlocked.Exchange(ref this.ownerThread, 0);
			}
			else
			{
				num = this.ownerThread;
			}
			if (num != managedThreadId)
			{
				throw new ExtendedPropertyDictionary.InstrumentationException("Concurrent access detected", this, num);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002211E File Offset: 0x0002031E
		private void ClearData()
		{
			if (this.data != null)
			{
				this.data.Clear();
				this.data = null;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00022140 File Offset: 0x00020340
		private void PrepareEmptyData(int recommendedInitialCapacity)
		{
			if (this.data == null)
			{
				this.data = this.CreateDictionary(recommendedInitialCapacity);
				return;
			}
			this.data.Clear();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00022169 File Offset: 0x00020369
		private Dictionary<string, object> CreateDictionary(int initialCapacity)
		{
			return new Dictionary<string, object>(initialCapacity, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022176 File Offset: 0x00020376
		private void ThrowIfReadOnlyOrDeleted()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("This extended property operation cannot be performed in read-only mode.");
			}
			if (base.DataRow != null && base.DataRow.IsDeleted)
			{
				throw new InvalidOperationException("This extended property operation cannot be performed in a deleted item.");
			}
		}

		// Token: 0x04000421 RID: 1057
		private const int MaxPropNameLength = 255;

		// Token: 0x04000422 RID: 1058
		private const int PooledBufferSize = 1024;

		// Token: 0x04000423 RID: 1059
		private static readonly TypeEntry[] SupportedTypes = new TypeEntry[]
		{
			new TypeEntry(typeof(DBNull), StreamPropertyType.Null),
			new TypeEntry(typeof(bool), StreamPropertyType.Bool),
			new TypeEntry(typeof(byte), StreamPropertyType.Byte),
			new TypeEntry(typeof(sbyte), StreamPropertyType.SByte),
			new TypeEntry(typeof(short), StreamPropertyType.Int16),
			new TypeEntry(typeof(ushort), StreamPropertyType.UInt16),
			new TypeEntry(typeof(int), StreamPropertyType.Int32),
			new TypeEntry(typeof(uint), StreamPropertyType.UInt32),
			new TypeEntry(typeof(long), StreamPropertyType.Int64),
			new TypeEntry(typeof(ulong), StreamPropertyType.UInt64),
			new TypeEntry(typeof(float), StreamPropertyType.Single),
			new TypeEntry(typeof(double), StreamPropertyType.Double),
			new TypeEntry(typeof(decimal), StreamPropertyType.Decimal),
			new TypeEntry(typeof(char), StreamPropertyType.Char),
			new TypeEntry(typeof(string), StreamPropertyType.String),
			new TypeEntry(typeof(DateTime), StreamPropertyType.DateTime),
			new TypeEntry(typeof(Guid), StreamPropertyType.Guid),
			new TypeEntry(typeof(IPAddress), StreamPropertyType.IPAddress),
			new TypeEntry(typeof(IPEndPoint), StreamPropertyType.IPEndPoint),
			new TypeEntry(typeof(RoutingAddress), StreamPropertyType.RoutingAddress),
			new TypeEntry(typeof(ADObjectId), StreamPropertyType.ADObjectIdUTF8),
			new TypeEntry(typeof(Microsoft.Exchange.Data.Directory.Recipient.RecipientType), StreamPropertyType.RecipientType),
			new TypeEntry(typeof(bool[]), StreamPropertyType.Bool | StreamPropertyType.Array),
			new TypeEntry(typeof(byte[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array),
			new TypeEntry(typeof(sbyte[]), StreamPropertyType.SByte | StreamPropertyType.Array),
			new TypeEntry(typeof(short[]), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.Array),
			new TypeEntry(typeof(ushort[]), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array),
			new TypeEntry(typeof(int[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array),
			new TypeEntry(typeof(uint[]), StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(long[]), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(ulong[]), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(float[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(double[]), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(decimal[]), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(char[]), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(string[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
			new TypeEntry(typeof(DateTime[]), StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(Guid[]), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(IPAddress[]), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(IPEndPoint[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(RoutingAddress[]), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(ADObjectId[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array),
			new TypeEntry(typeof(List<bool>), StreamPropertyType.Bool | StreamPropertyType.List),
			new TypeEntry(typeof(List<byte>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.List),
			new TypeEntry(typeof(List<sbyte>), StreamPropertyType.SByte | StreamPropertyType.List),
			new TypeEntry(typeof(List<short>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.List),
			new TypeEntry(typeof(List<ushort>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List),
			new TypeEntry(typeof(List<int>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List),
			new TypeEntry(typeof(List<uint>), StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<long>), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<ulong>), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<float>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<double>), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<decimal>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<char>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<string>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
			new TypeEntry(typeof(List<DateTime>), StreamPropertyType.DateTime | StreamPropertyType.List),
			new TypeEntry(typeof(List<Guid>), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.List),
			new TypeEntry(typeof(List<IPAddress>), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List),
			new TypeEntry(typeof(List<IPEndPoint>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List),
			new TypeEntry(typeof(List<RoutingAddress>), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List),
			new TypeEntry(typeof(List<ADObjectId>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List)
		};

		// Token: 0x04000424 RID: 1060
		private static readonly BufferPool bufferPool = new BufferPool(1024, true);

		// Token: 0x04000425 RID: 1061
		private static Dictionary<Type, StreamPropertyType> validTypes;

		// Token: 0x04000426 RID: 1062
		private static int initialDictionaryCapacity = 3;

		// Token: 0x04000427 RID: 1063
		private readonly DataColumn column;

		// Token: 0x04000428 RID: 1064
		private readonly byte blobCollectionKey;

		// Token: 0x04000429 RID: 1065
		private readonly BlobCollection blobCollection;

		// Token: 0x0400042A RID: 1066
		private volatile Dictionary<string, object> data;

		// Token: 0x0400042B RID: 1067
		private int ownerThread;

		// Token: 0x0400042C RID: 1068
		private volatile bool pendingDeferredLoad;

		// Token: 0x0400042D RID: 1069
		private bool dirty;

		// Token: 0x0400042E RID: 1070
		private bool isReadOnly;

		// Token: 0x020000E8 RID: 232
		public class InstrumentationException : Exception
		{
			// Token: 0x06000876 RID: 2166 RVA: 0x00022988 File Offset: 0x00020B88
			public InstrumentationException(string msg, object victim, int otherThreadId) : base(msg)
			{
				this.Victim = victim;
				this.ThisThreadId = Thread.CurrentThread.ManagedThreadId;
				this.OtherThreadId = otherThreadId;
			}

			// Token: 0x0400042F RID: 1071
			public object Victim;

			// Token: 0x04000430 RID: 1072
			public int ThisThreadId;

			// Token: 0x04000431 RID: 1073
			public int OtherThreadId;
		}
	}
}
