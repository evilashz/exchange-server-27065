using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000103 RID: 259
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ApnsCertificateException : PushNotificationPermanentException
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x00019D4E File Offset: 0x00017F4E
		public ApnsCertificateException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00019D57 File Offset: 0x00017F57
		public ApnsCertificateException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00019D61 File Offset: 0x00017F61
		protected ApnsCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00019D6B File Offset: 0x00017F6B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
