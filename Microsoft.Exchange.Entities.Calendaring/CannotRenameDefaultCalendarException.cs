using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotRenameDefaultCalendarException : InvalidRequestException
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000297E File Offset: 0x00000B7E
		public CannotRenameDefaultCalendarException() : base(CalendaringStrings.CannotRenameDefaultCalendar)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000298B File Offset: 0x00000B8B
		public CannotRenameDefaultCalendarException(Exception innerException) : base(CalendaringStrings.CannotRenameDefaultCalendar, innerException)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002999 File Offset: 0x00000B99
		protected CannotRenameDefaultCalendarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A3 File Offset: 0x00000BA3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
