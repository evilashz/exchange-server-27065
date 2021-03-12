using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Principal;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Data.DocumentLibrary.SharepointService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006F1 RID: 1777
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointSession
	{
		// Token: 0x06004676 RID: 18038 RVA: 0x0012C0E6 File Offset: 0x0012A2E6
		internal SharepointSession(SharepointSiteId sharepointId, WindowsPrincipal windowsPrincipal)
		{
			if (sharepointId == null)
			{
				throw new ArgumentNullException("sharepointId");
			}
			if (windowsPrincipal == null)
			{
				throw new ArgumentNullException("windowsPrincipal");
			}
			this.windowsIdentity = (WindowsIdentity)windowsPrincipal.Identity;
			this.sharepointId = sharepointId;
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x0012C124 File Offset: 0x0012A324
		public static SharepointSession Open(ObjectId objectId, IPrincipal authenticatedUser)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (authenticatedUser == null)
			{
				throw new ArgumentNullException("authenticatedUser");
			}
			SharepointSiteId sharepointSiteId = objectId as SharepointSiteId;
			WindowsPrincipal windowsPrincipal = authenticatedUser as WindowsPrincipal;
			if (sharepointSiteId == null)
			{
				throw new ArgumentException("objectId");
			}
			if (windowsPrincipal == null)
			{
				throw new ArgumentException("authenticatedUser");
			}
			return new SharepointSession(sharepointSiteId, windowsPrincipal);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0012C17E File Offset: 0x0012A37E
		public static SharepointSession Open(SharepointWeb web, IPrincipal authenticatedUser)
		{
			if (web == null)
			{
				throw new ArgumentNullException("web");
			}
			return SharepointSession.Open(web.Id, authenticatedUser);
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x0012C19A File Offset: 0x0012A39A
		public ObjectId Id
		{
			get
			{
				return this.sharepointId;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x0012C1A2 File Offset: 0x0012A3A2
		public Uri Uri
		{
			get
			{
				return this.sharepointId.SiteUri;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x0012C1AF File Offset: 0x0012A3AF
		public string DisplayName
		{
			get
			{
				if (this.Uri.Segments.Length > 1)
				{
					return this.Uri.Segments[this.Uri.Segments.Length - 1];
				}
				return this.Uri.Host;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x0012C1E8 File Offset: 0x0012A3E8
		public Uri BaseSiteUri
		{
			get
			{
				if (!this.baseSiteUriInitialized)
				{
					if (this.Uri.Segments.Length > 1)
					{
						WindowsImpersonationContext windowsImpersonationContext = Utils.ImpersonateUser(this.Identity);
						try
						{
							try
							{
								using (Webs webs = new Webs(this.Uri.GetLeftPart(UriPartial.Authority)))
								{
									webs.Credentials = CredentialCache.DefaultCredentials;
									UriBuilder uriBuilder = new UriBuilder(this.Uri.GetLeftPart(UriPartial.Authority));
									for (int i = 1; i < this.Uri.Segments.Length - 1; i++)
									{
										UriBuilder uriBuilder2 = uriBuilder;
										uriBuilder2.Path += this.Uri.Segments[i];
									}
									string uriString = webs.WebUrlFromPageUrl(uriBuilder.Uri.ToString());
									this.baseSiteUri = new Uri(uriString);
									this.baseSiteUriInitialized = true;
								}
							}
							catch (SoapException)
							{
								Utils.UndoContext(ref windowsImpersonationContext);
								this.baseSiteUri = this.Uri;
								this.baseSiteUriInitialized = true;
							}
							catch
							{
								Utils.UndoContext(ref windowsImpersonationContext);
								throw;
							}
							goto IL_10A;
						}
						finally
						{
							Utils.UndoContext(ref windowsImpersonationContext);
						}
					}
					this.baseSiteUri = this.Uri;
					this.baseSiteUriInitialized = true;
				}
				IL_10A:
				return this.baseSiteUri;
			}
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x0012C33C File Offset: 0x0012A53C
		public ITableView GetView(PredefinedListType predefinedListType, params PropertyDefinition[] propsToReturn)
		{
			return this.InternalGetView(ListBaseType.Any, predefinedListType, null, propsToReturn);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x0012C348 File Offset: 0x0012A548
		public ITableView GetView(PredefinedListType predefinedListType, SortBy[] sortBy, PropertyDefinition[] propsToReturn)
		{
			return this.InternalGetView(ListBaseType.Any, predefinedListType, sortBy, propsToReturn);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x0012C354 File Offset: 0x0012A554
		public ITableView GetView(ListBaseType listBaseType, params PropertyDefinition[] propsToReturn)
		{
			return this.InternalGetView(listBaseType, PredefinedListType.Any, null, propsToReturn);
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x0012C360 File Offset: 0x0012A560
		public ITableView GetView(ListBaseType listBaseType, SortBy[] sortBy, PropertyDefinition[] propsToReturn)
		{
			return this.InternalGetView(listBaseType, PredefinedListType.Any, sortBy, propsToReturn);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x0012C65C File Offset: 0x0012A85C
		private ITableView InternalGetView(ListBaseType listBaseType, PredefinedListType predefinedListType, SortBy[] sortBy, params PropertyDefinition[] propsToReturn)
		{
			if (propsToReturn == null)
			{
				throw new ArgumentNullException("propsToReturn");
			}
			if (propsToReturn.Length == 0)
			{
				throw new ArgumentException("propsToReturn");
			}
			EnumValidator.ThrowIfInvalid<ListBaseType>(listBaseType, "listBaseType");
			EnumValidator.ThrowIfInvalid<PredefinedListType>(predefinedListType, "listBaseType");
			if (listBaseType != ListBaseType.Any && predefinedListType != PredefinedListType.Any)
			{
				throw new ArgumentException("listBaseType && predefinedListType");
			}
			return Utils.DoSharepointTask<ArrayTableView>(this.Identity, this.Id, (SharepointSiteId)this.Id, true, Utils.MethodType.GetView, delegate
			{
				List<object[]> list = new List<object[]>();
				ArrayTableView result;
				using (Lists lists = new Lists(this.Uri.ToString()))
				{
					lists.Credentials = CredentialCache.DefaultCredentials;
					foreach (object obj in lists.GetListCollection().SelectNodes("/sp:List", SharepointHelpers.SharepointNamespaceManager))
					{
						XmlNode xmlNode = (XmlNode)obj;
						object[] valuesFromCAMLView = SharepointHelpers.GetValuesFromCAMLView(SharepointListSchema.Instance, xmlNode, null, new PropertyDefinition[]
						{
							SharepointListSchema.ID,
							SharepointListSchema.ListType,
							SharepointListSchema.PredefinedListType,
							SharepointListSchema.IsHidden
						});
						int num = 0;
						int num2 = num + 1;
						int num3 = num2 + 1;
						int num4 = num3 + 1;
						string text = valuesFromCAMLView[num] as string;
						if (text != null && valuesFromCAMLView[num2] is int && valuesFromCAMLView[num3] is int && valuesFromCAMLView[num4] is bool && !(bool)valuesFromCAMLView[num4])
						{
							ListBaseType listBaseType2 = (ListBaseType)valuesFromCAMLView[num2];
							PredefinedListType predefinedListType2 = (PredefinedListType)valuesFromCAMLView[num3];
							if ((listBaseType == ListBaseType.Any || listBaseType == listBaseType2) && (predefinedListType == PredefinedListType.Any || predefinedListType == predefinedListType2))
							{
								SharepointListId sharepointListId;
								if (listBaseType2 == ListBaseType.DocumentLibrary)
								{
									sharepointListId = new SharepointListId(text, this.Uri, null, UriFlags.SharepointDocumentLibrary);
								}
								else
								{
									sharepointListId = new SharepointListId(text, this.Uri, null, UriFlags.SharepointList);
								}
								sharepointListId.Cache = new KeyValuePair<string, XmlNode>?(new KeyValuePair<string, XmlNode>(this.Identity.Name, xmlNode));
								object[] valuesFromCAMLView2 = SharepointHelpers.GetValuesFromCAMLView(SharepointListSchema.Instance, xmlNode, null, propsToReturn);
								for (int i = 0; i < propsToReturn.Length; i++)
								{
									DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propsToReturn[i] as DocumentLibraryPropertyDefinition;
									if (documentLibraryPropertyDefinition != null)
									{
										if (documentLibraryPropertyDefinition.PropertyId == DocumentLibraryPropertyId.Id)
										{
											valuesFromCAMLView2[i] = sharepointListId;
										}
										else if (documentLibraryPropertyDefinition.PropertyId == DocumentLibraryPropertyId.Uri)
										{
											Uri uri = SharepointHelpers.GetValuesFromCAMLView(SharepointListSchema.Instance, xmlNode, null, new PropertyDefinition[]
											{
												SharepointListSchema.DefaultViewUri
											})[0] as Uri;
											if (uri != null)
											{
												valuesFromCAMLView2[i] = new UriBuilder(sharepointListId.SiteUri.Scheme, sharepointListId.SiteUri.Host, sharepointListId.SiteUri.Port, uri.ToString()).Uri;
											}
										}
									}
								}
								list.Add(valuesFromCAMLView2);
							}
						}
					}
					result = new ArrayTableView(null, sortBy, propsToReturn, list);
				}
				return result;
			});
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x0012C804 File Offset: 0x0012AA04
		public ReadOnlyCollection<SharepointWeb> GetSubWebs()
		{
			return Utils.DoSharepointTask<ReadOnlyCollection<SharepointWeb>>(this.Identity, this.Id, null, true, Utils.MethodType.GetView, delegate
			{
				ReadOnlyCollection<SharepointWeb> result;
				using (Webs webs = new Webs(this.Uri.ToString()))
				{
					webs.Credentials = CredentialCache.DefaultCredentials;
					XmlNode webCollection = webs.GetWebCollection();
					List<SharepointWeb> list = new List<SharepointWeb>();
					foreach (object obj in webCollection.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						list.Add(new SharepointWeb(xmlNode.Attributes["Title"].Value, new SharepointSiteId(xmlNode.Attributes["Url"].Value, UriFlags.Sharepoint)));
					}
					result = new ReadOnlyCollection<SharepointWeb>(list);
				}
				return result;
			});
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x0012C831 File Offset: 0x0012AA31
		internal WindowsIdentity Identity
		{
			get
			{
				return this.windowsIdentity;
			}
		}

		// Token: 0x04002697 RID: 9879
		private readonly SharepointSiteId sharepointId;

		// Token: 0x04002698 RID: 9880
		private readonly WindowsIdentity windowsIdentity;

		// Token: 0x04002699 RID: 9881
		private Uri baseSiteUri;

		// Token: 0x0400269A RID: 9882
		private bool baseSiteUriInitialized;
	}
}
