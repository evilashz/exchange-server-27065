using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000475 RID: 1141
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServiceShuttingDownException : AmCommonException
	{
		// Token: 0x06002BE5 RID: 11237 RVA: 0x000BE41C File Offset: 0x000BC61C
		public AmServiceShuttingDownException() : base(ReplayStrings.AmServiceShuttingDown)
		{
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000BE42E File Offset: 0x000BC62E
		public AmServiceShuttingDownException(Exception innerException) : base(ReplayStrings.AmServiceShuttingDown, innerException)
		{
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000BE441 File Offset: 0x000BC641
		protected AmServiceShuttingDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000BE44B File Offset: 0x000BC64B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
