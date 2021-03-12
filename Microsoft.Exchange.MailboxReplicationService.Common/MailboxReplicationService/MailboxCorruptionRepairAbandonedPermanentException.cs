using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030F RID: 783
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxCorruptionRepairAbandonedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002503 RID: 9475 RVA: 0x00050D7D File Offset: 0x0004EF7D
		public MailboxCorruptionRepairAbandonedPermanentException(DateTime firstRepairAttemptedAt) : base(MrsStrings.IsIntegTooLongError(firstRepairAttemptedAt))
		{
			this.firstRepairAttemptedAt = firstRepairAttemptedAt;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00050D92 File Offset: 0x0004EF92
		public MailboxCorruptionRepairAbandonedPermanentException(DateTime firstRepairAttemptedAt, Exception innerException) : base(MrsStrings.IsIntegTooLongError(firstRepairAttemptedAt), innerException)
		{
			this.firstRepairAttemptedAt = firstRepairAttemptedAt;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00050DA8 File Offset: 0x0004EFA8
		protected MailboxCorruptionRepairAbandonedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.firstRepairAttemptedAt = (DateTime)info.GetValue("firstRepairAttemptedAt", typeof(DateTime));
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x00050DD2 File Offset: 0x0004EFD2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("firstRepairAttemptedAt", this.firstRepairAttemptedAt);
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00050DED File Offset: 0x0004EFED
		public DateTime FirstRepairAttemptedAt
		{
			get
			{
				return this.firstRepairAttemptedAt;
			}
		}

		// Token: 0x04001010 RID: 4112
		private readonly DateTime firstRepairAttemptedAt;
	}
}
