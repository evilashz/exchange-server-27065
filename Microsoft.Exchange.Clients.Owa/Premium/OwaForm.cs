using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000367 RID: 871
	public class OwaForm : OwaPage
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x000BD07C File Offset: 0x000BB27C
		internal OwaForm()
		{
			this.owaFormInternal = new OwaFormInternal(base.OwaContext);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000BD095 File Offset: 0x000BB295
		internal OwaForm(bool setNoCacheNoStore) : base(setNoCacheNoStore)
		{
			this.owaFormInternal = new OwaFormInternal(base.OwaContext);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000BD0AF File Offset: 0x000BB2AF
		internal T Initialize<T>(params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.owaFormInternal.Initialize<T>(true, false, prefetchProperties);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000BD0BF File Offset: 0x000BB2BF
		internal T Initialize<T>(bool itemRequired, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.owaFormInternal.Initialize<T>(itemRequired, false, prefetchProperties);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000BD0CF File Offset: 0x000BB2CF
		internal MessageItem InitializeAsMessageItem(params PropertyDefinition[] prefetchProperties)
		{
			return this.owaFormInternal.Initialize<MessageItem>(true, true, prefetchProperties);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000BD0DF File Offset: 0x000BB2DF
		protected void MarkPayloadAsRead()
		{
			this.owaFormInternal.MarkPayloadAsRead();
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000BD0EC File Offset: 0x000BB2EC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.owaFormInternal.MarkPayloadAsRead();
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000BD100 File Offset: 0x000BB300
		protected override void OnUnload(EventArgs e)
		{
			if (this.owaFormInternal != null)
			{
				this.owaFormInternal.Dispose();
				this.owaFormInternal = null;
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000BD11C File Offset: 0x000BB31C
		protected string RenderEmbeddedUrl()
		{
			return this.owaFormInternal.RenderEmbeddedUrl();
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000BD129 File Offset: 0x000BB329
		protected void RenderEmbeddedItemIds()
		{
			this.owaFormInternal.RenderEmbeddedItemIds();
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000BD136 File Offset: 0x000BB336
		protected void RenderJavascriptEncodedItemChangeKey()
		{
			this.owaFormInternal.RenderJavascriptEncodedItemChangeKey();
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000BD143 File Offset: 0x000BB343
		protected void RenderJavascriptEncodedItemId()
		{
			this.owaFormInternal.RenderJavascriptEncodedItemId();
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000BD150 File Offset: 0x000BB350
		protected void RenderMessageInformation(TextWriter writer)
		{
			this.owaFormInternal.RenderMessageInformation(writer);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000BD15E File Offset: 0x000BB35E
		protected void RenderSubjectAttributes()
		{
			this.owaFormInternal.RenderSubjectAttributes();
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x000BD16B File Offset: 0x000BB36B
		protected bool ShowAttachmentWell
		{
			get
			{
				return this.owaFormInternal.ShowAttachmentWell;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x000BD178 File Offset: 0x000BB378
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x000BD185 File Offset: 0x000BB385
		internal Item Item
		{
			get
			{
				return this.owaFormInternal.Item;
			}
			set
			{
				this.owaFormInternal.Item = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x000BD193 File Offset: 0x000BB393
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x000BD1A0 File Offset: 0x000BB3A0
		internal IList<AttachmentLink> AttachmentLinks
		{
			get
			{
				return this.owaFormInternal.AttachmentLinks;
			}
			set
			{
				this.owaFormInternal.AttachmentLinks = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000BD1AE File Offset: 0x000BB3AE
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x000BD1BB File Offset: 0x000BB3BB
		protected bool IsPreviewForm
		{
			get
			{
				return this.owaFormInternal.IsPreviewForm;
			}
			set
			{
				this.owaFormInternal.IsPreviewForm = value;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x000BD1C9 File Offset: 0x000BB3C9
		protected bool HasCategories
		{
			get
			{
				return this.owaFormInternal.HasCategories;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000BD1D6 File Offset: 0x000BB3D6
		protected bool IsEmbeddedItem
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItem;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x000BD1E3 File Offset: 0x000BB3E3
		protected virtual bool IsPublicItem
		{
			get
			{
				return this.owaFormInternal.IsPublicItem;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x000BD1F0 File Offset: 0x000BB3F0
		protected virtual bool IsOtherMailboxItem
		{
			get
			{
				return this.owaFormInternal.IsOtherMailboxItem;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000BD1FD File Offset: 0x000BB3FD
		protected bool IsItemNull
		{
			get
			{
				return this.owaFormInternal.IsItemNull;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000BD20A File Offset: 0x000BB40A
		protected virtual bool IsSubjectEditable
		{
			get
			{
				return this.owaFormInternal.IsSubjectEditable;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000BD217 File Offset: 0x000BB417
		protected virtual bool IsItemEditable
		{
			get
			{
				return this.owaFormInternal.IsItemEditable;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000BD224 File Offset: 0x000BB424
		protected bool IsInDeleteItems
		{
			get
			{
				return this.owaFormInternal.IsInDeleteItems;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x000BD231 File Offset: 0x000BB431
		protected bool UserCanDeleteItem
		{
			get
			{
				return this.owaFormInternal.UserCanDeleteItem;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000BD23E File Offset: 0x000BB43E
		protected bool UserCanEditItem
		{
			get
			{
				return this.owaFormInternal.UserCanEditItem;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x000BD24B File Offset: 0x000BB44B
		protected string ParentItemIdBase64String
		{
			get
			{
				return this.owaFormInternal.ParentFolderIdBase64String;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000BD258 File Offset: 0x000BB458
		protected string ParentFolderIdBase64String
		{
			get
			{
				return this.owaFormInternal.ParentFolderIdBase64String;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x000BD265 File Offset: 0x000BB465
		internal OwaStoreObjectId ParentFolderId
		{
			get
			{
				return this.owaFormInternal.ParentFolderId;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x000BD272 File Offset: 0x000BB472
		protected string ItemClassName
		{
			get
			{
				return this.owaFormInternal.ItemClassName;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x000BD27F File Offset: 0x000BB47F
		protected ClientSMimeControlStatus ClientSMimeControlStatus
		{
			get
			{
				return this.owaFormInternal.ClientSMimeControlStatus;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x000BD28C File Offset: 0x000BB48C
		protected bool ForceAllowWebBeacon
		{
			get
			{
				return this.IsRequestCallbackForWebBeacons;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x000BD294 File Offset: 0x000BB494
		protected bool IsRequestCallbackForWebBeacons
		{
			get
			{
				return Utilities.IsRequestCallbackForWebBeacons(base.Request);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000BD2A1 File Offset: 0x000BB4A1
		protected bool ForceEnableItemLink
		{
			get
			{
				return this.IsRequestCallbackForPhishing;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000BD2A9 File Offset: 0x000BB4A9
		protected bool IsRequestCallbackForPhishing
		{
			get
			{
				return Utilities.IsRequestCallbackForPhishing(base.Request);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000BD2B6 File Offset: 0x000BB4B6
		protected bool IsJunkOrPhishing
		{
			get
			{
				return this.owaFormInternal.IsJunkOrPhishing;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000BD2C3 File Offset: 0x000BB4C3
		protected bool IsReportItem
		{
			get
			{
				return this.owaFormInternal.IsReportItem;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000BD2D0 File Offset: 0x000BB4D0
		protected bool IsEmbeddedItemInSMimeMessage
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItemInSMimeMessage;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000BD2DD File Offset: 0x000BB4DD
		protected bool IsEmbeddedItemInNonSMimeItem
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItemInNonSMimeItem;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000BD2EA File Offset: 0x000BB4EA
		protected virtual bool IsIgnoredConversation
		{
			get
			{
				return this.owaFormInternal.IsIgnoredConversation;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000BD2F7 File Offset: 0x000BB4F7
		protected bool ShouldRenderDownloadAllLink
		{
			get
			{
				return this.owaFormInternal.ShouldRenderDownloadAllLink;
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000BD304 File Offset: 0x000BB504
		protected void SetShouldRenderDownloadAllLink(ArrayList attachmentWellInfos)
		{
			this.owaFormInternal.SetShouldRenderDownloadAllLink(attachmentWellInfos);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000BD312 File Offset: 0x000BB512
		protected void RenderDownloadAllAttachmentsLink()
		{
			this.owaFormInternal.RenderDownloadAllAttachmentsLink();
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000BD31F File Offset: 0x000BB51F
		protected void RenderAttachmentWellForReadItem(ArrayList attachmentWellRenderObjects)
		{
			this.owaFormInternal.RenderAttachmentWellForReadItem(attachmentWellRenderObjects);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000BD32D File Offset: 0x000BB52D
		protected void RenderActionButtons(bool isInJunkMailFolder, bool isPost)
		{
			this.owaFormInternal.RenderActionButtons(isInJunkMailFolder, isPost);
		}

		// Token: 0x04001780 RID: 6016
		private OwaFormInternal owaFormInternal;
	}
}
