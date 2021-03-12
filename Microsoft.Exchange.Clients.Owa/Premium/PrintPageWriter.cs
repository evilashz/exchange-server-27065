using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003F0 RID: 1008
	public sealed class PrintPageWriter : MeetingPageWriter
	{
		// Token: 0x060024DE RID: 9438 RVA: 0x000D61E4 File Offset: 0x000D43E4
		public PrintPageWriter(MeetingPageWriter delegateWriter) : base(null, null, false, delegateWriter.IsInDeletedItems, delegateWriter.IsEmbeddedItem, delegateWriter.IsInJunkEmailFolder, delegateWriter.IsSuspectedPhishingItem, delegateWriter.IsLinkEnabled)
		{
			if (delegateWriter == null)
			{
				throw new ArgumentNullException("delegateWriter");
			}
			this.delegateWriter = delegateWriter;
			if (delegateWriter.AttendeeResponseWell != null)
			{
				delegateWriter.AttendeeResponseWell = new PrintRecipientWell(delegateWriter.AttendeeResponseWell);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000D6246 File Offset: 0x000D4446
		protected internal override void BuildInfobar()
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000D6248 File Offset: 0x000D4448
		public override void RenderMeetingInfoArea(TextWriter writer, bool shouldRenderToolbars)
		{
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000D624A File Offset: 0x000D444A
		public override void RenderTitle(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			RenderingUtilities.RenderSubject(writer, this.delegateWriter.MeetingPageItem);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000D626C File Offset: 0x000D446C
		public override void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (Utilities.IsOnBehalfOf(this.delegateWriter.ActualSender, this.delegateWriter.OriginalSender))
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), RenderingUtilities.GetDisplaySenderName(this.delegateWriter.ActualSender), RenderingUtilities.GetDisplaySenderName(this.delegateWriter.OriginalSender));
				return;
			}
			writer.Write(RenderingUtilities.GetDisplaySenderName(this.delegateWriter.OriginalSender));
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000D62EB File Offset: 0x000D44EB
		public override void RenderSubject(TextWriter writer, bool disableEdit)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			RenderingUtilities.RenderSubject(writer, this.delegateWriter.MeetingPageItem);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000D630C File Offset: 0x000D450C
		public override void RenderWhen(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(this.delegateWriter.When);
			if (!string.IsNullOrEmpty(this.delegateWriter.OldWhen))
			{
				writer.Write(this.delegateWriter.MeetingPageUserContext.DirectionMark);
				writer.Write("(");
				writer.Write(this.delegateWriter.OldWhen);
				writer.Write(")");
				writer.Write(this.delegateWriter.MeetingPageUserContext.DirectionMark);
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000D63A0 File Offset: 0x000D45A0
		public override void RenderLocation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(this.delegateWriter.Location);
			if (!string.IsNullOrEmpty(this.delegateWriter.OldLocation))
			{
				writer.Write(this.delegateWriter.MeetingPageUserContext.DirectionMark);
				writer.Write("(");
				writer.Write(this.delegateWriter.OldLocation);
				writer.Write("<nobr>");
				writer.Write(")");
				writer.Write(this.delegateWriter.MeetingPageUserContext.DirectionMark);
				writer.Write("</nobr>");
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000D6448 File Offset: 0x000D4648
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				if (isDisposing && this.delegateWriter != null)
				{
					this.delegateWriter.Dispose();
				}
			}
			finally
			{
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000D6488 File Offset: 0x000D4688
		public override bool ShouldRenderRecipientWell(RecipientWellType recipientWellType)
		{
			return this.delegateWriter.ShouldRenderRecipientWell(recipientWellType);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000D6496 File Offset: 0x000D4696
		public override void RenderInspectorMailToolbar(TextWriter writer)
		{
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000D6498 File Offset: 0x000D4698
		public override void RenderDescription(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!string.IsNullOrEmpty(this.DescriptionTag))
			{
				Utilities.HtmlEncode(this.delegateWriter.DescriptionTag, writer);
			}
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000D64C6 File Offset: 0x000D46C6
		public override void RenderStartTime(TextWriter writer)
		{
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000D64C8 File Offset: 0x000D46C8
		public override Infobar FormInfobar
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x000D64CB File Offset: 0x000D46CB
		public override int StoreObjectType
		{
			get
			{
				return this.delegateWriter.StoreObjectType;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000D64D8 File Offset: 0x000D46D8
		public override RecipientWell RecipientWell
		{
			get
			{
				return new PrintRecipientWell(this.delegateWriter.RecipientWell);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000D64EA File Offset: 0x000D46EA
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x000D64F7 File Offset: 0x000D46F7
		public override RecipientWell AttendeeResponseWell
		{
			get
			{
				return this.delegateWriter.AttendeeResponseWell;
			}
			set
			{
				this.delegateWriter.AttendeeResponseWell = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000D6505 File Offset: 0x000D4705
		public override bool ShouldRenderSentField
		{
			get
			{
				return this.delegateWriter.ShouldRenderSentField;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000D6512 File Offset: 0x000D4712
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.delegateWriter.ShouldRenderAttendeeResponseWells;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000D651F File Offset: 0x000D471F
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x000D652C File Offset: 0x000D472C
		internal override CalendarItemBase CalendarItemBase
		{
			get
			{
				return this.delegateWriter.CalendarItemBase;
			}
			set
			{
				this.delegateWriter.CalendarItemBase = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000D653A File Offset: 0x000D473A
		public override bool ReminderIsSet
		{
			get
			{
				return this.delegateWriter.ReminderIsSet;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000D6547 File Offset: 0x000D4747
		protected internal override bool IsPreviewForm
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000D654A File Offset: 0x000D474A
		internal override Participant ActualSender
		{
			get
			{
				return this.delegateWriter.ActualSender;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000D6557 File Offset: 0x000D4757
		internal override Participant OriginalSender
		{
			get
			{
				return this.delegateWriter.OriginalSender;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000D6564 File Offset: 0x000D4764
		protected internal override UserContext MeetingPageUserContext
		{
			get
			{
				return this.delegateWriter.MeetingPageUserContext;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000D6571 File Offset: 0x000D4771
		public override string MeetingStatus
		{
			get
			{
				return this.delegateWriter.MeetingStatus;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000D657E File Offset: 0x000D477E
		protected internal override string DescriptionTag
		{
			get
			{
				return this.delegateWriter.DescriptionTag;
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000D658B File Offset: 0x000D478B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PrintPageWriter>(this);
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x000D6593 File Offset: 0x000D4793
		public override bool ShouldRenderReminder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001996 RID: 6550
		private MeetingPageWriter delegateWriter;
	}
}
