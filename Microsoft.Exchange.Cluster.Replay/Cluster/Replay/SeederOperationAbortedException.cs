using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000443 RID: 1091
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederOperationAbortedException : SeederServerException
	{
		// Token: 0x06002ADE RID: 10974 RVA: 0x000BC639 File Offset: 0x000BA839
		public SeederOperationAbortedException() : base(ReplayStrings.SeederOperationAborted)
		{
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000BC64B File Offset: 0x000BA84B
		public SeederOperationAbortedException(Exception innerException) : base(ReplayStrings.SeederOperationAborted, innerException)
		{
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000BC65E File Offset: 0x000BA85E
		protected SeederOperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000BC668 File Offset: 0x000BA868
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
