using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006B RID: 107
	internal class CompositePagedReader<TResult> : IPagedReader<TResult>, IEnumerable<!0>, IEnumerable where TResult : IConfigurable, new()
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public CompositePagedReader(params IPagedReader<TResult>[] subReaders)
		{
			if (subReaders == null)
			{
				throw new ArgumentNullException("subReaders");
			}
			if (subReaders.Length == 0)
			{
				throw new ArgumentException("subReaders must contain 1 or more elements");
			}
			this.readerQueue = new Queue<IPagedReader<TResult>>(subReaders);
			this.RetrievedAllData = false;
			this.PagesReturned = 0;
			this.pageSize = (from r in subReaders
			select r.PageSize).Max();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000C358 File Offset: 0x0000A558
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000C360 File Offset: 0x0000A560
		public bool RetrievedAllData { get; private set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000C369 File Offset: 0x0000A569
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000C371 File Offset: 0x0000A571
		public int PagesReturned { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000C37A File Offset: 0x0000A57A
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C384 File Offset: 0x0000A584
		public TResult[] ReadAllPages()
		{
			if (this.RetrievedAllData)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
			}
			if (this.PagesReturned > 0)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderReadAllAfterEnumerating);
			}
			List<TResult> list = new List<TResult>();
			foreach (TResult item in this)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C570 File Offset: 0x0000A770
		public IEnumerator<TResult> GetEnumerator()
		{
			if (this.RetrievedAllData)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
			}
			while (!this.RetrievedAllData)
			{
				TResult[] results = this.GetNextPage();
				foreach (TResult result in results)
				{
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C58C File Offset: 0x0000A78C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C594 File Offset: 0x0000A794
		public TResult[] GetNextPage()
		{
			if (!this.valid)
			{
				throw new InvalidOperationException("GetNextPage() called after reader was marked invalid");
			}
			if (this.RetrievedAllData)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
			}
			TResult[] result;
			try
			{
				IPagedReader<TResult> pagedReader = this.readerQueue.Peek();
				TResult[] nextPage = pagedReader.GetNextPage();
				this.PagesReturned++;
				if (pagedReader.RetrievedAllData)
				{
					this.readerQueue.Dequeue();
					this.RetrievedAllData = (this.readerQueue.Count == 0);
				}
				result = nextPage;
			}
			catch (PermanentDALException)
			{
				this.valid = false;
				this.RetrievedAllData = true;
				throw;
			}
			return result;
		}

		// Token: 0x0400027E RID: 638
		private readonly int pageSize;

		// Token: 0x0400027F RID: 639
		private bool valid = true;

		// Token: 0x04000280 RID: 640
		private Queue<IPagedReader<TResult>> readerQueue;
	}
}
