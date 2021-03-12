using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000320 RID: 800
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSignatureChangedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002559 RID: 9561 RVA: 0x000515FA File Offset: 0x0004F7FA
		public MailboxSignatureChangedTransientException() : base(MrsStrings.MoveRestartedDueToSignatureChange)
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00051607 File Offset: 0x0004F807
		public MailboxSignatureChangedTransientException(Exception innerException) : base(MrsStrings.MoveRestartedDueToSignatureChange, innerException)
		{
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00051615 File Offset: 0x0004F815
		protected MailboxSignatureChangedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0005161F File Offset: 0x0004F81F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
