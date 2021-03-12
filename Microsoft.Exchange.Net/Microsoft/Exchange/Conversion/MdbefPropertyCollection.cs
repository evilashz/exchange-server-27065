using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x020006AC RID: 1708
	internal class MdbefPropertyCollection : IDictionary<uint, object>, ICollection<KeyValuePair<uint, object>>, IEnumerable<KeyValuePair<uint, object>>, IEnumerable
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0003BDED File Offset: 0x00039FED
		public int Count
		{
			get
			{
				return this.properties.Count;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0003BDFA File Offset: 0x00039FFA
		public ICollection<uint> Keys
		{
			get
			{
				return this.properties.Keys;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0003BE07 File Offset: 0x0003A007
		public ICollection<object> Values
		{
			get
			{
				return this.properties.Values;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0003BE14 File Offset: 0x0003A014
		bool ICollection<KeyValuePair<uint, object>>.IsReadOnly
		{
			get
			{
				ICollection<KeyValuePair<uint, object>> collection = this.properties;
				return collection.IsReadOnly;
			}
		}

		// Token: 0x17000851 RID: 2129
		public object this[uint id]
		{
			get
			{
				return this.properties[id];
			}
			set
			{
				if (value == null)
				{
					this.properties.Remove(id);
					return;
				}
				MapiPropType propType = (MapiPropType)(id & 65535U);
				if (!MdbefPropertyCollection.TypeValid(propType, value))
				{
					throw new MdbefException("Property type is invalid for this tag");
				}
				this.properties[id] = value;
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0003BE88 File Offset: 0x0003A088
		public static MdbefPropertyCollection Create(byte[] blob, int startIndex, int length)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			MdbefReader mdbefReader = new MdbefReader(blob, startIndex, length);
			while (mdbefReader.ReadNextProperty())
			{
				mdbefPropertyCollection[(uint)mdbefReader.PropertyId] = mdbefReader.Value;
			}
			return mdbefPropertyCollection;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		public bool ContainsKey(uint key)
		{
			return this.properties.ContainsKey(key);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0003BECF File Offset: 0x0003A0CF
		public bool Remove(uint key)
		{
			return this.properties.Remove(key);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
		public byte[] GetBytes()
		{
			int num = 4;
			IEnumerator<KeyValuePair<uint, object>> enumerator = this.properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<uint, object> keyValuePair = enumerator.Current;
				MapiPropType propType = (MapiPropType)(keyValuePair.Key & 65535U);
				num += MdbefWriter.SizeOf(propType, keyValuePair.Value);
			}
			byte[] array = new byte[num];
			int offset = 0;
			offset = ExBitConverter.Write((uint)this.properties.Count, array, offset);
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				KeyValuePair<uint, object> keyValuePair2 = enumerator.Current;
				MdbefWriter.SerializeProperty(keyValuePair2.Key, keyValuePair2.Value, array, ref offset);
			}
			return array;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0003BF81 File Offset: 0x0003A181
		public virtual void Clear()
		{
			this.properties.Clear();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0003BF90 File Offset: 0x0003A190
		public void Add(uint key, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			MapiPropType propType = (MapiPropType)(key & 65535U);
			if (!MdbefPropertyCollection.TypeValid(propType, value))
			{
				throw new MdbefException("Property type is invalid for this tag");
			}
			this.properties.Add(key, value);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		public bool TryGetValue(uint key, out object value)
		{
			return this.properties.TryGetValue(key, out value);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0003BFE3 File Offset: 0x0003A1E3
		public IEnumerator<KeyValuePair<uint, object>> GetEnumerator()
		{
			return this.properties.GetEnumerator();
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0003BFF5 File Offset: 0x0003A1F5
		void ICollection<KeyValuePair<uint, object>>.Add(KeyValuePair<uint, object> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0003C00C File Offset: 0x0003A20C
		bool ICollection<KeyValuePair<uint, object>>.Contains(KeyValuePair<uint, object> item)
		{
			ICollection<KeyValuePair<uint, object>> collection = this.properties;
			return collection.Contains(item);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0003C028 File Offset: 0x0003A228
		void ICollection<KeyValuePair<uint, object>>.CopyTo(KeyValuePair<uint, object>[] array, int arrayIndex)
		{
			ICollection<KeyValuePair<uint, object>> collection = this.properties;
			collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0003C044 File Offset: 0x0003A244
		bool ICollection<KeyValuePair<uint, object>>.Remove(KeyValuePair<uint, object> item)
		{
			ICollection<KeyValuePair<uint, object>> collection = this.properties;
			return collection.Remove(item);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0003C060 File Offset: 0x0003A260
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable enumerable = this.properties;
			return enumerable.GetEnumerator();
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0003C07C File Offset: 0x0003A27C
		private static bool TypeValid(MapiPropType propType, object value)
		{
			if (propType <= MapiPropType.ServerId)
			{
				if (propType <= MapiPropType.String)
				{
					switch (propType)
					{
					case MapiPropType.Short:
						return value is short;
					case MapiPropType.Int:
						return value is int;
					case MapiPropType.Float:
						return value is float;
					case MapiPropType.Double:
					case MapiPropType.AppTime:
						return value is double;
					case MapiPropType.Currency:
						break;
					case (MapiPropType)8:
					case (MapiPropType)9:
					case MapiPropType.Error:
						return false;
					case MapiPropType.Boolean:
						return value is bool;
					default:
						if (propType != MapiPropType.Long)
						{
							switch (propType)
							{
							case MapiPropType.AnsiString:
							case MapiPropType.String:
								return value is string;
							default:
								return false;
							}
						}
						break;
					}
					return value is long;
				}
				if (propType == MapiPropType.SysTime)
				{
					return value is DateTime;
				}
				if (propType == MapiPropType.Guid)
				{
					return value is Guid;
				}
				if (propType != MapiPropType.ServerId)
				{
					return false;
				}
			}
			else if (propType <= MapiPropType.LongArray)
			{
				if (propType != MapiPropType.Binary)
				{
					switch (propType)
					{
					case MapiPropType.ShortArray:
						return value is short[];
					case MapiPropType.IntArray:
						return value is int[];
					case MapiPropType.FloatArray:
						return value is float[];
					case MapiPropType.DoubleArray:
					case MapiPropType.AppTimeArray:
						return value is double[];
					case MapiPropType.CurrencyArray:
						break;
					default:
						if (propType != MapiPropType.LongArray)
						{
							return false;
						}
						break;
					}
					return value is long[];
				}
			}
			else if (propType <= MapiPropType.SysTimeArray)
			{
				switch (propType)
				{
				case MapiPropType.AnsiStringArray:
				case MapiPropType.StringArray:
					return value is string[];
				default:
					if (propType != MapiPropType.SysTimeArray)
					{
						return false;
					}
					return value is DateTime[];
				}
			}
			else
			{
				if (propType == MapiPropType.GuidArray)
				{
					return value is Guid[];
				}
				if (propType != MapiPropType.BinaryArray)
				{
					return false;
				}
				return value is byte[][];
			}
			return value is byte[];
		}

		// Token: 0x04001ECC RID: 7884
		private Dictionary<uint, object> properties = new Dictionary<uint, object>();
	}
}
