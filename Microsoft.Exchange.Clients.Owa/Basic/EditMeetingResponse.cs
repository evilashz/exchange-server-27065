using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000098 RID: 152
	public class EditMeetingResponse : OwaForm
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00028DCA File Offset: 0x00026FCA
		protected static bool HasAttachments
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00028DCD File Offset: 0x00026FCD
		protected static int RecipientItemTypeTo
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00028DD0 File Offset: 0x00026FD0
		protected static int RecipientItemTypeCc
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00028DD3 File Offset: 0x00026FD3
		protected static int RecipientItemTypeBcc
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00028DD6 File Offset: 0x00026FD6
		protected string MessageId
		{
			get
			{
				if (base.Item != null)
				{
					return base.Item.Id.ObjectId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00028DFB File Offset: 0x00026FFB
		protected string ChangeKey
		{
			get
			{
				if (base.Item != null)
				{
					return base.Item.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00028E1B File Offset: 0x0002701B
		protected string Location
		{
			get
			{
				return ItemUtility.GetProperty<string>(base.Item, CalendarItemBaseSchema.Location, string.Empty);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00028E32 File Offset: 0x00027032
		protected string When
		{
			get
			{
				return Utilities.GenerateWhen(base.Item);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00028E3F File Offset: 0x0002703F
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00028E47 File Offset: 0x00027047
		protected bool AddSignatureToBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00028E4A File Offset: 0x0002704A
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00028E52 File Offset: 0x00027052
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00028E5B File Offset: 0x0002705B
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00028E63 File Offset: 0x00027063
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00028E6C File Offset: 0x0002706C
		protected int MessageItemImportance
		{
			get
			{
				if (this.messageItem == null)
				{
					return 1;
				}
				return (int)this.messageItem.Importance;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00028E83 File Offset: 0x00027083
		protected bool ObjectClassIsMeetingResponse
		{
			get
			{
				return ObjectClass.IsMeetingResponse(this.ItemType);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00028E90 File Offset: 0x00027090
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.LoadMessage();
			bool flag = true;
			if (Utilities.GetQueryStringParameter(base.Request, "id", false) != null)
			{
				flag = false;
			}
			string formParameter = Utilities.GetFormParameter(base.Request, "hidcmdpst", false);
			if (flag && Utilities.IsPostRequest(base.Request) && !string.IsNullOrEmpty(formParameter))
			{
				if (base.Item == null)
				{
					base.Item = EditMessageHelper.CreateDraft(base.UserContext);
				}
				if (!string.IsNullOrEmpty(formParameter))
				{
					string text = EditMessageHelper.ExecutePostCommand(formParameter, base.Request, this.messageItem, base.UserContext);
					if (!string.IsNullOrEmpty(text))
					{
						base.Infobar.AddMessageText(text, InfobarMessageType.Error);
					}
				}
			}
			this.RenderMessage();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00028F40 File Offset: 0x00027140
		private void LoadMessage()
		{
			if (ObjectClass.IsMeetingResponse(this.ItemType))
			{
				base.Item = base.Initialize<MeetingResponse>(EditMeetingResponse.prefetchProperties);
				this.InitializeMeetingResponse();
			}
			else if (ObjectClass.IsMeetingRequest(this.ItemType))
			{
				base.Item = base.Initialize<MeetingRequest>(EditMeetingResponse.prefetchProperties);
			}
			else
			{
				if (!ObjectClass.IsMeetingCancellation(this.ItemType))
				{
					throw new OwaInvalidRequestException(string.Format("Unsupported item type '{0}' for edit meeting page", this.ItemType));
				}
				base.Infobar.AddMessageLocalized(-161808760, InfobarMessageType.Informational);
				base.Item = base.Initialize<MeetingCancellation>(EditMeetingResponse.prefetchProperties);
			}
			this.messageItem = (base.Item as MessageItem);
			this.recipientWell = new MessageRecipientWell(base.UserContext, this.messageItem);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00029004 File Offset: 0x00027204
		private void RenderMessage()
		{
			base.CreateAttachmentHelpers(AttachmentWellType.ReadWrite);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.To);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.Cc);
			this.hasRecipients |= this.recipientWell.HasRecipients(RecipientWellType.Bcc);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00029064 File Offset: 0x00027264
		public void RenderNavigation()
		{
			ModuleViewState moduleViewState = base.UserContext.LastClientViewState as ModuleViewState;
			if (moduleViewState != null)
			{
				this.navigationModule = moduleViewState.NavigationModule;
			}
			Navigation navigation = new Navigation(this.navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000290B4 File Offset: 0x000272B4
		public void RenderMostRecentRecipientsOrAnr()
		{
			if (this.messageItem == null)
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

		// Token: 0x0600050A RID: 1290 RVA: 0x000290F0 File Offset: 0x000272F0
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

		// Token: 0x0600050B RID: 1291 RVA: 0x0002912E File Offset: 0x0002732E
		private void RenderAnr()
		{
			this.HasUnresolvedRecipients = this.RecipientWell.RenderAnr(base.Response.Output, base.UserContext);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00029154 File Offset: 0x00027354
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.PeoplePicker, OptionsBar.RenderingFlags.RenderSearchLocationOnly, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00029184 File Offset: 0x00027384
		public void RenderEditMeetingResponseHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.Send);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderDivider();
			if (this.messageItem != null && this.messageItem.Importance == Importance.High)
			{
				toolbar.RenderButton(ToolbarButtons.ImportanceHigh, ToolbarButtonFlags.Selected);
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.ImportanceLow);
			}
			else if (this.messageItem != null && this.messageItem.Importance == Importance.Low)
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

		// Token: 0x0600050E RID: 1294 RVA: 0x0002928C File Offset: 0x0002748C
		public void RenderEditMeetingResponseFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000292BD File Offset: 0x000274BD
		protected override void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(base.Item, writer, base.OwaContext, base.Infobar);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000292E8 File Offset: 0x000274E8
		protected void RenderSender(TextWriter writer)
		{
			MeetingMessage meetingMessage = base.Item as MeetingMessage;
			if (Utilities.IsOnBehalfOf(meetingMessage.Sender, meetingMessage.From))
			{
				RenderingUtilities.RenderSenderOnBehalfOf(meetingMessage.Sender, meetingMessage.From, writer, base.UserContext);
				return;
			}
			RenderingUtilities.RenderSender(base.UserContext, writer, meetingMessage.From);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00029340 File Offset: 0x00027540
		private void InitializeMeetingResponse()
		{
			MeetingResponse meetingResponse = (MeetingResponse)base.Item;
			ResponseType responseType = meetingResponse.ResponseType;
			Strings.IDs stringId = -1018465893;
			switch (responseType)
			{
			case ResponseType.Tentative:
				stringId = -1248725275;
				break;
			case ResponseType.Accept:
				stringId = 1515395588;
				break;
			case ResponseType.Decline:
				stringId = -1707599932;
				break;
			}
			base.Infobar.AddMessageLocalized(stringId, InfobarMessageType.Informational);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000293A1 File Offset: 0x000275A1
		protected void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, this.messageItem);
		}

		// Token: 0x040003C3 RID: 963
		private const string MessageIdQueryStringParameter = "id";

		// Token: 0x040003C4 RID: 964
		private const string MessageIdFormParameter = "hidid";

		// Token: 0x040003C5 RID: 965
		private const string ChangeKeyFormParameter = "hidchk";

		// Token: 0x040003C6 RID: 966
		private const string CommandFormParameter = "hidcmdpst";

		// Token: 0x040003C7 RID: 967
		private const string SelectedUsingParameterName = "slUsng";

		// Token: 0x040003C8 RID: 968
		private const bool AddSignatureToBodyValue = false;

		// Token: 0x040003C9 RID: 969
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsDeliveryReceiptRequested
		};

		// Token: 0x040003CA RID: 970
		private MessageRecipientWell recipientWell;

		// Token: 0x040003CB RID: 971
		private bool hasRecipients;

		// Token: 0x040003CC RID: 972
		private bool hasUnresolvedRecipients;

		// Token: 0x040003CD RID: 973
		private NavigationModule navigationModule;

		// Token: 0x040003CE RID: 974
		private MessageItem messageItem;
	}
}
