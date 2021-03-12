using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CB RID: 1227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateClusterIsNotRunningException : RepairStateException
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000C23A3 File Offset: 0x000C05A3
		public RepairStateClusterIsNotRunningException() : base(ReplayStrings.RepairStateClusterIsNotRunning)
		{
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000C23B5 File Offset: 0x000C05B5
		public RepairStateClusterIsNotRunningException(Exception innerException) : base(ReplayStrings.RepairStateClusterIsNotRunning, innerException)
		{
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000C23C8 File Offset: 0x000C05C8
		protected RepairStateClusterIsNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000C23D2 File Offset: 0x000C05D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
