using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200004F RID: 79
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUserNotificationException : InvalidNotificationException
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000062F7 File Offset: 0x000044F7
		public InvalidUserNotificationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006300 File Offset: 0x00004500
		public InvalidUserNotificationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000630A File Offset: 0x0000450A
		protected InvalidUserNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006314 File Offset: 0x00004514
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
