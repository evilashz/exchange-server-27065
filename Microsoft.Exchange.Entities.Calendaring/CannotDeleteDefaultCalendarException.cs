using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotDeleteDefaultCalendarException : InvalidRequestException
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000294F File Offset: 0x00000B4F
		public CannotDeleteDefaultCalendarException() : base(CalendaringStrings.CannotDeleteDefaultCalendar)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000295C File Offset: 0x00000B5C
		public CannotDeleteDefaultCalendarException(Exception innerException) : base(CalendaringStrings.CannotDeleteDefaultCalendar, innerException)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000296A File Offset: 0x00000B6A
		protected CannotDeleteDefaultCalendarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002974 File Offset: 0x00000B74
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
