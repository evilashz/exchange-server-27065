using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200032F RID: 815
	internal abstract class CalendarViewPayloadWriter
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000B1A3B File Offset: 0x000AFC3B
		protected CalendarViewPayloadWriter(ISessionContext sessionContext, TextWriter output)
		{
			this.sessionContext = sessionContext;
			this.output = output;
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x000B1A51 File Offset: 0x000AFC51
		protected ISessionContext SessionContext
		{
			get
			{
				return this.sessionContext;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x000B1A59 File Offset: 0x000AFC59
		protected TextWriter Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x000B1A61 File Offset: 0x000AFC61
		protected Hashtable ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x000B1A69 File Offset: 0x000AFC69
		protected int SelectedItemIndex
		{
			get
			{
				return this.selectedItemIndex;
			}
		}

		// Token: 0x06001EFB RID: 7931
		public abstract void Render(int viewWidth, CalendarViewType viewType, ReadingPanePosition readingPanePosition, ReadingPanePosition requestReadingPanePosition);

		// Token: 0x06001EFC RID: 7932 RVA: 0x000B1A74 File Offset: 0x000AFC74
		private void RenderValue(TextWriter writer, string name, object value, bool addQuote)
		{
			writer.Write("\"");
			writer.Write(name);
			writer.Write("\":");
			if (addQuote)
			{
				writer.Write("\"");
			}
			writer.Write(value);
			if (addQuote)
			{
				writer.Write("\"");
			}
			writer.Write(",");
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000B1AD0 File Offset: 0x000AFCD0
		private void RenderValue(TextWriter writer, string name, string value, bool addQuote)
		{
			this.RenderValue(writer, name, value, addQuote);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000B1AEC File Offset: 0x000AFCEC
		private void RenderValue(TextWriter writer, string name, SanitizedHtmlString value, bool addQuote)
		{
			this.RenderValue(writer, name, value, addQuote);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000B1B06 File Offset: 0x000AFD06
		protected void RenderValue(TextWriter writer, string name, string value)
		{
			this.RenderValue(writer, name, Utilities.JavascriptEncode(value), true);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000B1B17 File Offset: 0x000AFD17
		protected void RenderValue(TextWriter writer, string name, SanitizedHtmlString value)
		{
			this.RenderValue(writer, name, Utilities.JavascriptEncode(value), true);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000B1B28 File Offset: 0x000AFD28
		protected void RenderValue(TextWriter writer, string name, int value)
		{
			this.RenderValue(writer, name, Convert.ToString(value, CultureInfo.InvariantCulture), false);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000B1B3E File Offset: 0x000AFD3E
		protected void RenderValue(TextWriter writer, string name, bool value)
		{
			this.RenderValue(writer, name, value ? 1 : 0);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000B1B4F File Offset: 0x000AFD4F
		protected void RenderValue(TextWriter writer, string name, double value, string format)
		{
			this.RenderValue(writer, name, value.ToString(format, CultureInfo.InvariantCulture), false);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000B1B68 File Offset: 0x000AFD68
		protected void RenderCalendarProperties(CalendarViewBase view)
		{
			CalendarAdapterBase calendarAdapter = view.CalendarAdapter;
			this.RenderValue(this.Output, "sFId", calendarAdapter.IdentityString);
			this.RenderValue(this.Output, "fHRtL", calendarAdapter.UserCanReadItem);
			this.RenderValue(this.Output, "Title", SanitizedHtmlString.GetSanitizedStringWithoutEncoding(calendarAdapter.CalendarTitle));
			this.RenderValue(this.Output, "sDD", view.FolderDateDescription);
			this.RenderValue(this.Output, "fCC", calendarAdapter.DataSource.UserCanCreateItem);
			CalendarAdapter calendarAdapter2 = calendarAdapter as CalendarAdapter;
			this.RenderValue(this.Output, "iSharedType", (int)calendarAdapter.DataSource.SharedType);
			if (calendarAdapter2 != null)
			{
				if (calendarAdapter2.PromotedFolderId != null)
				{
					this.RenderValue(this.Output, "sPromotedFolderId", calendarAdapter2.PromotedFolderId.ToBase64String());
				}
				this.RenderValue(this.Output, "sLegacyDN", calendarAdapter2.LegacyDN);
				if (calendarAdapter2.DataSource.SharedType == SharedType.InternalFreeBusy)
				{
					this.RenderValue(this.Output, "sCalendarOwnerDisplayName", calendarAdapter2.CalendarOwnerDisplayName);
				}
				this.RenderValue(this.Output, "iOlderExchangeCalendarType", (int)calendarAdapter2.OlderExchangeSharedCalendarType);
				this.RenderColor(calendarAdapter2);
				this.RenderValue(this.Output, "fPublishedOut", calendarAdapter2.IsPublishedOut);
				if (calendarAdapter2.IsExternalSharedInFolder)
				{
					if (calendarAdapter2.LastAttemptTime != ExDateTime.MinValue)
					{
						this.RenderValue(this.Output, "dtSyncTime", calendarAdapter2.LastAttemptTime.ToString("g", this.SessionContext.UserCulture));
					}
					if (calendarAdapter2.LastSuccessSyncTime != ExDateTime.MinValue)
					{
						this.RenderValue(this.Output, "dtSuccessSyncTime", calendarAdapter2.LastSuccessSyncTime.ToString("g", this.SessionContext.UserCulture));
					}
				}
				this.RenderValue(this.Output, "fArchive", calendarAdapter2.IsInArchiveMailbox);
				if (calendarAdapter2.DataSource.SharedType == SharedType.WebCalendar)
				{
					this.RenderValue(this.Output, "sWebCalUrl", calendarAdapter2.WebCalendarUrl);
					return;
				}
			}
			else if (calendarAdapter is PublishedCalendarAdapter)
			{
				PublishedCalendarAdapter publishedCalendarAdapter = (PublishedCalendarAdapter)calendarAdapter;
				this.RenderValue(this.Output, "sPublishRange", SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1428371010), new object[]
				{
					publishedCalendarAdapter.PublishedFromDateTime.ToShortDateString(),
					publishedCalendarAdapter.PublishedToDateTime.ToShortDateString()
				}));
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000B1DE4 File Offset: 0x000AFFE4
		protected void RenderEmptyCalendar(CalendarAdapterBase calendarAdapterBase, int index)
		{
			if (index > 0)
			{
				this.Output.Write(",");
			}
			this.Output.Write("{");
			this.RenderValue(this.Output, "sFId", calendarAdapterBase.IdentityString);
			if (calendarAdapterBase is CalendarAdapter)
			{
				CalendarAdapter calendarAdapter = (CalendarAdapter)calendarAdapterBase;
				this.RenderValue(this.Output, "iOlderExchangeCalendarType", (int)calendarAdapter.OlderExchangeSharedCalendarType);
			}
			this.Output.Write("\"fHRtL\": 0}");
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000B1E64 File Offset: 0x000B0064
		protected void RenderCalendars(CalendarAdapterBase[] adapters, ExDateTime[] days, CalendarViewPayloadWriter.RenderCalendarDelegate render)
		{
			for (int i = 0; i < adapters.Length; i++)
			{
				if (adapters[i].UserCanReadItem)
				{
					render(adapters[i], i);
				}
				else
				{
					this.RenderEmptyCalendar(adapters[i], i);
				}
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000B1EA0 File Offset: 0x000B00A0
		protected void RenderDay(TextWriter writer, DateRange[] ranges, CalendarViewType viewType)
		{
			string format = DateTimeUtilities.GetDaysFormat(this.SessionContext.DateFormat) ?? "%d";
			string format2;
			if (CalendarUtilities.FullMonthNameRequired(this.SessionContext.UserCulture))
			{
				format2 = "MMMM";
			}
			else
			{
				format2 = "MMM";
			}
			for (int i = 0; i < ranges.Length; i++)
			{
				if (i > 0)
				{
					writer.Write(",");
				}
				ExDateTime start = ranges[i].Start;
				writer.Write("new Day(\"");
				if (start.Day == 1 && viewType == CalendarViewType.Monthly)
				{
					writer.Write("<span class='divMonthlyViewMonthName'>");
					writer.Write(Utilities.JavascriptEncode(start.ToString(format2, this.SessionContext.UserCulture)));
					writer.Write("</span>&nbsp;");
				}
				writer.Write(Utilities.JavascriptEncode(start.ToString(format, this.SessionContext.UserCulture)));
				writer.Write("\",\"");
				if (viewType != CalendarViewType.Monthly)
				{
					writer.Write(Utilities.JavascriptEncode(start.ToString("dddd", this.SessionContext.UserCulture)));
				}
				writer.Write("\",");
				writer.Write(start.Day);
				writer.Write(",");
				writer.Write(start.Month);
				writer.Write(",");
				writer.Write(start.Year);
				writer.Write(DateTimeUtilities.IsToday(start) ? ",1" : ",0");
				writer.Write(")");
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000B201C File Offset: 0x000B021C
		private void RenderPrivateAppointmentData(ExDateTime itemStart, ExDateTime itemEnd)
		{
			this.output.Write("new Item(");
			this.output.Write("-1,");
			this.output.Write(" 0,");
			this.output.Write(" 0,\"");
			this.output.Write(DateTimeUtilities.GetJavascriptDate(itemStart));
			this.output.Write("\",\"");
			this.output.Write(DateTimeUtilities.GetJavascriptDate(itemEnd));
			this.output.Write("\",\"");
			this.output.Write(LocalizedStrings.GetJavascriptEncoded(840767634));
			this.output.Write("\",");
			this.output.Write(" \"\",");
			this.output.Write(" 2,");
			this.output.Write(" \"\",");
			this.output.Write(" 0,");
			this.output.Write(" 0,");
			this.output.Write(" 0,");
			this.output.Write(" \"\",");
			this.output.Write(" 1,");
			this.output.Write(" 1,");
			this.output.Write(" 0,");
			this.output.Write(" \"noClrCal\")");
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000B2180 File Offset: 0x000B0380
		private void RenderAppointmentData(CalendarViewBase view, int i, ExDateTime itemStart, ExDateTime itemEnd)
		{
			ICalendarDataSource dataSource = view.DataSource;
			CalendarItemTypeWrapper wrappedItemType = dataSource.GetWrappedItemType(i);
			this.output.Write("new Item(\"");
			OwaStoreObjectId itemId = dataSource.GetItemId(i);
			string changeKey = dataSource.GetChangeKey(i);
			PublishedCalendarDataSource publishedCalendarDataSource = dataSource as PublishedCalendarDataSource;
			if (publishedCalendarDataSource != null && publishedCalendarDataSource.DetailLevel != DetailLevelEnumType.AvailabilityOnly)
			{
				StoreObjectId itemStoreObjectId = publishedCalendarDataSource.GetItemStoreObjectId(i);
				Utilities.JavascriptEncode(itemStoreObjectId.ToString(), this.output);
				this.output.Write("\",\"");
				if (this.IsOneOfRecurrence(wrappedItemType))
				{
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(itemStoreObjectId.ProviderLevelItemId);
					Utilities.JavascriptEncode(storeObjectId.ToString(), this.output);
				}
				else
				{
					this.output.Write("0");
				}
			}
			else
			{
				if (itemId != null)
				{
					Utilities.JavascriptEncode(itemId.ToString(), this.output);
				}
				this.output.Write("\",\"");
				if (itemId != null && this.IsOneOfRecurrence(wrappedItemType))
				{
					OwaStoreObjectId providerLevelItemId = itemId.ProviderLevelItemId;
					Utilities.JavascriptEncode(providerLevelItemId.ToString(), this.output);
				}
				else
				{
					this.output.Write("0");
				}
			}
			this.output.Write("\",\"");
			if (changeKey != null)
			{
				Utilities.JavascriptEncode(changeKey, this.output);
			}
			this.output.Write("\",\"");
			this.output.Write(DateTimeUtilities.GetJavascriptDate(itemStart));
			this.output.Write("\",\"");
			this.output.Write(DateTimeUtilities.GetJavascriptDate(itemEnd));
			this.output.Write("\",\"");
			Utilities.JavascriptEncode(dataSource.GetSubject(i), this.output);
			this.output.Write("\",\"");
			Utilities.JavascriptEncode(dataSource.GetLocation(i), this.output);
			BusyTypeWrapper wrappedBusyType = dataSource.GetWrappedBusyType(i);
			this.output.Write("\",");
			this.output.Write((int)wrappedBusyType);
			this.output.Write(",\"");
			if (itemId != null)
			{
				Utilities.JavascriptEncode(ObjectClass.GetContainerMessageClass(itemId.StoreObjectType), this.output);
			}
			this.output.Write("\"");
			bool flag = dataSource.IsMeeting(i);
			this.output.Write(flag ? ",1" : ",0");
			this.output.Write(dataSource.IsCancelled(i) ? ",1" : ",0");
			bool flag2 = dataSource.IsOrganizer(i);
			this.output.Write(flag2 ? ",1" : ",0");
			this.output.Write(",\"");
			if (flag)
			{
				Utilities.JavascriptEncode(dataSource.GetOrganizerDisplayName(i), this.output);
			}
			this.output.Write("\"");
			bool flag3 = dataSource.IsPrivate(i);
			this.output.Write(flag3 ? ",1," : ",0,");
			this.output.Write((int)wrappedItemType);
			this.output.Write(dataSource.HasAttachment(i) ? ",1" : ",0");
			this.output.Write(",\"");
			this.output.Write(dataSource.GetCssClassName(i));
			this.output.Write("\"");
			this.output.Write(")");
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000B24CC File Offset: 0x000B06CC
		private bool IsOneOfRecurrence(CalendarItemTypeWrapper calendarItemType)
		{
			return calendarItemType == CalendarItemTypeWrapper.Occurrence || calendarItemType == CalendarItemTypeWrapper.Exception;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000B24D8 File Offset: 0x000B06D8
		protected void RenderData(CalendarViewBase view, OwaStoreObjectId selectedItemId)
		{
			bool flag = true;
			int num = 0;
			if (view.RemovedItemCount > 0)
			{
				this.itemIndex = new Hashtable();
			}
			else
			{
				this.itemIndex = null;
			}
			this.selectedItemIndex = -1;
			TimeSpan t = TimeSpan.MinValue;
			PositionInTime positionInTime = PositionInTime.None;
			int num2 = 0;
			for (int i = 0; i < view.DataSource.Count; i++)
			{
				if (!view.IsItemRemoved(i))
				{
					bool flag2 = false;
					OwaStoreObjectId itemId = view.DataSource.GetItemId(i);
					if (view.DataSource.SharedType != SharedType.None)
					{
						flag2 = view.DataSource.IsPrivate(i);
					}
					if (!flag)
					{
						this.output.Write(",");
					}
					flag = false;
					num2++;
					if (this.itemIndex != null)
					{
						this.itemIndex[i] = num;
						num++;
					}
					ExDateTime startTime = view.DataSource.GetStartTime(i);
					ExDateTime endTime = view.DataSource.GetEndTime(i);
					if (flag2)
					{
						this.RenderPrivateAppointmentData(startTime, endTime);
					}
					else
					{
						this.RenderAppointmentData(view, i, startTime, endTime);
					}
					if (!flag2)
					{
						if (selectedItemId != null)
						{
							if (selectedItemId.Equals(itemId))
							{
								this.selectedItemIndex = ((this.itemIndex != null) ? ((int)this.itemIndex[i]) : i);
							}
						}
						else
						{
							bool flag3 = false;
							TimeSpan timeSpan = TimeSpan.MinValue;
							ExDateTime localTime = DateTimeUtilities.GetLocalTime();
							PositionInTime positionInTime2;
							if (endTime < localTime)
							{
								positionInTime2 = PositionInTime.Past;
							}
							else if (startTime > localTime)
							{
								positionInTime2 = PositionInTime.Future;
							}
							else
							{
								positionInTime2 = PositionInTime.Present;
							}
							if (positionInTime2 == PositionInTime.Past)
							{
								timeSpan = localTime - endTime;
								if (positionInTime == PositionInTime.Past)
								{
									if (timeSpan < t)
									{
										flag3 = true;
									}
								}
								else if (positionInTime == PositionInTime.None)
								{
									flag3 = true;
								}
							}
							else if (positionInTime2 == PositionInTime.Present)
							{
								timeSpan = endTime - localTime;
								if (positionInTime == PositionInTime.Present)
								{
									if (timeSpan < t)
									{
										flag3 = true;
									}
								}
								else
								{
									flag3 = true;
								}
							}
							else if (positionInTime2 == PositionInTime.Future)
							{
								timeSpan = startTime - localTime;
								if (positionInTime == PositionInTime.Future)
								{
									timeSpan = startTime - localTime;
									if (timeSpan < t)
									{
										flag3 = true;
									}
								}
								else if (positionInTime == PositionInTime.Past || positionInTime == PositionInTime.None)
								{
									flag3 = true;
								}
							}
							if (flag3)
							{
								this.selectedItemIndex = ((this.itemIndex != null) ? ((int)this.itemIndex[i]) : i);
								t = timeSpan;
								positionInTime = positionInTime2;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000B2724 File Offset: 0x000B0924
		protected void RenderEventAreaVisual(int idx, CalendarViewBase view, CalendarVisualContainer eventArea)
		{
			bool flag = true;
			for (int i = 0; i < eventArea.Count; i++)
			{
				EventAreaVisual eventAreaVisual = (EventAreaVisual)eventArea[i];
				if (!view.IsItemRemoved(eventAreaVisual.DataIndex))
				{
					if (!flag)
					{
						this.output.Write(",");
					}
					flag = false;
					int num = 0;
					if (eventAreaVisual.LeftBreak)
					{
						num |= 1;
					}
					if (eventAreaVisual.RightBreak)
					{
						num |= 2;
					}
					this.output.Write("new VisData(");
					this.Output.Write(idx);
					this.Output.Write(",");
					int num2 = (this.itemIndex != null) ? ((int)this.itemIndex[eventAreaVisual.DataIndex]) : eventAreaVisual.DataIndex;
					this.output.Write(num2);
					this.output.Write(",");
					this.output.Write((int)eventAreaVisual.Rect.X);
					this.output.Write(",");
					this.output.Write((int)eventAreaVisual.Rect.Y);
					this.output.Write(",");
					this.output.Write((int)eventAreaVisual.Rect.Width);
					this.output.Write(",");
					this.output.Write("0");
					this.output.Write(",");
					this.output.Write(num);
					this.output.Write(",");
					this.output.Write(eventAreaVisual.InnerBreaks);
					this.output.Write(",");
					if (num2 == this.selectedItemIndex)
					{
						this.output.Write("1");
					}
					else
					{
						this.output.Write("0");
					}
					this.output.Write(",-1");
					this.output.Write(")");
				}
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000B292C File Offset: 0x000B0B2C
		protected void RenderColor(CalendarAdapter calendarAdapter)
		{
			int num = -2;
			if (this.sessionContext.CanActAsOwner)
			{
				num = calendarAdapter.CalendarColor;
				if (!CalendarColorManager.IsColorIndexValid(num))
				{
					num = -2;
				}
			}
			this.RenderValue(this.Output, "iClr", CalendarColorManager.GetClientColorIndex(num));
		}

		// Token: 0x040016A3 RID: 5795
		private readonly ISessionContext sessionContext;

		// Token: 0x040016A4 RID: 5796
		private readonly TextWriter output;

		// Token: 0x040016A5 RID: 5797
		private int selectedItemIndex;

		// Token: 0x040016A6 RID: 5798
		private Hashtable itemIndex;

		// Token: 0x02000330 RID: 816
		// (Invoke) Token: 0x06001F0F RID: 7951
		protected delegate void RenderCalendarDelegate(CalendarAdapterBase CalendarAdapter, int index);
	}
}
