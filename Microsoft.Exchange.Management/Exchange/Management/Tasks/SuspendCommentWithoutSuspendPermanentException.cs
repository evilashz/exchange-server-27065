using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED2 RID: 3794
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendCommentWithoutSuspendPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8F3 RID: 43251 RVA: 0x0028ACF9 File Offset: 0x00288EF9
		public SuspendCommentWithoutSuspendPermanentException() : base(Strings.ErrorCannotSpecifySuspendCommentWithoutSuspend)
		{
		}

		// Token: 0x0600A8F4 RID: 43252 RVA: 0x0028AD06 File Offset: 0x00288F06
		public SuspendCommentWithoutSuspendPermanentException(Exception innerException) : base(Strings.ErrorCannotSpecifySuspendCommentWithoutSuspend, innerException)
		{
		}

		// Token: 0x0600A8F5 RID: 43253 RVA: 0x0028AD14 File Offset: 0x00288F14
		protected SuspendCommentWithoutSuspendPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A8F6 RID: 43254 RVA: 0x0028AD1E File Offset: 0x00288F1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
