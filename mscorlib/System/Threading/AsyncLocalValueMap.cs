using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x020004BC RID: 1212
	internal static class AsyncLocalValueMap
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000DDAF7 File Offset: 0x000DBCF7
		public static IAsyncLocalValueMap Empty { get; } = new AsyncLocalValueMap.EmptyAsyncLocalValueMap();

		// Token: 0x06003A68 RID: 14952 RVA: 0x000DDAFE File Offset: 0x000DBCFE
		public static bool IsEmpty(IAsyncLocalValueMap asyncLocalValueMap)
		{
			return asyncLocalValueMap == AsyncLocalValueMap.Empty;
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000DDB08 File Offset: 0x000DBD08
		public static IAsyncLocalValueMap Create(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
		{
			if (value == null && treatNullValueAsNonexistent)
			{
				return AsyncLocalValueMap.Empty;
			}
			return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(key, value);
		}

		// Token: 0x02000BAF RID: 2991
		private sealed class EmptyAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06006E11 RID: 28177 RVA: 0x0017A4D8 File Offset: 0x001786D8
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				if (value == null && treatNullValueAsNonexistent)
				{
					return this;
				}
				return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(key, value);
			}

			// Token: 0x06006E12 RID: 28178 RVA: 0x0017A4F8 File Offset: 0x001786F8
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				value = null;
				return false;
			}
		}

		// Token: 0x02000BB0 RID: 2992
		private sealed class OneElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06006E14 RID: 28180 RVA: 0x0017A506 File Offset: 0x00178706
			public OneElementAsyncLocalValueMap(IAsyncLocal key, object value)
			{
				this._key1 = key;
				this._value1 = value;
			}

			// Token: 0x06006E15 RID: 28181 RVA: 0x0017A51C File Offset: 0x0017871C
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				if (value != null || !treatNullValueAsNonexistent)
				{
					if (key != this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, key, value);
					}
					return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(key, value);
				}
				else
				{
					if (key != this._key1)
					{
						return this;
					}
					return AsyncLocalValueMap.Empty;
				}
			}

			// Token: 0x06006E16 RID: 28182 RVA: 0x0017A56A File Offset: 0x0017876A
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x04003515 RID: 13589
			private readonly IAsyncLocal _key1;

			// Token: 0x04003516 RID: 13590
			private readonly object _value1;
		}

		// Token: 0x02000BB1 RID: 2993
		private sealed class TwoElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06006E17 RID: 28183 RVA: 0x0017A583 File Offset: 0x00178783
			public TwoElementAsyncLocalValueMap(IAsyncLocal key1, object value1, IAsyncLocal key2, object value2)
			{
				this._key1 = key1;
				this._value1 = value1;
				this._key2 = key2;
				this._value2 = value2;
			}

			// Token: 0x06006E18 RID: 28184 RVA: 0x0017A5A8 File Offset: 0x001787A8
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				if (value != null || !treatNullValueAsNonexistent)
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(key, value, this._key2, this._value2);
					}
					if (key != this._key2)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2, key, value);
					}
					return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, key, value);
				}
				else
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(this._key2, this._value2);
					}
					if (key != this._key2)
					{
						return this;
					}
					return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(this._key1, this._value1);
				}
			}

			// Token: 0x06006E19 RID: 28185 RVA: 0x0017A658 File Offset: 0x00178858
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				if (key == this._key2)
				{
					value = this._value2;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x04003517 RID: 13591
			private readonly IAsyncLocal _key1;

			// Token: 0x04003518 RID: 13592
			private readonly IAsyncLocal _key2;

			// Token: 0x04003519 RID: 13593
			private readonly object _value1;

			// Token: 0x0400351A RID: 13594
			private readonly object _value2;
		}

		// Token: 0x02000BB2 RID: 2994
		private sealed class ThreeElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06006E1A RID: 28186 RVA: 0x0017A684 File Offset: 0x00178884
			public ThreeElementAsyncLocalValueMap(IAsyncLocal key1, object value1, IAsyncLocal key2, object value2, IAsyncLocal key3, object value3)
			{
				this._key1 = key1;
				this._value1 = value1;
				this._key2 = key2;
				this._value2 = value2;
				this._key3 = key3;
				this._value3 = value3;
			}

			// Token: 0x06006E1B RID: 28187 RVA: 0x0017A6BC File Offset: 0x001788BC
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				if (value != null || !treatNullValueAsNonexistent)
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(key, value, this._key2, this._value2, this._key3, this._value3);
					}
					if (key == this._key2)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, key, value, this._key3, this._value3);
					}
					if (key == this._key3)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2, key, value);
					}
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(4);
					multiElementAsyncLocalValueMap.UnsafeStore(0, this._key1, this._value1);
					multiElementAsyncLocalValueMap.UnsafeStore(1, this._key2, this._value2);
					multiElementAsyncLocalValueMap.UnsafeStore(2, this._key3, this._value3);
					multiElementAsyncLocalValueMap.UnsafeStore(3, key, value);
					return multiElementAsyncLocalValueMap;
				}
				else
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key2, this._value2, this._key3, this._value3);
					}
					if (key == this._key2)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, this._key3, this._value3);
					}
					if (key != this._key3)
					{
						return this;
					}
					return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2);
				}
			}

			// Token: 0x06006E1C RID: 28188 RVA: 0x0017A816 File Offset: 0x00178A16
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				if (key == this._key2)
				{
					value = this._value2;
					return true;
				}
				if (key == this._key3)
				{
					value = this._value3;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x0400351B RID: 13595
			private readonly IAsyncLocal _key1;

			// Token: 0x0400351C RID: 13596
			private readonly IAsyncLocal _key2;

			// Token: 0x0400351D RID: 13597
			private readonly IAsyncLocal _key3;

			// Token: 0x0400351E RID: 13598
			private readonly object _value1;

			// Token: 0x0400351F RID: 13599
			private readonly object _value2;

			// Token: 0x04003520 RID: 13600
			private readonly object _value3;
		}

		// Token: 0x02000BB3 RID: 2995
		private sealed class MultiElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06006E1D RID: 28189 RVA: 0x0017A855 File Offset: 0x00178A55
			internal MultiElementAsyncLocalValueMap(int count)
			{
				this._keyValues = new KeyValuePair<IAsyncLocal, object>[count];
			}

			// Token: 0x06006E1E RID: 28190 RVA: 0x0017A869 File Offset: 0x00178A69
			internal void UnsafeStore(int index, IAsyncLocal key, object value)
			{
				this._keyValues[index] = new KeyValuePair<IAsyncLocal, object>(key, value);
			}

			// Token: 0x06006E1F RID: 28191 RVA: 0x0017A880 File Offset: 0x00178A80
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				int i = 0;
				while (i < this._keyValues.Length)
				{
					if (key == this._keyValues[i].Key)
					{
						if (value != null || !treatNullValueAsNonexistent)
						{
							AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length);
							Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap._keyValues, 0, this._keyValues.Length);
							multiElementAsyncLocalValueMap._keyValues[i] = new KeyValuePair<IAsyncLocal, object>(key, value);
							return multiElementAsyncLocalValueMap;
						}
						if (this._keyValues.Length != 4)
						{
							AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap2 = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length - 1);
							if (i != 0)
							{
								Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap2._keyValues, 0, i);
							}
							if (i != this._keyValues.Length - 1)
							{
								Array.Copy(this._keyValues, i + 1, multiElementAsyncLocalValueMap2._keyValues, i, this._keyValues.Length - i - 1);
							}
							return multiElementAsyncLocalValueMap2;
						}
						if (i == 0)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[2].Key, this._keyValues[2].Value, this._keyValues[3].Key, this._keyValues[3].Value);
						}
						if (i == 1)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[2].Key, this._keyValues[2].Value, this._keyValues[3].Key, this._keyValues[3].Value);
						}
						if (i != 2)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[2].Key, this._keyValues[2].Value);
						}
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[3].Key, this._keyValues[3].Value);
					}
					else
					{
						i++;
					}
				}
				if (value == null && treatNullValueAsNonexistent)
				{
					return this;
				}
				if (this._keyValues.Length < 16)
				{
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap3 = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length + 1);
					Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap3._keyValues, 0, this._keyValues.Length);
					multiElementAsyncLocalValueMap3._keyValues[this._keyValues.Length] = new KeyValuePair<IAsyncLocal, object>(key, value);
					return multiElementAsyncLocalValueMap3;
				}
				AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(17);
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this._keyValues)
				{
					manyElementAsyncLocalValueMap[keyValuePair.Key] = keyValuePair.Value;
				}
				manyElementAsyncLocalValueMap[key] = value;
				return manyElementAsyncLocalValueMap;
			}

			// Token: 0x06006E20 RID: 28192 RVA: 0x0017ABE0 File Offset: 0x00178DE0
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this._keyValues)
				{
					if (key == keyValuePair.Key)
					{
						value = keyValuePair.Value;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x04003521 RID: 13601
			internal const int MaxMultiElements = 16;

			// Token: 0x04003522 RID: 13602
			private readonly KeyValuePair<IAsyncLocal, object>[] _keyValues;
		}

		// Token: 0x02000BB4 RID: 2996
		private sealed class ManyElementAsyncLocalValueMap : Dictionary<IAsyncLocal, object>, IAsyncLocalValueMap
		{
			// Token: 0x06006E21 RID: 28193 RVA: 0x0017AC23 File Offset: 0x00178E23
			public ManyElementAsyncLocalValueMap(int capacity) : base(capacity)
			{
			}

			// Token: 0x06006E22 RID: 28194 RVA: 0x0017AC2C File Offset: 0x00178E2C
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent)
			{
				int count = base.Count;
				bool flag = base.ContainsKey(key);
				if (value != null || !treatNullValueAsNonexistent)
				{
					AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(count + (flag ? 0 : 1));
					foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this)
					{
						manyElementAsyncLocalValueMap[keyValuePair.Key] = keyValuePair.Value;
					}
					manyElementAsyncLocalValueMap[key] = value;
					return manyElementAsyncLocalValueMap;
				}
				if (!flag)
				{
					return this;
				}
				if (count == 17)
				{
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(16);
					int num = 0;
					foreach (KeyValuePair<IAsyncLocal, object> keyValuePair2 in this)
					{
						if (key != keyValuePair2.Key)
						{
							multiElementAsyncLocalValueMap.UnsafeStore(num++, keyValuePair2.Key, keyValuePair2.Value);
						}
					}
					return multiElementAsyncLocalValueMap;
				}
				AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap2 = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(count - 1);
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair3 in this)
				{
					if (key != keyValuePair3.Key)
					{
						manyElementAsyncLocalValueMap2[keyValuePair3.Key] = keyValuePair3.Value;
					}
				}
				return manyElementAsyncLocalValueMap2;
			}
		}
	}
}
