using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003F9 RID: 1017
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecurrenceInfo
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x000BED98 File Offset: 0x000BCF98
		private RecurrenceInfo()
		{
			this.Period = 1;
			this.FirstDayOfWeek = 0;
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000BEDAE File Offset: 0x000BCFAE
		// (set) Token: 0x06002E7E RID: 11902 RVA: 0x000BEDB6 File Offset: 0x000BCFB6
		public RecurrenceGroup Group { get; private set; }

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000BEDBF File Offset: 0x000BCFBF
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000BEDC7 File Offset: 0x000BCFC7
		public RecurrenceTypeInBlob Type { get; private set; }

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000BEDD0 File Offset: 0x000BCFD0
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000BEDD8 File Offset: 0x000BCFD8
		public int Period { get; private set; }

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000BEDE1 File Offset: 0x000BCFE1
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000BEDE9 File Offset: 0x000BCFE9
		public DaysOfWeek DayMask { get; private set; }

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000BEDF2 File Offset: 0x000BCFF2
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000BEDFA File Offset: 0x000BCFFA
		public int DayOfMonth { get; private set; }

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000BEE03 File Offset: 0x000BD003
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x000BEE0B File Offset: 0x000BD00B
		public RecurrenceOrderType NthOccurrence { get; private set; }

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000BEE14 File Offset: 0x000BD014
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x000BEE1C File Offset: 0x000BD01C
		public int MonthOfYear { get; private set; }

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000BEE25 File Offset: 0x000BD025
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000BEE2D File Offset: 0x000BD02D
		public RecurrenceRangeType Range { get; private set; }

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000BEE36 File Offset: 0x000BD036
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000BEE3E File Offset: 0x000BD03E
		public int NumberOfOccurrences { get; private set; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000BEE47 File Offset: 0x000BD047
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x000BEE4F File Offset: 0x000BD04F
		public int FirstDayOfWeek { get; private set; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000BEE58 File Offset: 0x000BD058
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000BEE60 File Offset: 0x000BD060
		public ExDateTime StartDate { get; private set; }

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000BEE69 File Offset: 0x000BD069
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x000BEE71 File Offset: 0x000BD071
		public ExDateTime EndDate { get; private set; }

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000BEE7A File Offset: 0x000BD07A
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000BEE82 File Offset: 0x000BD082
		public ExDateTime[] DeletedOccurrences { get; private set; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000BEE8B File Offset: 0x000BD08B
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000BEE93 File Offset: 0x000BD093
		public IList<OccurrenceInfo> ModifiedOccurrences { get; private set; }

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000BEE9C File Offset: 0x000BD09C
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x000BEEA4 File Offset: 0x000BD0A4
		public AnomaliesFlags Anomalies { get; private set; }

		// Token: 0x06002E9B RID: 11931 RVA: 0x000BEEB0 File Offset: 0x000BD0B0
		internal static RecurrenceInfo GetInfo(Recurrence recurrenceObject)
		{
			RecurrenceInfo recurrenceInfo = null;
			InternalRecurrence internalRecurrence = recurrenceObject as InternalRecurrence;
			if (internalRecurrence != null)
			{
				recurrenceInfo = new RecurrenceInfo();
				recurrenceInfo.StartDate = internalRecurrence.Range.StartDate;
				recurrenceInfo.EndDate = internalRecurrence.EndDate;
				if (internalRecurrence.Range is EndDateRecurrenceRange)
				{
					recurrenceInfo.Range = RecurrenceRangeType.End;
					recurrenceInfo.EndDate = ((EndDateRecurrenceRange)internalRecurrence.Range).EndDate;
				}
				else if (internalRecurrence.Range is NumberedRecurrenceRange)
				{
					recurrenceInfo.Range = RecurrenceRangeType.AfterNOccur;
					recurrenceInfo.NumberOfOccurrences = ((NumberedRecurrenceRange)internalRecurrence.Range).NumberOfOccurrences;
				}
				else
				{
					recurrenceInfo.Range = RecurrenceRangeType.NoEnd;
				}
				recurrenceInfo.SetPatternSpecificProperties(internalRecurrence.Pattern);
				recurrenceInfo.Anomalies = internalRecurrence.Anomalies;
				recurrenceInfo.ModifiedOccurrences = internalRecurrence.GetModifiedOccurrences();
				recurrenceInfo.DeletedOccurrences = internalRecurrence.GetDeletedOccurrences();
			}
			return recurrenceInfo;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000BEF8C File Offset: 0x000BD18C
		private void SetPatternSpecificProperties(RecurrencePattern pattern)
		{
			if (pattern is DailyRecurrencePattern)
			{
				DailyRecurrencePattern dailyRecurrencePattern = (DailyRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Daily;
				this.Type = RecurrenceTypeInBlob.Minute;
				this.Period = dailyRecurrencePattern.RecurrenceInterval * 24 * 60;
				return;
			}
			if (pattern is WeeklyRecurrencePattern)
			{
				WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Weekly;
				this.Type = RecurrenceTypeInBlob.Week;
				this.Period = weeklyRecurrencePattern.RecurrenceInterval;
				this.DayMask = weeklyRecurrencePattern.DaysOfWeek;
				this.FirstDayOfWeek = (int)((WeeklyRecurrencePattern)pattern).FirstDayOfWeek;
				return;
			}
			if (pattern is MonthlyRecurrencePattern)
			{
				MonthlyRecurrencePattern monthlyRecurrencePattern = (MonthlyRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Monthly;
				this.Type = ((monthlyRecurrencePattern.CalendarType == CalendarType.Hijri) ? RecurrenceTypeInBlob.HjMonth : RecurrenceTypeInBlob.Month);
				this.Period = monthlyRecurrencePattern.RecurrenceInterval;
				this.DayOfMonth = monthlyRecurrencePattern.DayOfMonth;
				return;
			}
			if (pattern is YearlyRecurrencePattern)
			{
				YearlyRecurrencePattern yearlyRecurrencePattern = (YearlyRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Yearly;
				this.Type = ((yearlyRecurrencePattern.CalendarType == CalendarType.Hijri) ? RecurrenceTypeInBlob.HjMonth : RecurrenceTypeInBlob.Month);
				this.DayOfMonth = yearlyRecurrencePattern.DayOfMonth;
				this.Period = 12;
				return;
			}
			if (pattern is MonthlyThRecurrencePattern)
			{
				MonthlyThRecurrencePattern monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Monthly;
				this.Type = ((monthlyThRecurrencePattern.CalendarType == CalendarType.Hijri) ? RecurrenceTypeInBlob.HjMonthNth : RecurrenceTypeInBlob.MonthNth);
				this.DayMask = monthlyThRecurrencePattern.DaysOfWeek;
				this.NthOccurrence = monthlyThRecurrencePattern.Order;
				this.Period = monthlyThRecurrencePattern.RecurrenceInterval;
				return;
			}
			if (pattern is YearlyThRecurrencePattern)
			{
				YearlyThRecurrencePattern yearlyThRecurrencePattern = (YearlyThRecurrencePattern)pattern;
				this.Group = RecurrenceGroup.Yearly;
				this.Type = ((yearlyThRecurrencePattern.CalendarType == CalendarType.Hijri) ? RecurrenceTypeInBlob.HjMonthNth : RecurrenceTypeInBlob.MonthNth);
				this.DayMask = yearlyThRecurrencePattern.DaysOfWeek;
				this.NthOccurrence = yearlyThRecurrencePattern.Order;
				this.MonthOfYear = yearlyThRecurrencePattern.Month;
				this.Period = 12;
				return;
			}
			if (pattern is DailyRegeneratingPattern)
			{
				DailyRegeneratingPattern dailyRegeneratingPattern = (DailyRegeneratingPattern)pattern;
				this.Type = RecurrenceTypeInBlob.Minute;
				this.Group = RecurrenceGroup.Daily;
				this.Period = dailyRegeneratingPattern.RecurrenceInterval * 24 * 60;
				return;
			}
			if (pattern is WeeklyRegeneratingPattern)
			{
				WeeklyRegeneratingPattern weeklyRegeneratingPattern = (WeeklyRegeneratingPattern)pattern;
				this.Type = RecurrenceTypeInBlob.Minute;
				this.Group = RecurrenceGroup.Weekly;
				this.DayMask = DaysOfWeek.Monday;
				this.Period = weeklyRegeneratingPattern.RecurrenceInterval * 7 * 24 * 60;
				return;
			}
			if (pattern is MonthlyRegeneratingPattern)
			{
				MonthlyRegeneratingPattern monthlyRegeneratingPattern = (MonthlyRegeneratingPattern)pattern;
				this.Type = RecurrenceTypeInBlob.MonthNth;
				this.Group = RecurrenceGroup.Monthly;
				this.DayMask = DaysOfWeek.Monday;
				this.NthOccurrence = (RecurrenceOrderType)0;
				this.Period = monthlyRegeneratingPattern.RecurrenceInterval;
				return;
			}
			if (pattern is YearlyRegeneratingPattern)
			{
				YearlyRegeneratingPattern yearlyRegeneratingPattern = (YearlyRegeneratingPattern)pattern;
				this.Type = RecurrenceTypeInBlob.Month;
				this.Group = RecurrenceGroup.Yearly;
				this.DayOfMonth = 1;
				this.Period = 12 * yearlyRegeneratingPattern.RecurrenceInterval;
			}
		}
	}
}
