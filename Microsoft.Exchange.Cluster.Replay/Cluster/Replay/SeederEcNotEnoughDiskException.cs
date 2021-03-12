using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048A RID: 1162
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederEcNotEnoughDiskException : SeederServerException
	{
		// Token: 0x06002C5F RID: 11359 RVA: 0x000BF44D File Offset: 0x000BD64D
		public SeederEcNotEnoughDiskException() : base(ReplayStrings.SeederEcNotEnoughDiskException)
		{
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000BF45F File Offset: 0x000BD65F
		public SeederEcNotEnoughDiskException(Exception innerException) : base(ReplayStrings.SeederEcNotEnoughDiskException, innerException)
		{
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000BF472 File Offset: 0x000BD672
		protected SeederEcNotEnoughDiskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000BF47C File Offset: 0x000BD67C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
