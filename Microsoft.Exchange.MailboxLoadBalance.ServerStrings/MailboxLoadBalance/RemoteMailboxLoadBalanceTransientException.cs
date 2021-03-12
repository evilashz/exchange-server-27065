using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RemoteMailboxLoadBalanceTransientException : MailboxLoadBalanceTransientException
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000025D4 File Offset: 0x000007D4
		public RemoteMailboxLoadBalanceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025DD File Offset: 0x000007DD
		public RemoteMailboxLoadBalanceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025E7 File Offset: 0x000007E7
		protected RemoteMailboxLoadBalanceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025F1 File Offset: 0x000007F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
