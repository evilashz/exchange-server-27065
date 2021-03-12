using System;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000158 RID: 344
	internal class MailboxFolderViewStates : FolderViewStates
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00051F3C File Offset: 0x0005013C
		internal MailboxFolderViewStates(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.folder = folder;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00051F59 File Offset: 0x00050159
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00051F6C File Offset: 0x0005016C
		internal override CalendarViewType CalendarViewType
		{
			get
			{
				return Utilities.GetFolderProperty<CalendarViewType>(this.folder, ViewStateProperties.CalendarViewType, CalendarViewType.Min);
			}
			set
			{
				if (!FolderViewStates.ValidateCalendarViewType(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set CalendarViewType to invalid value.");
				}
				this.folder[ViewStateProperties.CalendarViewType] = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00051F9C File Offset: 0x0005019C
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00051FAF File Offset: 0x000501AF
		internal override int DailyViewDays
		{
			get
			{
				return Utilities.GetFolderProperty<int>(this.folder, ViewStateProperties.DailyViewDays, 1);
			}
			set
			{
				if (!FolderViewStates.ValidateDailyViewDays(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set DailyViewDays to invalid value.");
				}
				this.folder[ViewStateProperties.DailyViewDays] = value;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00051FDF File Offset: 0x000501DF
		internal override bool GetMultiLine(bool defaultValue)
		{
			return Utilities.GetFolderProperty<bool>(this.folder, ViewStateProperties.MultiLine, defaultValue);
		}

		// Token: 0x1700032F RID: 815
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x00051FF2 File Offset: 0x000501F2
		internal override bool MultiLine
		{
			set
			{
				this.folder[ViewStateProperties.MultiLine] = value;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0005200C File Offset: 0x0005020C
		internal override ReadingPanePosition GetReadingPanePosition(ReadingPanePosition defaultValue)
		{
			ReadingPanePosition folderProperty = Utilities.GetFolderProperty<ReadingPanePosition>(this.folder, ViewStateProperties.ReadingPanePosition, defaultValue);
			if (!FolderViewStates.ValidateReadingPanePosition(folderProperty))
			{
				return defaultValue;
			}
			return folderProperty;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00052036 File Offset: 0x00050236
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00052040 File Offset: 0x00050240
		internal override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.GetReadingPanePosition(ReadingPanePosition.Right);
			}
			set
			{
				if (!FolderViewStates.ValidateReadingPanePosition(value))
				{
					this.folder[ViewStateProperties.ReadingPanePosition] = ReadingPanePosition.Right;
					throw new ArgumentOutOfRangeException("value = " + value, "Cannot set ReadingPanePosition to invalid value.");
				}
				this.folder[ViewStateProperties.ReadingPanePosition] = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0005209C File Offset: 0x0005029C
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000520AF File Offset: 0x000502AF
		internal override ReadingPanePosition ReadingPanePositionMultiDay
		{
			get
			{
				return Utilities.GetFolderProperty<ReadingPanePosition>(this.folder, ViewStateProperties.ReadingPanePositionMultiDay, ReadingPanePosition.Off);
			}
			set
			{
				if (!FolderViewStates.ValidateReadingPanePosition(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ReadingPanePositionMultiDay to invalid value.");
				}
				this.folder[ViewStateProperties.ReadingPanePositionMultiDay] = value;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000520DF File Offset: 0x000502DF
		internal override string GetSortColumn(string defaultValue)
		{
			return Utilities.GetFolderProperty<string>(this.folder, ViewStateProperties.SortColumn, defaultValue);
		}

		// Token: 0x17000332 RID: 818
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x000520F2 File Offset: 0x000502F2
		internal override string SortColumn
		{
			set
			{
				this.folder[ViewStateProperties.SortColumn] = value;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00052105 File Offset: 0x00050305
		internal override SortOrder GetSortOrder(SortOrder defaultValue)
		{
			return Utilities.GetFolderProperty<SortOrder>(this.folder, ViewStateProperties.SortOrder, defaultValue);
		}

		// Token: 0x17000333 RID: 819
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00052118 File Offset: 0x00050318
		internal override SortOrder SortOrder
		{
			set
			{
				if (!FolderViewStates.ValidateSortOrder(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set SortOrder to invalid value.");
				}
				this.folder[ViewStateProperties.SortOrder] = value;
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00052148 File Offset: 0x00050348
		internal override int GetViewHeight(int defaultViewHeight)
		{
			return Utilities.GetFolderProperty<int>(this.folder, ViewStateProperties.ViewHeight, defaultViewHeight);
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0005215B File Offset: 0x0005035B
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00052168 File Offset: 0x00050368
		internal override int ViewHeight
		{
			get
			{
				return this.GetViewHeight(250);
			}
			set
			{
				if (!FolderViewStates.ValidateWidthOrHeight(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ViewHeight to invalid value: " + value);
				}
				this.folder[ViewStateProperties.ViewHeight] = value;
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000521A3 File Offset: 0x000503A3
		internal override int GetViewWidth(int defaultViewWidth)
		{
			return Utilities.GetFolderProperty<int>(this.folder, ViewStateProperties.ViewWidth, defaultViewWidth);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000521B6 File Offset: 0x000503B6
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000521C3 File Offset: 0x000503C3
		internal override int ViewWidth
		{
			get
			{
				return this.GetViewWidth(450);
			}
			set
			{
				if (!FolderViewStates.ValidateWidthOrHeight(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ViewWidth to invalid value: " + value);
				}
				this.folder[ViewStateProperties.ViewWidth] = value;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000521FE File Offset: 0x000503FE
		internal override void Save()
		{
			this.folder.Save();
		}

		// Token: 0x04000872 RID: 2162
		private Folder folder;
	}
}
