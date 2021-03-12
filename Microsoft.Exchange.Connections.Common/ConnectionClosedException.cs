using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003A RID: 58
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionClosedException : TransientException
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public ConnectionClosedException() : base(CXStrings.ConnectionClosedError)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public ConnectionClosedException(Exception innerException) : base(CXStrings.ConnectionClosedError, innerException)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003AC3 File Offset: 0x00001CC3
		protected ConnectionClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003ACD File Offset: 0x00001CCD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
