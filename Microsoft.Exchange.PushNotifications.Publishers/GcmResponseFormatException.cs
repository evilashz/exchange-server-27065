using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000106 RID: 262
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GcmResponseFormatException : PushNotificationPermanentException
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x00019DC3 File Offset: 0x00017FC3
		public GcmResponseFormatException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00019DCC File Offset: 0x00017FCC
		public GcmResponseFormatException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00019DD6 File Offset: 0x00017FD6
		protected GcmResponseFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
