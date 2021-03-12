using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001165 RID: 4453
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDateValueException : ReportingException
	{
		// Token: 0x0600B5E5 RID: 46565 RVA: 0x0029EFC0 File Offset: 0x0029D1C0
		public InvalidDateValueException(DateTime date, DateTime minDate, DateTime maxDate) : base(Strings.InvalidDateValueException(date, minDate, maxDate))
		{
			this.date = date;
			this.minDate = minDate;
			this.maxDate = maxDate;
		}

		// Token: 0x0600B5E6 RID: 46566 RVA: 0x0029EFE5 File Offset: 0x0029D1E5
		public InvalidDateValueException(DateTime date, DateTime minDate, DateTime maxDate, Exception innerException) : base(Strings.InvalidDateValueException(date, minDate, maxDate), innerException)
		{
			this.date = date;
			this.minDate = minDate;
			this.maxDate = maxDate;
		}

		// Token: 0x0600B5E7 RID: 46567 RVA: 0x0029F00C File Offset: 0x0029D20C
		protected InvalidDateValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.date = (DateTime)info.GetValue("date", typeof(DateTime));
			this.minDate = (DateTime)info.GetValue("minDate", typeof(DateTime));
			this.maxDate = (DateTime)info.GetValue("maxDate", typeof(DateTime));
		}

		// Token: 0x0600B5E8 RID: 46568 RVA: 0x0029F081 File Offset: 0x0029D281
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("date", this.date);
			info.AddValue("minDate", this.minDate);
			info.AddValue("maxDate", this.maxDate);
		}

		// Token: 0x1700396E RID: 14702
		// (get) Token: 0x0600B5E9 RID: 46569 RVA: 0x0029F0BE File Offset: 0x0029D2BE
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x1700396F RID: 14703
		// (get) Token: 0x0600B5EA RID: 46570 RVA: 0x0029F0C6 File Offset: 0x0029D2C6
		public DateTime MinDate
		{
			get
			{
				return this.minDate;
			}
		}

		// Token: 0x17003970 RID: 14704
		// (get) Token: 0x0600B5EB RID: 46571 RVA: 0x0029F0CE File Offset: 0x0029D2CE
		public DateTime MaxDate
		{
			get
			{
				return this.maxDate;
			}
		}

		// Token: 0x040062D4 RID: 25300
		private readonly DateTime date;

		// Token: 0x040062D5 RID: 25301
		private readonly DateTime minDate;

		// Token: 0x040062D6 RID: 25302
		private readonly DateTime maxDate;
	}
}
