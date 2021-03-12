using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002EB RID: 747
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskSchedulerStoppedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x0004FF4A File Offset: 0x0004E14A
		public TaskSchedulerStoppedPermanentException() : base(MrsStrings.TaskSchedulerStopped)
		{
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0004FF57 File Offset: 0x0004E157
		public TaskSchedulerStoppedPermanentException(Exception innerException) : base(MrsStrings.TaskSchedulerStopped, innerException)
		{
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0004FF65 File Offset: 0x0004E165
		protected TaskSchedulerStoppedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0004FF6F File Offset: 0x0004E16F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
