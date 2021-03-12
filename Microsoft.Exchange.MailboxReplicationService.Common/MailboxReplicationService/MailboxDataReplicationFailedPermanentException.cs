using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FD RID: 765
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDataReplicationFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024AA RID: 9386 RVA: 0x000504EF File Offset: 0x0004E6EF
		public MailboxDataReplicationFailedPermanentException(LocalizedString failureReason) : base(MrsStrings.MailboxDataReplicationFailed(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00050504 File Offset: 0x0004E704
		public MailboxDataReplicationFailedPermanentException(LocalizedString failureReason, Exception innerException) : base(MrsStrings.MailboxDataReplicationFailed(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0005051A File Offset: 0x0004E71A
		protected MailboxDataReplicationFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (LocalizedString)info.GetValue("failureReason", typeof(LocalizedString));
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00050544 File Offset: 0x0004E744
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x00050564 File Offset: 0x0004E764
		public LocalizedString FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x04000FFF RID: 4095
		private readonly LocalizedString failureReason;
	}
}
