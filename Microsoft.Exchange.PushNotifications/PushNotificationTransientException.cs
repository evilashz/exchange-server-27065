using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200004D RID: 77
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PushNotificationTransientException : LocalizedException
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000062A9 File Offset: 0x000044A9
		public PushNotificationTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000062B2 File Offset: 0x000044B2
		public PushNotificationTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000062BC File Offset: 0x000044BC
		protected PushNotificationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000062C6 File Offset: 0x000044C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
