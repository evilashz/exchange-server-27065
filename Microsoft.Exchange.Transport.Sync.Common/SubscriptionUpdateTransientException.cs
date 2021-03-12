using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000005 RID: 5
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionUpdateTransientException : TransientException
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004821 File Offset: 0x00002A21
		public SubscriptionUpdateTransientException() : base(Strings.SubscriptionUpdateTransientException)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000482E File Offset: 0x00002A2E
		public SubscriptionUpdateTransientException(Exception innerException) : base(Strings.SubscriptionUpdateTransientException, innerException)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000483C File Offset: 0x00002A3C
		protected SubscriptionUpdateTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004846 File Offset: 0x00002A46
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
