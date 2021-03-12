using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000060 RID: 96
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionInvalidVersionException : TransientException
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public SubscriptionInvalidVersionException() : base(Strings.SubscriptionInvalidVersionException)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public SubscriptionInvalidVersionException(Exception innerException) : base(Strings.SubscriptionInvalidVersionException, innerException)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006BD3 File Offset: 0x00004DD3
		protected SubscriptionInvalidVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006BDD File Offset: 0x00004DDD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
