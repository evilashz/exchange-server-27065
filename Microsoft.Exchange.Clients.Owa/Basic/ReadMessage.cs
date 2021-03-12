using System;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000A6 RID: 166
	public class ReadMessage : OwaForm
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x000300A8 File Offset: 0x0002E2A8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (ObjectClass.IsMessage(base.OwaContext.FormsRegistryContext.Type, false))
			{
				this.message = base.Initialize<MessageItem>(ReadMessage.prefetchProperties);
			}
			else
			{
				this.message = base.InitializeAsMessageItem(ReadMessage.prefetchProperties);
			}
			this.recipientWell = new MessageRecipientWell(base.UserContext, this.message);
			RenderingUtilities.RenderReplyForwardMessageStatus(this.message, base.Infobar, base.UserContext);
			object obj = this.message.TryGetProperty(MessageItemSchema.IsDraft);
			if (obj is bool && (bool)obj)
			{
				base.Infobar.AddMessageLocalized(-1981719796, InfobarMessageType.Informational);
			}
			else
			{
				InfobarMessageBuilder.AddImportance(base.Infobar, this.message);
				InfobarMessageBuilder.AddSensitivity(base.Infobar, this.message);
				InfobarMessageBuilder.AddCompliance(base.UserContext, base.Infobar, this.message, false);
				if (Utilities.IsClearSigned(this.message) || Utilities.IsOpaqueSigned(this.message))
				{
					base.Infobar.AddMessageLocalized(-1329088272, InfobarMessageType.Warning);
				}
				else if (Utilities.IsEncrypted(this.message))
				{
					base.Infobar.AddMessageLocalized(-767943720, InfobarMessageType.Warning);
				}
			}
			InfobarMessageBuilder.AddFlag(base.Infobar, this.message, base.UserContext);
			if (this.message.Id != null && !this.message.IsRead)
			{
				this.message.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, this.message), false);
			}
			this.isJunk = false;
			if (!this.isEmbeddedItem)
			{
				this.isJunk = Utilities.IsDefaultFolderId(base.Item.Session, this.CurrentFolderId, DefaultFolderType.JunkEmail);
			}
			base.HandleReadReceipt(this.message);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0003026C File Offset: 0x0002E46C
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Mail, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00030298 File Offset: 0x0002E498
		public void RenderMailSecondaryNavigation()
		{
			MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, this.CurrentFolderId, null, null, null);
			mailSecondaryNavigation.Render(base.Response.Output);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000302D4 File Offset: 0x0002E4D4
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.Mail, OptionsBar.RenderingFlags.None, OptionsBar.BuildFolderSearchUrlSuffix(base.UserContext, this.CurrentFolderId));
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00030312 File Offset: 0x0002E512
		public void RenderHeaderToolbar()
		{
			ReadMessageToolbarUtility.BuildHeaderToolbar(base.UserContext, base.Response.Output, base.IsEmbeddedItem, this.message, this.isJunk, JunkEmailUtilities.IsSuspectedPhishingItem(this.message), JunkEmailUtilities.IsItemLinkEnabled(this.message));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00030354 File Offset: 0x0002E554
		public void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			if (!base.IsEmbeddedItem)
			{
				toolbar.RenderButton(ToolbarButtons.Previous);
				toolbar.RenderButton(ToolbarButtons.Next);
			}
			toolbar.RenderEnd();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000303A3 File Offset: 0x0002E5A3
		protected void RenderSender()
		{
			RenderingUtilities.RenderSender(base.UserContext, base.SanitizingResponse, this.message);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000303BC File Offset: 0x0002E5BC
		protected void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.SanitizingResponse, this.message);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000303CF File Offset: 0x0002E5CF
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(this.message, base.SanitizingResponse, "DIV.PlainText");
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000303E7 File Offset: 0x0002E5E7
		protected void RenderJavascriptEncodedClassName()
		{
			Utilities.JavascriptEncode(base.ParentItem.ClassName, base.SanitizingResponse);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000303FF File Offset: 0x0002E5FF
		protected string MessageItemId
		{
			get
			{
				return base.ItemId.ToBase64String();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0003040C File Offset: 0x0002E60C
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00030414 File Offset: 0x0002E614
		internal StoreObjectId CurrentFolderId
		{
			get
			{
				if (!base.IsEmbeddedItem)
				{
					return base.Item.ParentId;
				}
				return base.ParentItem.ParentId;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00030435 File Offset: 0x0002E635
		protected string CurrentFolderIdString
		{
			get
			{
				return this.CurrentFolderId.ToBase64String();
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00030442 File Offset: 0x0002E642
		protected bool ShowBccInSentItems
		{
			get
			{
				return this.recipientWell.HasRecipients(RecipientWellType.Bcc);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00030450 File Offset: 0x0002E650
		protected string Subject
		{
			get
			{
				string text = ItemUtility.GetProperty<string>(base.Item, ItemSchema.Subject, string.Empty);
				if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
				{
					text = LocalizedStrings.GetNonEncoded(730745110);
				}
				return text;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00030487 File Offset: 0x0002E687
		protected ExDateTime MessageSentTime
		{
			get
			{
				return this.message.SentTime;
			}
		}

		// Token: 0x04000461 RID: 1121
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.BlockStatus,
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid,
			ItemSchema.EdgePcl,
			ItemSchema.LinkEnabled,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			MessageItemSchema.IsReadReceiptPending
		};

		// Token: 0x04000462 RID: 1122
		private MessageItem message;

		// Token: 0x04000463 RID: 1123
		private MessageRecipientWell recipientWell;

		// Token: 0x04000464 RID: 1124
		private bool isJunk;
	}
}
