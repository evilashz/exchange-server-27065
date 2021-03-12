using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000069 RID: 105
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverEmailNotificationDeadLoopException : MobilePermanentException
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000CD96 File Offset: 0x0000AF96
		public MobileDriverEmailNotificationDeadLoopException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000CD9F File Offset: 0x0000AF9F
		public MobileDriverEmailNotificationDeadLoopException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		protected MobileDriverEmailNotificationDeadLoopException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
