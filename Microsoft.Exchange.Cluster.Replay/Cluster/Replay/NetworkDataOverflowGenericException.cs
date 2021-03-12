using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B8 RID: 952
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkDataOverflowGenericException : NetworkTransportException
	{
		// Token: 0x060027EE RID: 10222 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		public NetworkDataOverflowGenericException() : base(ReplayStrings.NetworkDataOverflowGeneric)
		{
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000B6DBE File Offset: 0x000B4FBE
		public NetworkDataOverflowGenericException(Exception innerException) : base(ReplayStrings.NetworkDataOverflowGeneric, innerException)
		{
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000B6DD1 File Offset: 0x000B4FD1
		protected NetworkDataOverflowGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000B6DDB File Offset: 0x000B4FDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
