using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxLoadBalanceTransientException : AnchorTransientException
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002586 File Offset: 0x00000786
		public MailboxLoadBalanceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000258F File Offset: 0x0000078F
		public MailboxLoadBalanceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002599 File Offset: 0x00000799
		protected MailboxLoadBalanceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025A3 File Offset: 0x000007A3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
