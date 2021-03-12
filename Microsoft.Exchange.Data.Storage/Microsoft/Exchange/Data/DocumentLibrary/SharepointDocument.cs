using System;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006DE RID: 1758
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointDocument : SharepointDocumentLibraryItem, IDocument, IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x060045FF RID: 17919 RVA: 0x00129CAC File Offset: 0x00127EAC
		internal SharepointDocument(SharepointDocumentLibraryItemId id, SharepointSession session, XmlNode dataCache) : base(id, session, dataCache, SharepointDocumentSchema.Instance)
		{
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00129CBC File Offset: 0x00127EBC
		public new static SharepointDocument Read(SharepointSession session, ObjectId id)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			SharepointDocumentLibraryItemId sharepointDocumentLibraryItemId = id as SharepointDocumentLibraryItemId;
			if (sharepointDocumentLibraryItemId == null)
			{
				throw new ArgumentException("id");
			}
			if (sharepointDocumentLibraryItemId.UriFlags != UriFlags.SharepointDocument)
			{
				throw new ArgumentException("id");
			}
			return (SharepointDocument)SharepointDocumentLibraryItem.Read(session, id);
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x00129D1C File Offset: 0x00127F1C
		public VersionControl VersionControl
		{
			get
			{
				string text = this.TryGetProperty(SharepointDocumentSchema.CheckedOutUserId) as string;
				int versionId = (int)base[SharepointDocumentSchema.VersionId];
				return new VersionControl(!string.IsNullOrEmpty(text), text, versionId);
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x00129D5B File Offset: 0x00127F5B
		public string FileType
		{
			get
			{
				return base.GetValueOrDefault<string>(SharepointDocumentSchema.FileType);
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x00129D68 File Offset: 0x00127F68
		public long Size
		{
			get
			{
				return (long)base[SharepointDocumentSchema.FileSize];
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00129E80 File Offset: 0x00128080
		public Stream GetDocument()
		{
			return Utils.DoSharepointTask<Stream>(this.Session.Identity, base.Id, (SharepointSiteId)base.Id, true, Utils.MethodType.GetStream, delegate
			{
				Stream stream = null;
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Headers.Set("Pragma", "no-cache");
				httpWebRequest.Headers.Set("Depth", "0");
				httpWebRequest.ContentType = "text/xml";
				httpWebRequest.ContentLength = 0L;
				httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				stream = httpWebResponse.GetResponseStream();
				bool flag = false;
				try
				{
					SharepointDocumentLibraryItemId sharepointDocumentLibraryItemId = (SharepointDocumentLibraryItemId)base.Id;
					XmlNode nodeForItem = SharepointDocumentLibraryItem.GetNodeForItem(this.Session, sharepointDocumentLibraryItemId);
					SharepointDocument sharepointDocument = new SharepointDocument(sharepointDocumentLibraryItemId, this.Session, nodeForItem);
					if (sharepointDocument.VersionControl.TipVersion != this.VersionControl.TipVersion)
					{
						throw new DocumentModifiedException(base.Id, this.Uri.ToString());
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						stream.Dispose();
						stream = null;
					}
				}
				return stream;
			});
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x00129EB1 File Offset: 0x001280B1
		public override SharepointItemType ItemType
		{
			get
			{
				return SharepointItemType.Document;
			}
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x00129EB5 File Offset: 0x001280B5
		public override object TryGetProperty(PropertyDefinition propDef)
		{
			if (propDef == SharepointDocumentSchema.VersionControl)
			{
				return this.VersionControl;
			}
			return base.TryGetProperty(propDef);
		}
	}
}
