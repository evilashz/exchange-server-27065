using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FA RID: 762
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RootFolderNotFoundPermananentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600249C RID: 9372 RVA: 0x000503D0 File Offset: 0x0004E5D0
		public RootFolderNotFoundPermananentException() : base(MrsStrings.MailboxRootFolderNotFound)
		{
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000503DD File Offset: 0x0004E5DD
		public RootFolderNotFoundPermananentException(Exception innerException) : base(MrsStrings.MailboxRootFolderNotFound, innerException)
		{
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000503EB File Offset: 0x0004E5EB
		protected RootFolderNotFoundPermananentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000503F5 File Offset: 0x0004E5F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
