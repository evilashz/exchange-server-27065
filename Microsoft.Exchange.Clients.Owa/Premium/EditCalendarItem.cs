using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200044C RID: 1100
	public class EditCalendarItem : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000E0F63 File Offset: 0x000DF163
		protected static AttachmentWellType EditCalendarAttachmentWellType
		{
			get
			{
				return AttachmentWellType.ReadWrite;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000E0F66 File Offset: 0x000DF166
		protected static int StoreObjectTypeCalendarItem
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000E0F6A File Offset: 0x000DF16A
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000E0F6D File Offset: 0x000DF16D
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000E0F70 File Offset: 0x000DF170
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000E0F73 File Offset: 0x000DF173
		protected override bool UseStrictMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000E0F78 File Offset: 0x000DF178
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.infobar.SetInfobarClass("infobarEdit");
			this.infobar.SetShouldHonorHideByDefault(true);
			this.calendarItemBase = base.Initialize<CalendarItemBase>(false, new PropertyDefinition[]
			{
				CalendarItemBaseSchema.CalendarItemType,
				StoreObjectSchema.EffectiveRights
			});
			if (this.calendarItemBase != null)
			{
				if (string.Equals(base.OwaContext.FormsRegistryContext.Action, "Open", StringComparison.OrdinalIgnoreCase))
				{
					this.newItemType = NewItemType.ExplicitDraft;
				}
				else
				{
					this.newItemType = NewItemType.ImplicitDraft;
					base.DeleteExistingDraft = true;
				}
			}
			else
			{
				this.calendarItemBase = Utilities.CreateDraftMeetingRequestFromQueryString(base.UserContext, base.Request, new PropertyDefinition[]
				{
					StoreObjectSchema.EffectiveRights
				});
				if (this.calendarItemBase != null)
				{
					this.newItemType = NewItemType.ImplicitDraft;
					base.DeleteExistingDraft = true;
					base.Item = this.calendarItemBase;
				}
			}
			if (this.calendarItemBase != null)
			{
				this.isMeeting = this.calendarItemBase.IsMeeting;
				this.startTime = this.calendarItemBase.StartTime;
				this.endTime = this.calendarItemBase.EndTime;
				if (this.calendarItemBase.IsAllDayEvent && !this.IsRecurringMaster)
				{
					this.endTime = this.endTime.IncrementDays(-1);
				}
				this.recipientWell = new CalendarItemRecipientWell(this.calendarItemBase);
				this.bodyMarkup = BodyConversionUtilities.GetBodyFormatOfEditItem(this.calendarItemBase, this.newItemType, base.UserContext.UserOptions);
				this.toolbar = new EditCalendarItemToolbar(this.calendarItemBase, this.isMeeting, this.bodyMarkup, this.IsPublicItem);
				this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
				this.isBeingCanceled = (Utilities.GetQueryStringParameter(base.Request, "c", false) != null);
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "er", false);
				if (queryStringParameter != null)
				{
					if (this.calendarItemBase.CalendarItemType != CalendarItemType.RecurringMaster || !(this.calendarItemBase is CalendarItem))
					{
						throw new OwaInvalidRequestException("Invalid calendar item type.  Only recurring masters support specifying an end range");
					}
					this.endRange = ExDateTime.MinValue;
					try
					{
						this.endRange = DateTimeUtilities.ParseIsoDate(queryStringParameter, base.UserContext.TimeZone);
					}
					catch (OwaParsingErrorException)
					{
						ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid end range provided on URL '{0}'", queryStringParameter);
						throw new OwaInvalidRequestException(string.Format("Invalid end range provided on URL '{0}'", queryStringParameter));
					}
					if (this.endRange != ExDateTime.MinValue)
					{
						CalendarItem calendarItem = (CalendarItem)this.calendarItemBase;
						this.occurrenceCount = MeetingUtilities.CancelRecurrence(calendarItem, this.endRange);
						if (this.occurrenceCount == 0)
						{
							this.isBeingCanceled = true;
						}
					}
				}
				string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "od", false);
				if (queryStringParameter2 != null)
				{
					try
					{
						this.occurrenceDate = DateTimeUtilities.ParseIsoDate(queryStringParameter2, base.UserContext.TimeZone).Date;
						goto IL_303;
					}
					catch (OwaParsingErrorException)
					{
						ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid occurrence date specified on URL '{0}'", queryStringParameter2);
						throw new OwaInvalidRequestException(string.Format("Invalid occurrence date provided on URL '{0}'", queryStringParameter2));
					}
				}
				this.occurrenceDate = DateTimeUtilities.GetLocalTime().Date;
				IL_303:
				CalendarUtilities.AddCalendarInfobarMessages(this.infobar, this.calendarItemBase, null, base.UserContext);
				if (this.isBeingCanceled)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(1328030972), InfobarMessageType.Informational);
				}
				this.disableOccurrenceReminderUI = MeetingUtilities.CheckShouldDisableOccurrenceReminderUI(this.calendarItemBase);
				if (this.disableOccurrenceReminderUI && !this.IsPublicItem)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-891369593), InfobarMessageType.Informational);
				}
				if (!(this.calendarItemBase is CalendarItem))
				{
					this.recurrenceUtilities = new RecurrenceUtilities(null, base.SanitizingResponse);
					return;
				}
				if (0 < this.occurrenceCount)
				{
					EndDateRecurrenceRange range = new EndDateRecurrenceRange(((CalendarItem)this.calendarItemBase).Recurrence.Range.StartDate, this.endRange.IncrementDays(-1));
					this.recurrenceUtilities = new RecurrenceUtilities(new Recurrence(((CalendarItem)this.calendarItemBase).Recurrence.Pattern, range), base.SanitizingResponse);
					return;
				}
				this.recurrenceUtilities = new RecurrenceUtilities(((CalendarItem)this.calendarItemBase).Recurrence, base.SanitizingResponse);
				return;
			}
			else
			{
				this.isMeeting = (Utilities.GetQueryStringParameter(base.Request, "mr", false) != null);
				if (this.isMeeting && this.IsPublicItem)
				{
					throw new OwaInvalidRequestException("Can't create meeting in Owa Public Folders");
				}
				this.isAllDayEvent = (Utilities.GetQueryStringParameter(base.Request, "evt", false) != null);
				bool flag = Utilities.GetQueryStringParameter(base.Request, "tm", false) != null || this.isAllDayEvent;
				string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "st", false);
				if (queryStringParameter3 != null)
				{
					try
					{
						this.startTime = DateTimeUtilities.ParseIsoDate(queryStringParameter3, base.UserContext.TimeZone);
					}
					catch (OwaParsingErrorException)
					{
						ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid start date provided on URL '{0}'", queryStringParameter3);
						throw new OwaInvalidRequestException(string.Format("Invalid start date provided on URL '{0}'", queryStringParameter3));
					}
				}
				if (flag || this.startTime == ExDateTime.MinValue)
				{
					ExDateTime localTime = DateTimeUtilities.GetLocalTime();
					if (this.startTime == ExDateTime.MinValue)
					{
						this.startTime = new ExDateTime(base.UserContext.TimeZone, localTime.Year, localTime.Month, localTime.Day, localTime.Hour, localTime.Minute, 0);
					}
					else
					{
						this.startTime = new ExDateTime(base.UserContext.TimeZone, this.startTime.Year, this.startTime.Month, this.startTime.Day, localTime.Hour, localTime.Minute, 0);
					}
					if (this.isAllDayEvent && this.startTime.Hour == 23)
					{
						if (this.startTime.Minute >= 30)
						{
							this.startTime = this.startTime.Date;
						}
						else
						{
							this.startTime = new ExDateTime(base.UserContext.TimeZone, this.startTime.Year, this.startTime.Month, this.startTime.Day, 23, 0, 0);
							this.endTime = new ExDateTime(base.UserContext.TimeZone, this.startTime.Year, this.startTime.Month, this.startTime.Day, 23, 30, 0);
						}
					}
					else if (this.startTime.Minute != 0 && this.startTime.Minute != 30)
					{
						this.startTime = this.startTime.AddMinutes((double)(30 - this.startTime.Minute % 30));
					}
				}
				if (this.endTime == ExDateTime.MinValue)
				{
					this.endTime = this.startTime.AddMinutes(60.0);
				}
				this.recipientWell = new CalendarItemRecipientWell();
				this.bodyMarkup = base.UserContext.UserOptions.ComposeMarkup;
				this.toolbar = new EditCalendarItemToolbar(null, this.isMeeting, this.bodyMarkup, this.IsPublicItem);
				this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
				this.recurrenceUtilities = new RecurrenceUtilities(null, base.SanitizingResponse);
				return;
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000E16D8 File Offset: 0x000DF8D8
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			bool flag = BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(this.calendarItemBase, writer, this.newItemType, base.OwaContext, ref this.shouldPromptUserForUnblockingOnFormLoad, ref this.hasInlineImages, this.infobar, base.IsRequestCallbackForWebBeacons, this.bodyMarkup);
			if (flag)
			{
				this.calendarItemBase.Load();
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000E172C File Offset: 0x000DF92C
		protected void CreateAttachmentHelpers()
		{
			if (this.calendarItemBase != null)
			{
				this.attachmentInformation = AttachmentWell.GetAttachmentInformation(this.calendarItemBase, base.AttachmentLinks, base.UserContext.IsPublicLogon);
				InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentInformation);
				if (infobarRenderingHelper.HasLevelOne)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
				}
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000E1792 File Offset: 0x000DF992
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000E179A File Offset: 0x000DF99A
		protected void RenderStartTime()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.SanitizingResponse, this.startTime);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000E17AD File Offset: 0x000DF9AD
		protected void RenderEndTime()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.SanitizingResponse, this.endTime);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000E17C0 File Offset: 0x000DF9C0
		protected void RenderOccurrenceDate()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.SanitizingResponse, this.occurrenceDate);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000E17D3 File Offset: 0x000DF9D3
		protected void RenderStartTimeDropdownList()
		{
			TimeDropDownList.RenderTimePicker(base.SanitizingResponse, this.startTime, "divSTime");
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000E17EB File Offset: 0x000DF9EB
		protected void RenderEndTimeDropdownList()
		{
			TimeDropDownList.RenderTimePicker(base.SanitizingResponse, this.endTime, "divETime");
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000E1803 File Offset: 0x000DFA03
		protected void RenderStartDate()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.SanitizingResponse, "divSDate", this.startTime.Date);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000E1820 File Offset: 0x000DFA20
		protected void RenderEndDate()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.SanitizingResponse, "divEDate", this.endTime.Date);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000E183D File Offset: 0x000DFA3D
		protected void RenderReminderDropdownList()
		{
			CalendarUtilities.RenderReminderDropdownList(base.SanitizingResponse, this.calendarItemBase, this.ReminderIsSet, this.DisableOccurrenceReminderUI || this.IsPublicItem);
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000E1867 File Offset: 0x000DFA67
		protected void RenderBusyTypeDropdownList()
		{
			CalendarUtilities.RenderBusyTypeDropdownList(base.SanitizingResponse, this.calendarItemBase, false);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000E187B File Offset: 0x000DFA7B
		protected void RenderToolbar()
		{
			this.toolbar.Render(base.SanitizingResponse);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000E1890 File Offset: 0x000DFA90
		protected void BuildCalendarInfobar()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "clr", false);
			if (queryStringParameter == null)
			{
				return;
			}
			CalendarUtilities.BuildCalendarInfobar(this.infobar, base.UserContext, this.FolderId ?? base.UserContext.CalendarFolderOwaId, CalendarColorManager.ParseColorIndexString(queryStringParameter, true), true);
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000E18E1 File Offset: 0x000DFAE1
		internal OwaStoreObjectId FolderId
		{
			get
			{
				if (this.folderId == null)
				{
					if (base.TargetFolderId != null)
					{
						this.folderId = base.TargetFolderId;
					}
					else if (base.Item != null)
					{
						this.folderId = base.ParentFolderId;
					}
				}
				return this.folderId;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000E191B File Offset: 0x000DFB1B
		protected bool IsFolderIdNull
		{
			get
			{
				return this.FolderId == null;
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000E1926 File Offset: 0x000DFB26
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000E1939 File Offset: 0x000DFB39
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000E195A File Offset: 0x000DFB5A
		protected void RenderSender()
		{
			RenderingUtilities.RenderSender(base.UserContext, base.SanitizingResponse, this.calendarItemBase);
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000E1974 File Offset: 0x000DFB74
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				string untitled = this.isMeeting ? LocalizedStrings.GetNonEncoded(-1500721828) : LocalizedStrings.GetNonEncoded(-1178892512);
				RenderingUtilities.RenderSubject(base.SanitizingResponse, this.calendarItemBase, untitled);
				return;
			}
			RenderingUtilities.RenderSubject(base.SanitizingResponse, this.calendarItemBase);
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000E19C7 File Offset: 0x000DFBC7
		protected bool DisableReminderCheckBox
		{
			get
			{
				return this.DisableOccurrenceReminderUI || this.IsPublicItem;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000E19D9 File Offset: 0x000DFBD9
		protected bool DisablePrivateCheckBox
		{
			get
			{
				return !this.IsSingleOrRecurringMaster || this.IsPublicItem || this.IsInSharedFolder;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000E19F3 File Offset: 0x000DFBF3
		protected bool IsInSharedFolder
		{
			get
			{
				return this.FolderId != null && this.FolderId.IsOtherMailbox;
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000E1A0A File Offset: 0x000DFC0A
		protected void RenderJavascriptEncodedClassName()
		{
			if (this.calendarItemBase != null)
			{
				Utilities.JavascriptEncode(this.calendarItemBase.ClassName, base.SanitizingResponse);
				return;
			}
			Utilities.JavascriptEncode("IPM.Appointment");
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000E1A36 File Offset: 0x000DFC36
		protected void RenderJavascriptEncodedCalendarItemBaseChangeKey()
		{
			if (this.calendarItemBase != null)
			{
				Utilities.JavascriptEncode(this.calendarItemBase.Id.ChangeKeyAsBase64String(), base.SanitizingResponse);
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000E1A5C File Offset: 0x000DFC5C
		protected void RenderJavascriptEncodedCalendarItemBaseMasterId()
		{
			if (this.calendarItemBase != null && (this.calendarItemBase.CalendarItemType == CalendarItemType.Occurrence || this.calendarItemBase.CalendarItemType == CalendarItemType.Exception))
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObject(this.calendarItemBase);
				Utilities.JavascriptEncode(owaStoreObjectId.ProviderLevelItemId.ToString(), base.SanitizingResponse);
			}
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000E1AAF File Offset: 0x000DFCAF
		protected override void RenderJavaScriptEncodedTargetFolderId()
		{
			if (this.FolderId != null)
			{
				Utilities.JavascriptEncode(this.FolderId.ToBase64String(), base.SanitizingResponse);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000E1ACF File Offset: 0x000DFCCF
		protected string Location
		{
			get
			{
				if (this.calendarItemBase == null)
				{
					return string.Empty;
				}
				return Utilities.HtmlEncode(this.calendarItemBase.Location);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000E1AEF File Offset: 0x000DFCEF
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x000E1AF7 File Offset: 0x000DFCF7
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000E1AFF File Offset: 0x000DFCFF
		protected bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000E1B07 File Offset: 0x000DFD07
		protected bool MeetingRequestWasSent
		{
			get
			{
				return this.calendarItemBase != null && this.IsMeeting && this.calendarItemBase.MeetingRequestWasSent;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000E1B28 File Offset: 0x000DFD28
		protected bool IsPrivate
		{
			get
			{
				if (this.calendarItemBase != null)
				{
					object obj = this.calendarItemBase.TryGetProperty(ItemSchema.Sensitivity);
					return obj is Sensitivity && (Sensitivity)obj == Sensitivity.Private;
				}
				return false;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000E1B64 File Offset: 0x000DFD64
		protected bool ReminderIsSet
		{
			get
			{
				if (this.calendarItemBase != null)
				{
					object obj = this.calendarItemBase.TryGetProperty(ItemSchema.ReminderIsSet);
					return obj is bool && (bool)obj;
				}
				return base.UserContext.UserOptions.EnableReminders && !this.IsPublicItem;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000E1BB8 File Offset: 0x000DFDB8
		protected bool IsAllDayEvent
		{
			get
			{
				if (this.calendarItemBase != null)
				{
					return this.calendarItemBase.IsAllDayEvent;
				}
				return this.isAllDayEvent;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000E1BD4 File Offset: 0x000DFDD4
		protected bool IsResponseRequested
		{
			get
			{
				if (this.calendarItemBase != null)
				{
					object obj = this.calendarItemBase.TryGetProperty(ItemSchema.IsResponseRequested);
					return !(obj is bool) || (bool)obj;
				}
				return true;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000E1C0C File Offset: 0x000DFE0C
		protected bool HasAttachments
		{
			get
			{
				return this.calendarItemBase != null && this.calendarItemBase.AttachmentCollection != null && 0 < this.calendarItemBase.AttachmentCollection.Count;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000E1C3A File Offset: 0x000DFE3A
		protected bool IsBeingCanceled
		{
			get
			{
				return this.isBeingCanceled;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000E1C42 File Offset: 0x000DFE42
		protected bool DisableOccurrenceReminderUI
		{
			get
			{
				return this.disableOccurrenceReminderUI;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000E1C4A File Offset: 0x000DFE4A
		protected bool IsRecurringMaster
		{
			get
			{
				return this.calendarItemBase != null && this.calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x000E1C64 File Offset: 0x000DFE64
		protected string When
		{
			get
			{
				if (0 < this.occurrenceCount)
				{
					if (this.calendarItemBase.CalendarItemType != CalendarItemType.RecurringMaster || !(this.calendarItemBase is CalendarItem))
					{
						throw new OwaInvalidRequestException("Invalid calendar item type.  Only recurring masters support specifying an end range");
					}
					CalendarItem calendarItem = (CalendarItem)this.calendarItemBase;
					EndDateRecurrenceRange range = new EndDateRecurrenceRange(calendarItem.Recurrence.Range.StartDate, this.endRange.IncrementDays(-1));
					Recurrence recurrence = new Recurrence(calendarItem.Recurrence.Pattern, range);
					return Utilities.HtmlEncode(CalendarUtilities.GenerateWhen(base.UserContext, calendarItem.StartTime, calendarItem.EndTime, recurrence));
				}
				else
				{
					if (!(this.calendarItemBase is CalendarItem))
					{
						return "&nbsp;";
					}
					CalendarItem calendarItem2 = (CalendarItem)this.calendarItemBase;
					if (calendarItem2.Recurrence != null)
					{
						return Utilities.HtmlEncode(calendarItem2.GenerateWhen());
					}
					return "&nbsp;";
				}
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000E1D39 File Offset: 0x000DFF39
		public ArrayList AttachmentInformation
		{
			get
			{
				return this.attachmentInformation;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000E1D41 File Offset: 0x000DFF41
		protected RecurrenceUtilities RecurrenceUtilities
		{
			get
			{
				return this.recurrenceUtilities;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000E1D49 File Offset: 0x000DFF49
		protected bool ShouldPromptUser
		{
			get
			{
				return this.shouldPromptUserForUnblockingOnFormLoad;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000E1D51 File Offset: 0x000DFF51
		protected bool HasInlineImages
		{
			get
			{
				return this.hasInlineImages;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000E1D59 File Offset: 0x000DFF59
		protected bool IsCalendarItemBaseNull
		{
			get
			{
				return this.calendarItemBase == null;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000E1D66 File Offset: 0x000DFF66
		protected bool IsSingleOrRecurringMaster
		{
			get
			{
				return this.calendarItemBase == null || this.calendarItemBase.CalendarItemType == CalendarItemType.Single || this.calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster;
			}
		}

		// Token: 0x04001BBC RID: 7100
		private const int DefaultMeetingLength = 60;

		// Token: 0x04001BBD RID: 7101
		private CalendarItemBase calendarItemBase;

		// Token: 0x04001BBE RID: 7102
		private EditCalendarItemToolbar toolbar;

		// Token: 0x04001BBF RID: 7103
		private Infobar infobar = new Infobar();

		// Token: 0x04001BC0 RID: 7104
		private CalendarItemRecipientWell recipientWell;

		// Token: 0x04001BC1 RID: 7105
		private ArrayList attachmentInformation;

		// Token: 0x04001BC2 RID: 7106
		private bool isAllDayEvent;

		// Token: 0x04001BC3 RID: 7107
		private bool isMeeting;

		// Token: 0x04001BC4 RID: 7108
		private ExDateTime startTime = ExDateTime.MinValue;

		// Token: 0x04001BC5 RID: 7109
		private ExDateTime endTime = ExDateTime.MinValue;

		// Token: 0x04001BC6 RID: 7110
		private ExDateTime endRange = ExDateTime.MinValue;

		// Token: 0x04001BC7 RID: 7111
		private RecurrenceUtilities recurrenceUtilities;

		// Token: 0x04001BC8 RID: 7112
		private Markup bodyMarkup;

		// Token: 0x04001BC9 RID: 7113
		private bool shouldPromptUserForUnblockingOnFormLoad;

		// Token: 0x04001BCA RID: 7114
		private bool hasInlineImages;

		// Token: 0x04001BCB RID: 7115
		private NewItemType newItemType;

		// Token: 0x04001BCC RID: 7116
		private bool disableOccurrenceReminderUI;

		// Token: 0x04001BCD RID: 7117
		private bool isBeingCanceled;

		// Token: 0x04001BCE RID: 7118
		private int occurrenceCount;

		// Token: 0x04001BCF RID: 7119
		private ExDateTime occurrenceDate = ExDateTime.MinValue;

		// Token: 0x04001BD0 RID: 7120
		private OwaStoreObjectId folderId;

		// Token: 0x0200044D RID: 1101
		private static class QueryParameters
		{
			// Token: 0x04001BD1 RID: 7121
			public const string MeetingRequest = "mr";

			// Token: 0x04001BD2 RID: 7122
			public const string AllDayEvent = "evt";

			// Token: 0x04001BD3 RID: 7123
			public const string Time = "tm";

			// Token: 0x04001BD4 RID: 7124
			public const string StartTime = "st";

			// Token: 0x04001BD5 RID: 7125
			public const string Cancel = "c";

			// Token: 0x04001BD6 RID: 7126
			public const string OccurrenceDate = "od";

			// Token: 0x04001BD7 RID: 7127
			public const string EndRange = "er";

			// Token: 0x04001BD8 RID: 7128
			public const string Color = "clr";
		}
	}
}
