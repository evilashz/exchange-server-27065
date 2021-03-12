using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E0 RID: 1760
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointDocumentLibrary : SharepointList, IDocumentLibrary, IReadOnlyPropertyBag
	{
		// Token: 0x06004615 RID: 17941 RVA: 0x0012A3D4 File Offset: 0x001285D4
		internal SharepointDocumentLibrary(SharepointListId siteId, SharepointSession session, XmlNode dataCache) : base(siteId, session, dataCache)
		{
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x0012A3E0 File Offset: 0x001285E0
		public new static SharepointDocumentLibrary Read(SharepointSession session, ObjectId listId)
		{
			if (listId == null)
			{
				throw new ArgumentNullException("listId");
			}
			SharepointListId sharepointListId = listId as SharepointListId;
			if (sharepointListId == null)
			{
				throw new ArgumentException("listId");
			}
			if (sharepointListId.UriFlags != UriFlags.SharepointDocumentLibrary)
			{
				throw new ArgumentNullException("listId");
			}
			return (SharepointDocumentLibrary)SharepointList.Read(session, sharepointListId);
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x0012A430 File Offset: 0x00128630
		ObjectId IDocumentLibrary.Id
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x0012A438 File Offset: 0x00128638
		string IDocumentLibrary.Title
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x0012A440 File Offset: 0x00128640
		public string Description
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointListSchema.Description);
			}
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x0012A450 File Offset: 0x00128650
		public List<KeyValuePair<string, Uri>> GetHierarchy()
		{
			SharepointListId sharepointListId = base.Id as SharepointListId;
			List<KeyValuePair<string, Uri>> list = new List<KeyValuePair<string, Uri>>(1);
			if (sharepointListId.SiteUri.Segments.Length > 1)
			{
				list.Add(new KeyValuePair<string, Uri>(sharepointListId.SiteUri.Segments[sharepointListId.SiteUri.Segments.Length - 1].TrimEnd(new char[]
				{
					'/',
					'\\'
				}), sharepointListId.SiteUri));
			}
			else
			{
				list.Add(new KeyValuePair<string, Uri>(sharepointListId.SiteUri.Host, sharepointListId.SiteUri));
			}
			return list;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x0012A4E2 File Offset: 0x001286E2
		IDocumentLibraryItem IDocumentLibrary.Read(ObjectId objectId, params PropertyDefinition[] propsToReturn)
		{
			return this.Read(objectId, propsToReturn);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x0012A4EC File Offset: 0x001286EC
		public SharepointDocumentLibraryItem Read(ObjectId objectId, params PropertyDefinition[] propsToReturn)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			SharepointItemId sharepointItemId = objectId as SharepointItemId;
			SharepointListId sharepointListId = base.Id as SharepointListId;
			if (sharepointItemId == null)
			{
				throw new ArgumentException("objectId as SharepointItemId");
			}
			if (sharepointItemId.ListName != sharepointListId.ListName || sharepointItemId.SiteUri != sharepointListId.SiteUri)
			{
				throw new ObjectNotFoundException(objectId, Strings.ExObjectNotFound(objectId.ToString()));
			}
			return SharepointDocumentLibraryItem.Read(this.Session, objectId);
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x0012A56C File Offset: 0x0012876C
		public ITableView GetView(QueryFilter query, SortBy[] sortBy, DocumentLibraryQueryOptions queryOptions, params PropertyDefinition[] propsToReturn)
		{
			return SharepointDocumentLibraryFolder.InternalGetView(query, sortBy, queryOptions, propsToReturn, this.Session, (SharepointListId)base.Id);
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x0012A589 File Offset: 0x00128789
		public override SharepointItemType ItemType
		{
			get
			{
				return SharepointItemType.DocumentLibrary;
			}
		}
	}
}
