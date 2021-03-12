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
	// Token: 0x02000464 RID: 1124
	public class PrintPost : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002A26 RID: 10790 RVA: 0x000EC440 File Offset: 0x000EA640
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string type = base.OwaContext.FormsRegistryContext.Type;
			if (ObjectClass.IsPost(type))
			{
				this.post = base.Initialize<PostItem>(PrintPost.prefetchProperties);
			}
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.post, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
			this.shouldRenderAttachmentWell = PrintAttachmentWell.ShouldRenderAttachments(this.attachmentWellRenderObjects);
			if (this.post.Importance == Importance.High)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(-77932258);
			}
			else if (this.post.Importance == Importance.Low)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(1502599728);
			}
			switch (this.post.Sensitivity)
			{
			case Sensitivity.Personal:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(567923294);
				break;
			case Sensitivity.Private:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(-1268489823);
				break;
			case Sensitivity.CompanyConfidential:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(-819101664);
				break;
			}
			this.categoriesString = ItemUtility.GetCategoriesAsString(this.post);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000EC55B File Offset: 0x000EA75B
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			BodyConversionUtilities.GeneratePrintMessageBody(this.post, writer, base.OwaContext, base.IsEmbeddedItem, base.IsEmbeddedItem ? base.RenderEmbeddedUrl() : null, base.ForceAllowWebBeacon, base.ForceEnableItemLink);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000EC594 File Offset: 0x000EA794
		protected void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (Utilities.IsOnBehalfOf(this.post.Sender, this.post.From))
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), RenderingUtilities.GetDisplaySenderName(this.post.Sender), RenderingUtilities.GetDisplaySenderName(this.post.From));
				return;
			}
			writer.Write(RenderingUtilities.GetDisplaySenderName(this.post.Sender));
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000EC613 File Offset: 0x000EA813
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.Response.Output, this.post, LocalizedStrings.GetNonEncoded(-439597685));
				return;
			}
			RenderingUtilities.RenderSubject(base.Response.Output, this.post);
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000EC64F File Offset: 0x000EA84F
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000EC657 File Offset: 0x000EA857
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000EC65F File Offset: 0x000EA85F
		protected string ImportanceString
		{
			get
			{
				return this.importanceString;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x000EC667 File Offset: 0x000EA867
		protected string SensitivityString
		{
			get
			{
				return this.sensitivityString;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x000EC66F File Offset: 0x000EA86F
		protected string CategoriesString
		{
			get
			{
				return this.categoriesString;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000EC677 File Offset: 0x000EA877
		protected ExDateTime MessageSentTime
		{
			get
			{
				return this.post.PostedTime;
			}
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000EC684 File Offset: 0x000EA884
		protected void RenderJavascriptEncodedFolderId()
		{
			if (this.folderId != null)
			{
				Utilities.JavascriptEncode(this.folderId.ToBase64String(), base.Response.Output);
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000EC6AC File Offset: 0x000EA8AC
		protected void RenderPostedFolder(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, base.ParentFolderId, new PropertyDefinition[0]))
			{
				string displayName = folder.DisplayName;
				Utilities.HtmlEncode(displayName, writer);
				this.folderId = base.ParentFolderId;
			}
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000EC718 File Offset: 0x000EA918
		protected void RenderConversation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			Utilities.HtmlEncode(this.post.ConversationTopic, writer);
		}

		// Token: 0x04001C84 RID: 7300
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.BlockStatus,
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime
		};

		// Token: 0x04001C85 RID: 7301
		private PostItem post;

		// Token: 0x04001C86 RID: 7302
		private OwaStoreObjectId folderId;

		// Token: 0x04001C87 RID: 7303
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001C88 RID: 7304
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001C89 RID: 7305
		private string sensitivityString;

		// Token: 0x04001C8A RID: 7306
		private string importanceString;

		// Token: 0x04001C8B RID: 7307
		private string categoriesString;
	}
}
