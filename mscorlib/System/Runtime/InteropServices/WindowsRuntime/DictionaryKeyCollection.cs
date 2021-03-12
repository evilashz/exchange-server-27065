using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A0 RID: 2464
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryKeyCollection<TKey, TValue> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060062C3 RID: 25283 RVA: 0x00150172 File Offset: 0x0014E372
		public DictionaryKeyCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x00150190 File Offset: 0x0014E390
		public void CopyTo(TKey[] array, int index)
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
				array[num++] = keyValuePair.Key;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060062C5 RID: 25285 RVA: 0x00150248 File Offset: 0x0014E448
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060062C6 RID: 25286 RVA: 0x00150255 File Offset: 0x0014E455
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x00150258 File Offset: 0x0014E458
		void ICollection<!0>.Add(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x00150269 File Offset: 0x0014E469
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x0015027A File Offset: 0x0014E47A
		public bool Contains(TKey item)
		{
			return this.dictionary.ContainsKey(item);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x00150288 File Offset: 0x0014E488
		bool ICollection<!0>.Remove(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x00150299 File Offset: 0x0014E499
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x001502A1 File Offset: 0x0014E4A1
		public IEnumerator<TKey> GetEnumerator()
		{
			return new DictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002C2A RID: 11306
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
