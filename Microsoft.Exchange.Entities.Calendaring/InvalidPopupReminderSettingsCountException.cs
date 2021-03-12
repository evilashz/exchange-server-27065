using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPopupReminderSettingsCountException : InvalidRequestException
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002DFA File Offset: 0x00000FFA
		public InvalidPopupReminderSettingsCountException(int count) : base(CalendaringStrings.InvalidPopupReminderSettingsCount(count))
		{
			this.count = count;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E0F File Offset: 0x0000100F
		public InvalidPopupReminderSettingsCountException(int count, Exception innerException) : base(CalendaringStrings.InvalidPopupReminderSettingsCount(count), innerException)
		{
			this.count = count;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E25 File Offset: 0x00001025
		protected InvalidPopupReminderSettingsCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.count = (int)info.GetValue("count", typeof(int));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E4F File Offset: 0x0000104F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("count", this.count);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002E6A File Offset: 0x0000106A
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly int count;
	}
}
