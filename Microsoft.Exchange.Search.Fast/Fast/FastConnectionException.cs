using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200002E RID: 46
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FastConnectionException : ComponentFailedTransientException
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public FastConnectionException() : base(Strings.ConnectionFailure)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F100 File Offset: 0x0000D300
		public FastConnectionException(Exception innerException) : base(Strings.ConnectionFailure, innerException)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000F10E File Offset: 0x0000D30E
		protected FastConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000F118 File Offset: 0x0000D318
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
