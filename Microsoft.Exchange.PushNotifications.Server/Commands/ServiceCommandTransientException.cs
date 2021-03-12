using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceCommandTransientException : PushNotificationTransientException
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00004A71 File Offset: 0x00002C71
		public ServiceCommandTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004A7A File Offset: 0x00002C7A
		public ServiceCommandTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004A84 File Offset: 0x00002C84
		protected ServiceCommandTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004A8E File Offset: 0x00002C8E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
