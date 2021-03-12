using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullPopupReminderSettingsException : InvalidRequestException
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002DCB File Offset: 0x00000FCB
		public NullPopupReminderSettingsException() : base(CalendaringStrings.NullPopupReminderSettings)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public NullPopupReminderSettingsException(Exception innerException) : base(CalendaringStrings.NullPopupReminderSettings, innerException)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002DE6 File Offset: 0x00000FE6
		protected NullPopupReminderSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
