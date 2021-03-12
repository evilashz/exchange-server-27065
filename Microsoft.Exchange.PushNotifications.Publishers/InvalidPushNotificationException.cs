using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000102 RID: 258
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPushNotificationException : InvalidNotificationException
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x00019D27 File Offset: 0x00017F27
		public InvalidPushNotificationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00019D30 File Offset: 0x00017F30
		public InvalidPushNotificationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00019D3A File Offset: 0x00017F3A
		protected InvalidPushNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00019D44 File Offset: 0x00017F44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
