using System;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;

namespace Microsoft.Office.CompliancePolicy.Validators
{
	// Token: 0x02000141 RID: 321
	internal class SharepointCsomProvider : ISharepointCsomProvider
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00033740 File Offset: 0x00031940
		public void LoadWebInfo(ClientContext context, out string webUrl, out string webTitle, out Guid siteId, out Guid webId)
		{
			Web web = context.Web;
			Site site = context.Site;
			context.Load<Web>(web, new Expression<Func<Web, object>>[0]);
			context.Load<Site>(site, new Expression<Func<Site, object>>[0]);
			context.ExecuteQuery();
			webUrl = web.Url;
			webTitle = web.Title;
			siteId = site.Id;
			webId = web.Id;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000337A8 File Offset: 0x000319A8
		public ResultTableCollection ExecuteSearch(ClientContext context, string location, bool searchOnlySiteCollection)
		{
			SearchExecutor searchExecutor = new SearchExecutor(context);
			KeywordQuery keywordQuery = searchOnlySiteCollection ? SharepointCsomProvider.GetKeywordQueryForSiteCollectionOnly(location, context) : SharepointCsomProvider.GetKeywordQuery(location, context);
			ClientResult<ResultTableCollection> clientResult = searchExecutor.ExecuteQuery(keywordQuery);
			context.ExecuteQuery();
			return clientResult.Value;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000337E4 File Offset: 0x000319E4
		public ResultTableCollection ExecuteSearch(ClientContext context, Guid webId, Guid siteId)
		{
			SearchExecutor searchExecutor = new SearchExecutor(context);
			KeywordQuery keywordQuery = SharepointCsomProvider.GetKeywordQuery(webId, siteId, context);
			ClientResult<ResultTableCollection> clientResult = searchExecutor.ExecuteQuery(keywordQuery);
			context.ExecuteQuery();
			return clientResult.Value;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00033818 File Offset: 0x00031A18
		private static KeywordQuery GetKeywordQuery(string location, ClientRuntimeContext context)
		{
			KeywordQuery keywordQuery = new KeywordQuery(context);
			keywordQuery.QueryText = string.Format(CultureInfo.InvariantCulture, "Path=\"{0}\"", new object[]
			{
				location
			});
			keywordQuery.RowLimit = 5;
			keywordQuery.SelectProperties.Add("WebId");
			keywordQuery.SelectProperties.Add("SiteId");
			keywordQuery.SelectProperties.Add("contentclass");
			keywordQuery.SelectProperties.Add("Path");
			keywordQuery.SelectProperties.Add("Title");
			SharepointCsomProvider.PopulateQueryDefaults(keywordQuery);
			return keywordQuery;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000338AC File Offset: 0x00031AAC
		private static KeywordQuery GetKeywordQuery(Guid webId, Guid siteId, ClientContext context)
		{
			KeywordQuery keywordQuery = new KeywordQuery(context);
			keywordQuery.RowLimit = 1;
			keywordQuery.QueryText = string.Format(CultureInfo.InvariantCulture, "SiteId:\"{0}\" AND WebId:\"{1}\" AND (contentclass=\"STS_Web\" OR contentclass=\"STS_Site\")", new object[]
			{
				siteId.ToString("D"),
				webId.ToString("D")
			});
			keywordQuery.SelectProperties.Add("contentclass");
			keywordQuery.SelectProperties.Add("Path");
			keywordQuery.SelectProperties.Add("Title");
			SharepointCsomProvider.PopulateQueryDefaults(keywordQuery);
			return keywordQuery;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003393C File Offset: 0x00031B3C
		private static KeywordQuery GetKeywordQueryForSiteCollectionOnly(string location, ClientContext context)
		{
			KeywordQuery keywordQuery = new KeywordQuery(context);
			keywordQuery.QueryText = string.Format(CultureInfo.InvariantCulture, "Path:\"{0}\" AND contentclass=\"STS_Site\"", new object[]
			{
				location
			});
			keywordQuery.RowLimit = 2;
			SharepointCsomProvider.PopulateQueryDefaults(keywordQuery);
			return keywordQuery;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003397F File Offset: 0x00031B7F
		private static void PopulateQueryDefaults(KeywordQuery query)
		{
			query.ProcessBestBets = false;
			query.BypassResultTypes = true;
			query.Properties["EnableStacking"] = false;
			query.EnableStemming = false;
			query.EnableQueryRules = false;
		}

		// Token: 0x040004C0 RID: 1216
		private const string PathMatchFormat = "Path=\"{0}\"";

		// Token: 0x040004C1 RID: 1217
		private const string SiteAndWebExactMatchFormat = "SiteId:\"{0}\" AND WebId:\"{1}\" AND (contentclass=\"STS_Web\" OR contentclass=\"STS_Site\")";

		// Token: 0x040004C2 RID: 1218
		private const string SiteCollectionOnlyPathMatchFormat = "Path:\"{0}\" AND contentclass=\"STS_Site\"";

		// Token: 0x040004C3 RID: 1219
		private const string EnableResultTableStackingPropertyName = "EnableStacking";
	}
}
