using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000189 RID: 393
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxReplicationTransientException : TransientException
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0002176C File Offset: 0x0001F96C
		public MailboxReplicationTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00021775 File Offset: 0x0001F975
		public MailboxReplicationTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002177F File Offset: 0x0001F97F
		protected MailboxReplicationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00021789 File Offset: 0x0001F989
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
