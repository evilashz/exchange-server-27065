using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033C RID: 828
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptSyncStatePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060025E7 RID: 9703 RVA: 0x0005245D File Offset: 0x0005065D
		public CorruptSyncStatePermanentException(string mbxId) : base(MrsStrings.ReportFailingMoveBecauseSyncStateIssue(mbxId))
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00052472 File Offset: 0x00050672
		public CorruptSyncStatePermanentException(string mbxId, Exception innerException) : base(MrsStrings.ReportFailingMoveBecauseSyncStateIssue(mbxId), innerException)
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00052488 File Offset: 0x00050688
		protected CorruptSyncStatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxId = (string)info.GetValue("mbxId", typeof(string));
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000524B2 File Offset: 0x000506B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxId", this.mbxId);
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000524CD File Offset: 0x000506CD
		public string MbxId
		{
			get
			{
				return this.mbxId;
			}
		}

		// Token: 0x04001040 RID: 4160
		private readonly string mbxId;
	}
}
