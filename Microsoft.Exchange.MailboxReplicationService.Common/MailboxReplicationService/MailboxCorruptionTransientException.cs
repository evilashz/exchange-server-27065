using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000321 RID: 801
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxCorruptionTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x00051629 File Offset: 0x0004F829
		public MailboxCorruptionTransientException() : base(MrsStrings.MoveRestartDueToIsIntegCheck)
		{
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00051636 File Offset: 0x0004F836
		public MailboxCorruptionTransientException(Exception innerException) : base(MrsStrings.MoveRestartDueToIsIntegCheck, innerException)
		{
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00051644 File Offset: 0x0004F844
		protected MailboxCorruptionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0005164E File Offset: 0x0004F84E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
