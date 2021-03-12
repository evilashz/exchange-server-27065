using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000489 RID: 1161
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AttributedValue<T> : IEquatable<AttributedValue<T>>
	{
		// Token: 0x06003381 RID: 13185 RVA: 0x000D17AA File Offset: 0x000CF9AA
		internal AttributedValue(T value, string[] attributions)
		{
			this.value = value;
			this.attributions = attributions;
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000D17C0 File Offset: 0x000CF9C0
		// (set) Token: 0x06003383 RID: 13187 RVA: 0x000D17C8 File Offset: 0x000CF9C8
		public T Value
		{
			get
			{
				return this.value;
			}
			internal set
			{
				this.value = value;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000D17D1 File Offset: 0x000CF9D1
		// (set) Token: 0x06003385 RID: 13189 RVA: 0x000D17D9 File Offset: 0x000CF9D9
		public string[] Attributions
		{
			get
			{
				return this.attributions;
			}
			internal set
			{
				this.attributions = value;
			}
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000D17E4 File Offset: 0x000CF9E4
		public static void AddToList(List<AttributedValue<T>> list, AttributedValue<T> attributedValue)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (attributedValue == null)
			{
				throw new ArgumentNullException("attributedValue");
			}
			bool flag = false;
			foreach (AttributedValue<T> attributedValue2 in list)
			{
				if (AttributedValue<T>.ValuesEqual(attributedValue2.Value, attributedValue.Value))
				{
					flag = true;
					List<string> list2 = new List<string>();
					foreach (string item in attributedValue2.Attributions)
					{
						if (!list2.Contains(item))
						{
							list2.Add(item);
						}
					}
					foreach (string item2 in attributedValue.Attributions)
					{
						if (!list2.Contains(item2))
						{
							list2.Add(item2);
						}
					}
					attributedValue2.Attributions = list2.ToArray();
					break;
				}
			}
			if (!flag)
			{
				list.Add(attributedValue);
			}
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000D18F4 File Offset: 0x000CFAF4
		public static T GetValueFromAttribution(List<AttributedValue<T>> list, string attribution)
		{
			T result = default(T);
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (string.IsNullOrWhiteSpace(attribution))
			{
				throw new ArgumentNullException("attribution");
			}
			bool flag = false;
			foreach (AttributedValue<T> attributedValue in list)
			{
				foreach (string a in attributedValue.Attributions)
				{
					if (string.Equals(a, attribution, StringComparison.OrdinalIgnoreCase))
					{
						result = attributedValue.Value;
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000D19A4 File Offset: 0x000CFBA4
		public bool Equals(AttributedValue<T> other)
		{
			return other != null && (object.ReferenceEquals(this, other) || (AttributedValue<T>.ArraysEqual(this.attributions, other.attributions) && AttributedValue<T>.ValuesEqual(this.Value, other.Value)));
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000D19F4 File Offset: 0x000CFBF4
		private static bool ValuesEqual(object value1, object value2)
		{
			if (value1 == null && value2 == null)
			{
				return true;
			}
			if (value1 == null || value2 == null)
			{
				return false;
			}
			if (!value1.GetType().Equals(value2.GetType()))
			{
				return false;
			}
			if (value1 is string)
			{
				return ((string)value1).Equals((string)value2);
			}
			if (value1 is IEquatable<T>)
			{
				return ((IEquatable<T>)value1).Equals((T)((object)value2));
			}
			return value1.GetType().IsArray && AttributedValue<T>.ArraysEqual(value1, value2);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000D1A70 File Offset: 0x000CFC70
		private static bool ArraysEqual(object value1, object value2)
		{
			Array array = value1 as Array;
			Array array2 = value2 as Array;
			if (array.Length != array2.Length)
			{
				return false;
			}
			bool[] array3 = new bool[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < array2.Length; j++)
				{
					if (!array3[j] && AttributedValue<T>.ValuesEqual(array.GetValue(i), array2.GetValue(j)))
					{
						array3[j] = true;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001BCB RID: 7115
		private T value;

		// Token: 0x04001BCC RID: 7116
		private string[] attributions;
	}
}
