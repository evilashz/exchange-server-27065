using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000322 RID: 802
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ContainerMailboxesChangedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002561 RID: 9569 RVA: 0x00051658 File Offset: 0x0004F858
		public ContainerMailboxesChangedTransientException() : base(MrsStrings.MoveRestartDueToContainerMailboxesChanged)
		{
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00051665 File Offset: 0x0004F865
		public ContainerMailboxesChangedTransientException(Exception innerException) : base(MrsStrings.MoveRestartDueToContainerMailboxesChanged, innerException)
		{
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00051673 File Offset: 0x0004F873
		protected ContainerMailboxesChangedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0005167D File Offset: 0x0004F87D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
