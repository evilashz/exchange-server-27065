using System;
using System.Collections;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000580 RID: 1408
	internal struct CerHashtable<K, V> where K : class
	{
		// Token: 0x06004244 RID: 16964 RVA: 0x000F5ADC File Offset: 0x000F3CDC
		private static int GetHashCodeHelper(K key)
		{
			string text = key as string;
			if (text == null)
			{
				return key.GetHashCode();
			}
			return text.GetLegacyNonRandomizedHashCode();
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000F5B0C File Offset: 0x000F3D0C
		private void Rehash(int newSize)
		{
			CerHashtable<K, V>.Table table = new CerHashtable<K, V>.Table(newSize);
			CerHashtable<K, V>.Table table2 = this.m_Table;
			if (table2 != null)
			{
				K[] keys = table2.m_keys;
				V[] values = table2.m_values;
				for (int i = 0; i < keys.Length; i++)
				{
					K k = keys[i];
					if (k != null)
					{
						table.Insert(k, values[i]);
					}
				}
			}
			Volatile.Write<CerHashtable<K, V>.Table>(ref this.m_Table, table);
		}

		// Token: 0x170009D1 RID: 2513
		internal V this[K key]
		{
			get
			{
				CerHashtable<K, V>.Table table = Volatile.Read<CerHashtable<K, V>.Table>(ref this.m_Table);
				if (table == null)
				{
					return default(V);
				}
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = table.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = Volatile.Read<K>(ref keys[num2]);
					if (k == null)
					{
						goto IL_7F;
					}
					if (k.Equals(key))
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				return table.m_values[num2];
				IL_7F:
				return default(V);
			}
			set
			{
				CerHashtable<K, V>.Table table = this.m_Table;
				if (table != null)
				{
					int num = 2 * (table.m_count + 1);
					if (num >= table.m_keys.Length)
					{
						this.Rehash(num);
					}
				}
				else
				{
					this.Rehash(7);
				}
				this.m_Table.Insert(key, value);
			}
		}

		// Token: 0x04001B33 RID: 6963
		private CerHashtable<K, V>.Table m_Table;

		// Token: 0x04001B34 RID: 6964
		private const int MinSize = 7;

		// Token: 0x02000C02 RID: 3074
		private class Table
		{
			// Token: 0x06006F0F RID: 28431 RVA: 0x0017E0EC File Offset: 0x0017C2EC
			internal Table(int size)
			{
				size = HashHelpers.GetPrime(size);
				this.m_keys = new K[size];
				this.m_values = new V[size];
			}

			// Token: 0x06006F10 RID: 28432 RVA: 0x0017E114 File Offset: 0x0017C314
			internal void Insert(K key, V value)
			{
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = this.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = keys[num2];
					if (k == null)
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				this.m_count++;
				this.m_values[num2] = value;
				Volatile.Write<K>(ref keys[num2], key);
			}

			// Token: 0x04003646 RID: 13894
			internal K[] m_keys;

			// Token: 0x04003647 RID: 13895
			internal V[] m_values;

			// Token: 0x04003648 RID: 13896
			internal int m_count;
		}
	}
}
