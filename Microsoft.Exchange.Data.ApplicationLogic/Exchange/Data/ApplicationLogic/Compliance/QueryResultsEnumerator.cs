using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000CA RID: 202
	internal abstract class QueryResultsEnumerator : IEnumerator<List<object[]>>, IDisposable, IEnumerator
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x000225EA File Offset: 0x000207EA
		protected QueryResultsEnumerator(QueryResult queryResult, int batchSize)
		{
			this.queryResult = queryResult;
			this.batchSize = batchSize;
			this.disposed = false;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00022607 File Offset: 0x00020807
		protected QueryResultsEnumerator(QueryResult queryResult) : this(queryResult, 2000)
		{
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00022615 File Offset: 0x00020815
		public List<object[]> Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0002261D File Offset: 0x0002081D
		object IEnumerator.Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00022625 File Offset: 0x00020825
		public virtual void Dispose()
		{
			if (!this.disposed && this.queryResult != null)
			{
				this.queryResult.Dispose();
				this.disposed = true;
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002264C File Offset: 0x0002084C
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

		// Token: 0x06000899 RID: 2201 RVA: 0x000226E4 File Offset: 0x000208E4
		public virtual void Reset()
		{
			if (this.queryResult != null)
			{
				this.queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
			}
		}

		// Token: 0x0600089A RID: 2202
		protected abstract void HandleException(Exception ex);

		// Token: 0x040003C8 RID: 968
		private readonly QueryResult queryResult;

		// Token: 0x040003C9 RID: 969
		private readonly int batchSize;

		// Token: 0x040003CA RID: 970
		private List<object[]> current;

		// Token: 0x040003CB RID: 971
		private bool disposed;
	}
}
