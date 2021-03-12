using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000037 RID: 55
	public class CalendarViewParameters : ICalendarViewParameters
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005F88 File Offset: 0x00004188
		public CalendarViewParameters(ExDateTime? startTime, ExDateTime? endTime)
		{
			this.startTime = startTime;
			this.endTime = endTime;
			ExDateTime today = ExDateTime.Today;
			ExDateTime exDateTime = new ExDateTime(today.TimeZone, today.Year, today.Month, 1);
			this.EffectiveStartTime = ((startTime != null) ? startTime.Value : ((endTime != null) ? endTime.Value.AddMonths(-3) : exDateTime));
			this.EffectiveEndTime = ((endTime != null) ? this.endTime.Value : this.EffectiveStartTime.AddMonths(3));
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000602C File Offset: 0x0000422C
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006034 File Offset: 0x00004234
		public ExDateTime EffectiveEndTime { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000603D File Offset: 0x0000423D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006045 File Offset: 0x00004245
		public ExDateTime EffectiveStartTime { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006050 File Offset: 0x00004250
		public bool HasExplicitEndTime
		{
			get
			{
				return this.endTime != null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000606C File Offset: 0x0000426C
		public bool HasExplicitStartTime
		{
			get
			{
				return this.startTime != null;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006087 File Offset: 0x00004287
		public bool IsDefaultView
		{
			get
			{
				return !this.HasExplicitStartTime && !this.HasExplicitEndTime;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000609C File Offset: 0x0000429C
		private static bool IsValidFilter(QueryFilter queryFilter, ref ExDateTime? start, ref ExDateTime? end)
		{
			return CalendarViewParameters.IsValidComparisonFilter(queryFilter as ComparisonFilter, ref start, ref end) || CalendarViewParameters.IsValidAndFilter(queryFilter as AndFilter, ref start, ref end);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000060BC File Offset: 0x000042BC
		private static bool IsValidAndFilter(AndFilter andFilter, ref ExDateTime? start, ref ExDateTime? end)
		{
			if (andFilter == null)
			{
				return false;
			}
			foreach (QueryFilter queryFilter in andFilter.Filters)
			{
				if (!CalendarViewParameters.IsValidFilter(queryFilter, ref start, ref end))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006120 File Offset: 0x00004320
		private static bool IsValidComparisonFilter(ComparisonFilter comparisonFilter, ref ExDateTime? start, ref ExDateTime? end)
		{
			if (comparisonFilter == null)
			{
				return false;
			}
			if (comparisonFilter.PropertyValue is ExDateTime)
			{
				ExDateTime value = (ExDateTime)comparisonFilter.PropertyValue;
				if (comparisonFilter.Property == CalendarItemInstanceSchema.EndTime && comparisonFilter.ComparisonOperator == ComparisonOperator.GreaterThanOrEqual)
				{
					if (start != null)
					{
						return false;
					}
					start = new ExDateTime?(value);
				}
				if (comparisonFilter.Property == CalendarItemInstanceSchema.StartTime && comparisonFilter.ComparisonOperator == ComparisonOperator.LessThanOrEqual)
				{
					if (end != null)
					{
						return false;
					}
					end = new ExDateTime?(value);
				}
			}
			return true;
		}

		// Token: 0x0400006A RID: 106
		public const int DefaultPeriodInMonths = 3;

		// Token: 0x0400006B RID: 107
		private readonly ExDateTime? endTime;

		// Token: 0x0400006C RID: 108
		private readonly ExDateTime? startTime;
	}
}
