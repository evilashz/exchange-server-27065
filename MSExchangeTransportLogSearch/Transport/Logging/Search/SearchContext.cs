using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000035 RID: 53
	internal class SearchContext
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00008D18 File Offset: 0x00006F18
		public SearchContext(LogEvaluator evaluator, LogCursor cursor)
		{
			this.evaluator = evaluator;
			if (evaluator.Searches != null && evaluator.Searches.Count != 0)
			{
				this.searchEnumerator = evaluator.Searches.GetEnumerator();
				this.searchEnumerator.MoveNext();
			}
			this.cursor = cursor;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008D70 File Offset: 0x00006F70
		public bool MoveNext()
		{
			if (this.searchEnumerator == null)
			{
				return this.LinearMoveNext();
			}
			return this.IndexedMoveNext();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008D87 File Offset: 0x00006F87
		public bool LinearMoveNext()
		{
			while (this.cursor.MoveNext())
			{
				if (this.evaluator.Evaluate(this.cursor))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public bool IndexedMoveNext()
		{
			for (;;)
			{
				IndexedSearch indexedSearch = this.searchEnumerator.Current;
				while (!this.cursor.SearchNext(indexedSearch.ColumnName, indexedSearch.Value))
				{
					this.cursor.ResetSearchContext();
					if (!this.searchEnumerator.MoveNext())
					{
						return false;
					}
					indexedSearch = this.searchEnumerator.Current;
				}
				object field = this.cursor.GetField(0);
				if (field != null)
				{
					DateTime t = (DateTime)field;
					if (!(t < this.cursor.Begin) && !(t > this.cursor.End) && this.evaluator.Evaluate(this.cursor))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040000CF RID: 207
		private LogEvaluator evaluator;

		// Token: 0x040000D0 RID: 208
		private IEnumerator<IndexedSearch> searchEnumerator;

		// Token: 0x040000D1 RID: 209
		private LogCursor cursor;
	}
}
