using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000313 RID: 787
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobHAStallTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002517 RID: 9495 RVA: 0x00050F72 File Offset: 0x0004F172
		public RelinquishJobHAStallTransientException() : base(MrsStrings.JobHasBeenRelinquishedDueToHAStall)
		{
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00050F7F File Offset: 0x0004F17F
		public RelinquishJobHAStallTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToHAStall, innerException)
		{
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00050F8D File Offset: 0x0004F18D
		protected RelinquishJobHAStallTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00050F97 File Offset: 0x0004F197
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
