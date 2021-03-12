using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045A RID: 1114
	public class MeetingPage : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002916 RID: 10518 RVA: 0x000E8828 File Offset: 0x000E6A28
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "t");
			this.MeetingPageWriterFactory(queryStringParameter, e);
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(base.Item, base.IsEmbeddedItem, base.ForceEnableItemLink, base.UserContext, out this.isInJunkEmailFolder, out this.isSuspectedPhishingItem, out this.itemLinkEnabled, out this.isJunkOrPhishing);
			if (this.isJunkOrPhishing)
			{
				this.bodyMarkup = Markup.PlainText;
			}
			if (!ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(queryStringParameter) && !base.IsPreviewForm)
			{
				MeetingMessage meetingMessage = base.Item as MeetingMessage;
				if (meetingMessage == null)
				{
					throw new OwaInvalidOperationException("Item must be of MeetingMessage type");
				}
				if (meetingMessage != null && !meetingMessage.IsRead)
				{
					meetingMessage.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, meetingMessage), false);
				}
			}
			object obj = base.Item.TryGetProperty(ItemSchema.SentTime);
			if (obj != null && !(obj is PropertyError))
			{
				this.sentTime = (ExDateTime)obj;
			}
			if (this.sharedFromOlderVersion)
			{
				this.meetingPageWriter.FormInfobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(1896884103), new object[]
				{
					this.receiverDisplayName
				}), InfobarMessageType.Informational);
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000E8944 File Offset: 0x000E6B44
		protected virtual void MeetingPageWriterFactory(string itemType, EventArgs e)
		{
			if (ObjectClass.IsMeetingRequest(itemType))
			{
				this.meetingPageWriter = new MeetingInviteWriter(base.Item = base.Initialize<MeetingRequest>(MeetingPageWriter.MeetingMessagePrefetchProperties), base.UserContext, null, base.IsPreviewForm, base.IsInDeleteItems, base.IsEmbeddedItem, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, this.itemLinkEnabled);
				if (!this.IsDraft)
				{
					this.shouldRenderToolbar = true;
					if (!base.IsEmbeddedItem && this.IsOwnerMailboxSession && (this.IsItemFromOtherMailbox || this.IsDelegated || Utilities.IsItemInExternalSharedInFolder(base.UserContext, base.Item)))
					{
						this.GetPrincipalCalendarFolderId();
					}
				}
			}
			else if (ObjectClass.IsMeetingCancellation(itemType))
			{
				this.meetingPageWriter = new MeetingCancelWriter(base.Item = base.Initialize<MeetingCancellation>(MeetingPageWriter.MeetingMessagePrefetchProperties), base.UserContext, null, base.IsPreviewForm, base.IsInDeleteItems, base.IsEmbeddedItem, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, this.itemLinkEnabled);
				if (!this.IsDraft)
				{
					this.shouldRenderToolbar = true;
					if (!base.IsEmbeddedItem && this.IsOwnerMailboxSession && (this.IsItemFromOtherMailbox || this.IsDelegated || Utilities.IsItemInExternalSharedInFolder(base.UserContext, base.Item)))
					{
						this.GetPrincipalCalendarFolderId();
					}
				}
			}
			else if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(itemType))
			{
				CalendarItemBase calendarItemBase = base.Item = base.Initialize<CalendarItemBase>(MeetingPageWriter.CalendarPrefetchProperties);
				this.isCalendarItem = true;
				this.isMeeting = calendarItemBase.IsMeeting;
				this.meetingPageWriter = new CalendarItemWriter(calendarItemBase, base.UserContext, base.IsPreviewForm, base.IsInDeleteItems, base.IsEmbeddedItem, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, this.itemLinkEnabled);
				this.shouldRenderToolbar = true;
				if (!base.IsEmbeddedItem && this.IsOwnerMailboxSession && (this.IsItemFromOtherMailbox || Utilities.IsItemInExternalSharedInFolder(base.UserContext, base.Item)))
				{
					this.GetPrincipalCalendarFolderId();
				}
			}
			else
			{
				if (!ObjectClass.IsMeetingResponse(itemType))
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Unsupported item type '{0}' for meeting page", itemType);
					throw new OwaInvalidRequestException(string.Format("Unsupported item type '{0}' for edit meeting page", itemType));
				}
				MeetingResponse meetingResponse = base.Item = base.Initialize<MeetingResponse>(MeetingPageWriter.MeetingMessagePrefetchProperties);
				this.meetingPageWriter = new MeetingResponseWriter(meetingResponse, base.UserContext, base.IsPreviewForm, base.IsInDeleteItems, base.IsEmbeddedItem, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, this.itemLinkEnabled);
				if (meetingResponse.From != null && this.IsDraft)
				{
					this.shouldRenderSendOnBehalf = (string.CompareOrdinal(base.UserContext.ExchangePrincipal.LegacyDn, meetingResponse.From.EmailAddress) != 0);
				}
				this.shouldRenderToolbar = true;
			}
			if (this.MeetingPageWriter.ShouldRenderReminder && this.MeetingPageWriter.CalendarItemBase != null)
			{
				this.disableOccurrenceReminderUI = MeetingUtilities.CheckShouldDisableOccurrenceReminderUI(this.MeetingPageWriter.CalendarItemBase);
				if (this.disableOccurrenceReminderUI && !this.IsPublicItem)
				{
					this.MeetingPageWriter.FormInfobar.AddMessage(SanitizedHtmlString.FromStringId(-891369593), InfobarMessageType.Informational);
				}
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000E8C7C File Offset: 0x000E6E7C
		private void GetPrincipalCalendarFolderId()
		{
			try
			{
				this.principalCalendarFolderId = this.meetingPageWriter.GetPrincipalCalendarFolderId(this.isCalendarItem);
			}
			catch (OwaSharedFromOlderVersionException)
			{
				this.sharedFromOlderVersion = true;
				if (base.Item is MeetingMessage)
				{
					CalendarUtilities.GetReceiverGSCalendarIdStringAndDisplayName(base.UserContext, (MeetingMessage)base.Item, out this.principalCalendarFolderId, out this.receiverDisplayName);
				}
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000E8CEC File Offset: 0x000E6EEC
		protected override void OnUnload(EventArgs e)
		{
			try
			{
				if (this.meetingPageWriter != null)
				{
					this.meetingPageWriter.Dispose();
					this.meetingPageWriter = null;
				}
			}
			finally
			{
				base.OnUnload(e);
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000E8D30 File Offset: 0x000E6F30
		protected virtual void LoadMessageBodyIntoStream(TextWriter writer)
		{
			string action = base.IsPreviewForm ? "Preview" : string.Empty;
			string attachmentUrl = null;
			if (base.IsEmbeddedItemInNonSMimeItem)
			{
				attachmentUrl = base.RenderEmbeddedUrl();
			}
			base.AttachmentLinks = BodyConversionUtilities.GenerateNonEditableMessageBodyAndRenderInfobarMessages(base.Item, writer, base.OwaContext, this.meetingPageWriter.FormInfobar, base.ForceAllowWebBeacon, base.ForceEnableItemLink, base.Item.ClassName, action, string.Empty, base.IsEmbeddedItemInNonSMimeItem, attachmentUrl);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000E8DAC File Offset: 0x000E6FAC
		protected void CreateAttachmentHelpers()
		{
			this.attachmentInformation = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem, base.ForceEnableItemLink);
			this.hasAttachments = RenderingUtilities.AddAttachmentInfobarMessages(base.Item, base.IsEmbeddedItem, base.ForceEnableItemLink, this.meetingPageWriter.FormInfobar, this.attachmentInformation);
			base.SetShouldRenderDownloadAllLink(this.attachmentInformation);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000E8E24 File Offset: 0x000E7024
		protected void RenderCalendarItemId()
		{
			CalendarItemBase calendarItemBase = this.meetingPageWriter.CalendarItemBase;
			if (calendarItemBase == null || calendarItemBase.Id == null)
			{
				base.Response.Write("null");
				return;
			}
			base.Response.Write("\"");
			Utilities.JavascriptEncode(Utilities.GetIdAsString(calendarItemBase), base.Response.Output);
			base.Response.Write("\"");
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000E8E90 File Offset: 0x000E7090
		protected void BuildCalendarInfobar()
		{
			if (base.IsPreviewForm || base.IsEmbeddedItem)
			{
				return;
			}
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "clr", false);
			if (queryStringParameter == null)
			{
				return;
			}
			CalendarUtilities.BuildCalendarInfobar(this.meetingPageWriter.FormInfobar, base.UserContext, base.ParentFolderId, CalendarColorManager.ParseColorIndexString(queryStringParameter, true), false);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000E8EE8 File Offset: 0x000E70E8
		protected void RenderReminderDropdownList()
		{
			CalendarUtilities.RenderReminderDropdownList(base.Response.Output, base.Item, this.MeetingPageWriter.ReminderIsSet, base.IsEmbeddedItem || this.DisableOccurrenceReminderUI || this.IsPublicItem || !this.CanEdit);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000E8F3A File Offset: 0x000E713A
		protected void RenderBusyTypeDropdownList()
		{
			CalendarUtilities.RenderBusyTypeDropdownList(base.Response.Output, base.Item, base.IsEmbeddedItem || !this.CanEdit);
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000E8F66 File Offset: 0x000E7166
		protected bool DisableReminderCheckbox
		{
			get
			{
				return base.IsEmbeddedItem || this.DisableOccurrenceReminderUI || this.IsPublicItem || !this.CanEdit;
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000E8F8B File Offset: 0x000E718B
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000E8F9E File Offset: 0x000E719E
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000E8FBF File Offset: 0x000E71BF
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(base.Item, base.Response.Output, "DIV#divBdy");
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000E8FDC File Offset: 0x000E71DC
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000E8FFE File Offset: 0x000E71FE
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000E9020 File Offset: 0x000E7220
		protected void RenderJavascriptEncodedCalendarItemBaseMasterId()
		{
			CalendarItemBase calendarItemBase = this.meetingPageWriter.CalendarItemBase;
			if (this.isCalendarItem && !base.IsEmbeddedItem && !base.IsInDeleteItems && (calendarItemBase.CalendarItemType == CalendarItemType.Occurrence || calendarItemBase.CalendarItemType == CalendarItemType.Exception))
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObject(calendarItemBase);
				Utilities.JavascriptEncode(owaStoreObjectId.ProviderLevelItemId.ToString(), base.Response.Output);
			}
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000E9085 File Offset: 0x000E7285
		protected void RenderJavascriptEncodedPrincipalCalendarFolderId()
		{
			if (this.principalCalendarFolderId != null)
			{
				Utilities.JavascriptEncode(this.principalCalendarFolderId, base.Response.Output);
			}
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000E90A8 File Offset: 0x000E72A8
		protected void RenderSentTime(TextWriter writer)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			this.RenderSentTime(sanitizingStringBuilder);
			writer.Write(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000E90D0 File Offset: 0x000E72D0
		protected void RenderSentTime(SanitizingStringBuilder<OwaHtml> stringBuilder)
		{
			if (base.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos))
			{
				stringBuilder.Append("<span class=\"spnSentInSender snt\">");
			}
			stringBuilder.Append("<span>");
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(295620541));
			stringBuilder.Append("</span>");
			stringBuilder.Append("<span id=spnSent> ");
			stringBuilder.Append(base.UserContext.DirectionMark);
			RenderingUtilities.RenderSentTime(stringBuilder, this.SentTime, base.UserContext);
			stringBuilder.Append("</span>");
			if (base.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos))
			{
				stringBuilder.Append("</span>");
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000E917E File Offset: 0x000E737E
		protected bool IsFromOlderVersion
		{
			get
			{
				return this.sharedFromOlderVersion;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000E9186 File Offset: 0x000E7386
		protected bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x000E918E File Offset: 0x000E738E
		protected bool MeetingRequestWasSent
		{
			get
			{
				return base.Item is CalendarItemBase && this.IsMeeting && (base.Item as CalendarItemBase).MeetingRequestWasSent;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000E91B9 File Offset: 0x000E73B9
		protected bool CanEdit
		{
			get
			{
				if (this.canEdit == null)
				{
					this.canEdit = new bool?(ItemUtility.UserCanEditItem(base.Item) && !this.IsInExternalSharedInFolder);
				}
				return this.canEdit.Value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x000E91F8 File Offset: 0x000E73F8
		protected bool CanDelete
		{
			get
			{
				if (this.canDelete == null)
				{
					if (base.Item is CalendarItemBase)
					{
						this.canDelete = new bool?(CalendarUtilities.UserCanDeleteCalendarItem(base.Item as CalendarItemBase));
					}
					else
					{
						this.canDelete = new bool?(ItemUtility.UserCanDeleteItem(base.Item));
					}
					this.canDelete = new bool?(this.canDelete.Value && !this.IsInExternalSharedInFolder);
				}
				return this.canDelete.Value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x000E9281 File Offset: 0x000E7481
		protected bool CanReply
		{
			get
			{
				return ItemUtility.IsReplySupported(base.Item);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x000E928E File Offset: 0x000E748E
		protected bool CanForward
		{
			get
			{
				return ItemUtility.IsForwardSupported(base.Item);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000E929B File Offset: 0x000E749B
		protected bool IsInExternalSharedInFolder
		{
			get
			{
				if (this.isInExternalSharedInFolder == null)
				{
					this.isInExternalSharedInFolder = new bool?(Utilities.IsItemInExternalSharedInFolder(base.UserContext, base.Item));
				}
				return this.isInExternalSharedInFolder.Value;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x000E92D1 File Offset: 0x000E74D1
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000E92D9 File Offset: 0x000E74D9
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x000E92E1 File Offset: 0x000E74E1
		protected MeetingPageWriter MeetingPageWriter
		{
			get
			{
				return this.meetingPageWriter;
			}
			set
			{
				this.meetingPageWriter = value;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x000E92EC File Offset: 0x000E74EC
		protected bool IsPrivate
		{
			get
			{
				object obj = base.Item.TryGetProperty(ItemSchema.Sensitivity);
				return obj is Sensitivity && (Sensitivity)obj == Sensitivity.Private;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000E9320 File Offset: 0x000E7520
		protected bool IsDraft
		{
			get
			{
				object obj = base.Item.TryGetProperty(MessageItemSchema.IsDraft);
				return obj is bool && (bool)obj;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000E934E File Offset: 0x000E754E
		protected bool ShouldRenderSentField
		{
			get
			{
				return this.sentTime != ExDateTime.MinValue && this.MeetingPageWriter.ShouldRenderSentField;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000E936F File Offset: 0x000E756F
		protected ExDateTime SentTime
		{
			get
			{
				return this.sentTime;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000E9377 File Offset: 0x000E7577
		protected bool ShouldRenderReminder
		{
			get
			{
				return this.MeetingPageWriter.ShouldRenderReminder;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x000E9384 File Offset: 0x000E7584
		protected bool ShouldRenderInspectorMailToolbar
		{
			get
			{
				return !base.IsPreviewForm && !base.IsEmbeddedItem;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000E9399 File Offset: 0x000E7599
		protected bool ShouldRenderToolbar
		{
			get
			{
				return !this.IsPublicItem && !base.IsEmbeddedItem && this.shouldRenderToolbar;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000E93B3 File Offset: 0x000E75B3
		protected bool ShouldRenderSendOnBehalf
		{
			get
			{
				return this.shouldRenderSendOnBehalf;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x000E93BB File Offset: 0x000E75BB
		protected virtual bool HasAttachments
		{
			get
			{
				return this.hasAttachments;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x000E93C3 File Offset: 0x000E75C3
		public ArrayList AttachmentInformation
		{
			get
			{
				return this.attachmentInformation;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000E93CC File Offset: 0x000E75CC
		protected string SequenceNumber
		{
			get
			{
				object obj = base.Item.TryGetProperty(CalendarItemBaseSchema.AppointmentSequenceNumber);
				if (obj is int)
				{
					return ((int)obj).ToString(CultureInfo.InvariantCulture);
				}
				return "0";
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000E940C File Offset: 0x000E760C
		protected bool EnablePrivateCheckbox
		{
			get
			{
				return this.CanEdit && !this.IsPublicItem && !base.IsEmbeddedItem && !this.IsItemFromOtherMailbox && (this.meetingPageWriter.CalendarItemBase == null || (this.meetingPageWriter.CalendarItemBase != null && (this.meetingPageWriter.CalendarItemBase.CalendarItemType == CalendarItemType.Single || this.meetingPageWriter.CalendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)));
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000E9480 File Offset: 0x000E7680
		protected bool DisableOccurrenceReminderUI
		{
			get
			{
				return this.disableOccurrenceReminderUI;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000E9488 File Offset: 0x000E7688
		protected bool IsInJunkMailFolder
		{
			get
			{
				return this.isInJunkEmailFolder;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000E9490 File Offset: 0x000E7690
		protected bool IsSuspectedPhishingItem
		{
			get
			{
				return this.isSuspectedPhishingItem;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000E9498 File Offset: 0x000E7698
		protected bool IsSuspectedPhishingItemWithoutLinkEnabled
		{
			get
			{
				return this.isSuspectedPhishingItem && !this.itemLinkEnabled;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000E94AD File Offset: 0x000E76AD
		protected bool IsDelegated
		{
			get
			{
				return !this.isCalendarItem && ((MeetingMessage)base.Item).IsDelegated();
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000E94C9 File Offset: 0x000E76C9
		protected bool IsItemFromOtherMailbox
		{
			get
			{
				return base.UserContext.IsInOtherMailbox(base.Item);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000E94DC File Offset: 0x000E76DC
		protected bool IsOwnerMailboxSession
		{
			get
			{
				return base.UserContext.MailboxSession.LogonType == LogonType.Owner;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000E94F1 File Offset: 0x000E76F1
		protected int MeetingPageWriterStoreObjectType
		{
			get
			{
				return this.meetingPageWriter.StoreObjectType;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000E94FE File Offset: 0x000E76FE
		protected static int StoreObjectTypeMeetingResponse
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000E9502 File Offset: 0x000E7702
		protected static int StoreObjectTypeMeetingRequest
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000E9506 File Offset: 0x000E7706
		protected static int StoreObjectTypeMeetingCancellation
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000E950A File Offset: 0x000E770A
		protected static int StoreObjectTypeCalendarItem
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000E9510 File Offset: 0x000E7710
		protected RecipientJunkEmailContextMenuType RecipientJunkEmailMenuType
		{
			get
			{
				RecipientJunkEmailContextMenuType result = RecipientJunkEmailContextMenuType.None;
				if (base.UserContext.IsJunkEmailEnabled)
				{
					result = RecipientJunkEmailContextMenuType.SenderAndRecipient;
				}
				return result;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000E952F File Offset: 0x000E772F
		protected FlagAction FlagAction
		{
			get
			{
				return FlagContextMenu.GetFlagActionForItem(base.UserContext, base.Item);
			}
		}

		// Token: 0x04001C22 RID: 7202
		private const string TypeString = "t";

		// Token: 0x04001C23 RID: 7203
		private MeetingPageWriter meetingPageWriter;

		// Token: 0x04001C24 RID: 7204
		private bool shouldRenderToolbar;

		// Token: 0x04001C25 RID: 7205
		private Markup bodyMarkup;

		// Token: 0x04001C26 RID: 7206
		private ArrayList attachmentInformation;

		// Token: 0x04001C27 RID: 7207
		private bool hasAttachments;

		// Token: 0x04001C28 RID: 7208
		private ExDateTime sentTime = ExDateTime.MinValue;

		// Token: 0x04001C29 RID: 7209
		private bool isCalendarItem;

		// Token: 0x04001C2A RID: 7210
		private bool isMeeting;

		// Token: 0x04001C2B RID: 7211
		private bool sharedFromOlderVersion;

		// Token: 0x04001C2C RID: 7212
		private string receiverDisplayName;

		// Token: 0x04001C2D RID: 7213
		private bool? isInExternalSharedInFolder;

		// Token: 0x04001C2E RID: 7214
		private bool? canEdit;

		// Token: 0x04001C2F RID: 7215
		private bool? canDelete;

		// Token: 0x04001C30 RID: 7216
		private bool disableOccurrenceReminderUI;

		// Token: 0x04001C31 RID: 7217
		private bool isInJunkEmailFolder;

		// Token: 0x04001C32 RID: 7218
		private bool isSuspectedPhishingItem;

		// Token: 0x04001C33 RID: 7219
		private bool itemLinkEnabled;

		// Token: 0x04001C34 RID: 7220
		private bool isJunkOrPhishing;

		// Token: 0x04001C35 RID: 7221
		private string principalCalendarFolderId;

		// Token: 0x04001C36 RID: 7222
		private bool shouldRenderSendOnBehalf;
	}
}
