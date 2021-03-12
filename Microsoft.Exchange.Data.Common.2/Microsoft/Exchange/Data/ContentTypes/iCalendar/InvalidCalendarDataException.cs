using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class InvalidCalendarDataException : ExchangeDataException
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0002E941 File Offset: 0x0002CB41
		public InvalidCalendarDataException(string message) : base(message)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002E94A File Offset: 0x0002CB4A
		public InvalidCalendarDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002E954 File Offset: 0x0002CB54
		protected InvalidCalendarDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
