using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200003B RID: 59
	internal abstract class QueryResultsEnumerator : IEnumerator<List<object[]>>, IDisposable, IEnumerator
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000DB49 File Offset: 0x0000BD49
		protected QueryResultsEnumerator(QueryResult queryResult, int batchSize)
		{
			this.queryResult = queryResult;
			this.batchSize = batchSize;
			this.disposed = false;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000DB66 File Offset: 0x0000BD66
		protected QueryResultsEnumerator(QueryResult queryResult) : this(queryResult, 2000)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public List<object[]> Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public virtual void Dispose()
		{
			if (!this.disposed && this.queryResult != null)
			{
				this.queryResult.Dispose();
				this.disposed = true;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		object IEnumerator.Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public virtual bool MoveNext()
		{
			this.current = new List<object[]>(this.batchSize);
			if (this.queryResult == null)
			{
				return false;
			}
			Exception ex = null;
			int i = 0;
			try
			{
				while (i < this.batchSize)
				{
					object[][] rows = this.queryResult.GetRows(100);
					if (rows.Length <= 0)
					{
						break;
					}
					i += rows.Length;
					this.current.AddRange(rows);
					if (!this.ProcessResults(rows))
					{
						break;
					}
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (InvalidFolderLanguageIdException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				this.HandleException(ex);
				i = 0;
			}
			return i > 0;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DC48 File Offset: 0x0000BE48
		public virtual void Reset()
		{
			if (this.queryResult != null)
			{
				this.queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
			}
		}

		// Token: 0x06000227 RID: 551
		protected abstract bool ProcessResults(object[][] partialResults);

		// Token: 0x06000228 RID: 552
		protected abstract void HandleException(Exception ex);

		// Token: 0x040001BD RID: 445
		private readonly QueryResult queryResult;

		// Token: 0x040001BE RID: 446
		private readonly int batchSize;

		// Token: 0x040001BF RID: 447
		private List<object[]> current;

		// Token: 0x040001C0 RID: 448
		private bool disposed;
	}
}
