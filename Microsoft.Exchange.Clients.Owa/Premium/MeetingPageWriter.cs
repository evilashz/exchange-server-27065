using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200032C RID: 812
	public abstract class MeetingPageWriter : DisposeTrackableBase
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x000B0FC4 File Offset: 0x000AF1C4
		internal MeetingPageWriter(Item meetingPageItem, UserContext userContext, bool isPreviewForm, bool isInDeletedItems, bool isEmbeddedItem, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled)
		{
			this.meetingPageItem = meetingPageItem;
			this.UserContext = userContext;
			this.isPreviewForm = isPreviewForm;
			this.isInDeletedItems = isInDeletedItems;
			this.isEmbeddedItem = isEmbeddedItem;
			this.isInJunkEmailFolder = isInJunkEmailFolder;
			this.isSuspectedPhishingItem = isSuspectedPhishingItem;
			this.isLinkEnabled = isLinkEnabled;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000B1014 File Offset: 0x000AF214
		internal bool ProcessMeetingMessage(MeetingMessage meetingMessage, bool doCalendarItemSave)
		{
			this.CalendarItemBase = MeetingUtilities.TryPreProcessCalendarItem(meetingMessage, this.UserContext, doCalendarItemSave);
			if (this.CalendarItemBase != null)
			{
				return this.CalendarItemBase.IsOrganizer();
			}
			return meetingMessage.IsOrganizer();
		}

		// Token: 0x06001EAC RID: 7852
		protected internal abstract void BuildInfobar();

		// Token: 0x06001EAD RID: 7853 RVA: 0x000B1043 File Offset: 0x000AF243
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.CalendarItemBase != null)
			{
				this.CalendarItemBase.Dispose();
				this.CalendarItemBase = null;
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000B1062 File Offset: 0x000AF262
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingPageWriter>(this);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000B106C File Offset: 0x000AF26C
		public virtual void RenderInspectorMailToolbar(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ReadMessageToolbar readMessageToolbar = new ReadMessageToolbar(this.IsInDeletedItems, this.IsEmbeddedItem, this.meetingPageItem, this.IsInJunkEmailFolder, this.IsSuspectedPhishingItem, this.IsLinkEnabled);
			readMessageToolbar.Render(writer);
		}

		// Token: 0x06001EB0 RID: 7856
		public abstract void RenderTitle(TextWriter writer);

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000B10B8 File Offset: 0x000AF2B8
		public virtual void RenderMeetingInfoArea(TextWriter writer, bool shouldRenderToolbars)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div id=\"divMtgInfo\" ");
			string value = this.GetMeetingInfoClass();
			if (!string.IsNullOrEmpty(value))
			{
				writer.Write("class =\"");
				writer.Write(value);
				writer.Write("\"");
			}
			writer.Write(">");
			this.RenderMeetingInfoHeader(writer);
			writer.Write("<div id=\"divMtgInfoToolbars\"");
			value = this.GetMeetingToolbarClass();
			if (!string.IsNullOrEmpty(value))
			{
				writer.Write(" class =\"");
				writer.Write(value);
				writer.Write("\"");
			}
			writer.Write(">");
			if (shouldRenderToolbars)
			{
				this.RenderOpenCalendarToolbar(writer);
				this.RenderWhen(writer);
				this.RenderMeetingActionsToolbar(writer);
			}
			else
			{
				writer.Write("<div id=\"divWhenL\" class=\"roWellLabel pvwLabel\">");
				writer.Write(SanitizedHtmlString.FromStringId(-524211323));
				writer.Write("</div>");
				this.RenderWhen(writer);
			}
			writer.Write("</div>");
			this.RenderLocation(writer);
			writer.Write("</div>");
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000B11C2 File Offset: 0x000AF3C2
		protected virtual void RenderMeetingInfoHeader(TextWriter writer)
		{
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000B11C4 File Offset: 0x000AF3C4
		protected virtual string GetMeetingInfoClass()
		{
			return string.Empty;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000B11CB File Offset: 0x000AF3CB
		protected virtual string GetMeetingToolbarClass()
		{
			return string.Empty;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000B11D2 File Offset: 0x000AF3D2
		protected virtual void RenderMeetingActionsToolbar(TextWriter writer)
		{
			if (this.Toolbar != null)
			{
				this.Toolbar.Render(writer);
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B11E8 File Offset: 0x000AF3E8
		protected virtual void RenderOpenCalendarToolbar(TextWriter writer)
		{
			OpenCalendarToolbar openCalendarToolbar = new OpenCalendarToolbar();
			openCalendarToolbar.Render(writer);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000B1202 File Offset: 0x000AF402
		public virtual void RenderSender(TextWriter writer)
		{
			this.RenderSender(writer, null);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000B120C File Offset: 0x000AF40C
		public void RenderSender(TextWriter writer, RenderSubHeaderDelegate renderSubHeader)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (Utilities.IsOnBehalfOf(this.ActualSender, this.OriginalSender))
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), RenderingUtilities.GetSender(this.UserContext, this.ActualSender, "spnFrom", renderSubHeader), RenderingUtilities.GetSender(this.UserContext, this.OriginalSender, "spnOrg", null));
				return;
			}
			RenderingUtilities.RenderSender(this.UserContext, writer, this.OriginalSender, renderSubHeader);
		}

		// Token: 0x06001EB9 RID: 7865
		public abstract void RenderSubject(TextWriter writer, bool disableEdit);

		// Token: 0x06001EBA RID: 7866 RVA: 0x000B128C File Offset: 0x000AF48C
		public virtual void RenderSendOnBehalf(TextWriter writer)
		{
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000B128E File Offset: 0x000AF48E
		public virtual void RenderWhen(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div id=\"divMtgTbWhen\">");
			writer.Write(this.When);
			writer.Write("</div>");
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B12C0 File Offset: 0x000AF4C0
		public virtual void RenderLocation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div id=\"divMtgTbWhere\">");
			writer.Write("<div id=\"divLocationL\" class=\"roWellLabel pvwLabel");
			writer.Write("\">");
			writer.Write(SanitizedHtmlString.FromStringId(1666265192));
			writer.Write("</div><div id=\"divLoc\">");
			writer.Write(this.Location);
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000B1339 File Offset: 0x000AF539
		public virtual void RenderDescription(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!string.IsNullOrEmpty(this.DescriptionTag))
			{
				writer.Write("<div class=dscrp>");
				Utilities.HtmlEncode(this.DescriptionTag, writer);
				writer.Write("</div>");
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000B1378 File Offset: 0x000AF578
		public virtual void RenderStartTime(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			object obj = this.meetingPageItem.TryGetProperty(CalendarItemInstanceSchema.StartTime);
			if (obj is ExDateTime)
			{
				RenderingUtilities.RenderDateTimeScriptObject(writer, (ExDateTime)obj);
				return;
			}
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			RenderingUtilities.RenderDateTimeScriptObject(writer, localTime);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000B13C6 File Offset: 0x000AF5C6
		public virtual bool ShouldRenderRecipientWell(RecipientWellType recipientWellType)
		{
			return this.RecipientWell.HasRecipients(recipientWellType);
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001EC0 RID: 7872
		public abstract int StoreObjectType { get; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001EC1 RID: 7873
		public abstract RecipientWell RecipientWell { get; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x000B13D4 File Offset: 0x000AF5D4
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x000B13DC File Offset: 0x000AF5DC
		public virtual RecipientWell AttendeeResponseWell
		{
			get
			{
				return this.attendeeResponseWell;
			}
			set
			{
				this.attendeeResponseWell = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x000B13E5 File Offset: 0x000AF5E5
		public virtual bool ShouldRenderSentField
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000B13E8 File Offset: 0x000AF5E8
		public virtual bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000B13EC File Offset: 0x000AF5EC
		public string GetPrincipalCalendarFolderId(bool isCalendarItem)
		{
			string result = null;
			ExchangePrincipal exchangePrincipal;
			if (this.CalendarItemBase != null && this.CalendarItemBase.ParentId != null)
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(this.CalendarItemBase.ParentId, Utilities.GetMailboxSessionLegacyDN(this.CalendarItemBase));
				result = owaStoreObjectId.ToBase64String();
			}
			else if (!isCalendarItem && this.UserContext.DelegateSessionManager.TryGetExchangePrincipal((this.meetingPageItem as MeetingMessage).ReceivedRepresenting.EmailAddress, out exchangePrincipal))
			{
				using (OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(exchangePrincipal, this.UserContext))
				{
					OwaStoreObjectId owaStoreObjectId2 = Utilities.TryGetDefaultFolderId(this.UserContext, owaStoreObjectIdSessionHandle.Session as MailboxSession, DefaultFolderType.Calendar);
					if (owaStoreObjectId2 == null)
					{
						throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892));
					}
					result = owaStoreObjectId2.ToBase64String();
				}
			}
			return result;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		protected internal virtual SanitizedHtmlString Location
		{
			get
			{
				string text = this.meetingPageItem.TryGetProperty(CalendarItemBaseSchema.Location) as string;
				if (text != null)
				{
					return Utilities.SanitizeHtmlEncode(text);
				}
				return SanitizedHtmlString.Empty;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x000B14F6 File Offset: 0x000AF6F6
		protected internal SanitizedHtmlString When
		{
			get
			{
				return Utilities.SanitizeHtmlEncode(Utilities.GenerateWhen(this.meetingPageItem));
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000B1508 File Offset: 0x000AF708
		protected internal virtual string OldWhen
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000B150F File Offset: 0x000AF70F
		protected internal virtual string OldLocation
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000B1516 File Offset: 0x000AF716
		internal Item MeetingPageItem
		{
			get
			{
				return this.meetingPageItem;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x000B151E File Offset: 0x000AF71E
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x000B1526 File Offset: 0x000AF726
		internal virtual CalendarItemBase CalendarItemBase
		{
			get
			{
				return this.calendarItemBase;
			}
			set
			{
				this.calendarItemBase = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000B152F File Offset: 0x000AF72F
		protected internal virtual UserContext MeetingPageUserContext
		{
			get
			{
				return this.UserContext;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000B1537 File Offset: 0x000AF737
		public virtual Infobar FormInfobar
		{
			get
			{
				if (this.infobar == null)
				{
					this.infobar = new Infobar();
					this.BuildInfobar();
				}
				return this.infobar;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000B1558 File Offset: 0x000AF758
		public virtual bool ReminderIsSet
		{
			get
			{
				object obj = this.meetingPageItem.TryGetProperty(ItemSchema.ReminderIsSet);
				return obj is bool && (bool)obj;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001ED1 RID: 7889
		public abstract bool ShouldRenderReminder { get; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x000B1586 File Offset: 0x000AF786
		protected internal virtual bool IsPreviewForm
		{
			get
			{
				return this.isPreviewForm;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000B158E File Offset: 0x000AF78E
		protected internal bool IsInDeletedItems
		{
			get
			{
				return this.isInDeletedItems;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x000B1596 File Offset: 0x000AF796
		protected internal bool IsEmbeddedItem
		{
			get
			{
				return this.isEmbeddedItem;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000B159E File Offset: 0x000AF79E
		protected internal bool IsInJunkEmailFolder
		{
			get
			{
				return this.isInJunkEmailFolder;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000B15A6 File Offset: 0x000AF7A6
		protected internal bool IsSuspectedPhishingItem
		{
			get
			{
				return this.isSuspectedPhishingItem;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000B15AE File Offset: 0x000AF7AE
		protected internal bool IsLinkEnabled
		{
			get
			{
				return this.isLinkEnabled;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001ED8 RID: 7896
		internal abstract Participant OriginalSender { get; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001ED9 RID: 7897
		internal abstract Participant ActualSender { get; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000B15B6 File Offset: 0x000AF7B6
		protected internal virtual string DescriptionTag
		{
			get
			{
				return LocalizedStrings.GetNonEncoded(-1033607801);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000B15C4 File Offset: 0x000AF7C4
		public virtual string MeetingStatus
		{
			get
			{
				string result = null;
				if (this.calendarItemBase != null)
				{
					if (this.calendarItemBase.IsOrganizer())
					{
						if (!Utilities.IsPublic(this.CalendarItemBase))
						{
							result = LocalizedStrings.GetNonEncoded(-323372768);
						}
					}
					else if (this.calendarItemBase.IsMeeting)
					{
						switch (this.calendarItemBase.ResponseType)
						{
						case ResponseType.Tentative:
							result = LocalizedStrings.GetNonEncoded(1798747159);
							break;
						case ResponseType.Accept:
							result = LocalizedStrings.GetNonEncoded(988533680);
							break;
						case ResponseType.Decline:
							result = LocalizedStrings.GetNonEncoded(884780479);
							break;
						case ResponseType.NotResponded:
							result = LocalizedStrings.GetNonEncoded(-244705250);
							break;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000B166B File Offset: 0x000AF86B
		internal virtual Toolbar Toolbar
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400168B RID: 5771
		protected internal const string MeetingPageToolbarId = "mpToolbar";

		// Token: 0x0400168C RID: 5772
		protected internal const string StartSubjectMarkup = "<div id=\"divSubj\" tabindex=0 _editable=1>";

		// Token: 0x0400168D RID: 5773
		protected internal const string StartSubjectMarkupNoEdit = "<div id=\"divSubj\">";

		// Token: 0x0400168E RID: 5774
		protected internal const string EndDivMarkup = "</div>";

		// Token: 0x0400168F RID: 5775
		private const bool ShouldRenderAttendeeResponseWellsValue = false;

		// Token: 0x04001690 RID: 5776
		protected internal const string OneButtonToolbarClass = "oneBtnTb";

		// Token: 0x04001691 RID: 5777
		protected internal const string ThreeButtonsToolbarClass = "threeBtnTb";

		// Token: 0x04001692 RID: 5778
		internal static readonly PropertyDefinition[] MeetingMessagePrefetchProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead,
			MessageItemSchema.IsDraft,
			MeetingMessageSchema.CalendarProcessed,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x04001693 RID: 5779
		internal static readonly PropertyDefinition[] CalendarPrefetchProperties = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.CalendarItemType,
			ItemSchema.SentTime,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			StoreObjectSchema.EffectiveRights,
			CalendarItemBaseSchema.MeetingRequestWasSent
		};

		// Token: 0x04001694 RID: 5780
		protected internal UserContext UserContext;

		// Token: 0x04001695 RID: 5781
		private Item meetingPageItem;

		// Token: 0x04001696 RID: 5782
		private CalendarItemBase calendarItemBase;

		// Token: 0x04001697 RID: 5783
		private Infobar infobar;

		// Token: 0x04001698 RID: 5784
		private RecipientWell attendeeResponseWell;

		// Token: 0x04001699 RID: 5785
		private bool isPreviewForm;

		// Token: 0x0400169A RID: 5786
		private bool isInDeletedItems;

		// Token: 0x0400169B RID: 5787
		private bool isEmbeddedItem;

		// Token: 0x0400169C RID: 5788
		private bool isInJunkEmailFolder;

		// Token: 0x0400169D RID: 5789
		private bool isSuspectedPhishingItem;

		// Token: 0x0400169E RID: 5790
		private bool isLinkEnabled;
	}
}
