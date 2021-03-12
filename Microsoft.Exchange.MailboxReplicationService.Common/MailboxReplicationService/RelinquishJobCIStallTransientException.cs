using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000314 RID: 788
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobCIStallTransientException : RelinquishJobTransientException
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x00050FA1 File Offset: 0x0004F1A1
		public RelinquishJobCIStallTransientException() : base(MrsStrings.JobHasBeenRelinquishedDueToCIStall)
		{
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00050FAE File Offset: 0x0004F1AE
		public RelinquishJobCIStallTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToCIStall, innerException)
		{
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00050FBC File Offset: 0x0004F1BC
		protected RelinquishJobCIStallTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00050FC6 File Offset: 0x0004F1C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
