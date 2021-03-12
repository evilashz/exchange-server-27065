using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000148 RID: 328
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PushNotificationBaseException : LocalizedException
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x000520C7 File Offset: 0x000502C7
		public PushNotificationBaseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000520D0 File Offset: 0x000502D0
		public PushNotificationBaseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000520DA File Offset: 0x000502DA
		protected PushNotificationBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000520E4 File Offset: 0x000502E4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
