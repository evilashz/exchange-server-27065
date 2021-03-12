using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076E RID: 1902
	[Serializable]
	public class RecurrenceCalendarTypeNotSupportedException : RecurrenceFormatException
	{
		// Token: 0x060048A8 RID: 18600 RVA: 0x00131589 File Offset: 0x0012F789
		internal RecurrenceCalendarTypeNotSupportedException(LocalizedString message, CalendarType calendarType, Stream stream) : base(message, stream)
		{
			this.calendarType = calendarType;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0013159A File Offset: 0x0012F79A
		protected RecurrenceCalendarTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.calendarType = (CalendarType)info.GetValue("calendarType", typeof(CalendarType));
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x001315C4 File Offset: 0x0012F7C4
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x001315CC File Offset: 0x0012F7CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("calendarType", this.calendarType);
		}

		// Token: 0x04002766 RID: 10086
		private const string CalendarTypeLabel = "calendarType";

		// Token: 0x04002767 RID: 10087
		private CalendarType calendarType;
	}
}
