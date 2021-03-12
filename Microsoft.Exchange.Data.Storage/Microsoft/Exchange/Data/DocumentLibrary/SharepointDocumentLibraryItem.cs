using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.DocumentLibrary.SharepointService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006DD RID: 1757
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SharepointDocumentLibraryItem : SharepointObject, IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x060045EA RID: 17898 RVA: 0x00129598 File Offset: 0x00127798
		internal SharepointDocumentLibraryItem(SharepointDocumentLibraryItemId id, SharepointSession session, XmlNode dataCache, Schema schema) : base(id, session, dataCache, schema)
		{
			this.CultureInfo = id.CultureInfo;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x001296D8 File Offset: 0x001278D8
		public new static SharepointDocumentLibraryItem Read(SharepointSession session, ObjectId objectId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			SharepointDocumentLibraryItemId itemId = objectId as SharepointDocumentLibraryItemId;
			if (itemId == null)
			{
				throw new ArgumentException("objectId");
			}
			if (itemId.UriFlags != UriFlags.SharepointDocument && itemId.UriFlags != UriFlags.SharepointFolder)
			{
				throw new ArgumentException("objectId");
			}
			if (session.Uri != itemId.SiteUri)
			{
				throw new ObjectNotFoundException(itemId, Strings.ExObjectMovedOrDeleted(itemId.ToString()));
			}
			if (itemId.Cache != null && itemId.Cache.Value.Key == session.Identity.Name)
			{
				if (itemId.UriFlags == UriFlags.SharepointFolder)
				{
					return new SharepointDocumentLibraryFolder(itemId, session, itemId.Cache.Value.Value);
				}
				if (itemId.UriFlags == UriFlags.SharepointDocument)
				{
					return new SharepointDocument(itemId, session, itemId.Cache.Value.Value);
				}
			}
			return Utils.DoSharepointTask<SharepointDocumentLibraryItem>(session.Identity, itemId, itemId, false, Utils.MethodType.Read, delegate
			{
				XmlNode nodeForItem = SharepointDocumentLibraryItem.GetNodeForItem(session, itemId);
				if (nodeForItem != null)
				{
					SharepointList sharepointList = SharepointList.Read(session, new SharepointListId(itemId.ListName, itemId.SiteUri, null, UriFlags.SharepointDocumentLibrary));
					itemId.Cache = new KeyValuePair<string, XmlNode>?(new KeyValuePair<string, XmlNode>(session.Identity.Name, nodeForItem));
					itemId.CultureInfo = sharepointList.GetRegionalSettings();
					if (itemId.UriFlags == UriFlags.SharepointFolder)
					{
						return new SharepointDocumentLibraryFolder(itemId, session, itemId.Cache.Value.Value);
					}
					if (itemId.UriFlags == UriFlags.SharepointDocument)
					{
						return new SharepointDocument(itemId, session, itemId.Cache.Value.Value);
					}
				}
				throw new ObjectNotFoundException(itemId, Strings.ExObjectNotFound(itemId.ToString()));
			});
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x00129884 File Offset: 0x00127A84
		public string DisplayName
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.Name);
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x00129891 File Offset: 0x00127A91
		public override Uri Uri
		{
			get
			{
				return base.GetValueOrDefault<Uri>(SharepointDocumentLibraryItemSchema.EncodedAbsoluteUri);
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x0012989E File Offset: 0x00127A9E
		ObjectId IDocumentLibraryItem.Id
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x001298A6 File Offset: 0x00127AA6
		public bool IsFolder
		{
			get
			{
				return (bool)base[SharepointDocumentLibraryItemSchema.FileSystemObjectType];
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x001298B8 File Offset: 0x00127AB8
		IDocumentLibraryFolder IDocumentLibraryItem.Parent
		{
			get
			{
				return this.ParentFolder;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x001298C0 File Offset: 0x00127AC0
		IDocumentLibrary IDocumentLibraryItem.Library
		{
			get
			{
				return this.Library;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x001298C8 File Offset: 0x00127AC8
		public override string Title
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.Name);
			}
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x001298D8 File Offset: 0x00127AD8
		public List<KeyValuePair<string, Uri>> GetHierarchy()
		{
			SharepointDocumentLibraryItemId sharepointDocumentLibraryItemId = base.Id as SharepointDocumentLibraryItemId;
			List<KeyValuePair<string, Uri>> list = new List<KeyValuePair<string, Uri>>(sharepointDocumentLibraryItemId.ItemHierarchy.Count);
			if (sharepointDocumentLibraryItemId.SiteUri.Segments.Length > 1)
			{
				list.Add(new KeyValuePair<string, Uri>(sharepointDocumentLibraryItemId.SiteUri.Segments[sharepointDocumentLibraryItemId.SiteUri.Segments.Length - 1], sharepointDocumentLibraryItemId.SiteUri));
			}
			else
			{
				list.Add(new KeyValuePair<string, Uri>(sharepointDocumentLibraryItemId.SiteUri.Host, sharepointDocumentLibraryItemId.SiteUri));
			}
			UriBuilder uriBuilder = new UriBuilder(sharepointDocumentLibraryItemId.SiteUri);
			for (int i = 0; i < sharepointDocumentLibraryItemId.ItemHierarchy.Count - 1; i++)
			{
				uriBuilder.Path = uriBuilder.Path + "/" + sharepointDocumentLibraryItemId.ItemHierarchy[i];
				list.Add(new KeyValuePair<string, Uri>(sharepointDocumentLibraryItemId.ItemHierarchy[i], uriBuilder.Uri));
			}
			return list;
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x001299C0 File Offset: 0x00127BC0
		public ExDateTime? LastModified
		{
			get
			{
				object valueOrDefault = base.GetValueOrDefault<object>(SharepointDocumentLibraryItemSchema.LastModifiedTime);
				if (valueOrDefault != null)
				{
					return new ExDateTime?((ExDateTime)valueOrDefault);
				}
				return null;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x001299F1 File Offset: 0x00127BF1
		public string Editor
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.Editor);
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x00129A00 File Offset: 0x00127C00
		public ExDateTime? CreatedDate
		{
			get
			{
				object valueOrDefault = base.GetValueOrDefault<object>(SharepointDocumentLibraryItemSchema.CreationTime);
				if (valueOrDefault != null)
				{
					return new ExDateTime?((ExDateTime)valueOrDefault);
				}
				return null;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x00129A31 File Offset: 0x00127C31
		public string DocIcon
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.DocIcon);
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x00129A3E File Offset: 0x00127C3E
		public string ModifiedBy
		{
			get
			{
				return this.Editor;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x00129A46 File Offset: 0x00127C46
		public string CreatedBy
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.Author);
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x00129A53 File Offset: 0x00127C53
		public string ServerUri
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.ServerUri);
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x00129A60 File Offset: 0x00127C60
		public string BaseName
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentLibraryItemSchema.BaseName);
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x00129A70 File Offset: 0x00127C70
		public SharepointDocumentLibraryFolder ParentFolder
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent;
				}
				SharepointDocumentLibraryItemId sharepointDocumentLibraryItemId = base.Id as SharepointDocumentLibraryItemId;
				if (sharepointDocumentLibraryItemId.ItemHierarchy.Count > 2)
				{
					List<string> list = new List<string>(sharepointDocumentLibraryItemId.ItemHierarchy);
					list.RemoveAt(list.Count - 1);
					string propertyValue = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					SharepointListId listId;
					if (list.Count == 1)
					{
						listId = new SharepointListId(sharepointDocumentLibraryItemId.ListName, sharepointDocumentLibraryItemId.SiteUri, sharepointDocumentLibraryItemId.CultureInfo, UriFlags.SharepointDocumentLibrary);
					}
					else
					{
						listId = new SharepointDocumentLibraryItemId("-1", sharepointDocumentLibraryItemId.ListName, sharepointDocumentLibraryItemId.SiteUri, sharepointDocumentLibraryItemId.CultureInfo, UriFlags.SharepointFolder, list);
					}
					PropertyDefinition[] propsToReturn = new PropertyDefinition[]
					{
						SharepointDocumentLibraryItemSchema.ID
					};
					ITableView tableView = SharepointDocumentLibraryFolder.InternalGetView(new ComparisonFilter(ComparisonOperator.Equal, SharepointDocumentLibraryItemSchema.Name, propertyValue), null, DocumentLibraryQueryOptions.FoldersAndFiles, propsToReturn, this.Session, listId);
					if (tableView.EstimatedRowCount == 1)
					{
						this.parent = SharepointDocumentLibraryFolder.Read(this.Session, (ObjectId)tableView.GetRows(1)[0][0]);
					}
				}
				return this.parent;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x00129B8C File Offset: 0x00127D8C
		public SharepointDocumentLibrary Library
		{
			get
			{
				if (this.documentLibrary == null)
				{
					SharepointItemId sharepointItemId = this.SharepointId as SharepointItemId;
					SharepointListId listId = new SharepointListId(sharepointItemId.ListName, sharepointItemId.SiteUri, sharepointItemId.CultureInfo, UriFlags.SharepointDocumentLibrary);
					this.documentLibrary = (SharepointDocumentLibrary)SharepointList.Read(this.Session, listId);
				}
				return this.documentLibrary;
			}
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00129BE4 File Offset: 0x00127DE4
		internal static XmlNode GetNodeForItem(SharepointSession session, SharepointDocumentLibraryItemId itemId)
		{
			XmlNode result;
			using (Lists lists = new Lists(session.Uri.ToString()))
			{
				lists.Credentials = CredentialCache.DefaultCredentials;
				XmlNode query = SharepointHelpers.GenerateQueryCAML(new ComparisonFilter(ComparisonOperator.Equal, SharepointDocumentLibraryItemSchema.ID, itemId.ItemId));
				XmlNode queryOptions = SharepointHelpers.GenerateQueryOptionsXml(itemId.ParentDirectoryStructure);
				XmlNode viewFields = SharepointHelpers.GenerateViewFieldCAML(SharepointDocumentSchema.Instance, SharepointDocumentSchema.Instance.AllProperties.Keys);
				XmlNode listItems = lists.GetListItems(itemId.ListName, null, query, viewFields, "2", queryOptions);
				XmlNodeList xmlNodeList = listItems.SelectNodes("/rs:data/z:row", SharepointHelpers.SharepointNamespaceManager);
				result = ((xmlNodeList.Count == 1) ? xmlNodeList[0] : null);
			}
			return result;
		}

		// Token: 0x0400264B RID: 9803
		private SharepointDocumentLibrary documentLibrary;

		// Token: 0x0400264C RID: 9804
		private SharepointDocumentLibraryFolder parent;
	}
}
