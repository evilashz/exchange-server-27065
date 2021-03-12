using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A6 RID: 1190
	[DataContract]
	public class UMAAHolidaySchedule
	{
		// Token: 0x06003B0B RID: 15115 RVA: 0x000B2A84 File Offset: 0x000B0C84
		public UMAAHolidaySchedule(HolidaySchedule holiday)
		{
			this.Name = holiday.Name;
			this.GreetingFileName = holiday.Greeting;
			this.StartDate = holiday.StartDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
			this.EndDate = holiday.EndDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
			this.StartDateDisplay = holiday.StartDate.ToString(EcpDateTimeHelper.GetUserDateFormat(), CultureInfo.CurrentCulture);
			this.EndDateDisplay = holiday.EndDate.ToString(EcpDateTimeHelper.GetUserDateFormat(), CultureInfo.CurrentCulture);
		}

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x000B2B27 File Offset: 0x000B0D27
		// (set) Token: 0x06003B0D RID: 15117 RVA: 0x000B2B2F File Offset: 0x000B0D2F
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000B2B38 File Offset: 0x000B0D38
		// (set) Token: 0x06003B0F RID: 15119 RVA: 0x000B2B40 File Offset: 0x000B0D40
		[DataMember]
		public string StartDate { get; private set; }

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000B2B49 File Offset: 0x000B0D49
		// (set) Token: 0x06003B11 RID: 15121 RVA: 0x000B2B51 File Offset: 0x000B0D51
		[DataMember]
		public string StartDateDisplay { get; private set; }

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x06003B12 RID: 15122 RVA: 0x000B2B5A File Offset: 0x000B0D5A
		// (set) Token: 0x06003B13 RID: 15123 RVA: 0x000B2B62 File Offset: 0x000B0D62
		[DataMember]
		public string EndDate { get; private set; }

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000B2B6B File Offset: 0x000B0D6B
		// (set) Token: 0x06003B15 RID: 15125 RVA: 0x000B2B73 File Offset: 0x000B0D73
		[DataMember]
		public string EndDateDisplay { get; private set; }

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x06003B16 RID: 15126 RVA: 0x000B2B7C File Offset: 0x000B0D7C
		// (set) Token: 0x06003B17 RID: 15127 RVA: 0x000B2B84 File Offset: 0x000B0D84
		[DataMember]
		public string GreetingFileName { get; private set; }

		// Token: 0x06003B18 RID: 15128 RVA: 0x000B2B90 File Offset: 0x000B0D90
		public HolidaySchedule ToHolidaySchedule()
		{
			this.Validate();
			return new HolidaySchedule(this.Name, this.GreetingFileName, DateTime.ParseExact(this.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture), DateTime.ParseExact(this.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture));
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000B2BE0 File Offset: 0x000B0DE0
		private void Validate()
		{
			this.Name.FaultIfNullOrEmpty(Strings.UMHolidayScheduleNameNotSet);
			this.GreetingFileName.FaultIfNullOrEmpty(Strings.UMHolidayScheduleGreetingFileNameNotSet);
			this.StartDate.FaultIfNullOrEmpty(Strings.UMHolidayScheduleStartDateNotSet);
			this.EndDate.FaultIfNullOrEmpty(Strings.UMHolidayScheduleEndDateNotSet);
		}
	}
}
