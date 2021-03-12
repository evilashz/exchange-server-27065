using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000057 RID: 87
	public class RefCountedSet<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008749 File Offset: 0x00006949
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008758 File Offset: 0x00006958
		public void Add(T item)
		{
			if (!this.dictionary.ContainsKey(item))
			{
				this.dictionary[item] = 0;
			}
			Dictionary<T, int> dictionary;
			(dictionary = this.dictionary)[item] = dictionary[item] + 1;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000879C File Offset: 0x0000699C
		public bool Remove(T item)
		{
			if (!this.dictionary.ContainsKey(item))
			{
				return false;
			}
			Dictionary<T, int> dictionary;
			(dictionary = this.dictionary)[item] = dictionary[item] - 1;
			return this.dictionary[item] == 0 && this.dictionary.Remove(item);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000087EE File Offset: 0x000069EE
		public void Clear()
		{
			this.dictionary.Clear();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008938 File Offset: 0x00006B38
		public IEnumerator<T> GetEnumerator()
		{
			foreach (KeyValuePair<T, int> kvp in this.dictionary)
			{
				KeyValuePair<T, int> keyValuePair = kvp;
				yield return keyValuePair.Key;
			}
			yield break;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008954 File Offset: 0x00006B54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000194 RID: 404
		private Dictionary<T, int> dictionary = new Dictionary<T, int>();
	}
}
