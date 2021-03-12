using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D7 RID: 471
	internal abstract class RequestIndexEntryHandler<T> : IRequestIndexEntryHandler where T : class, IRequestIndexEntry
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x0002BC12 File Offset: 0x00029E12
		public virtual T CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, IConfigurationSession session)
		{
			throw new NotSupportedException("Unless overridden, a handler does not support creating IRequestIndexEntries using IConfigurationSession.");
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0002BC1E File Offset: 0x00029E1E
		public virtual T CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, RequestIndexId requestIndexId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600136E RID: 4974
		public abstract void Delete(RequestIndexEntryProvider requestIndexEntryProvider, T instance);

		// Token: 0x0600136F RID: 4975 RVA: 0x0002BC25 File Offset: 0x00029E25
		public virtual T[] Find(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return Array<T>.Empty;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0002BC2C File Offset: 0x00029E2C
		public virtual IEnumerable<T> FindPaged(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return Array<T>.Empty;
		}

		// Token: 0x06001371 RID: 4977
		public abstract T Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity);

		// Token: 0x06001372 RID: 4978
		public abstract void Save(RequestIndexEntryProvider requestIndexEntryProvider, T instance);

		// Token: 0x06001373 RID: 4979 RVA: 0x0002BC33 File Offset: 0x00029E33
		IRequestIndexEntry IRequestIndexEntryHandler.CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, IConfigurationSession session)
		{
			return this.CreateRequestIndexEntryFromRequestJob(requestJob, session);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0002BC42 File Offset: 0x00029E42
		IRequestIndexEntry IRequestIndexEntryHandler.CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, RequestIndexId requestIndexId)
		{
			return this.CreateRequestIndexEntryFromRequestJob(requestJob, requestIndexId);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0002BC51 File Offset: 0x00029E51
		void IRequestIndexEntryHandler.Delete(RequestIndexEntryProvider requestIndexEntryProvider, IRequestIndexEntry instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			this.Delete(requestIndexEntryProvider, (T)((object)instance));
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0002BC6E File Offset: 0x00029E6E
		IRequestIndexEntry[] IRequestIndexEntryHandler.Find(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return (IRequestIndexEntry[])this.Find(requestIndexEntryProvider, filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0002BC82 File Offset: 0x00029E82
		IEnumerable<IRequestIndexEntry> IRequestIndexEntryHandler.FindPaged(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return this.FindPaged(requestIndexEntryProvider, filter, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0002BC94 File Offset: 0x00029E94
		IRequestIndexEntry IRequestIndexEntryHandler.Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity)
		{
			Type typeFromHandle = typeof(T);
			if (identity.IndexId.RequestIndexEntryType != typeFromHandle)
			{
				throw new ArgumentException(string.Format("The provided identity is requesting an IRequestIndexEntry of type {0}, but this handler only supports type {1}.", identity.IndexId.RequestIndexEntryType.Name, typeFromHandle.Name), "identity");
			}
			return this.Read(requestIndexEntryProvider, identity);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0002BCF7 File Offset: 0x00029EF7
		void IRequestIndexEntryHandler.Save(RequestIndexEntryProvider requestIndexEntryProvider, IRequestIndexEntry instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			this.Save(requestIndexEntryProvider, (T)((object)instance));
		}
	}
}
