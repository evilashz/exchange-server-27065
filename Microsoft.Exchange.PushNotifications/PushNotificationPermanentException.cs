using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PushNotificationPermanentException : LocalizedException
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00006282 File Offset: 0x00004482
		public PushNotificationPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000628B File Offset: 0x0000448B
		public PushNotificationPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006295 File Offset: 0x00004495
		protected PushNotificationPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000629F File Offset: 0x0000449F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
