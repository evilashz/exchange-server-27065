using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A2 RID: 2466
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryValueCollection<TKey, TValue> : ICollection<!1>, IEnumerable<!1>, IEnumerable
	{
		// Token: 0x060062D3 RID: 25299 RVA: 0x00150333 File Offset: 0x0014E533
		public DictionaryValueCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x00150350 File Offset: 0x0014E550
		public void CopyTo(TValue[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Length <= index && this.Count > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_IndexOutOfRangeException"));
			}
			if (array.Length - index < this.dictionary.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			int num = index;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.dictionary)
			{
				array[num++] = keyValuePair.Value;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x00150408 File Offset: 0x0014E608
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060062D6 RID: 25302 RVA: 0x00150415 File Offset: 0x0014E615
		bool ICollection<!1>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x00150418 File Offset: 0x0014E618
		void ICollection<!1>.Add(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x00150429 File Offset: 0x0014E629
		void ICollection<!1>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x0015043C File Offset: 0x0014E63C
		public bool Contains(TValue item)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (TValue y in this)
			{
				if (@default.Equals(item, y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x00150494 File Offset: 0x0014E694
		bool ICollection<!1>.Remove(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x001504A5 File Offset: 0x0014E6A5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!1>)this).GetEnumerator();
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x001504AD File Offset: 0x0014E6AD
		public IEnumerator<TValue> GetEnumerator()
		{
			return new DictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002C2D RID: 11309
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
