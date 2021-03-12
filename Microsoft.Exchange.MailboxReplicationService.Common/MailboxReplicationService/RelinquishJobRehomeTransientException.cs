using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030A RID: 778
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobRehomeTransientException : RelinquishJobTransientException
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x000509B9 File Offset: 0x0004EBB9
		public RelinquishJobRehomeTransientException() : base(MrsStrings.JobHasBeenRelinquished)
		{
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000509C6 File Offset: 0x0004EBC6
		public RelinquishJobRehomeTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquished, innerException)
		{
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000509D4 File Offset: 0x0004EBD4
		protected RelinquishJobRehomeTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000509DE File Offset: 0x0004EBDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
