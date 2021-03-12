using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000104 RID: 260
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ApnsFeedbackException : PushNotificationPermanentException
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x00019D75 File Offset: 0x00017F75
		public ApnsFeedbackException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00019D7E File Offset: 0x00017F7E
		public ApnsFeedbackException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00019D88 File Offset: 0x00017F88
		protected ApnsFeedbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00019D92 File Offset: 0x00017F92
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
