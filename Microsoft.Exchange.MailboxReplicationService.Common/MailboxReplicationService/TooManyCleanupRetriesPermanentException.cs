using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000329 RID: 809
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyCleanupRetriesPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002581 RID: 9601 RVA: 0x000518D6 File Offset: 0x0004FAD6
		public TooManyCleanupRetriesPermanentException() : base(MrsStrings.ErrorTooManyCleanupRetries)
		{
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000518E3 File Offset: 0x0004FAE3
		public TooManyCleanupRetriesPermanentException(Exception innerException) : base(MrsStrings.ErrorTooManyCleanupRetries, innerException)
		{
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000518F1 File Offset: 0x0004FAF1
		protected TooManyCleanupRetriesPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000518FB File Offset: 0x0004FAFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
