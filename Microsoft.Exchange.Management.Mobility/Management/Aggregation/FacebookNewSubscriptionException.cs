using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000062 RID: 98
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FacebookNewSubscriptionException : LocalizedException
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00010236 File Offset: 0x0000E436
		public FacebookNewSubscriptionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001023F File Offset: 0x0000E43F
		public FacebookNewSubscriptionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00010249 File Offset: 0x0000E449
		protected FacebookNewSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00010253 File Offset: 0x0000E453
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
