using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000406 RID: 1030
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OneDriveProItemsPagingMetadata : AttachmentItemsPagingMetadata
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0007E758 File Offset: 0x0007C958
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x0007E760 File Offset: 0x0007C960
		[DataMember]
		public string ChangeToken { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0007E769 File Offset: 0x0007C969
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x0007E77B File Offset: 0x0007C97B
		[DataMember]
		public OneDriveProItemsPage[] PageCache
		{
			get
			{
				return this.PageMap.Values.ToArray<OneDriveProItemsPage>();
			}
			set
			{
				this.CreatePageMapFromArray(value);
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0007E7A4 File Offset: 0x0007C9A4
		internal IEnumerable<IListItem> GetItems(UserContext userContext, string endPointUrl, string documentLibrary, string location, IndexedPageView requestedData, AttachmentItemsSort sort, out int totalItemCount, DataProviderCallLogEvent logEvent)
		{
			IEnumerable<IListItem> result;
			using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, endPointUrl))
			{
				totalItemCount = 0;
				IList documentsLibrary = OneDriveProUtilities.GetDocumentsLibrary(clientContext, documentLibrary);
				OneDriveProItemsPage page = this.UpdatePageCache(clientContext, userContext, documentsLibrary, documentLibrary, location, requestedData, sort, logEvent);
				CamlQuery camlDataQuery = this.GetCamlDataQuery(location, requestedData, this.GetListItemCollectionPosition(page), sort);
				IListItemCollection items = documentsLibrary.GetItems(camlDataQuery);
				IFolder folder = string.IsNullOrEmpty(location) ? documentsLibrary.RootFolder : clientContext.Web.GetFolderByServerRelativeUrl(location);
				items.Load(clientContext, new Expression<Func<ListItemCollection, object>>[0]);
				folder.Load(clientContext, new Expression<Func<Folder, object>>[]
				{
					(Folder x) => (object)x.ItemCount
				});
				OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "GetItems");
				int startIndex = requestedData.Offset % 200;
				int endIndex = startIndex + requestedData.MaxRows;
				totalItemCount = folder.ItemCount;
				result = items.ToList<IListItem>().Where((IListItem item, int index) => index >= startIndex && index < endIndex);
			}
			return result;
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007E91C File Offset: 0x0007CB1C
		private ConcurrentDictionary<int, OneDriveProItemsPage> PageMap
		{
			get
			{
				if (this.pageMap == null)
				{
					this.pageMap = new ConcurrentDictionary<int, OneDriveProItemsPage>();
				}
				return this.pageMap;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0007E938 File Offset: 0x0007CB38
		private OneDriveProItemsPage UpdatePageCache(IClientContext clientContext, UserContext userContext, IList list, string listName, string location, IndexedPageView requestedData, AttachmentItemsSort sort, DataProviderCallLogEvent logEvent)
		{
			string changeToken;
			bool flag;
			this.GetListItemChangesSinceToken(clientContext, userContext.LogonIdentity, listName, location, out changeToken, out flag, logEvent);
			this.ChangeToken = changeToken;
			if (flag)
			{
				this.PageMap.Clear();
			}
			int num = this.ComputeStartPageIndex(requestedData);
			OneDriveProItemsPage nearestPage = this.GetNearestPage(num);
			int num2 = (nearestPage != null) ? nearestPage.PageIndex : -1;
			if (nearestPage == null || num != nearestPage.PageIndex)
			{
				ListItemCollectionPosition listItemCollectionPosition = this.GetListItemCollectionPosition(nearestPage);
				CamlQuery query = OneDriveProUtilities.CreatePagedCamlPageQuery(location, sort, listItemCollectionPosition, Math.Abs(num - num2) * 200 + 200);
				IListItemCollection items = list.GetItems(query);
				items.Load(clientContext, new Expression<Func<ListItemCollection, object>>[0]);
				OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "UpdatePageCache");
				this.UpdateCache(items, nearestPage);
			}
			OneDriveProItemsPage result;
			this.PageMap.TryGetValue(num, out result);
			return result;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0007EA08 File Offset: 0x0007CC08
		private CamlQuery GetCamlDataQuery(string location, IndexedPageView requestedData, ListItemCollectionPosition position, AttachmentItemsSort sort)
		{
			int numberOfItems = requestedData.Offset % 200 + requestedData.MaxRows;
			return OneDriveProUtilities.CreatePagedCamlDataQuery(location, sort, position, numberOfItems);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007EA34 File Offset: 0x0007CC34
		private void GetListItemChangesSinceToken(IClientContext context, OwaIdentity identity, string listName, string location, out string changeToken, out bool hasChanges, DataProviderCallLogEvent logEvent)
		{
			changeToken = null;
			hasChanges = false;
			DownloadResult downloadResult = OneDriveProUtilities.SendRestRequest("POST", string.Format("{0}/_vti_bin/client.svc/web/lists/getByTitle('{1}')/GetListItemChangesSinceToken", context.Url, listName), identity, this.GetRequestStream(location, this.ChangeToken), logEvent, "GetListItemChangesSinceToken");
			if (!downloadResult.IsSucceeded)
			{
				OneDriveProItemsPagingMetadata.TraceError(OneDriveProItemsPagingMetadata.LogMetadata.GetListItemChangesSinceToken, downloadResult.Exception);
				hasChanges = true;
				return;
			}
			using (XmlReader xmlReader = XmlReader.Create(downloadResult.ResponseStream))
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						if (xmlReader.LocalName == "Changes")
						{
							changeToken = xmlReader.GetAttribute("LastChangeToken");
						}
						else if (xmlReader.LocalName == "row" && xmlReader.NamespaceURI == "#RowsetSchema")
						{
							hasChanges = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0007EB20 File Offset: 0x0007CD20
		private Stream GetRequestStream(string location, string changeToken)
		{
			StringBuilder stringBuilder = new StringBuilder("{'query':{'__metadata':{'type':'SP.ChangeLogItemQuery'},'Query':'',");
			stringBuilder.Append("'ViewFields':'<ViewFields><FieldRef Name=\"ID\"/></ViewFields>',");
			stringBuilder.Append("'RowLimit':'1',");
			if (!string.IsNullOrEmpty(changeToken))
			{
				stringBuilder.AppendFormat("'ChangeToken':'{0}',", changeToken);
			}
			stringBuilder.Append("'QueryOptions':'");
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xmlTextWriter.WriteStartElement("QueryOptions");
					xmlTextWriter.WriteElementString("DateInUtc", "TRUE");
					xmlTextWriter.WriteElementString("OptimizeFor", "FolderUrls");
					xmlTextWriter.WriteStartElement("ViewAttributes");
					xmlTextWriter.WriteAttributeString("Scope", "Default");
					xmlTextWriter.WriteEndElement();
					if (!string.IsNullOrEmpty(location))
					{
						xmlTextWriter.WriteElementString("Folder", location);
					}
					xmlTextWriter.WriteEndElement();
				}
			}
			stringBuilder.Append("'}}");
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			return new MemoryStream(bytes, 0, bytes.Length);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0007EC40 File Offset: 0x0007CE40
		private void UpdateCache(IListItemCollection itemCollection, OneDriveProItemsPage page)
		{
			int num = (page == null) ? -1 : page.PageIndex;
			for (int i = 199; i < itemCollection.Count(); i += 200)
			{
				IListItem item = itemCollection[i];
				int num2 = i / 200 + num + 1;
				this.PageMap[num2] = new OneDriveProItemsPage(num2, item);
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007EC9C File Offset: 0x0007CE9C
		private ListItemCollectionPosition GetListItemCollectionPosition(OneDriveProItemsPage page)
		{
			if (page != null)
			{
				return new ListItemCollectionPosition
				{
					PagingInfo = string.Format("Paged=TRUE&p_SortBehavior={0}&p_FSObjType={1}&p_FileLeafRef={2}&p_ID={3}", new object[]
					{
						page.SortBehavior,
						page.ObjectType,
						page.Name,
						page.ID
					})
				};
			}
			return null;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0007ECF4 File Offset: 0x0007CEF4
		private OneDriveProItemsPage GetNearestPage(int pageIndex)
		{
			IOrderedEnumerable<int> orderedEnumerable = from x in this.PageMap.Keys
			orderby x
			select x;
			int num = -1;
			foreach (int num2 in orderedEnumerable)
			{
				if (num2 > pageIndex)
				{
					break;
				}
				num = num2;
			}
			if (num == -1)
			{
				return null;
			}
			return this.PageMap[num];
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0007ED80 File Offset: 0x0007CF80
		private int ComputeStartPageIndex(IndexedPageView requestedData)
		{
			return requestedData.Offset / 200 - 1;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0007ED90 File Offset: 0x0007CF90
		private void CreatePageMapFromArray(OneDriveProItemsPage[] pages)
		{
			this.PageMap.Clear();
			foreach (OneDriveProItemsPage oneDriveProItemsPage in pages)
			{
				this.PageMap[oneDriveProItemsPage.PageIndex] = oneDriveProItemsPage;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0007EDCE File Offset: 0x0007CFCE
		private static void TraceError(OneDriveProItemsPagingMetadata.LogMetadata error, object data)
		{
			OwaApplication.GetRequestDetailsLogger.Set(error, data);
		}

		// Token: 0x040012FD RID: 4861
		private const string ViewFieldsJson = "'ViewFields':'<ViewFields><FieldRef Name=\"ID\"/></ViewFields>',";

		// Token: 0x040012FE RID: 4862
		private const string RowLimitJson = "'RowLimit':'1',";

		// Token: 0x040012FF RID: 4863
		private const string QueryOptionsJsonPrefix = "'QueryOptions':'";

		// Token: 0x04001300 RID: 4864
		private const string QueryOptionsElementName = "QueryOptions";

		// Token: 0x04001301 RID: 4865
		private const string ChangeTokenJsonFormat = "'ChangeToken':'{0}',";

		// Token: 0x04001302 RID: 4866
		private const string DateInUtcElementName = "DateInUtc";

		// Token: 0x04001303 RID: 4867
		private const string OptimizeForElementName = "OptimizeFor";

		// Token: 0x04001304 RID: 4868
		private const string ViewAttributesElementName = "ViewAttributes";

		// Token: 0x04001305 RID: 4869
		private const string ScopeAttributeName = "Scope";

		// Token: 0x04001306 RID: 4870
		private const string FolderElementName = "Folder";

		// Token: 0x04001307 RID: 4871
		private const string ChangesElementName = "Changes";

		// Token: 0x04001308 RID: 4872
		private const string RowElementName = "row";

		// Token: 0x04001309 RID: 4873
		private const string RowsetSchemaNamespaceUri = "#RowsetSchema";

		// Token: 0x0400130A RID: 4874
		private const string LastChangeTokenAttributeName = "LastChangeToken";

		// Token: 0x0400130B RID: 4875
		private const string TrueValue = "TRUE";

		// Token: 0x0400130C RID: 4876
		private const string FolderUrlsValue = "FolderUrls";

		// Token: 0x0400130D RID: 4877
		private const string DefaultValue = "Default";

		// Token: 0x0400130E RID: 4878
		private const string GetListItemChangesSinceLastTokenUrlFormat = "{0}/_vti_bin/client.svc/web/lists/getByTitle('{1}')/GetListItemChangesSinceToken";

		// Token: 0x0400130F RID: 4879
		private const string GetListItemChangesSinceLastTokenJsonPrefix = "{'query':{'__metadata':{'type':'SP.ChangeLogItemQuery'},'Query':'',";

		// Token: 0x04001310 RID: 4880
		private const string GetListItemChangesSinceLastTokenJsonSuffix = "'}}";

		// Token: 0x04001311 RID: 4881
		private const string PostMethod = "POST";

		// Token: 0x04001312 RID: 4882
		private const string ListItemCollectionPositionFormat = "Paged=TRUE&p_SortBehavior={0}&p_FSObjType={1}&p_FileLeafRef={2}&p_ID={3}";

		// Token: 0x04001313 RID: 4883
		internal const int PageSize = 200;

		// Token: 0x04001314 RID: 4884
		private ConcurrentDictionary<int, OneDriveProItemsPage> pageMap = new ConcurrentDictionary<int, OneDriveProItemsPage>();

		// Token: 0x02000407 RID: 1031
		private enum LogMetadata
		{
			// Token: 0x04001318 RID: 4888
			[DisplayName("SDPP.GLICST")]
			GetListItemChangesSinceToken
		}
	}
}
