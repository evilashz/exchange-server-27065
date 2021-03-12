using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009BB RID: 2491
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x0600637B RID: 25467 RVA: 0x001521FD File Offset: 0x001503FD
		public ReadOnlyDictionaryValueEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x00152226 File Offset: 0x00150426
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x00152233 File Offset: 0x00150433
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600637E RID: 25470 RVA: 0x00152240 File Offset: 0x00150440
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x00152250 File Offset: 0x00150450
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x00152270 File Offset: 0x00150470
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002C3B RID: 11323
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002C3C RID: 11324
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
