using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005F RID: 95
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionInvalidPasswordException : TransientException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00006B89 File Offset: 0x00004D89
		public SubscriptionInvalidPasswordException() : base(Strings.SubscriptionInvalidPasswordException)
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006B96 File Offset: 0x00004D96
		public SubscriptionInvalidPasswordException(Exception innerException) : base(Strings.SubscriptionInvalidPasswordException, innerException)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00006BA4 File Offset: 0x00004DA4
		protected SubscriptionInvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006BAE File Offset: 0x00004DAE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
