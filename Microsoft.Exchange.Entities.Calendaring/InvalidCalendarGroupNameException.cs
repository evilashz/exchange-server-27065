using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000005 RID: 5
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCalendarGroupNameException : InvalidRequestException
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002801 File Offset: 0x00000A01
		public InvalidCalendarGroupNameException() : base(CalendaringStrings.InvalidCalendarGroupName)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000280E File Offset: 0x00000A0E
		public InvalidCalendarGroupNameException(Exception innerException) : base(CalendaringStrings.InvalidCalendarGroupName, innerException)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000281C File Offset: 0x00000A1C
		protected InvalidCalendarGroupNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002826 File Offset: 0x00000A26
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
