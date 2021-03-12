using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200007B RID: 123
	public abstract class OwaForm : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		protected static void RemoveContactPhoto(ArrayList attachmentWellRenderObjects)
		{
			if (attachmentWellRenderObjects == null)
			{
				throw new ArgumentNullException("attachmentWellRenderObjects");
			}
			for (int i = 0; i < attachmentWellRenderObjects.Count; i++)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)attachmentWellRenderObjects[i];
				using (Attachment attachment = attachmentWellInfo.OpenAttachment())
				{
					attachment.Load(new PropertyDefinition[]
					{
						AttachmentSchema.IsContactPhoto
					});
					if (attachment.IsContactPhoto)
					{
						attachmentWellRenderObjects.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001EE38 File Offset: 0x0001D038
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001EE3C File Offset: 0x0001D03C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (Utilities.GetQueryStringParameter(base.Request, "atttyp", false) != null && Utilities.GetQueryStringParameter(base.Request, "atttyp", false) == "embdd")
			{
				this.isEmbeddedItem = true;
			}
			string text = null;
			string text2 = null;
			this.itemId = QueryStringUtilities.CreateItemStoreObjectId(base.UserContext.MailboxSession, base.Request, false);
			if (this.itemId == null)
			{
				if (Utilities.IsPostRequest(base.Request))
				{
					text = Utilities.GetFormParameter(base.Request, "hidid", false);
					text2 = Utilities.GetFormParameter(base.Request, "hidchk", false);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.itemStoreId = Utilities.CreateItemId(base.UserContext.MailboxSession, text, text2);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.itemId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, text);
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001EF20 File Offset: 0x0001D120
		protected override void OnUnload(EventArgs e)
		{
			if (this.parentItem != null)
			{
				this.parentItem.Dispose();
				this.parentItem = null;
			}
			if (this.item != null)
			{
				this.item.Dispose();
				this.item = null;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001EF58 File Offset: 0x0001D158
		protected virtual void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string attachmentUrl = null;
			if (this.IsEmbeddedItem)
			{
				attachmentUrl = AttachmentWell.RenderEmbeddedUrl(this.itemId.ToBase64String());
			}
			this.AttachmentLinks = BodyConversionUtilities.GenerateNonEditableMessageBodyAndRenderInfobarMessages(this.item, writer, base.OwaContext, this.infobar, this.IsRequestCallbackForWebBeacons, this.IsRequestCallbackForPhishing, this.ItemType, string.Empty, string.Empty, this.IsEmbeddedItem, attachmentUrl);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
		protected void CreateAttachmentHelpers(AttachmentWellType wellType)
		{
			if (JunkEmailUtilities.IsJunkOrPhishing(this.Item, this.IsEmbeddedItem, base.UserContext))
			{
				this.shouldRenderAttachmentWell = false;
				return;
			}
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.Item, this.AttachmentLinks, base.UserContext.IsPublicLogon, this.IsEmbeddedItem);
			if (this.attachmentWellRenderObjects != null && this.attachmentWellRenderObjects.Count > 0 && Utilities.IsClearSigned(this.Item))
			{
				AttachmentUtility.RemoveSmimeAttachment(this.attachmentWellRenderObjects);
			}
			if (this.Item is Contact)
			{
				OwaForm.RemoveContactPhoto(this.attachmentWellRenderObjects);
			}
			InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
			if (wellType == AttachmentWellType.ReadOnly && infobarRenderingHelper.HasLevelOneAndBlock)
			{
				this.infobar.AddMessageText(string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-824680214), new object[]
				{
					infobarRenderingHelper.FileNameStringForLevelOneAndBlock
				}), InfobarMessageType.Informational);
			}
			else if (wellType != AttachmentWellType.ReadOnly && infobarRenderingHelper.HasLevelOneAndBlock)
			{
				this.infobar.AddMessageLocalized(-2118248931, InfobarMessageType.Informational);
			}
			bool flag = AttachmentUtility.IsOutLine(this.attachmentWellRenderObjects);
			this.shouldRenderAttachmentWell = (wellType == AttachmentWellType.ReadWrite || (flag && wellType == AttachmentWellType.ReadOnly && (infobarRenderingHelper.HasLevelTwo || infobarRenderingHelper.HasLevelThree || infobarRenderingHelper.HasWebReadyFirst)));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001F110 File Offset: 0x0001D310
		protected void RenderEmbeddedItemIds()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "attcnt");
			int num;
			if (!int.TryParse(queryStringParameter, out num))
			{
				throw new OwaInvalidRequestException("Invalid attachment count querystring parameter");
			}
			base.Response.Write("new Array(\"");
			base.Response.Write(Utilities.UrlEncode(this.itemId.ToBase64String()));
			base.Response.Write("\"");
			for (int i = 0; i < num; i++)
			{
				string name = "attid" + i.ToString(CultureInfo.InvariantCulture);
				string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, name);
				base.Response.Write(",\"");
				base.Response.Write(Utilities.JavascriptEncode(Utilities.UrlEncode(queryStringParameter2)));
				base.Response.Write("\"");
			}
			base.Response.Write(")");
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		internal void HandleReadReceipt(MessageItem message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (Utilities.GetQueryStringParameter(base.Request, "sndrct", false) != null)
			{
				message.SendReadReceipt();
				this.Infobar.AddMessageLocalized(641302712, InfobarMessageType.Informational);
				return;
			}
			if (!this.IsEmbeddedItem)
			{
				InfobarMessageBuilder.AddSendReceiptNotice(base.UserContext, this.Infobar, message);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001F258 File Offset: 0x0001D458
		protected virtual string ItemType
		{
			get
			{
				if (base.OwaContext.FormsRegistryContext.Type != null)
				{
					return base.OwaContext.FormsRegistryContext.Type;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001F282 File Offset: 0x0001D482
		protected virtual string ApplicationElement
		{
			get
			{
				return Convert.ToString(base.OwaContext.FormsRegistryContext.ApplicationElement);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001F29E File Offset: 0x0001D49E
		protected virtual string Action
		{
			get
			{
				if (base.OwaContext.FormsRegistryContext.Action != null)
				{
					return base.OwaContext.FormsRegistryContext.Action;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		protected virtual string State
		{
			get
			{
				if (base.OwaContext.FormsRegistryContext.State != null)
				{
					return base.OwaContext.FormsRegistryContext.State;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001F2F2 File Offset: 0x0001D4F2
		internal StoreObjectId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001F2FA File Offset: 0x0001D4FA
		protected virtual bool ShowInfobar
		{
			get
			{
				return this.Infobar.MessageCount > 0;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001F30A File Offset: 0x0001D50A
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0001F312 File Offset: 0x0001D512
		internal Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001F31B File Offset: 0x0001D51B
		internal Item ParentItem
		{
			get
			{
				return this.parentItem;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001F323 File Offset: 0x0001D523
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0001F32B File Offset: 0x0001D52B
		internal IList<AttachmentLink> AttachmentLinks
		{
			get
			{
				return this.attachmentLinks;
			}
			set
			{
				this.attachmentLinks = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0001F334 File Offset: 0x0001D534
		protected bool IsText
		{
			get
			{
				return this.Item.Body.Format == BodyFormat.TextPlain;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001F349 File Offset: 0x0001D549
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0001F351 File Offset: 0x0001D551
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001F359 File Offset: 0x0001D559
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001F361 File Offset: 0x0001D561
		protected bool IsInDeleteItems
		{
			get
			{
				return !this.isEmbeddedItem && this.Item != null && Utilities.IsItemInDefaultFolder(this.Item, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001F383 File Offset: 0x0001D583
		protected bool IsEmbeddedItem
		{
			get
			{
				return this.isEmbeddedItem;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001F38B File Offset: 0x0001D58B
		protected AttachmentWell.AttachmentWellFlags AttachmentWellFlags
		{
			get
			{
				return this.attachmentWellFlags;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001F393 File Offset: 0x0001D593
		protected bool IsRequestCallbackForPhishing
		{
			get
			{
				return Utilities.IsRequestCallbackForPhishing(base.Request);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
		protected bool IsRequestCallbackForWebBeacons
		{
			get
			{
				return Utilities.IsRequestCallbackForWebBeacons(base.Request);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001F3AD File Offset: 0x0001D5AD
		internal T Initialize<T>(params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.Initialize<T>(true, false, prefetchProperties);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		internal T Initialize<T>(bool itemRequired, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.Initialize<T>(itemRequired, false, prefetchProperties);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001F3C3 File Offset: 0x0001D5C3
		internal MessageItem InitializeAsMessageItem(params PropertyDefinition[] prefetchProperties)
		{
			return this.Initialize<MessageItem>(true, true, prefetchProperties);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		private T Initialize<T>(bool itemRequired, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			object obj = null;
			object preFormActionId = base.OwaContext.PreFormActionId;
			if (preFormActionId != null)
			{
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)preFormActionId;
				obj = Utilities.GetItem<T>(base.UserContext, owaStoreObjectId, forceAsMessageItem, prefetchProperties);
			}
			else if (this.isEmbeddedItem)
			{
				obj = Utilities.GetItemForRequest<T>(base.OwaContext, out this.parentItem, forceAsMessageItem, prefetchProperties);
			}
			else if (this.itemStoreId != null)
			{
				obj = Utilities.GetItem<T>(base.UserContext, this.itemStoreId, forceAsMessageItem, prefetchProperties);
			}
			else if (this.itemId != null)
			{
				obj = Utilities.GetItem<T>(base.UserContext, this.itemId, forceAsMessageItem, prefetchProperties);
			}
			else if (itemRequired)
			{
				throw new OwaInvalidRequestException("Missing 'id' URL parameter or 'hidid' form parameter");
			}
			this.item = (obj as Item);
			if (this.isEmbeddedItem && this.parentItem != null)
			{
				this.itemId = this.parentItem.Id.ObjectId;
				this.attachmentWellFlags |= AttachmentWell.AttachmentWellFlags.RenderEmbeddedAttachment;
			}
			if (!this.isEmbeddedItem)
			{
				if (this.IsRequestCallbackForPhishing && this.item != null)
				{
					JunkEmailUtilities.SetLinkEnabled(this.item, prefetchProperties);
				}
				Utilities.SetWebBeaconPolicy(this.IsRequestCallbackForWebBeacons, this.item, prefetchProperties);
			}
			if (obj == null)
			{
				return default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001F510 File Offset: 0x0001D710
		protected virtual void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None);
			optionsBar.Render(helpFile);
		}

		// Token: 0x040002A1 RID: 673
		private const string ItemIdFormParameter = "hidid";

		// Token: 0x040002A2 RID: 674
		private const string ChangeKeyFormParameter = "hidchk";

		// Token: 0x040002A3 RID: 675
		protected const string OutputDisplayCharset = "utf-8";

		// Token: 0x040002A4 RID: 676
		private Item item;

		// Token: 0x040002A5 RID: 677
		private IList<AttachmentLink> attachmentLinks;

		// Token: 0x040002A6 RID: 678
		protected bool isEmbeddedItem;

		// Token: 0x040002A7 RID: 679
		private Infobar infobar = new Infobar();

		// Token: 0x040002A8 RID: 680
		private Item parentItem;

		// Token: 0x040002A9 RID: 681
		private bool shouldRenderAttachmentWell;

		// Token: 0x040002AA RID: 682
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x040002AB RID: 683
		private StoreObjectId itemId;

		// Token: 0x040002AC RID: 684
		private StoreId itemStoreId;

		// Token: 0x040002AD RID: 685
		private AttachmentWell.AttachmentWellFlags attachmentWellFlags = AttachmentWell.AttachmentWellFlags.RenderEmbeddedItem;
	}
}
