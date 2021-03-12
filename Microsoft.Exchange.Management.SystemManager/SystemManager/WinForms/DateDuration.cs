using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200012B RID: 299
	public class DateDuration : ICloneable
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		public DateDuration(DateTime? startDate, DateTime? endDate)
		{
			if (startDate != null)
			{
				startDate = new DateTime?((startDate.Value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(startDate.Value, DateTimeKind.Local) : startDate.Value.Date);
			}
			if (endDate != null)
			{
				endDate = new DateTime?((endDate.Value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(endDate.Value, DateTimeKind.Local) : endDate.Value.Date);
				endDate = new DateTime?(endDate.Value.Date + new TimeSpan(23, 59, 59));
			}
			if (startDate != null && endDate != null && endDate.Value.CompareTo(startDate.Value) < 0)
			{
				throw new StrongTypeFormatException(Strings.DateDurationOutOfRangeErrorMessage, string.Empty);
			}
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		public bool IsWithDate()
		{
			return this.StartDate != null || this.EndDate != null;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002AD05 File Offset: 0x00028F05
		public DateTime? StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0002AD0D File Offset: 0x00028F0D
		public DateTime? EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002AD18 File Offset: 0x00028F18
		public override string ToString()
		{
			string text = string.Empty;
			if (this.StartDate != null)
			{
				text = string.Format("{0} {1}", Strings.DateDurationAfter, this.StartDate.Value.ToLongDateString());
			}
			string text2 = string.Empty;
			if (this.EndDate != null)
			{
				text2 = string.Format("{0} {1}", Strings.DateDurationBefore, this.EndDate.Value.ToLongDateString());
			}
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				return string.Format("{0} {1} {2}", text, (this.StartDate == null) ? string.Empty : Strings.DateDurationAnd, text2);
			}
			return text + text2;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002ADF1 File Offset: 0x00028FF1
		public object Clone()
		{
			return new DateDuration(this.StartDate, this.EndDate);
		}

		// Token: 0x040004DB RID: 1243
		private DateTime? startDate;

		// Token: 0x040004DC RID: 1244
		private DateTime? endDate;
	}
}
