using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200001D RID: 29
	public abstract class MeetingPageWriter : DisposeTrackableBase
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00007657 File Offset: 0x00005857
		internal MeetingPageWriter(Item meetingPageItem, UserContext userContext)
		{
			this.meetingPageItem = meetingPageItem;
			this.userContext = userContext;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000766D File Offset: 0x0000586D
		internal bool ProcessMeetingMessage(MeetingMessage meetingMessage, bool doCalendarItemSave)
		{
			this.CalendarItemBase = MeetingUtilities.TryPreProcessCalendarItem(meetingMessage, this.userContext, doCalendarItemSave);
			if (this.CalendarItemBase != null)
			{
				return this.CalendarItemBase.IsOrganizer();
			}
			return meetingMessage.IsOrganizer();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000769C File Offset: 0x0000589C
		protected static void RenderResponseEditTypeSelectToolbar(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table class=\"stb\" cellpadding=0 cellspacing=0><tr>");
			for (int i = 0; i <= 2; i++)
			{
				writer.Write("<td class=\"btncl\"><input class=\"rdobtn\" type=\"radio\" name=\"");
				writer.Write("rdoRsp");
				writer.Write("\" id=\"");
				writer.Write("rdoRsp");
				writer.Write(i + 1);
				writer.Write("\" value=\"");
				writer.Write(i);
				writer.Write("\"");
				if (i == 1)
				{
					writer.Write(" checked");
				}
				writer.Write("><label for=\"");
				writer.Write("rdoRsp");
				writer.Write(i + 1);
				writer.Write("\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(MeetingPageWriter.responseEditTypeStringIds[i]));
				writer.Write("</label></td>");
			}
			writer.Write("<td class=\"w100\"></tr></table>");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007787 File Offset: 0x00005987
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007789 File Offset: 0x00005989
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingPageWriter>(this);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007791 File Offset: 0x00005991
		public virtual void RenderSubject(TextWriter writer)
		{
			RenderingUtilities.RenderSubject(writer, this.meetingPageItem);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000779F File Offset: 0x0000599F
		public virtual void RenderToolbar(TextWriter writer)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000077A4 File Offset: 0x000059A4
		public virtual void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (Utilities.IsOnBehalfOf(this.ActualSender, this.OriginalSender))
			{
				RenderingUtilities.RenderSenderOnBehalfOf(this.ActualSender, this.OriginalSender, writer, this.userContext);
				return;
			}
			RenderingUtilities.RenderSender(this.userContext, writer, this.OriginalSender);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007800 File Offset: 0x00005A00
		public virtual void RenderSentTime(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(LocalizedStrings.GetHtmlEncoded(295620541));
			writer.Write("&nbsp;");
			ExDateTime property = ItemUtility.GetProperty<ExDateTime>(this.meetingPageItem, ItemSchema.SentTime, ExDateTime.MinValue);
			if (property != ExDateTime.MinValue)
			{
				RenderingUtilities.RenderSentTime(writer, property, this.userContext);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007866 File Offset: 0x00005A66
		public virtual bool ShouldRenderRecipientWell(RecipientWellType recipientWellType)
		{
			return this.RecipientWell.HasRecipients(recipientWellType);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007874 File Offset: 0x00005A74
		public virtual void RenderWhen(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<td class=\"hdmsb\">");
			writer.Write(SanitizedHtmlString.FromStringId(-524211323));
			writer.Write("</td><td class=\"hdmtxt\"><span>");
			writer.Write(this.When);
			writer.Write("</span></td>");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000078CC File Offset: 0x00005ACC
		public virtual void RenderLocation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<td class=\"hdmsb\">");
			writer.Write(SanitizedHtmlString.FromStringId(-1134349396));
			writer.Write("</td><td class=\"hdmtxt\"><span>");
			writer.Write(this.Location);
			writer.Write("</span></td>");
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D5 RID: 213
		public abstract RecipientWell RecipientWell { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D6 RID: 214
		public abstract int StoreObjectType { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00007924 File Offset: 0x00005B24
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000792C File Offset: 0x00005B2C
		public virtual RecipientWell AttendeeResponseWell
		{
			get
			{
				return this.attendeeResponseWell;
			}
			set
			{
				this.attendeeResponseWell = (value as CalendarItemAttendeeResponseRecipientWell);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000793A File Offset: 0x00005B3A
		public virtual bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000793D File Offset: 0x00005B3D
		public virtual bool HasToolbar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00007940 File Offset: 0x00005B40
		protected SanitizedHtmlString When
		{
			get
			{
				return Utilities.SanitizeHtmlEncode(Utilities.GenerateWhen(this.meetingPageItem));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00007954 File Offset: 0x00005B54
		protected virtual SanitizedHtmlString Location
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DD RID: 221
		internal abstract Participant OriginalSender { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DE RID: 222
		internal abstract Participant ActualSender { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00007986 File Offset: 0x00005B86
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000798E File Offset: 0x00005B8E
		internal CalendarItemBase CalendarItemBase
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

		// Token: 0x04000086 RID: 134
		private const bool ShouldRenderAttendeeResponseWellsValue = false;

		// Token: 0x04000087 RID: 135
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
			ItemSchema.ReminderIsSet
		};

		// Token: 0x04000088 RID: 136
		private static Strings.IDs[] responseEditTypeStringIds = new Strings.IDs[]
		{
			-114654491,
			1050381195,
			-990767046
		};

		// Token: 0x04000089 RID: 137
		private CalendarItemBase calendarItemBase;

		// Token: 0x0400008A RID: 138
		private Item meetingPageItem;

		// Token: 0x0400008B RID: 139
		private CalendarItemAttendeeResponseRecipientWell attendeeResponseWell;

		// Token: 0x0400008C RID: 140
		protected UserContext userContext;
	}
}
