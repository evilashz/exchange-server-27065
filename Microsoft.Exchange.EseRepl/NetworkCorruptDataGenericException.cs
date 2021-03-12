using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCorruptDataGenericException : NetworkTransportException
	{
		// Token: 0x0600026F RID: 623 RVA: 0x000092A9 File Offset: 0x000074A9
		public NetworkCorruptDataGenericException() : base(Strings.NetworkCorruptDataGeneric)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000092BB File Offset: 0x000074BB
		public NetworkCorruptDataGenericException(Exception innerException) : base(Strings.NetworkCorruptDataGeneric, innerException)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000092CE File Offset: 0x000074CE
		protected NetworkCorruptDataGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000092D8 File Offset: 0x000074D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
