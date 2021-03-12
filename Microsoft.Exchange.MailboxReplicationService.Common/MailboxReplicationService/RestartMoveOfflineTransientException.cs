using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033A RID: 826
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestartMoveOfflineTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025DD RID: 9693 RVA: 0x00052368 File Offset: 0x00050568
		public RestartMoveOfflineTransientException(LocalizedString mbxId) : base(MrsStrings.ReportRemovingTargetMailboxDueToOfflineMoveFailure(mbxId))
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0005237D File Offset: 0x0005057D
		public RestartMoveOfflineTransientException(LocalizedString mbxId, Exception innerException) : base(MrsStrings.ReportRemovingTargetMailboxDueToOfflineMoveFailure(mbxId), innerException)
		{
			this.mbxId = mbxId;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00052393 File Offset: 0x00050593
		protected RestartMoveOfflineTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxId = (LocalizedString)info.GetValue("mbxId", typeof(LocalizedString));
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000523BD File Offset: 0x000505BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxId", this.mbxId);
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000523DD File Offset: 0x000505DD
		public LocalizedString MbxId
		{
			get
			{
				return this.mbxId;
			}
		}

		// Token: 0x0400103E RID: 4158
		private readonly LocalizedString mbxId;
	}
}
