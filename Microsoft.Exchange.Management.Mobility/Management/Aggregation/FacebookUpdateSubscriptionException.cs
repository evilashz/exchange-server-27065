using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000063 RID: 99
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FacebookUpdateSubscriptionException : LocalizedException
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001025D File Offset: 0x0000E45D
		public FacebookUpdateSubscriptionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00010266 File Offset: 0x0000E466
		public FacebookUpdateSubscriptionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010270 File Offset: 0x0000E470
		protected FacebookUpdateSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001027A File Offset: 0x0000E47A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
