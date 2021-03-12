using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030D RID: 781
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobStalledPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x00050BDB File Offset: 0x0004EDDB
		public JobStalledPermanentException(LocalizedString failureReason, int agentId) : base(MrsStrings.JobIsStalledAndFailed(failureReason, agentId))
		{
			this.failureReason = failureReason;
			this.agentId = agentId;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00050BF8 File Offset: 0x0004EDF8
		public JobStalledPermanentException(LocalizedString failureReason, int agentId, Exception innerException) : base(MrsStrings.JobIsStalledAndFailed(failureReason, agentId), innerException)
		{
			this.failureReason = failureReason;
			this.agentId = agentId;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00050C18 File Offset: 0x0004EE18
		protected JobStalledPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (LocalizedString)info.GetValue("failureReason", typeof(LocalizedString));
			this.agentId = (int)info.GetValue("agentId", typeof(int));
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00050C6D File Offset: 0x0004EE6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
			info.AddValue("agentId", this.agentId);
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00050C9E File Offset: 0x0004EE9E
		public LocalizedString FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00050CA6 File Offset: 0x0004EEA6
		public int AgentId
		{
			get
			{
				return this.agentId;
			}
		}

		// Token: 0x0400100C RID: 4108
		private readonly LocalizedString failureReason;

		// Token: 0x0400100D RID: 4109
		private readonly int agentId;
	}
}
