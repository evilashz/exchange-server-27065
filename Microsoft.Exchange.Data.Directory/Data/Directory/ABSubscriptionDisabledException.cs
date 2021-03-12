using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A66 RID: 2662
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABSubscriptionDisabledException : ABOperationException
	{
		// Token: 0x06007EF5 RID: 32501 RVA: 0x001A3C7F File Offset: 0x001A1E7F
		public ABSubscriptionDisabledException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x001A3C88 File Offset: 0x001A1E88
		public ABSubscriptionDisabledException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x001A3C92 File Offset: 0x001A1E92
		protected ABSubscriptionDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x001A3C9C File Offset: 0x001A1E9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
