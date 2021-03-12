using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationsBrokerException : LocalizedException
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002EEF File Offset: 0x000010EF
		public NotificationsBrokerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002EF8 File Offset: 0x000010F8
		public NotificationsBrokerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F02 File Offset: 0x00001102
		protected NotificationsBrokerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F0C File Offset: 0x0000110C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
