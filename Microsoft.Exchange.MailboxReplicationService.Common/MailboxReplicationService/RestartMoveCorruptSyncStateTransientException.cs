using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033B RID: 827
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestartMoveCorruptSyncStateTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025E2 RID: 9698 RVA: 0x000523E5 File Offset: 0x000505E5
		public RestartMoveCorruptSyncStateTransientException(string mbxId) : base(MrsStrings.ReportRestartingMoveBecauseSyncStateDoesNotExist(mbxId))
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000523FA File Offset: 0x000505FA
		public RestartMoveCorruptSyncStateTransientException(string mbxId, Exception innerException) : base(MrsStrings.ReportRestartingMoveBecauseSyncStateDoesNotExist(mbxId), innerException)
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00052410 File Offset: 0x00050610
		protected RestartMoveCorruptSyncStateTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxId = (string)info.GetValue("mbxId", typeof(string));
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0005243A File Offset: 0x0005063A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxId", this.mbxId);
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00052455 File Offset: 0x00050655
		public string MbxId
		{
			get
			{
				return this.mbxId;
			}
		}

		// Token: 0x0400103F RID: 4159
		private readonly string mbxId;
	}
}
