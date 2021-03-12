using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006C RID: 108
	internal class ConfigDataProviderPagedReader<TResult> : IPagedReader<TResult>, IEnumerable<!0>, IEnumerable where TResult : IConfigurable, new()
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000C640 File Offset: 0x0000A840
		public ConfigDataProviderPagedReader(IConfigDataProvider dataProvider, ADObjectId rootId, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			if (dataProvider == null)
			{
				throw new ArgumentNullException("dataProvider");
			}
			if (pageSize < 0 || pageSize > 10000)
			{
				throw new ArgumentOutOfRangeException("pageSize", pageSize, string.Format("pageSize should be between 1 and {0} or 0 to use the default page size: {1}", 10000, ConfigDataProviderPagedReader<TResult>.DefaultPageSize));
			}
			this.dataProvider = dataProvider;
			this.rootId = rootId;
			this.filter = filter;
			this.sortBy = sortBy;
			this.pageSize = ((pageSize == 0) ? ConfigDataProviderPagedReader<TResult>.DefaultPageSize : pageSize);
			this.RetrievedAllData = false;
			this.PagesReturned = 0;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public bool RetrievedAllData { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000C6FD File Offset: 0x0000A8FD
		public int PagesReturned { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C706 File Offset: 0x0000A906
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C710 File Offset: 0x0000A910
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

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C8FC File Offset: 0x0000AAFC
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

		// Token: 0x06000433 RID: 1075 RVA: 0x0000C918 File Offset: 0x0000AB18
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C920 File Offset: 0x0000AB20
		public TResult[] GetNextPage()
		{
			TResult[] result = null;
			QueryFilter pagingFilter = null;
			bool retrievedAllData = true;
			if (!this.valid)
			{
				throw new InvalidOperationException("GetNextPage() called after reader was marked invalid");
			}
			if (this.RetrievedAllData)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
			}
			try
			{
				pagingFilter = PagingHelper.GetPagingQueryFilter(this.filter, this.cookie);
				result = this.dataProvider.FindPaged<TResult>(pagingFilter, this.rootId, true, this.sortBy, this.pageSize).ToArray<TResult>();
			}
			catch (PermanentDALException)
			{
				this.valid = false;
				this.RetrievedAllData = true;
				throw;
			}
			this.cookie = PagingHelper.GetProcessedCookie(pagingFilter, out retrievedAllData);
			this.RetrievedAllData = retrievedAllData;
			this.PagesReturned++;
			return result;
		}

		// Token: 0x04000284 RID: 644
		public const int MaximumPageSize = 10000;

		// Token: 0x04000285 RID: 645
		public static readonly int DefaultPageSize = 1000;

		// Token: 0x04000286 RID: 646
		private readonly IConfigDataProvider dataProvider;

		// Token: 0x04000287 RID: 647
		private readonly ADObjectId rootId;

		// Token: 0x04000288 RID: 648
		private readonly QueryFilter filter;

		// Token: 0x04000289 RID: 649
		private readonly SortBy sortBy;

		// Token: 0x0400028A RID: 650
		private readonly int pageSize;

		// Token: 0x0400028B RID: 651
		private string cookie;

		// Token: 0x0400028C RID: 652
		private bool valid = true;
	}
}
