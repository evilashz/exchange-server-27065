using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000317 RID: 791
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobLongRunTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x000510C5 File Offset: 0x0004F2C5
		public RelinquishJobLongRunTransientException() : base(MrsStrings.JobHasBeenRelinquishedDueToLongRun)
		{
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000510D2 File Offset: 0x0004F2D2
		public RelinquishJobLongRunTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToLongRun, innerException)
		{
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000510E0 File Offset: 0x0004F2E0
		protected RelinquishJobLongRunTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000510EA File Offset: 0x0004F2EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
