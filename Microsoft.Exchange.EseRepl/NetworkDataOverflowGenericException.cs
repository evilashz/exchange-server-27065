using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004E RID: 78
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkDataOverflowGenericException : NetworkTransportException
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00009364 File Offset: 0x00007564
		public NetworkDataOverflowGenericException() : base(Strings.NetworkDataOverflowGeneric)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009376 File Offset: 0x00007576
		public NetworkDataOverflowGenericException(Exception innerException) : base(Strings.NetworkDataOverflowGeneric, innerException)
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009389 File Offset: 0x00007589
		protected NetworkDataOverflowGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009393 File Offset: 0x00007593
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
