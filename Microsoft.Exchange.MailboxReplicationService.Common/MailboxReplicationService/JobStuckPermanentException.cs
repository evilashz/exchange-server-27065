using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030E RID: 782
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobStuckPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024FD RID: 9469 RVA: 0x00050CAE File Offset: 0x0004EEAE
		public JobStuckPermanentException(DateTime lastProgressTimestamp, DateTime jobPickupTimestamp) : base(MrsStrings.JobIsStuck(lastProgressTimestamp, jobPickupTimestamp))
		{
			this.lastProgressTimestamp = lastProgressTimestamp;
			this.jobPickupTimestamp = jobPickupTimestamp;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00050CCB File Offset: 0x0004EECB
		public JobStuckPermanentException(DateTime lastProgressTimestamp, DateTime jobPickupTimestamp, Exception innerException) : base(MrsStrings.JobIsStuck(lastProgressTimestamp, jobPickupTimestamp), innerException)
		{
			this.lastProgressTimestamp = lastProgressTimestamp;
			this.jobPickupTimestamp = jobPickupTimestamp;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00050CEC File Offset: 0x0004EEEC
		protected JobStuckPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lastProgressTimestamp = (DateTime)info.GetValue("lastProgressTimestamp", typeof(DateTime));
			this.jobPickupTimestamp = (DateTime)info.GetValue("jobPickupTimestamp", typeof(DateTime));
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00050D41 File Offset: 0x0004EF41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lastProgressTimestamp", this.lastProgressTimestamp);
			info.AddValue("jobPickupTimestamp", this.jobPickupTimestamp);
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00050D6D File Offset: 0x0004EF6D
		public DateTime LastProgressTimestamp
		{
			get
			{
				return this.lastProgressTimestamp;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x00050D75 File Offset: 0x0004EF75
		public DateTime JobPickupTimestamp
		{
			get
			{
				return this.jobPickupTimestamp;
			}
		}

		// Token: 0x0400100E RID: 4110
		private readonly DateTime lastProgressTimestamp;

		// Token: 0x0400100F RID: 4111
		private readonly DateTime jobPickupTimestamp;
	}
}
