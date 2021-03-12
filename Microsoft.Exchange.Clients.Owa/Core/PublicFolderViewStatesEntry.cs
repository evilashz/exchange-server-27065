using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000228 RID: 552
	public class PublicFolderViewStatesEntry
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x00071217 File Offset: 0x0006F417
		internal PublicFolderViewStatesEntry(string folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			this.folderId = folderId;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00071234 File Offset: 0x0006F434
		internal PublicFolderViewStatesEntry()
		{
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0007123C File Offset: 0x0006F43C
		internal bool Dirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00071244 File Offset: 0x0006F444
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x0007124C File Offset: 0x0006F44C
		internal string FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00071255 File Offset: 0x0006F455
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0007125D File Offset: 0x0006F45D
		internal long LastAccessedDateTimeTicks
		{
			get
			{
				return this.lastAccessedDateTimeTicks;
			}
			set
			{
				this.lastAccessedDateTimeTicks = value;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00071268 File Offset: 0x0006F468
		internal void UpdateLastAccessedDateTimeTicks()
		{
			this.lastAccessedDateTimeTicks = ExDateTime.UtcNow.UtcTicks;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00071288 File Offset: 0x0006F488
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000712A4 File Offset: 0x0006F4A4
		internal int CalendarViewType
		{
			get
			{
				if (this.calendarViewType != null)
				{
					return this.calendarViewType.Value;
				}
				return 1;
			}
			set
			{
				this.calendarViewType = new int?(value);
				this.dirty = true;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000712B9 File Offset: 0x0006F4B9
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000712D5 File Offset: 0x0006F4D5
		internal int DailyViewDays
		{
			get
			{
				if (this.dailyViewDays != null)
				{
					return this.dailyViewDays.Value;
				}
				return 1;
			}
			set
			{
				this.dailyViewDays = new int?(value);
				this.dirty = true;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000712EA File Offset: 0x0006F4EA
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000712F2 File Offset: 0x0006F4F2
		internal bool? MultiLine
		{
			get
			{
				return this.multiLine;
			}
			set
			{
				this.multiLine = value;
				this.dirty = true;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00071302 File Offset: 0x0006F502
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0007130A File Offset: 0x0006F50A
		internal int? ReadingPanePosition
		{
			get
			{
				return this.readingPanePosition;
			}
			set
			{
				this.readingPanePosition = value;
				this.dirty = true;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0007131A File Offset: 0x0006F51A
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00071336 File Offset: 0x0006F536
		internal int ReadingPanePositionMultiDay
		{
			get
			{
				if (this.readingPanePositionMultiDay != null)
				{
					return this.readingPanePositionMultiDay.Value;
				}
				return 0;
			}
			set
			{
				this.readingPanePositionMultiDay = new int?(value);
				this.dirty = true;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0007134B File Offset: 0x0006F54B
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00071353 File Offset: 0x0006F553
		internal string SortColumn
		{
			get
			{
				return this.sortColumn;
			}
			set
			{
				this.sortColumn = value;
				this.dirty = true;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00071363 File Offset: 0x0006F563
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0007136B File Offset: 0x0006F56B
		internal int? SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				this.sortOrder = value;
				this.dirty = true;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0007137B File Offset: 0x0006F57B
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00071383 File Offset: 0x0006F583
		internal int? ViewHeight
		{
			get
			{
				return this.viewHeight;
			}
			set
			{
				this.viewHeight = value;
				this.dirty = true;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00071393 File Offset: 0x0006F593
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x0007139B File Offset: 0x0006F59B
		internal int? ViewWidth
		{
			get
			{
				return this.viewWidth;
			}
			set
			{
				this.viewWidth = value;
				this.dirty = true;
			}
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000713AC File Offset: 0x0006F5AC
		internal void RenderEntryXml(XmlTextWriter xmlWriter, string entryName)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			xmlWriter.WriteStartElement(entryName);
			xmlWriter.WriteAttributeString("folderId", this.folderId);
			xmlWriter.WriteAttributeString("lastAccessedDateTimeTicks", this.lastAccessedDateTimeTicks.ToString());
			if (this.calendarViewType != null)
			{
				xmlWriter.WriteAttributeString("calendarViewType", this.calendarViewType.Value.ToString());
			}
			if (this.dailyViewDays != null)
			{
				xmlWriter.WriteAttributeString("dailyViewDays", this.dailyViewDays.Value.ToString());
			}
			if (this.multiLine != null)
			{
				xmlWriter.WriteAttributeString("multiLine", this.multiLine.Value.ToString());
			}
			if (this.readingPanePosition != null)
			{
				xmlWriter.WriteAttributeString("readingPanePosition", this.readingPanePosition.Value.ToString());
			}
			if (this.readingPanePositionMultiDay != null)
			{
				xmlWriter.WriteAttributeString("readingPanePositionMultiDay", this.readingPanePositionMultiDay.Value.ToString());
			}
			if (!string.IsNullOrEmpty(this.sortColumn))
			{
				xmlWriter.WriteAttributeString("sortColumn", this.sortColumn);
			}
			if (this.sortOrder != null)
			{
				xmlWriter.WriteAttributeString("sortOrder", this.sortOrder.Value.ToString());
			}
			if (this.viewHeight != null)
			{
				xmlWriter.WriteAttributeString("viewHeight", this.viewHeight.Value.ToString());
			}
			if (this.viewWidth != null)
			{
				xmlWriter.WriteAttributeString("viewWidth", this.viewWidth.Value.ToString());
			}
			xmlWriter.WriteFullEndElement();
			this.dirty = false;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0007158C File Offset: 0x0006F78C
		internal void ParseEntry(XmlTextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentException("reader cannot be null");
			}
			try
			{
				if (reader.HasAttributes)
				{
					int i = 0;
					while (i < reader.AttributeCount)
					{
						reader.MoveToAttribute(i);
						string name;
						if ((name = reader.Name) == null)
						{
							goto IL_23A;
						}
						if (<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6001267-1 == null)
						{
							<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6001267-1 = new Dictionary<string, int>(11)
							{
								{
									"folderId",
									0
								},
								{
									"lastAccessedDateTimeTicks",
									1
								},
								{
									"calendarViewType",
									2
								},
								{
									"dailyViewDays",
									3
								},
								{
									"multiLine",
									4
								},
								{
									"readingPanePosition",
									5
								},
								{
									"readingPanePositionMultiDay",
									6
								},
								{
									"sortColumn",
									7
								},
								{
									"sortOrder",
									8
								},
								{
									"viewHeight",
									9
								},
								{
									"viewWidth",
									10
								}
							};
						}
						int num;
						if (!<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6001267-1.TryGetValue(name, out num))
						{
							goto IL_23A;
						}
						switch (num)
						{
						case 0:
							this.folderId = reader.Value;
							break;
						case 1:
							this.lastAccessedDateTimeTicks = long.Parse(reader.Value);
							break;
						case 2:
						{
							int num2 = int.Parse(reader.Value);
							if (FolderViewStates.ValidateCalendarViewType((CalendarViewType)num2))
							{
								this.calendarViewType = new int?(num2);
							}
							break;
						}
						case 3:
						{
							int num2 = int.Parse(reader.Value);
							if (num2 > 7)
							{
								num2 = 7;
							}
							else if (num2 < 1)
							{
								num2 = 1;
							}
							this.dailyViewDays = new int?(num2);
							break;
						}
						case 4:
							this.multiLine = new bool?(Convert.ToBoolean(reader.Value));
							break;
						case 5:
							this.readingPanePosition = new int?(int.Parse(reader.Value));
							break;
						case 6:
							this.readingPanePositionMultiDay = new int?(int.Parse(reader.Value));
							break;
						case 7:
							this.sortColumn = reader.Value;
							break;
						case 8:
							this.sortOrder = new int?(int.Parse(reader.Value));
							break;
						case 9:
							this.viewHeight = new int?(int.Parse(reader.Value));
							break;
						case 10:
							this.viewWidth = new int?(int.Parse(reader.Value));
							break;
						default:
							goto IL_23A;
						}
						IL_240:
						i++;
						continue;
						IL_23A:
						PublicFolderViewStatesEntry.ThrowParserException(reader);
						goto IL_240;
					}
					reader.MoveToElement();
				}
			}
			catch (FormatException)
			{
				PublicFolderViewStatesEntry.ThrowParserException(reader);
			}
			catch (OverflowException)
			{
				PublicFolderViewStatesEntry.ThrowParserException(reader);
			}
			if (this.folderId == null)
			{
				PublicFolderViewStatesEntry.ThrowParserException(reader);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00071848 File Offset: 0x0006FA48
		private static void ThrowParserException(XmlTextReader reader)
		{
			throw new OwaPublicFolderViewStatesParseException(string.Format(CultureInfo.InvariantCulture, "Invalid request. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				"Parsing PublicFolderViewStates' Entry"
			}), null, null);
		}

		// Token: 0x04000CBC RID: 3260
		private const string LastAccessedDateTimeTicksAttribute = "lastAccessedDateTimeTicks";

		// Token: 0x04000CBD RID: 3261
		private const string FolderIdAttribute = "folderId";

		// Token: 0x04000CBE RID: 3262
		private const string CalendarViewTypeAttribute = "calendarViewType";

		// Token: 0x04000CBF RID: 3263
		private const string DailyViewDaysAttribute = "dailyViewDays";

		// Token: 0x04000CC0 RID: 3264
		private const string MultiLineAttribute = "multiLine";

		// Token: 0x04000CC1 RID: 3265
		private const string ReadingPanePositionAttribute = "readingPanePosition";

		// Token: 0x04000CC2 RID: 3266
		private const string ReadingPanePositionMultiDayAttribute = "readingPanePositionMultiDay";

		// Token: 0x04000CC3 RID: 3267
		private const string SortColumnAttribute = "sortColumn";

		// Token: 0x04000CC4 RID: 3268
		private const string SortOrderAttribute = "sortOrder";

		// Token: 0x04000CC5 RID: 3269
		private const string ViewHeightAttribute = "viewHeight";

		// Token: 0x04000CC6 RID: 3270
		private const string ViewWidthAttribute = "viewWidth";

		// Token: 0x04000CC7 RID: 3271
		private long lastAccessedDateTimeTicks;

		// Token: 0x04000CC8 RID: 3272
		private string folderId;

		// Token: 0x04000CC9 RID: 3273
		private bool dirty;

		// Token: 0x04000CCA RID: 3274
		private int? calendarViewType;

		// Token: 0x04000CCB RID: 3275
		private int? dailyViewDays;

		// Token: 0x04000CCC RID: 3276
		private bool? multiLine;

		// Token: 0x04000CCD RID: 3277
		private int? readingPanePosition;

		// Token: 0x04000CCE RID: 3278
		private int? readingPanePositionMultiDay;

		// Token: 0x04000CCF RID: 3279
		private string sortColumn;

		// Token: 0x04000CD0 RID: 3280
		private int? sortOrder;

		// Token: 0x04000CD1 RID: 3281
		private int? viewHeight;

		// Token: 0x04000CD2 RID: 3282
		private int? viewWidth;

		// Token: 0x02000229 RID: 553
		internal class LastAccessDateTimeTicksComparer : IComparer<PublicFolderViewStatesEntry>
		{
			// Token: 0x060012B9 RID: 4793 RVA: 0x000718A7 File Offset: 0x0006FAA7
			public int Compare(PublicFolderViewStatesEntry x, PublicFolderViewStatesEntry y)
			{
				if (x.LastAccessedDateTimeTicks < y.LastAccessedDateTimeTicks)
				{
					return 1;
				}
				if (x.LastAccessedDateTimeTicks > y.LastAccessedDateTimeTicks)
				{
					return -1;
				}
				return 0;
			}
		}
	}
}
