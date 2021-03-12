using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F5 RID: 757
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UpdateFolderFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002487 RID: 9351 RVA: 0x0005029C File Offset: 0x0004E49C
		public UpdateFolderFailedTransientException() : base(MrsStrings.UpdateFolderFailed)
		{
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000502A9 File Offset: 0x0004E4A9
		public UpdateFolderFailedTransientException(Exception innerException) : base(MrsStrings.UpdateFolderFailed, innerException)
		{
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000502B7 File Offset: 0x0004E4B7
		protected UpdateFolderFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000502C1 File Offset: 0x0004E4C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
