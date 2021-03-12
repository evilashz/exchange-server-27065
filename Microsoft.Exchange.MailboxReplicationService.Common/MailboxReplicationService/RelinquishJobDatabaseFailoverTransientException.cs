using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000311 RID: 785
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobDatabaseFailoverTransientException : RelinquishJobTransientException
	{
		// Token: 0x0600250E RID: 9486 RVA: 0x00050ECB File Offset: 0x0004F0CB
		public RelinquishJobDatabaseFailoverTransientException() : base(MrsStrings.JobHasBeenRelinquishedDueToDatabaseFailover)
		{
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		public RelinquishJobDatabaseFailoverTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToDatabaseFailover, innerException)
		{
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00050EE6 File Offset: 0x0004F0E6
		protected RelinquishJobDatabaseFailoverTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00050EF0 File Offset: 0x0004F0F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
