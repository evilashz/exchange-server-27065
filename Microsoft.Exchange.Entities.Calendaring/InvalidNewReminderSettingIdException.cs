using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidNewReminderSettingIdException : InvalidRequestException
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002E72 File Offset: 0x00001072
		public InvalidNewReminderSettingIdException() : base(CalendaringStrings.InvalidNewReminderSettingId)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E7F File Offset: 0x0000107F
		public InvalidNewReminderSettingIdException(Exception innerException) : base(CalendaringStrings.InvalidNewReminderSettingId, innerException)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E8D File Offset: 0x0000108D
		protected InvalidNewReminderSettingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E97 File Offset: 0x00001097
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
