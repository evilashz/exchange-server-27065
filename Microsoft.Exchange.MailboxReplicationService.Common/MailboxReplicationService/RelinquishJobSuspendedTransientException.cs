using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000308 RID: 776
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobSuspendedTransientException : RelinquishJobTransientException
	{
		// Token: 0x060024DE RID: 9438 RVA: 0x0005095B File Offset: 0x0004EB5B
		public RelinquishJobSuspendedTransientException() : base(MrsStrings.JobHasBeenRelinquished)
		{
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00050968 File Offset: 0x0004EB68
		public RelinquishJobSuspendedTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquished, innerException)
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00050976 File Offset: 0x0004EB76
		protected RelinquishJobSuspendedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00050980 File Offset: 0x0004EB80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
