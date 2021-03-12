using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C1 RID: 961
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EndDateRecurrenceRange : RecurrenceRange
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000AE2F4 File Offset: 0x000AC4F4
		// (set) Token: 0x06002BBA RID: 11194 RVA: 0x000AE2FC File Offset: 0x000AC4FC
		public ExDateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			private set
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(value.HasTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "EndDateRecurrenceRange.EndDate_set: value has no time zone", new object[0]);
				if (value < this.StartDate)
				{
					throw new ArgumentException(ServerStrings.ExEndDateEarlierThanStartDate);
				}
				this.endDate = value.Date;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000AE34C File Offset: 0x000AC54C
		// (set) Token: 0x06002BBC RID: 11196 RVA: 0x000AE354 File Offset: 0x000AC554
		public override ExDateTime StartDate
		{
			get
			{
				return base.StartDate;
			}
			protected set
			{
				if (value > this.EndDate)
				{
					throw new ArgumentException(ServerStrings.ExStartDateLaterThanEndDate);
				}
				base.StartDate = value;
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000AE37C File Offset: 0x000AC57C
		public EndDateRecurrenceRange(ExDateTime startDate, ExDateTime endDate)
		{
			ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(startDate.TimeZone == endDate.TimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "EndDateRecurrenceRange constructor.\nstartDate.TimeZone={0}\nendDateTime.TimeZone={1}", new object[]
			{
				startDate.TimeZone,
				endDate.TimeZone
			});
			this.StartDate = startDate;
			this.EndDate = endDate;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AE3E0 File Offset: 0x000AC5E0
		public override bool Equals(RecurrenceRange value)
		{
			if (!(value is EndDateRecurrenceRange))
			{
				return false;
			}
			EndDateRecurrenceRange endDateRecurrenceRange = (EndDateRecurrenceRange)value;
			return endDateRecurrenceRange.EndDate == this.endDate && base.Equals(value);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AE41C File Offset: 0x000AC61C
		public override string ToString()
		{
			return string.Format("Starts {0}, ends {1}", this.StartDate.ToString(DateTimeFormatInfo.InvariantInfo), this.EndDate.ToString(DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x04001865 RID: 6245
		private ExDateTime endDate = ExDateTime.MaxValue;
	}
}
