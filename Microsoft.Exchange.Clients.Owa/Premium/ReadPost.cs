using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000471 RID: 1137
	public class ReadPost : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x000F28E0 File Offset: 0x000F0AE0
		protected static int StoreObjectTypePost
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000F28E4 File Offset: 0x000F0AE4
		protected static string SaveNamespace
		{
			get
			{
				return "ReadPost";
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000F28EB File Offset: 0x000F0AEB
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000F28F4 File Offset: 0x000F0AF4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.post = base.Initialize<PostItem>(ReadPost.prefetchProperties);
			if (!base.IsPreviewForm && !base.IsEmbeddedItem)
			{
				this.post.MarkAsRead(false);
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(this.post, base.IsEmbeddedItem, base.ForceEnableItemLink, base.UserContext, out this.isInJunkmailFolder, out flag, out flag2, out flag3);
			this.isSuspectedPhishingItemWithoutLinkEnabled = (flag && !flag2);
			this.toolbar = new ReadPostToolbar(base.IsEmbeddedItem, base.Item);
			InfobarMessageBuilder.AddImportance(this.infobar, this.post);
			InfobarMessageBuilder.AddSensitivity(this.infobar, this.post);
			InfobarMessageBuilder.AddFlag(this.infobar, this.post, base.UserContext);
			InfobarMessageBuilder.AddCompliance(base.UserContext, this.infobar, this.post, false);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000F29DC File Offset: 0x000F0BDC
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, this.post);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000F29F0 File Offset: 0x000F0BF0
		protected void RenderJavascriptEncodedMessageId()
		{
			string s = OwaStoreObjectId.CreateFromStoreObject(this.post).ToBase64String();
			Utilities.JavascriptEncode(s, base.Response.Output);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000F2A21 File Offset: 0x000F0C21
		protected void RenderJavascriptEncodedMessageChangeKey()
		{
			Utilities.JavascriptEncode(this.post.Id.ChangeKeyAsBase64String(), base.Response.Output);
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000F2A43 File Offset: 0x000F0C43
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000F2A4B File Offset: 0x000F0C4B
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000F2A53 File Offset: 0x000F0C53
		protected void RenderCategories()
		{
			CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, this.post);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000F2A6C File Offset: 0x000F0C6C
		protected void RenderConversation(TextWriter writer)
		{
			Utilities.HtmlEncode(this.post.ConversationTopic, writer);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000F2A80 File Offset: 0x000F0C80
		protected void RenderPostedFolder(TextWriter writer)
		{
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, base.ParentFolderId, new PropertyDefinition[0]))
			{
				Utilities.HtmlEncode(folder.DisplayName, writer);
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000F2AD0 File Offset: 0x000F0CD0
		protected ExDateTime PostedTime
		{
			get
			{
				return this.post.PostedTime;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000F2ADD File Offset: 0x000F0CDD
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000F2AE5 File Offset: 0x000F0CE5
		protected void RenderToolbar()
		{
			this.toolbar.Render(base.Response.Output);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000F2AFD File Offset: 0x000F0CFD
		protected void RenderSender(TextWriter writer)
		{
			RenderingUtilities.RenderSender(base.UserContext, writer, this.post);
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000F2B11 File Offset: 0x000F0D11
		protected bool HasSender
		{
			get
			{
				return this.post.Sender != null;
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000F2B24 File Offset: 0x000F0D24
		protected void CreateAttachmentHelpers()
		{
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.post, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem, base.ForceEnableItemLink);
			this.shouldRenderAttachmentWell = RenderingUtilities.AddAttachmentInfobarMessages(base.Item, base.IsEmbeddedItem, base.ForceEnableItemLink, this.infobar, this.attachmentWellRenderObjects);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000F2B88 File Offset: 0x000F0D88
		protected void LoadPostBodyIntoStream(TextWriter writer)
		{
			string action = base.IsPreviewForm ? "Preview" : string.Empty;
			string attachmentUrl = null;
			if (base.IsEmbeddedItemInNonSMimeItem)
			{
				attachmentUrl = base.RenderEmbeddedUrl();
			}
			base.AttachmentLinks = BodyConversionUtilities.GenerateNonEditableMessageBodyAndRenderInfobarMessages(this.post, writer, base.OwaContext, this.infobar, base.ForceAllowWebBeacon, base.ForceEnableItemLink, this.post.ClassName, action, string.Empty, base.IsEmbeddedItemInNonSMimeItem, attachmentUrl);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000F2BFD File Offset: 0x000F0DFD
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.Response.Output, this.post, LocalizedStrings.GetNonEncoded(-439597685));
				return;
			}
			RenderingUtilities.RenderSubject(base.Response.Output, this.post);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000F2C39 File Offset: 0x000F0E39
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(this.post, base.Response.Output, "DIV#divBdy");
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000F2C56 File Offset: 0x000F0E56
		protected bool IsSuspectedPhishingItemWithoutLinkEnabled
		{
			get
			{
				return this.isSuspectedPhishingItemWithoutLinkEnabled;
			}
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000F2C5E File Offset: 0x000F0E5E
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000F2C80 File Offset: 0x000F0E80
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000F2CA2 File Offset: 0x000F0EA2
		protected bool IsInJunkMailFolder
		{
			get
			{
				return this.isInJunkmailFolder;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000F2CAA File Offset: 0x000F0EAA
		protected bool CanCreateItemInParentFolder
		{
			get
			{
				return Utilities.CanCreateItemInFolder(base.UserContext, base.ParentFolderId);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000F2CBD File Offset: 0x000F0EBD
		protected FlagAction FlagAction
		{
			get
			{
				return FlagContextMenu.GetFlagActionForItem(base.UserContext, this.post);
			}
		}

		// Token: 0x04001CD4 RID: 7380
		private static readonly StorePropertyDefinition[] prefetchProperties = new StorePropertyDefinition[]
		{
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid,
			ItemSchema.EdgePcl,
			StoreObjectSchema.EffectiveRights,
			ItemSchema.IsClassified,
			ItemSchema.LinkEnabled,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.Categories,
			ItemSchema.FlagCompleteTime,
			ItemSchema.FlagStatus,
			MessageItemSchema.ReplyTime
		};

		// Token: 0x04001CD5 RID: 7381
		private PostItem post;

		// Token: 0x04001CD6 RID: 7382
		private Infobar infobar = new Infobar();

		// Token: 0x04001CD7 RID: 7383
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001CD8 RID: 7384
		private ReadPostToolbar toolbar;

		// Token: 0x04001CD9 RID: 7385
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001CDA RID: 7386
		private bool isSuspectedPhishingItemWithoutLinkEnabled;

		// Token: 0x04001CDB RID: 7387
		private bool isInJunkmailFolder;

		// Token: 0x04001CDC RID: 7388
		private Markup bodyMarkup;
	}
}
