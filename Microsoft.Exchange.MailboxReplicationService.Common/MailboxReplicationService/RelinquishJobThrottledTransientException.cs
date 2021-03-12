using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000309 RID: 777
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobThrottledTransientException : RelinquishJobTransientException
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x0005098A File Offset: 0x0004EB8A
		public RelinquishJobThrottledTransientException() : base(MrsStrings.JobHasBeenRelinquished)
		{
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00050997 File Offset: 0x0004EB97
		public RelinquishJobThrottledTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquished, innerException)
		{
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000509A5 File Offset: 0x0004EBA5
		protected RelinquishJobThrottledTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000509AF File Offset: 0x0004EBAF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
