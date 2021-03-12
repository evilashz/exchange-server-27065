using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003E8 RID: 1000
	internal abstract class PrintCalendarVisual
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x000D43A0 File Offset: 0x000D25A0
		public static void RenderBackground(TextWriter writer, string cssClass)
		{
			writer.Write("<div class=\"visualBack\">");
			writer.Write("<div class=\"visualBackInner ");
			writer.Write(cssClass);
			writer.Write("\"></div>");
			writer.Write("</div>");
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x000D43D5 File Offset: 0x000D25D5
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x000D43DD File Offset: 0x000D25DD
		internal ISessionContext SessionContext { get; private set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000D43E6 File Offset: 0x000D25E6
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x000D43EE File Offset: 0x000D25EE
		internal double Left { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000D43F7 File Offset: 0x000D25F7
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x000D43FF File Offset: 0x000D25FF
		internal double Top { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000D4408 File Offset: 0x000D2608
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x000D4410 File Offset: 0x000D2610
		internal double Width { get; set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000D4419 File Offset: 0x000D2619
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x000D4421 File Offset: 0x000D2621
		internal double Height { get; set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x000D442A File Offset: 0x000D262A
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x000D4432 File Offset: 0x000D2632
		internal BusyTypeWrapper BusyType { get; private set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x000D443B File Offset: 0x000D263B
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x000D4443 File Offset: 0x000D2643
		internal string Subject { get; private set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x000D444C File Offset: 0x000D264C
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x000D4454 File Offset: 0x000D2654
		internal string Location { get; private set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000D445D File Offset: 0x000D265D
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x000D4465 File Offset: 0x000D2665
		internal string Organizer { get; private set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000D446E File Offset: 0x000D266E
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x000D4476 File Offset: 0x000D2676
		internal bool IsPrivate { get; private set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x000D447F File Offset: 0x000D267F
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x000D4487 File Offset: 0x000D2687
		internal bool HasAttachment { get; private set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x000D4490 File Offset: 0x000D2690
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x000D4498 File Offset: 0x000D2698
		internal CalendarItemTypeWrapper RecurrenceType { get; private set; }

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000D44A1 File Offset: 0x000D26A1
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x000D44A9 File Offset: 0x000D26A9
		internal string CssClass { get; private set; }

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x000D44B2 File Offset: 0x000D26B2
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x000D44BA File Offset: 0x000D26BA
		internal ExDateTime StartTime { get; private set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x000D44C3 File Offset: 0x000D26C3
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x000D44CB File Offset: 0x000D26CB
		internal ExDateTime EndTime { get; private set; }

		// Token: 0x060024A0 RID: 9376 RVA: 0x000D44D4 File Offset: 0x000D26D4
		public PrintCalendarVisual(ISessionContext sessionContext, CalendarVisual visual, ICalendarDataSource dataSource) : this(sessionContext, visual.Rect.X, visual.Rect.Y, visual.Rect.Width, visual.Rect.Height, visual.DataIndex, dataSource)
		{
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000D4510 File Offset: 0x000D2710
		public PrintCalendarVisual(ISessionContext sessionContext, double left, double top, double width, double height, int index, ICalendarDataSource dataSource)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.SessionContext = sessionContext;
			this.Left = left;
			this.Top = top;
			this.Width = width;
			this.Height = height;
			this.BusyType = dataSource.GetWrappedBusyType(index);
			this.IsPrivate = dataSource.IsPrivate(index);
			if (this.IsPrivate && dataSource.SharedType != SharedType.None)
			{
				this.Subject = LocalizedStrings.GetNonEncoded(840767634);
			}
			else
			{
				this.Subject = dataSource.GetSubject(index);
				this.Location = dataSource.GetLocation(index);
				this.Organizer = (dataSource.IsMeeting(index) ? dataSource.GetOrganizerDisplayName(index) : null);
			}
			this.HasAttachment = dataSource.HasAttachment(index);
			this.RecurrenceType = dataSource.GetWrappedItemType(index);
			this.CssClass = dataSource.GetCssClassName(index);
			this.StartTime = dataSource.GetStartTime(index);
			this.EndTime = dataSource.GetEndTime(index);
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000D462C File Offset: 0x000D282C
		protected virtual string TimeDescription
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000D4641 File Offset: 0x000D2841
		protected bool IsDarkBackground
		{
			get
			{
				return this.CssClass != null && Array.Exists<string>(PrintCalendarVisual.DarkClass, (string v) => v == this.CssClass);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000D4664 File Offset: 0x000D2864
		public virtual void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div class=\"calendarVisual ");
			writer.Write(this.CssClass);
			writer.Write("\" style=\"");
			this.RenderVisualPosition(writer);
			writer.Write("\">");
			this.RenderBackground(writer);
			if (this.BusyType != BusyTypeWrapper.Busy)
			{
				this.RenderFreeBusy(writer, false);
			}
			writer.Write("<table class=\"visualTable\"><tr>");
			if (this.BusyType != BusyTypeWrapper.Busy)
			{
				writer.Write("<td class=\"freeBusy\"></td>");
			}
			writer.Write("<td><div class=\"visualBody ");
			this.RenderExtraClasses(writer);
			writer.Write("\"><div class=\"textContainer\">");
			this.RenderVisualContent(writer);
			writer.Write("</div>");
			writer.Write("</div>");
			writer.Write("</td></tr></table></div>");
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000D472F File Offset: 0x000D292F
		protected virtual void RenderExtraClasses(TextWriter writer)
		{
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000D4731 File Offset: 0x000D2931
		protected virtual void RenderVisualPosition(TextWriter writer)
		{
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000D4734 File Offset: 0x000D2934
		protected void RenderVisualContent(TextWriter writer)
		{
			this.RenderStringArea(writer, this.TimeDescription, "subject");
			this.RenderIcons(writer);
			this.RenderStringArea(writer, this.Subject, "subject");
			this.RenderStringArea(writer, this.Location, "location");
			this.RenderStringArea(writer, this.Organizer, "organizer");
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000D4790 File Offset: 0x000D2990
		protected void RenderStringArea(TextWriter writer, string text, string cssClass)
		{
			if (!string.IsNullOrEmpty(text))
			{
				writer.Write("<span class=\"");
				writer.Write(cssClass);
				writer.Write(" visualText\">");
				writer.Write(text);
				writer.Write(" ");
				writer.Write("</span>");
				writer.Write(this.SessionContext.GetDirectionMark());
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000D47F0 File Offset: 0x000D29F0
		protected void RenderIcons(TextWriter writer)
		{
			this.RenderIcons(writer, false);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000D47FC File Offset: 0x000D29FC
		protected void RenderIcons(TextWriter writer, bool noBackground)
		{
			if (this.RecurrenceType == CalendarItemTypeWrapper.Occurrence)
			{
				this.SessionContext.RenderThemeImage(writer, (this.IsDarkBackground && !noBackground) ? ThemeFileId.PrintRecurringAppointmentWhite : ThemeFileId.PrintRecurringAppointment, "imgItemType", new object[0]);
			}
			else if (this.RecurrenceType == CalendarItemTypeWrapper.Exception)
			{
				this.SessionContext.RenderThemeImage(writer, (this.IsDarkBackground && !noBackground) ? ThemeFileId.PrintExceptionWhite : ThemeFileId.PrintException, "imgItemType", new object[0]);
			}
			if (this.IsPrivate)
			{
				this.SessionContext.RenderThemeImage(writer, (this.IsDarkBackground && !noBackground) ? ThemeFileId.PrintPrivateWhite : ThemeFileId.PrintPrivate, "imgPrivate", new object[0]);
			}
			if (this.HasAttachment)
			{
				this.SessionContext.RenderThemeImage(writer, (this.IsDarkBackground && !noBackground) ? ThemeFileId.PrintAttachment3White : ThemeFileId.PrintAttachment3, "imgAttachment", new object[0]);
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000D48D0 File Offset: 0x000D2AD0
		protected void RenderFreeBusy(TextWriter writer, bool fixHeight)
		{
			writer.Write("<div class=\"freeBusyContainer");
			if (fixHeight)
			{
				writer.Write(" fixHeightFB");
			}
			writer.Write("\">");
			switch (this.BusyType)
			{
			case BusyTypeWrapper.Free:
				PrintCalendarVisual.RenderBackground(writer, "free");
				break;
			case BusyTypeWrapper.Tentative:
				this.SessionContext.RenderThemeImage(writer, fixHeight ? ThemeFileId.PrintTentativeForAgenda : ThemeFileId.PrintTentative);
				break;
			case BusyTypeWrapper.Busy:
				PrintCalendarVisual.RenderBackground(writer, "busy");
				break;
			case BusyTypeWrapper.OOF:
				PrintCalendarVisual.RenderBackground(writer, "oof");
				break;
			}
			writer.Write("</div>");
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000D496D File Offset: 0x000D2B6D
		protected void RenderBackground(TextWriter writer)
		{
			PrintCalendarVisual.RenderBackground(writer, this.CssClass);
		}

		// Token: 0x04001969 RID: 6505
		private static readonly string[] DarkClass = new string[]
		{
			"cat0",
			"cat1",
			"cat4",
			"cat5",
			"cat6",
			"cat7",
			"cat8",
			"cat9",
			"cat11",
			"cat12",
			"cat13",
			"cat14",
			"cat15",
			"cat16",
			"cat17",
			"cat18",
			"cat19",
			"cat20",
			"cat21",
			"cat22",
			"cat23",
			"cat24"
		};
	}
}
