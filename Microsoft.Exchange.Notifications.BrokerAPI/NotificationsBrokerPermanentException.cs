using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationsBrokerPermanentException : NotificationsBrokerException
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002F16 File Offset: 0x00001116
		public NotificationsBrokerPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F1F File Offset: 0x0000111F
		public NotificationsBrokerPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F29 File Offset: 0x00001129
		protected NotificationsBrokerPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F33 File Offset: 0x00001133
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
