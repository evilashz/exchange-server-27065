using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000455 RID: 1109
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GranularReplicationOverflowException : TransientException
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x000BD261 File Offset: 0x000BB461
		public GranularReplicationOverflowException() : base(ReplayStrings.GranularReplicationOverflow)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000BD26E File Offset: 0x000BB46E
		public GranularReplicationOverflowException(Exception innerException) : base(ReplayStrings.GranularReplicationOverflow, innerException)
		{
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000BD27C File Offset: 0x000BB47C
		protected GranularReplicationOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000BD286 File Offset: 0x000BB486
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
