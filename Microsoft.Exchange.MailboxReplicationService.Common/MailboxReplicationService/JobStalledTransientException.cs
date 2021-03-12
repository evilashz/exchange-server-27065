using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030C RID: 780
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobStalledTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x00050A17 File Offset: 0x0004EC17
		public JobStalledTransientException(string jobId, string mdbId, LocalizedString failureReason, string agentName, int agentId) : base(MrsStrings.JobIsStalled(jobId, mdbId, failureReason, agentName, agentId))
		{
			this.jobId = jobId;
			this.mdbId = mdbId;
			this.failureReason = failureReason;
			this.agentName = agentName;
			this.agentId = agentId;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00050A50 File Offset: 0x0004EC50
		public JobStalledTransientException(string jobId, string mdbId, LocalizedString failureReason, string agentName, int agentId, Exception innerException) : base(MrsStrings.JobIsStalled(jobId, mdbId, failureReason, agentName, agentId), innerException)
		{
			this.jobId = jobId;
			this.mdbId = mdbId;
			this.failureReason = failureReason;
			this.agentName = agentName;
			this.agentId = agentId;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00050A8C File Offset: 0x0004EC8C
		protected JobStalledTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.jobId = (string)info.GetValue("jobId", typeof(string));
			this.mdbId = (string)info.GetValue("mdbId", typeof(string));
			this.failureReason = (LocalizedString)info.GetValue("failureReason", typeof(LocalizedString));
			this.agentName = (string)info.GetValue("agentName", typeof(string));
			this.agentId = (int)info.GetValue("agentId", typeof(int));
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00050B44 File Offset: 0x0004ED44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("jobId", this.jobId);
			info.AddValue("mdbId", this.mdbId);
			info.AddValue("failureReason", this.failureReason);
			info.AddValue("agentName", this.agentName);
			info.AddValue("agentId", this.agentId);
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x00050BB3 File Offset: 0x0004EDB3
		public string JobId
		{
			get
			{
				return this.jobId;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00050BBB File Offset: 0x0004EDBB
		public string MdbId
		{
			get
			{
				return this.mdbId;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00050BC3 File Offset: 0x0004EDC3
		public LocalizedString FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00050BCB File Offset: 0x0004EDCB
		public string AgentName
		{
			get
			{
				return this.agentName;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x00050BD3 File Offset: 0x0004EDD3
		public int AgentId
		{
			get
			{
				return this.agentId;
			}
		}

		// Token: 0x04001007 RID: 4103
		private readonly string jobId;

		// Token: 0x04001008 RID: 4104
		private readonly string mdbId;

		// Token: 0x04001009 RID: 4105
		private readonly LocalizedString failureReason;

		// Token: 0x0400100A RID: 4106
		private readonly string agentName;

		// Token: 0x0400100B RID: 4107
		private readonly int agentId;
	}
}
