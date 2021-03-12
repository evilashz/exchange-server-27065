using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RemoteMailboxLoadBalancePermanentException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025AD File Offset: 0x000007AD
		public RemoteMailboxLoadBalancePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B6 File Offset: 0x000007B6
		public RemoteMailboxLoadBalancePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C0 File Offset: 0x000007C0
		protected RemoteMailboxLoadBalancePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025CA File Offset: 0x000007CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
