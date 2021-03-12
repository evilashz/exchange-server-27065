using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200004E RID: 78
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidNotificationException : PushNotificationPermanentException
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000062D0 File Offset: 0x000044D0
		public InvalidNotificationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000062D9 File Offset: 0x000044D9
		public InvalidNotificationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000062E3 File Offset: 0x000044E3
		protected InvalidNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000062ED File Offset: 0x000044ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
