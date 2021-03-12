using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000096 RID: 150
	public class EditCalendarItem : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00027122 File Offset: 0x00025322
		protected static int MaxLocationLength
		{
			get
			{
				return 255;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00027129 File Offset: 0x00025329
		protected static int RecipientItemTypeTo
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0002712C File Offset: 0x0002532C
		protected static int RecipientItemTypeCc
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0002712F File Offset: 0x0002532F
		protected static int RecipientItemTypeBcc
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00027132 File Offset: 0x00025332
		protected static int CalendarTabAppointment
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00027135 File Offset: 0x00025335
		protected string FolderId
		{
			get
			{
				if (this.folderId != null)
				{
					return this.folderId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00027150 File Offset: 0x00025350
		protected bool IsAppointment
		{
			get
			{
				return !this.CalendarItemBase.IsMeeting;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00027160 File Offset: 0x00025360
		protected bool IsNewAppointment
		{
			get
			{
				return this.IsAppointment && this.IsUnsaved;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00027172 File Offset: 0x00025372
		protected bool IsMeeting
		{
			get
			{
				return this.CalendarItemBase != null && this.CalendarItemBase.IsMeeting;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0002718C File Offset: 0x0002538C
		protected bool IsRecurringMaster
		{
			get
			{
				CalendarItem calendarItem = base.Item as CalendarItem;
				return calendarItem != null && calendarItem.Recurrence != null && calendarItem.CalendarItemType == CalendarItemType.RecurringMaster;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000271BC File Offset: 0x000253BC
		protected bool IsOccurrence
		{
			get
			{
				return this.CalendarItemBase != null && this.CalendarItemBase is CalendarItemOccurrence;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000271D6 File Offset: 0x000253D6
		protected bool IsUnsaved
		{
			get
			{
				return this.CalendarItemBase != null && this.CalendarItemBase.Id == null;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000271F0 File Offset: 0x000253F0
		protected bool MeetingRequestWasSent
		{
			get
			{
				return base.Item != null && this.IsMeeting && this.CalendarItemBase.MeetingRequestWasSent;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00027211 File Offset: 0x00025411
		protected string Id
		{
			get
			{
				if (this.CalendarItemBase != null && this.CalendarItemBase.Id != null)
				{
					return this.CalendarItemBase.Id.ObjectId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00027243 File Offset: 0x00025443
		protected string ChangeKey
		{
			get
			{
				if (this.CalendarItemBase != null && this.CalendarItemBase.Id != null)
				{
					return this.CalendarItemBase.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00027270 File Offset: 0x00025470
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00027278 File Offset: 0x00025478
		internal CalendarItemBase CalendarItemBase
		{
			get
			{
				return this.calendarItemBase;
			}
			set
			{
				base.Item = value;
				this.calendarItemBase = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00027288 File Offset: 0x00025488
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00027290 File Offset: 0x00025490
		protected bool HasRecipients
		{
			get
			{
				return this.recipientWell.HasRecipients(RecipientWellType.To) || this.recipientWell.HasRecipients(RecipientWellType.Cc) || this.recipientWell.HasRecipients(RecipientWellType.Bcc);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000272BC File Offset: 0x000254BC
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000272C4 File Offset: 0x000254C4
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000272CD File Offset: 0x000254CD
		public bool IsAllDayEvent
		{
			get
			{
				return this.CalendarItemBase != null && this.CalendarItemBase.IsAllDayEvent;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000272E4 File Offset: 0x000254E4
		public bool IsResponseRequested
		{
			get
			{
				if (this.CalendarItemBase != null)
				{
					object obj = this.CalendarItemBase.TryGetProperty(ItemSchema.IsResponseRequested);
					return obj is bool && (bool)obj;
				}
				return false;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0002731C File Offset: 0x0002551C
		public bool IsPrivate
		{
			get
			{
				return this.CalendarItemBase != null && this.CalendarItemBase.Sensitivity == Sensitivity.Private;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00027336 File Offset: 0x00025536
		protected string CalendarId
		{
			get
			{
				return this.CalendarItemBase.ParentId.ToBase64String();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00027348 File Offset: 0x00025548
		protected string When
		{
			get
			{
				if (!(this.CalendarItemBase is CalendarItem))
				{
					return "&nbsp;";
				}
				CalendarItem calendarItem = (CalendarItem)this.CalendarItemBase;
				if (calendarItem.Recurrence != null)
				{
					return Utilities.HtmlEncode(calendarItem.GenerateWhen());
				}
				return "&nbsp;";
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0002738D File Offset: 0x0002558D
		protected bool IsBeingCanceled
		{
			get
			{
				return this.isBeingCanceled;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00027395 File Offset: 0x00025595
		protected bool IsDirty
		{
			get
			{
				return CalendarUtilities.IsCalendarItemDirty(this.CalendarItemBase, base.UserContext);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000273A8 File Offset: 0x000255A8
		protected bool IsSendUpdateRequired
		{
			get
			{
				return this.IsMeeting && this.MeetingRequestWasSent && EditCalendarItemHelper.IsSendUpdateRequired(this.CalendarItemBase, base.UserContext);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000273CD File Offset: 0x000255CD
		protected string Subject
		{
			get
			{
				if (this.CalendarItemBase != null && this.CalendarItemBase.Subject != null)
				{
					return this.CalendarItemBase.Subject;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000273F5 File Offset: 0x000255F5
		protected string Location
		{
			get
			{
				if (this.CalendarItemBase != null && this.CalendarItemBase.Location != null)
				{
					return this.CalendarItemBase.Location;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00027420 File Offset: 0x00025620
		protected int DurationMinutes
		{
			get
			{
				return (int)(this.CalendarItemBase.EndTime - this.CalendarItemBase.StartTime).TotalMinutes;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00027451 File Offset: 0x00025651
		public string SendIssuesPrompt
		{
			get
			{
				if (this.sendIssuesPrompt != null)
				{
					return this.sendIssuesPrompt;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00027467 File Offset: 0x00025667
		protected int CalendarItemBaseImportance
		{
			get
			{
				if (this.CalendarItemBase == null)
				{
					return 1;
				}
				return (int)this.CalendarItemBase.Importance;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00027480 File Offset: 0x00025680
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string action = this.Action;
			if (Utilities.IsPostRequest(base.Request) && string.Equals(action, "Delete", StringComparison.Ordinal))
			{
				this.isBeingCanceled = true;
			}
			CalendarItemBase calendarItemBase = base.OwaContext.PreFormActionData as CalendarItemBase;
			this.folderId = EditCalendarItemHelper.GetCalendarFolderId(base.Request, base.UserContext);
			if (calendarItemBase != null)
			{
				this.CalendarItemBase = calendarItemBase;
				base.OwaContext.PreFormActionData = null;
			}
			else
			{
				this.LoadCalendarItem(this.folderId);
				bool flag = string.Equals(action, "New", StringComparison.Ordinal) && string.IsNullOrEmpty(Utilities.GetQueryStringParameter(base.Request, "id", false));
				if (flag)
				{
					bool isMeeting = Utilities.GetQueryStringParameter(base.Request, "mr", false) != null;
					this.CalendarItemBase.IsMeeting = isMeeting;
				}
			}
			if (Utilities.GetQueryStringParameter(base.Request, "sndpt", false) != null)
			{
				CalendarItemUtilities.BuildSendConfirmDialogPrompt(this.calendarItemBase, out this.sendIssuesPrompt);
			}
			if (!this.IsUnsaved && !this.isBeingCanceled)
			{
				CalendarUtilities.AddCalendarInfobarMessages(base.Infobar, this.CalendarItemBase, null, base.UserContext);
			}
			if (!this.IsUnsaved && this.IsOccurrence && !this.isBeingCanceled && this.CalendarItemBase.IsOrganizer())
			{
				SanitizedHtmlString messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(538626087), new object[]
				{
					"<a href=\"#\" onClick=\"return onClkES()\">",
					"</a>"
				});
				base.Infobar.AddMessageHtml(messageHtml, InfobarMessageType.Informational);
			}
			this.recipientWell = new CalendarItemRecipientWell(base.UserContext, this.CalendarItemBase);
			this.toolbar = new EditCalendarItemToolbar(this.IsUnsaved, this.IsMeeting, this.CalendarItemBase.MeetingRequestWasSent, this.CalendarItemBase.Importance, this.CalendarItemBase.CalendarItemType, base.Response.Output, true, this.isBeingCanceled);
			base.CreateAttachmentHelpers(AttachmentWellType.ReadWrite);
			CalendarModuleViewState calendarModuleViewState = base.UserContext.LastClientViewState as CalendarModuleViewState;
			if (calendarModuleViewState != null)
			{
				this.lastAccessedYear = calendarModuleViewState.DateTime.Year;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002769C File Offset: 0x0002589C
		private bool LoadCalendarItem(StoreObjectId folderId)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "id", false);
			string formParameter = Utilities.GetFormParameter(base.Request, "hidid", false);
			string formParameter2 = Utilities.GetFormParameter(base.Request, "hidchk", false);
			StoreObjectId storeObjectId = null;
			if (base.OwaContext.PreFormActionId != null)
			{
				storeObjectId = base.OwaContext.PreFormActionId.StoreObjectId;
			}
			else if (base.Request.RequestType == "GET" && !string.IsNullOrEmpty(queryStringParameter))
			{
				storeObjectId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, queryStringParameter);
			}
			else if (Utilities.IsPostRequest(base.Request) && !string.IsNullOrEmpty(formParameter) && !string.IsNullOrEmpty(formParameter2))
			{
				storeObjectId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, formParameter);
			}
			CalendarItemBase calendarItemBase;
			EditCalendarItemHelper.GetCalendarItem(base.UserContext, storeObjectId, folderId, formParameter2, true, out calendarItemBase);
			this.CalendarItemBase = calendarItemBase;
			return this.CalendarItemBase != null;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002778C File Offset: 0x0002598C
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Calendar, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000277B7 File Offset: 0x000259B7
		public void RenderHeaderToolbar()
		{
			this.toolbar.RenderStart();
			this.toolbar.RenderButtons();
			this.toolbar.RenderButton(ToolbarButtons.CloseImage);
			this.toolbar.RenderEnd();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000277EC File Offset: 0x000259EC
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.PeoplePicker, OptionsBar.RenderingFlags.RenderCalendarOptionsLink | OptionsBar.RenderingFlags.RenderSearchLocationOnly, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002781C File Offset: 0x00025A1C
		public void RenderEditCalendarItemFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00027850 File Offset: 0x00025A50
		public void RenderStartDateTime(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			CalendarUtilities.RenderDateTimeTable(writer, "selS", this.CalendarItemBase.StartTime, this.lastAccessedYear, base.UserContext.UserOptions.TimeFormat, string.Empty, "dtrpOnChange(this);", string.Empty);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000278A8 File Offset: 0x00025AA8
		public void RenderEndDateTime(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ExDateTime dateTime;
			if (this.CalendarItemBase.IsAllDayEvent)
			{
				dateTime = this.CalendarItemBase.EndTime.IncrementDays(-1);
			}
			else
			{
				dateTime = this.CalendarItemBase.EndTime;
			}
			CalendarUtilities.RenderDateTimeTable(writer, "selE", dateTime, this.lastAccessedYear, base.UserContext.UserOptions.TimeFormat, string.Empty, "dtrpOnChange(this);", string.Empty);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00027924 File Offset: 0x00025B24
		public void RenderFreeBusySelect(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteLine("<select class=\"sf\" name=\"selfb\" id=\"selfb\" onchange=\"onSelFbChg();\">");
			writer.WriteLine("<option value=\"2\"{1}>{0}</option>", LocalizedStrings.GetHtmlEncoded(2052801377), (this.CalendarItemBase.FreeBusyStatus == BusyType.Busy) ? " selected" : string.Empty);
			writer.WriteLine("<option value=\"0\"{1}>{0}</option>", LocalizedStrings.GetHtmlEncoded(-971703552), (this.CalendarItemBase.FreeBusyStatus == BusyType.Free) ? " selected" : string.Empty);
			writer.WriteLine("<option value=\"1\"{1}>{0}</option>", LocalizedStrings.GetHtmlEncoded(1797669216), (this.CalendarItemBase.FreeBusyStatus == BusyType.Tentative) ? " selected" : string.Empty);
			writer.WriteLine("<option value=\"3\"{1}>{0}</option>", LocalizedStrings.GetHtmlEncoded(1052192827), (this.CalendarItemBase.FreeBusyStatus == BusyType.OOF) ? " selected" : string.Empty);
			writer.WriteLine("</select>");
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00027A10 File Offset: 0x00025C10
		public void RenderShowTimeAsClassName(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (this.CalendarItemBase.FreeBusyStatus)
			{
			case BusyType.Unknown:
				writer.Write("fbgnone");
				return;
			case BusyType.Free:
				writer.Write("fbgfree");
				return;
			case BusyType.Tentative:
				writer.Write("fbgtent");
				return;
			case BusyType.Busy:
				writer.Write("fbgbusy");
				return;
			case BusyType.OOF:
				writer.Write("fbgoof");
				return;
			default:
				throw new OwaInvalidRequestException("Invalid FreeBusyStatus in calendar item.");
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00027A9C File Offset: 0x00025C9C
		public void RenderMostRecentRecipientsOrAnr(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"wh100\"><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1286941817));
			writer.Write("</caption>");
			if (this.CalendarItemBase == null)
			{
				this.RenderMrr(base.Response.Output);
			}
			else
			{
				this.RenderAnr(base.Response.Output);
				if (!this.HasUnresolvedRecipients)
				{
					this.RenderMrr(base.Response.Output);
				}
			}
			writer.WriteLine("<tr><td class=\"h100 lt\">&nbsp;</td></tr>");
			writer.Write("</table>");
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00027B38 File Offset: 0x00025D38
		private void RenderMrr(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			RecipientCache recipientCache = AutoCompleteCache.TryGetCache(base.OwaContext.UserContext, false);
			if (recipientCache != null)
			{
				recipientCache.SortByDisplayName();
				writer.Write("<tr><td class=\"lt\">");
				MRRSelect.Render(MRRSelect.Type.CalendarRecipients, recipientCache, writer);
				writer.Write("</td></tr>");
			}
			RecipientCache recipientCache2 = RoomsCache.TryGetCache(base.OwaContext.UserContext, false);
			if (recipientCache2 != null)
			{
				recipientCache2.SortByDisplayName();
				writer.Write("<tr><td class=\"lt\">");
				MRRSelect.Render(MRRSelect.Type.Resources, recipientCache2, writer);
				writer.Write("</td></tr>");
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00027BC5 File Offset: 0x00025DC5
		private void RenderAnr(TextWriter writer)
		{
			writer.Write("<tr><td class=\"lt\">");
			this.HasUnresolvedRecipients = this.RecipientWell.RenderAnr(base.Response.Output, base.UserContext);
			writer.Write("</td></tr>");
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00027BFF File Offset: 0x00025DFF
		protected override void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(this.CalendarItemBase, writer, base.OwaContext, base.Infobar);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00027C27 File Offset: 0x00025E27
		protected void RenderStartTime()
		{
			RenderingUtilities.RenderLocalDateTimeScriptVariable(base.Response.Output, this.CalendarItemBase.StartTime, "a_dtSt");
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00027C49 File Offset: 0x00025E49
		protected void RenderEndTime()
		{
			RenderingUtilities.RenderLocalDateTimeScriptVariable(base.Response.Output, this.CalendarItemBase.EndTime, "a_dtEt");
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00027C6B File Offset: 0x00025E6B
		protected AttachmentWellType EditCalendarAttachmentWellType
		{
			get
			{
				if (this.CalendarItemBase != null && MeetingUtilities.IsEditCalendarItemOccurence(this.CalendarItemBase))
				{
					return AttachmentWellType.ReadOnly;
				}
				return AttachmentWellType.ReadWrite;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00027C85 File Offset: 0x00025E85
		protected override void OnUnload(EventArgs e)
		{
			if (this.CalendarItemBase != null)
			{
				EditCalendarItemHelper.CreateUserContextData(base.UserContext, this.CalendarItemBase);
			}
			base.OnUnload(e);
		}

		// Token: 0x040003B8 RID: 952
		private CalendarItemRecipientWell recipientWell;

		// Token: 0x040003B9 RID: 953
		private bool hasUnresolvedRecipients;

		// Token: 0x040003BA RID: 954
		private EditCalendarItemToolbar toolbar;

		// Token: 0x040003BB RID: 955
		private StoreObjectId folderId;

		// Token: 0x040003BC RID: 956
		private bool isBeingCanceled;

		// Token: 0x040003BD RID: 957
		private int lastAccessedYear = -1;

		// Token: 0x040003BE RID: 958
		private CalendarItemBase calendarItemBase;

		// Token: 0x040003BF RID: 959
		private string sendIssuesPrompt;
	}
}
