using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200029F RID: 671
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSProxyConnectionLimitReachedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060022DB RID: 8923 RVA: 0x0004D97B File Offset: 0x0004BB7B
		public MRSProxyConnectionLimitReachedTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0004D984 File Offset: 0x0004BB84
		public MRSProxyConnectionLimitReachedTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0004D98E File Offset: 0x0004BB8E
		protected MRSProxyConnectionLimitReachedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0004D998 File Offset: 0x0004BB98
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
