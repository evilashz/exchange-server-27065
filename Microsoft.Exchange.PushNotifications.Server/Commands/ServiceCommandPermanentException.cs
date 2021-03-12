using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceCommandPermanentException : PushNotificationPermanentException
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00004A98 File Offset: 0x00002C98
		public ServiceCommandPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004AA1 File Offset: 0x00002CA1
		public ServiceCommandPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004AAB File Offset: 0x00002CAB
		protected ServiceCommandPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004AB5 File Offset: 0x00002CB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
