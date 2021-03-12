using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000099 RID: 153
	public class EditMessage : OwaForm
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000293EE File Offset: 0x000275EE
		protected static int RecipientItemTypeTo
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000293F1 File Offset: 0x000275F1
		protected static int RecipientItemTypeCc
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000293F4 File Offset: 0x000275F4
		protected static int RecipientItemTypeBcc
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000293F7 File Offset: 0x000275F7
		protected string MessageId
		{
			get
			{
				if (this.message != null)
				{
					return this.message.Id.ObjectId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0002941C File Offset: 0x0002761C
		protected string ChangeKey
		{
			get
			{
				if (this.message != null)
				{
					return this.message.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0002943C File Offset: 0x0002763C
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00029444 File Offset: 0x00027644
		protected bool AddSignatureToBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00029447 File Offset: 0x00027647
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0002944F File Offset: 0x0002764F
		protected bool HasRecipients
		{
			get
			{
				return this.hasRecipients;
			}
			set
			{
				this.hasRecipients = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00029458 File Offset: 0x00027658
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00029460 File Offset: 0x00027660
		protected bool HasUnresolvedRecipients
		{
			get
			{
				return this.hasUnresolvedRecipients;
			}
			set
			{
				this.hasUnresolvedRecipients = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0002946C File Offset: 0x0002766C
		protected bool ShouldInsertBlankLine
		{
			get
			{
				return !base.IsPostFromMyself() && (string.Compare(this.Action, "reply", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.Action, "replyAll", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.Action, "Forward", StringComparison.OrdinalIgnoreCase) == 0) && base.UserContext.IsFeatureEnabled(Feature.Signature) && base.UserContext.UserOptions.AutoAddSignature;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000294DF File Offset: 0x000276DF
		protected int MessageImportance
		{
			get
			{
				if (this.message == null)
				{
					return 1;
				}
				return (int)this.message.Importance;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000294F6 File Offset: 0x000276F6
		protected bool HasAutosaveErr
		{
			get
			{
				return this.hasAutosaveErr;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00029500 File Offset: 0x00027700
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "aserr", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				this.hasAutosaveErr = true;
			}
			bool flag = this.LoadMessage();
			if (this.message != null && Utilities.IsSMime(this.message))
			{
				throw new OwaNeedsSMimeControlToEditDraftException(LocalizedStrings.GetNonEncoded(-1507367759));
			}
			string formParameter = Utilities.GetFormParameter(base.Request, "hidcmdpst", false);
			if (flag && Utilities.IsPostRequest(base.Request) && !string.IsNullOrEmpty(formParameter))
			{
				if (this.message == null)
				{
					base.Item = (this.message = EditMessageHelper.CreateDraft(base.UserContext));
				}
				if (!string.IsNullOrEmpty(formParameter))
				{
					string text = EditMessageHelper.ExecutePostCommand(formParameter, base.Request, this.message, base.UserContext);
					if (!string.IsNullOrEmpty(text))
					{
						base.Infobar.AddMessageText(text, InfobarMessageType.Error);
					}
				}
			}
			if (this.message == null && !Utilities.IsPostRequest(base.Request))
			{
				base.Item = (this.message = Utilities.CreateDraftMessageFromQueryString(base.UserContext, base.Request));
			}
			this.RenderMessage();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00029624 File Offset: 0x00027824
		private bool LoadMessage()
		{
			StoreObjectId storeObjectId = QueryStringUtilities.CreateItemStoreObjectId(base.UserContext.MailboxSession, base.Request, false);
			if (storeObjectId != null)
			{
				base.Item = (this.message = Utilities.GetItem<MessageItem>(base.UserContext, storeObjectId, new PropertyDefinition[0]));
				return false;
			}
			string formParameter = Utilities.GetFormParameter(base.Request, "hidid", false);
			string formParameter2 = Utilities.GetFormParameter(base.Request, "hidchk", false);
			if (Utilities.IsPostRequest(base.Request) && !string.IsNullOrEmpty(formParameter) && !string.IsNullOrEmpty(formParameter2))
			{
				storeObjectId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, formParameter);
				base.Item = (this.message = Utilities.GetItem<MessageItem>(base.UserContext, storeObjectId, formParameter2, new PropertyDefinition[0]));
			}
			if (this.message == null)
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "email", false);
				if (!string.IsNullOrEmpty(queryStringParameter))
				{
					StoreObjectId storeObjectId2 = null;
					if (MailToParser.TryParseMailTo(queryStringParameter, base.UserContext, out storeObjectId2))
					{
						storeObjectId = storeObjectId2;
						base.Item = (this.message = Utilities.GetItem<MessageItem>(base.UserContext, storeObjectId, new PropertyDefinition[0]));
					}
				}
			}
			return true;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00029744 File Offset: 0x00027944
		private void RenderMessage()
		{
			if (this.message != null)
			{
				base.CreateAttachmentHelpers(AttachmentWellType.ReadWrite);
				InfobarMessageBuilder.AddSensitivity(base.Infobar, this.message);
				this.message.Load(EditMessage.prefetchProperties);
				InfobarMessageBuilder.AddCompliance(base.UserContext, base.Infobar, this.message, true);
			}
			this.recipientWell = new MessageRecipientWell(base.UserContext, this.message);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.To);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.Cc);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.Bcc);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000297FB File Offset: 0x000279FB
		protected override void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(this.message, writer, base.OwaContext, base.Infobar);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00029824 File Offset: 0x00027A24
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Mail, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002984F File Offset: 0x00027A4F
		public void RenderMostRecentRecipientsOrAnr()
		{
			if (this.message == null)
			{
				this.RenderMostRecentRecipients(base.Response.Output);
				return;
			}
			this.RenderAnr();
			if (!this.HasUnresolvedRecipients)
			{
				this.RenderMostRecentRecipients(base.Response.Output);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002988C File Offset: 0x00027A8C
		private void RenderMostRecentRecipients(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			RecipientCache recipientCache = AutoCompleteCache.TryGetCache(base.OwaContext.UserContext, false);
			if (recipientCache != null)
			{
				recipientCache.SortByDisplayName();
				MRRSelect.Render(MRRSelect.Type.MessageRecipients, recipientCache, writer);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000298CA File Offset: 0x00027ACA
		private void RenderAnr()
		{
			this.HasUnresolvedRecipients = this.RecipientWell.RenderAnr(base.Response.Output, base.UserContext);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000298F0 File Offset: 0x00027AF0
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.PeoplePicker, OptionsBar.RenderingFlags.RenderSearchLocationOnly, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00029920 File Offset: 0x00027B20
		public void RenderEditMessageHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.Send);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.Save);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderDivider();
			if (this.message != null && this.message.Importance == Importance.High)
			{
				toolbar.RenderButton(ToolbarButtons.ImportanceHigh, ToolbarButtonFlags.Selected);
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.ImportanceLow);
			}
			else if (this.message != null && this.message.Importance == Importance.Low)
			{
				toolbar.RenderButton(ToolbarButtons.ImportanceHigh);
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.ImportanceLow, ToolbarButtonFlags.Selected);
			}
			else
			{
				toolbar.RenderButton(ToolbarButtons.ImportanceHigh);
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.ImportanceLow);
			}
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.AttachFile);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.CheckNames);
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00029A38 File Offset: 0x00027C38
		public void RenderEditMessageFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00029A69 File Offset: 0x00027C69
		protected void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, this.message);
		}

		// Token: 0x040003CF RID: 975
		private const string MessageIdFormParameter = "hidid";

		// Token: 0x040003D0 RID: 976
		private const string ChangeKeyFormParameter = "hidchk";

		// Token: 0x040003D1 RID: 977
		private const string CommandFormParameter = "hidcmdpst";

		// Token: 0x040003D2 RID: 978
		private const string AutosaveErrorQuerystringParameter = "aserr";

		// Token: 0x040003D3 RID: 979
		private const bool AddSignatureToBodyValue = false;

		// Token: 0x040003D4 RID: 980
		private static readonly StorePropertyDefinition[] prefetchProperties = new StorePropertyDefinition[]
		{
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid
		};

		// Token: 0x040003D5 RID: 981
		private MessageItem message;

		// Token: 0x040003D6 RID: 982
		private MessageRecipientWell recipientWell;

		// Token: 0x040003D7 RID: 983
		private bool hasRecipients;

		// Token: 0x040003D8 RID: 984
		private bool hasUnresolvedRecipients;

		// Token: 0x040003D9 RID: 985
		private bool hasAutosaveErr;
	}
}
