using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000068 RID: 104
	internal class CachedIterator<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0000C0C9 File Offset: 0x0000A2C9
		public CachedIterator(IEnumerator<T> enumerator)
		{
			if (enumerator == null)
			{
				throw new ArgumentNullException("enumerator");
			}
			this.wrappedEnumerator = enumerator;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C290 File Offset: 0x0000A490
		public IEnumerator<T> GetEnumerator()
		{
			foreach (T element in this.cachedResults)
			{
				yield return element;
			}
			while (this.wrappedEnumerator.MoveNext())
			{
				this.cachedResults.Add(this.wrappedEnumerator.Current);
				yield return this.wrappedEnumerator.Current;
			}
			yield break;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400027C RID: 636
		private List<T> cachedResults = new List<T>();

		// Token: 0x0400027D RID: 637
		private IEnumerator<T> wrappedEnumerator;
	}
}
