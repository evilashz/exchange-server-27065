using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200009D RID: 157
	public class MeetingPage : OwaForm
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0002B20C File Offset: 0x0002940C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.MeetingPageWriterFactory(this.ItemType, e);
			if (!this.isCalendarItem)
			{
				MeetingMessage meetingMessage = base.Item as MeetingMessage;
				if (meetingMessage == null)
				{
					throw new OwaInvalidOperationException("Item must be of MeetingMessage type");
				}
				if (!meetingMessage.IsRead)
				{
					meetingMessage.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, meetingMessage), false);
				}
				base.HandleReadReceipt(meetingMessage);
			}
			else
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "dy", false);
				string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "mn", false);
				string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "yr", false);
				int num;
				int month;
				int year;
				if (!string.IsNullOrEmpty(queryStringParameter) && int.TryParse(queryStringParameter, out num) && !string.IsNullOrEmpty(queryStringParameter2) && int.TryParse(queryStringParameter2, out month) && !string.IsNullOrEmpty(queryStringParameter3) && int.TryParse(queryStringParameter3, out year))
				{
					try
					{
						this.day = new ExDateTime(base.UserContext.TimeZone, year, month, num).Date;
					}
					catch (ArgumentOutOfRangeException)
					{
						base.Infobar.AddMessageLocalized(883484089, InfobarMessageType.Error);
					}
				}
			}
			this.SetInfobarMessages();
			if (this.day == ExDateTime.MinValue)
			{
				if (this.meetingPageWriter.CalendarItemBase != null && !this.isMeetingResponse)
				{
					this.day = this.meetingPageWriter.CalendarItemBase.StartTime;
					return;
				}
				this.day = DateTimeUtilities.GetLocalTime().Date;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002B388 File Offset: 0x00029588
		protected void SetInfobarMessages()
		{
			if (!this.isCalendarItem)
			{
				RenderingUtilities.RenderReplyForwardMessageStatus(base.Item, base.Infobar, base.UserContext);
				if (this.isDelegated)
				{
					base.Infobar.AddMessageText(string.Format(LocalizedStrings.GetNonEncoded(this.delegateMessage), MeetingUtilities.GetReceivedOnBehalfOfDisplayName((MeetingMessage)base.Item)), InfobarMessageType.Informational);
				}
			}
			object obj = base.Item.TryGetProperty(MessageItemSchema.IsDraft);
			if (obj is bool && (bool)obj)
			{
				if (ObjectClass.IsMeetingResponse(this.ItemType))
				{
					base.Infobar.AddMessageLocalized(-1981719796, InfobarMessageType.Informational);
				}
			}
			else
			{
				InfobarMessageBuilder.AddImportance(base.Infobar, base.Item);
				InfobarMessageBuilder.AddSensitivity(base.Infobar, base.Item);
			}
			InfobarMessageBuilder.AddFlag(base.Infobar, base.Item, base.UserContext);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002B463 File Offset: 0x00029663
		protected override void OnUnload(EventArgs e)
		{
			if (this.meetingPageWriter != null)
			{
				this.meetingPageWriter.Dispose();
				this.meetingPageWriter = null;
			}
			base.OnUnload(e);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002B488 File Offset: 0x00029688
		private void MeetingPageWriterFactory(string itemType, EventArgs e)
		{
			if (ObjectClass.IsMeetingRequest(itemType))
			{
				MeetingRequest meetingRequest = base.Item = base.Initialize<MeetingRequest>(MeetingInviteWriter.PrefetchProperties);
				this.delegateMessage = 1491510515;
				this.meetingPageWriter = new MeetingInviteWriter(meetingRequest, base.UserContext, base.IsEmbeddedItem);
				if (meetingRequest.MeetingRequestType == MeetingMessageType.Outdated)
				{
					base.Infobar.AddMessageLocalized(1771878760, InfobarMessageType.Informational);
				}
				else if (this.meetingPageWriter.CalendarItemBase != null)
				{
					CalendarUtilities.AddCalendarInfobarMessages(base.Infobar, this.meetingPageWriter.CalendarItemBase, meetingRequest, base.UserContext);
				}
			}
			else if (ObjectClass.IsMeetingCancellation(itemType))
			{
				MeetingCancellation meetingCancellation = base.Item = base.Initialize<MeetingCancellation>(MeetingCancelWriter.PrefetchProperties);
				this.delegateMessage = 1953915685;
				this.meetingPageWriter = new MeetingCancelWriter(meetingCancellation, base.UserContext, base.IsEmbeddedItem);
				if (MeetingUtilities.MeetingCancellationIsOutOfDate(meetingCancellation))
				{
					base.Infobar.AddMessageLocalized(21101307, InfobarMessageType.Informational);
				}
				else
				{
					base.Infobar.AddMessageLocalized(-161808760, InfobarMessageType.Informational);
				}
			}
			else if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(itemType))
			{
				this.isCalendarItem = true;
				CalendarItemBase calendarItemBase = base.Item = base.Initialize<CalendarItemBase>(MeetingPageWriter.CalendarPrefetchProperties);
				this.meetingPageWriter = new CalendarItemWriter(calendarItemBase, base.UserContext);
				if (calendarItemBase != null)
				{
					CalendarUtilities.AddCalendarInfobarMessages(base.Infobar, this.meetingPageWriter.CalendarItemBase, null, base.UserContext);
				}
			}
			else
			{
				if (!ObjectClass.IsMeetingResponse(itemType))
				{
					ExTraceGlobals.CalendarCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unsupported item type '{0}' for meeting page", itemType);
					throw new OwaInvalidRequestException(string.Format("Unsupported item type '{0}' for edit meeting page", itemType));
				}
				this.isMeetingResponse = true;
				MeetingResponse meetingResponse = base.Item = base.Initialize<MeetingResponse>(MeetingResponseWriter.PrefetchProperties);
				this.delegateMessage = -1986433227;
				this.meetingPageWriter = new MeetingResponseWriter(meetingResponse, base.UserContext, base.IsEmbeddedItem);
				if (meetingResponse.From != null)
				{
					string messageText = string.Empty;
					switch (meetingResponse.ResponseType)
					{
					case ResponseType.Tentative:
						messageText = string.Format(LocalizedStrings.GetNonEncoded(-67265594), meetingResponse.From.DisplayName);
						break;
					case ResponseType.Accept:
						messageText = string.Format(LocalizedStrings.GetNonEncoded(1335319405), meetingResponse.From.DisplayName);
						break;
					case ResponseType.Decline:
						messageText = string.Format(LocalizedStrings.GetNonEncoded(-1091863618), meetingResponse.From.DisplayName);
						break;
					}
					base.Infobar.AddMessageText(messageText, InfobarMessageType.Informational);
				}
			}
			if (!this.isCalendarItem)
			{
				this.isDelegated = ((MeetingMessage)base.Item).IsDelegated();
			}
			this.CurrentFolderStoreObjectId = (base.IsEmbeddedItem ? base.ParentItem.ParentId : base.Item.ParentId);
			this.navigationModule = Navigation.GetNavigationModuleFromFolder(base.UserContext, this.CurrentFolderStoreObjectId);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002B768 File Offset: 0x00029968
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar.SearchModule searchModule = (this.navigationModule == NavigationModule.Mail) ? OptionsBar.SearchModule.Mail : OptionsBar.SearchModule.Calendar;
			OptionsBar.RenderingFlags renderingFlags = (this.navigationModule == NavigationModule.Mail) ? OptionsBar.RenderingFlags.None : OptionsBar.RenderingFlags.RenderCalendarOptionsLink;
			string searchUrlSuffix = (this.navigationModule == NavigationModule.Mail) ? OptionsBar.BuildFolderSearchUrlSuffix(base.UserContext, this.CurrentFolderStoreObjectId) : null;
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, searchModule, renderingFlags, searchUrlSuffix);
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002B7D0 File Offset: 0x000299D0
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(this.navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002B800 File Offset: 0x00029A00
		public void RenderSecondaryNavigation()
		{
			switch (this.navigationModule)
			{
			case NavigationModule.Mail:
				this.RenderMailSecondaryNavigation();
				return;
			case NavigationModule.Calendar:
				this.RenderCalendarSecondaryNavigation();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002B830 File Offset: 0x00029A30
		private void RenderMailSecondaryNavigation()
		{
			MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, this.CurrentFolderStoreObjectId, null, null, null);
			mailSecondaryNavigation.Render(base.Response.Output);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002B86C File Offset: 0x00029A6C
		private void RenderCalendarSecondaryNavigation()
		{
			CalendarSecondaryNavigation calendarSecondaryNavigation = new CalendarSecondaryNavigation(base.OwaContext, this.CurrentFolderStoreObjectId, new ExDateTime?(this.day), null);
			string text = calendarSecondaryNavigation.Render(base.Response.Output);
			if (!string.IsNullOrEmpty(text))
			{
				base.Infobar.AddMessageText(text, InfobarMessageType.Error);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public void RenderHeaderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, true);
			toolbar.RenderStart();
			if (!base.IsEmbeddedItem || !this.isCalendarItem || this.meetingPageWriter.CalendarItemBase.IsMeeting)
			{
				toolbar.RenderButton(ToolbarButtons.Reply);
				toolbar.RenderButton(ToolbarButtons.ReplyAll);
			}
			toolbar.RenderButton(ToolbarButtons.Forward);
			toolbar.RenderDivider();
			bool flag = false;
			if (!this.isCalendarItem && !base.IsEmbeddedItem)
			{
				toolbar.RenderButton(ToolbarButtons.Move);
				flag = true;
			}
			if (!base.IsEmbeddedItem)
			{
				toolbar.RenderButton(ToolbarButtons.Delete);
				flag = true;
			}
			if (flag)
			{
				toolbar.RenderDivider();
			}
			if (!base.IsEmbeddedItem && base.UserContext.IsJunkEmailEnabled)
			{
				toolbar.RenderButton(ToolbarButtons.Junk);
				toolbar.RenderDivider();
			}
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderFill();
			if (!base.IsEmbeddedItem && !this.isCalendarItem)
			{
				toolbar.RenderButton(ToolbarButtons.Previous);
				toolbar.RenderButton(ToolbarButtons.Next);
			}
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002B9D8 File Offset: 0x00029BD8
		public void RenderFooterToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			if (!base.IsEmbeddedItem && !this.isCalendarItem)
			{
				toolbar.RenderButton(ToolbarButtons.Previous);
				toolbar.RenderButton(ToolbarButtons.Next);
			}
			toolbar.RenderEnd();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002BA25 File Offset: 0x00029C25
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(base.Item, base.Response.Output, "DIV.PlainText");
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002BA42 File Offset: 0x00029C42
		protected void RenderJavascriptEncodedClassName()
		{
			Utilities.JavascriptEncode(base.ParentItem.ClassName, base.Response.Output);
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0002BA5F File Offset: 0x00029C5F
		protected MeetingPageWriter MeetingPageWriter
		{
			get
			{
				return this.meetingPageWriter;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0002BA67 File Offset: 0x00029C67
		protected string ChangeKey
		{
			get
			{
				if (base.Item != null && !base.IsEmbeddedItem)
				{
					return base.Item.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0002BA8F File Offset: 0x00029C8F
		protected NavigationModule NavigationModule
		{
			get
			{
				return this.navigationModule;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0002BA97 File Offset: 0x00029C97
		protected int SelectedYear
		{
			get
			{
				return this.day.Year;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0002BAA4 File Offset: 0x00029CA4
		protected int SelectedMonth
		{
			get
			{
				return this.day.Month;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0002BAB1 File Offset: 0x00029CB1
		protected int SelectedDay
		{
			get
			{
				return this.day.Day;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002BABE File Offset: 0x00029CBE
		protected string CurrentFolderId
		{
			get
			{
				return this.CurrentFolderStoreObjectId.ToBase64String();
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0002BACB File Offset: 0x00029CCB
		protected string ItemIdString
		{
			get
			{
				return base.ItemId.ToBase64String();
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0002BAD8 File Offset: 0x00029CD8
		protected bool IsDelegated
		{
			get
			{
				return !this.isCalendarItem && this.isDelegated;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0002BAEC File Offset: 0x00029CEC
		protected string Subject
		{
			get
			{
				string text = ItemUtility.GetProperty<string>(base.Item, ItemSchema.Subject, string.Empty);
				if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
				{
					text = LocalizedStrings.GetNonEncoded(-1500721828);
				}
				return text;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0002BB23 File Offset: 0x00029D23
		protected static int StoreObjectTypeMeetingResponse
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x04000417 RID: 1047
		private const string YearQueryParameter = "yr";

		// Token: 0x04000418 RID: 1048
		private const string MonthQueryParameter = "mn";

		// Token: 0x04000419 RID: 1049
		private const string DayQueryParameter = "dy";

		// Token: 0x0400041A RID: 1050
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			ItemSchema.BlockStatus,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.CompleteDate,
			ItemSchema.FlagCompleteTime,
			ItemSchema.FlagRequest,
			ItemSchema.FlagStatus,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead,
			MessageItemSchema.ReplyTime
		};

		// Token: 0x0400041B RID: 1051
		private NavigationModule navigationModule;

		// Token: 0x0400041C RID: 1052
		private MeetingPageWriter meetingPageWriter;

		// Token: 0x0400041D RID: 1053
		private ExDateTime day = ExDateTime.MinValue;

		// Token: 0x0400041E RID: 1054
		private bool isMeetingResponse;

		// Token: 0x0400041F RID: 1055
		private bool isCalendarItem;

		// Token: 0x04000420 RID: 1056
		private bool isDelegated;

		// Token: 0x04000421 RID: 1057
		private Strings.IDs delegateMessage = -1018465893;

		// Token: 0x04000422 RID: 1058
		internal StoreObjectId CurrentFolderStoreObjectId;
	}
}
