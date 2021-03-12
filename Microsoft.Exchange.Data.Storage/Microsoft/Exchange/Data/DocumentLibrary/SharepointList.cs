using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data.DocumentLibrary.SharepointService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006DF RID: 1759
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointList : SharepointObject
	{
		// Token: 0x06004608 RID: 17928 RVA: 0x00129ED0 File Offset: 0x001280D0
		internal SharepointList(SharepointListId listId, SharepointSession session, XmlNode dataCache) : base(listId, session, dataCache, SharepointListSchema.Instance)
		{
			if (listId.CultureInfo != null)
			{
				this.CultureInfo = listId.CultureInfo;
				return;
			}
			if (dataCache.ChildNodes.Count > 0)
			{
				foreach (object obj in dataCache.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name == "RegionalSettings")
					{
						using (IEnumerator enumerator2 = xmlNode.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								XmlNode xmlNode2 = (XmlNode)obj2;
								if (xmlNode2.Name == "Locale")
								{
									try
									{
										this.CultureInfo = CultureInfo.GetCultureInfo(int.Parse(xmlNode2.InnerText));
										listId.CultureInfo = this.CultureInfo;
									}
									catch (FormatException)
									{
										throw new CorruptDataException(this, Strings.ExCorruptRegionalSetting);
									}
									catch (ArgumentException)
									{
										throw new CorruptDataException(this, Strings.ExCorruptRegionalSetting);
									}
								}
							}
							break;
						}
					}
				}
				if (this.CultureInfo == null)
				{
					throw new CorruptDataException(this, Strings.ExCorruptRegionalSetting);
				}
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x0012A0D4 File Offset: 0x001282D4
		public new static SharepointList Read(SharepointSession session, ObjectId listId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (listId == null)
			{
				throw new ArgumentNullException("listId");
			}
			SharepointListId spListId = listId as SharepointListId;
			if (spListId == null)
			{
				throw new ArgumentException("listId");
			}
			if (spListId.SiteUri != session.Uri)
			{
				throw new ObjectNotFoundException(listId, Strings.ExObjectNotFound(listId.ToString()));
			}
			if ((spListId.UriFlags & UriFlags.SharepointList) != UriFlags.SharepointList && (spListId.UriFlags & UriFlags.SharepointDocumentLibrary) != UriFlags.SharepointDocumentLibrary)
			{
				throw new ArgumentException("listId");
			}
			if (spListId.Cache != null && spListId.Cache.Value.Key == session.Identity.Name)
			{
				return SharepointList.ReadHelper(spListId.Cache.Value.Value, session, spListId);
			}
			return Utils.DoSharepointTask<SharepointList>(session.Identity, spListId, spListId, false, Utils.MethodType.Read, delegate
			{
				using (Lists lists = new Lists(session.Uri.ToString()))
				{
					new List<Result<SharepointList>>();
					XmlNode xmlNode = lists.GetListAndView(spListId.ListName, null).SelectSingleNode("/sp:List", SharepointHelpers.SharepointNamespaceManager);
					if (xmlNode != null)
					{
						return SharepointList.ReadHelper(xmlNode, session, spListId);
					}
				}
				throw new ObjectNotFoundException(spListId, Strings.ExObjectNotFound(spListId.ToString()));
			});
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x0012A22B File Offset: 0x0012842B
		public override string Title
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointListSchema.Title);
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x0012A238 File Offset: 0x00128438
		public override SharepointItemType ItemType
		{
			get
			{
				return SharepointItemType.List;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x0012A23B File Offset: 0x0012843B
		public Uri SiteUri
		{
			get
			{
				return this.SharepointId.SiteUri;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x0012A248 File Offset: 0x00128448
		public override Uri Uri
		{
			get
			{
				SharepointListId sharepointListId = base.Id as SharepointListId;
				return new Uri(new Uri(sharepointListId.SiteUri.GetLeftPart(UriPartial.Authority)), this.DefautViewUri);
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x0012A27D File Offset: 0x0012847D
		public Uri DocTemplateUri
		{
			get
			{
				return base.GetValueOrDefault<Uri>(SharepointListSchema.DocTemplateUri);
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x0012A28A File Offset: 0x0012848A
		public Uri DefautViewUri
		{
			get
			{
				return base.GetValueOrDefault<Uri>(SharepointListSchema.DefaultViewUri);
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x0012A297 File Offset: 0x00128497
		public Uri ImageUri
		{
			get
			{
				return base.GetValueOrDefault<Uri>(SharepointListSchema.ImageUri);
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x0012A2A4 File Offset: 0x001284A4
		public override object TryGetProperty(PropertyDefinition propDef)
		{
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propDef as DocumentLibraryPropertyDefinition;
			if (documentLibraryPropertyDefinition != null && documentLibraryPropertyDefinition.PropertyId == DocumentLibraryPropertyId.Uri)
			{
				return this.Uri;
			}
			return base.TryGetProperty(propDef);
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x0012A2D2 File Offset: 0x001284D2
		internal bool HasRegionalSettings
		{
			get
			{
				return this.CultureInfo != null;
			}
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x0012A2E0 File Offset: 0x001284E0
		internal CultureInfo GetRegionalSettings()
		{
			if (this.HasRegionalSettings)
			{
				return this.CultureInfo;
			}
			SharepointListId sharepointListId = (SharepointListId)base.Id;
			SharepointListId listId = new SharepointListId(sharepointListId.ListName, sharepointListId.SiteUri, null, sharepointListId.UriFlags);
			SharepointList sharepointList = SharepointList.Read(this.Session, listId);
			if (!sharepointList.HasRegionalSettings)
			{
				throw new CorruptDataException(this, Strings.ExCorruptRegionalSetting);
			}
			this.CultureInfo = sharepointList.GetRegionalSettings();
			sharepointListId.CultureInfo = this.CultureInfo;
			return this.CultureInfo;
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x0012A360 File Offset: 0x00128560
		private static SharepointList ReadHelper(XmlNode node, SharepointSession session, SharepointListId spListId)
		{
			object[] valuesFromCAMLView = SharepointHelpers.GetValuesFromCAMLView(SharepointListSchema.Instance, node, null, new PropertyDefinition[]
			{
				SharepointListSchema.Name,
				SharepointListSchema.ListType,
				SharepointListSchema.PredefinedListType
			});
			ListBaseType listBaseType = (ListBaseType)valuesFromCAMLView[1];
			spListId.Cache = new KeyValuePair<string, XmlNode>?(new KeyValuePair<string, XmlNode>(session.Identity.Name, node));
			if (listBaseType == ListBaseType.DocumentLibrary)
			{
				return new SharepointDocumentLibrary(spListId, session, node);
			}
			return new SharepointList(spListId, session, node);
		}
	}
}
