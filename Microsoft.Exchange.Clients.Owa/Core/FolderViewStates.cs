using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000117 RID: 279
	internal abstract class FolderViewStates
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00042C22 File Offset: 0x00040E22
		internal static bool ValidateCalendarViewType(CalendarViewType value)
		{
			return value >= CalendarViewType.Min && value <= CalendarViewType.Max && value != CalendarViewType.WeeklyAgenda && value != CalendarViewType.WorkWeeklyAgenda;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00042C39 File Offset: 0x00040E39
		internal static bool ValidateDailyViewDays(int value)
		{
			return value >= 1 && value <= 7;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00042C48 File Offset: 0x00040E48
		internal static bool ValidateReadingPanePosition(ReadingPanePosition value)
		{
			return value >= ReadingPanePosition.Min && value <= ReadingPanePosition.Bottom;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00042C57 File Offset: 0x00040E57
		internal static bool ValidateSortOrder(SortOrder value)
		{
			return value == SortOrder.Ascending || value == SortOrder.Descending;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00042C62 File Offset: 0x00040E62
		internal static bool ValidateWidthOrHeight(int value)
		{
			return value >= 0;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600094C RID: 2380
		// (set) Token: 0x0600094D RID: 2381
		internal abstract CalendarViewType CalendarViewType { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600094E RID: 2382
		// (set) Token: 0x0600094F RID: 2383
		internal abstract int DailyViewDays { get; set; }

		// Token: 0x06000950 RID: 2384
		internal abstract bool GetMultiLine(bool defaultValue);

		// Token: 0x17000287 RID: 647
		// (set) Token: 0x06000951 RID: 2385
		internal abstract bool MultiLine { set; }

		// Token: 0x06000952 RID: 2386
		internal abstract ReadingPanePosition GetReadingPanePosition(ReadingPanePosition defaultValue);

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000953 RID: 2387
		// (set) Token: 0x06000954 RID: 2388
		internal abstract ReadingPanePosition ReadingPanePosition { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000955 RID: 2389
		// (set) Token: 0x06000956 RID: 2390
		internal abstract ReadingPanePosition ReadingPanePositionMultiDay { get; set; }

		// Token: 0x06000957 RID: 2391
		internal abstract string GetSortColumn(string defaultValue);

		// Token: 0x1700028A RID: 650
		// (set) Token: 0x06000958 RID: 2392
		internal abstract string SortColumn { set; }

		// Token: 0x06000959 RID: 2393
		internal abstract SortOrder GetSortOrder(SortOrder defaultValue);

		// Token: 0x1700028B RID: 651
		// (set) Token: 0x0600095A RID: 2394
		internal abstract SortOrder SortOrder { set; }

		// Token: 0x0600095B RID: 2395
		internal abstract int GetViewHeight(int defaultViewHeight);

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600095C RID: 2396
		// (set) Token: 0x0600095D RID: 2397
		internal abstract int ViewHeight { get; set; }

		// Token: 0x0600095E RID: 2398
		internal abstract int GetViewWidth(int defaultViewWidth);

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		internal abstract int ViewWidth { get; set; }

		// Token: 0x06000961 RID: 2401
		internal abstract void Save();

		// Token: 0x040006BB RID: 1723
		public const CalendarViewType DefaultCalendarViewType = CalendarViewType.Min;

		// Token: 0x040006BC RID: 1724
		public const int DefaultDailyViewDays = 1;

		// Token: 0x040006BD RID: 1725
		public const ReadingPanePosition DefaultReadingPanePosition = ReadingPanePosition.Right;

		// Token: 0x040006BE RID: 1726
		public const ReadingPanePosition DefaultReadingPanePositionMultiDay = ReadingPanePosition.Off;

		// Token: 0x040006BF RID: 1727
		public const int DefaultViewHeight = 250;

		// Token: 0x040006C0 RID: 1728
		public const int DefaultViewWidth = 450;

		// Token: 0x040006C1 RID: 1729
		public const int DefaultTaskViewWidth = 381;

		// Token: 0x040006C2 RID: 1730
		public const int DefaultPublicFolderViewWidth = 450;

		// Token: 0x040006C3 RID: 1731
		public const int MaxDailyViewDays = 7;

		// Token: 0x040006C4 RID: 1732
		public const int MinDailyViewDays = 1;
	}
}
