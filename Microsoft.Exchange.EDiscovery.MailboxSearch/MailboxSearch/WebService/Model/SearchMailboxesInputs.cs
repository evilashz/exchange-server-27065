using System;
using System.Collections.Generic;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000042 RID: 66
	internal class SearchMailboxesInputs
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0001477F File Offset: 0x0001297F
		public SearchMailboxesInputs()
		{
			this.Sources = new List<SearchSource>();
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00014792 File Offset: 0x00012992
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0001479A File Offset: 0x0001299A
		public bool IsLocalCall { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000147A3 File Offset: 0x000129A3
		// (set) Token: 0x06000308 RID: 776 RVA: 0x000147AB File Offset: 0x000129AB
		public List<SearchSource> Sources { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000147B4 File Offset: 0x000129B4
		// (set) Token: 0x0600030A RID: 778 RVA: 0x000147BC File Offset: 0x000129BC
		public PagingInfo PagingInfo { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000147C5 File Offset: 0x000129C5
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000147CD File Offset: 0x000129CD
		public CallerInfo CallerInfo { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000147D6 File Offset: 0x000129D6
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000147DE File Offset: 0x000129DE
		public SearchCriteria Criteria { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000147E7 File Offset: 0x000129E7
		// (set) Token: 0x06000310 RID: 784 RVA: 0x000147EF File Offset: 0x000129EF
		public string SearchConfigurationId { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000147F8 File Offset: 0x000129F8
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00014800 File Offset: 0x00012A00
		public string SearchQuery { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00014809 File Offset: 0x00012A09
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00014811 File Offset: 0x00012A11
		public string Language { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0001481A File Offset: 0x00012A1A
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00014822 File Offset: 0x00012A22
		public SearchType SearchType { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0001482B File Offset: 0x00012A2B
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00014833 File Offset: 0x00012A33
		public Guid RequestId { get; set; }

		// Token: 0x06000319 RID: 793 RVA: 0x0001483C File Offset: 0x00012A3C
		internal SearchMailboxesInputs Clone()
		{
			return (SearchMailboxesInputs)base.MemberwiseClone();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001484C File Offset: 0x00012A4C
		internal void UpdateRequest(SearchMailboxesRequest request, IEnumerable<SearchSource> sources)
		{
			request.Language = this.Language;
			request.PageDirection = DiscoveryEwsClient.GetPageDirection(this.PagingInfo.Direction);
			request.PageItemReference = ((this.PagingInfo.SortValue != null) ? this.PagingInfo.SortValue.ToString() : null);
			request.PageSize = this.PagingInfo.PageSize;
			request.PerformDeduplication = this.PagingInfo.ExcludeDuplicates;
			request.PreviewItemResponseShape = DiscoveryEwsClient.GetPreviewItemResponseShape(this.PagingInfo.BaseShape, this.PagingInfo.AdditionalProperties);
			request.SearchQueries = this.GetSearchQueries(sources);
			request.ResultType = DiscoveryEwsClient.GetSearchType(this.SearchType);
			if (request.SearchQueries.Count > 0 && request.SearchQueries[0].MailboxSearchScopes.Length > 0)
			{
				request.SearchQueries[0].MailboxSearchScopes[0].ExtendedAttributes.Add(new ExtendedAttribute("SearchType", this.SearchType.ToString()));
			}
			request.SortByProperty = (this.PagingInfo.OriginalSortByReference ?? DiscoveryEwsClient.GetSortbyProperty(this.PagingInfo.SortBy));
			request.SortOrder = DiscoveryEwsClient.GetSortDirection(this.PagingInfo.SortBy);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00014998 File Offset: 0x00012B98
		internal List<MailboxQuery> GetSearchQueries(IEnumerable<SearchSource> sources)
		{
			List<MailboxQuery> list = new List<MailboxQuery>();
			List<MailboxSearchScope> list2 = new List<MailboxSearchScope>();
			foreach (SearchSource searchSource in sources)
			{
				list2.Add(searchSource.GetScope());
			}
			MailboxQuery item = new MailboxQuery(this.SearchQuery, list2.ToArray());
			list.Add(item);
			return list;
		}
	}
}
