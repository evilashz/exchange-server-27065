using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BA RID: 954
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarViewQueryResumptionPoint : QueryResumptionPoint<ExDateTime>
	{
		// Token: 0x06002B8E RID: 11150 RVA: 0x000ADC30 File Offset: 0x000ABE30
		private CalendarViewQueryResumptionPoint(bool resumeToRecurringMeetings, byte[] instanceKey, ExDateTime sortKeyValue, bool hasSortKeyValue) : base(instanceKey, resumeToRecurringMeetings ? CalendarFolder.RecurringMeetingsSortKey : CalendarFolder.SingleInstanceMeetingsSortKey, sortKeyValue, hasSortKeyValue)
		{
			this.resumeToRecurringMeetings = resumeToRecurringMeetings;
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000ADC52 File Offset: 0x000ABE52
		public static string CurrentVersion
		{
			get
			{
				return QueryResumptionPoint<ExDateTime>.GetVersion("0");
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000ADC5E File Offset: 0x000ABE5E
		public bool ResumeToSingleInstanceMeetings
		{
			get
			{
				return !this.resumeToRecurringMeetings;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000ADC69 File Offset: 0x000ABE69
		public bool ResumeToRecurringMeetings
		{
			get
			{
				return this.resumeToRecurringMeetings;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000ADC71 File Offset: 0x000ABE71
		public override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && !this.resumeToRecurringMeetings;
			}
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000ADC88 File Offset: 0x000ABE88
		public static CalendarViewQueryResumptionPoint CreateInstance(bool resumeToRecurringMeetings, byte[] instanceKey, ExDateTime? sortKeyValue)
		{
			return new CalendarViewQueryResumptionPoint(resumeToRecurringMeetings, instanceKey, sortKeyValue ?? default(ExDateTime), sortKeyValue != null);
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000ADCC0 File Offset: 0x000ABEC0
		protected override string MinorVersion
		{
			get
			{
				return "0";
			}
		}

		// Token: 0x0400185A RID: 6234
		private const string CurrentMinorVersion = "0";

		// Token: 0x0400185B RID: 6235
		private readonly bool resumeToRecurringMeetings;
	}
}
