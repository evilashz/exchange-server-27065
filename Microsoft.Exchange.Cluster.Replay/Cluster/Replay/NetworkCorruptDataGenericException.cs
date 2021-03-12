using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B6 RID: 950
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCorruptDataGenericException : NetworkTransportException
	{
		// Token: 0x060027E5 RID: 10213 RVA: 0x000B6CF1 File Offset: 0x000B4EF1
		public NetworkCorruptDataGenericException() : base(ReplayStrings.NetworkCorruptDataGeneric)
		{
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000B6D03 File Offset: 0x000B4F03
		public NetworkCorruptDataGenericException(Exception innerException) : base(ReplayStrings.NetworkCorruptDataGeneric, innerException)
		{
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000B6D16 File Offset: 0x000B4F16
		protected NetworkCorruptDataGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000B6D20 File Offset: 0x000B4F20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
