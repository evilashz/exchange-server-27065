using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PushNotificationEndpointNotFoundException : PushNotificationTransientException
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000631E File Offset: 0x0000451E
		public PushNotificationEndpointNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006327 File Offset: 0x00004527
		public PushNotificationEndpointNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006331 File Offset: 0x00004531
		protected PushNotificationEndpointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000633B File Offset: 0x0000453B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
