using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FC RID: 1020
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendBlockedBackupInProgressException : TaskServerException
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000B992D File Offset: 0x000B7B2D
		public ReplayServiceSuspendBlockedBackupInProgressException() : base(ReplayStrings.ReplayServiceSuspendBlockedBackupInProgressException)
		{
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000B993F File Offset: 0x000B7B3F
		public ReplayServiceSuspendBlockedBackupInProgressException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendBlockedBackupInProgressException, innerException)
		{
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000B9952 File Offset: 0x000B7B52
		protected ReplayServiceSuspendBlockedBackupInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000B995C File Offset: 0x000B7B5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
