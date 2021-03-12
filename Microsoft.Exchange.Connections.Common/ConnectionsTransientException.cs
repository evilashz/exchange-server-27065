using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionsTransientException : TransientException
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003782 File Offset: 0x00001982
		public ConnectionsTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000378B File Offset: 0x0000198B
		public ConnectionsTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003795 File Offset: 0x00001995
		protected ConnectionsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000379F File Offset: 0x0000199F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
