using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidReminderSettingIdException : InvalidRequestException
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002EA1 File Offset: 0x000010A1
		public InvalidReminderSettingIdException() : base(CalendaringStrings.InvalidReminderSettingId)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EAE File Offset: 0x000010AE
		public InvalidReminderSettingIdException(Exception innerException) : base(CalendaringStrings.InvalidReminderSettingId, innerException)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002EBC File Offset: 0x000010BC
		protected InvalidReminderSettingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EC6 File Offset: 0x000010C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
