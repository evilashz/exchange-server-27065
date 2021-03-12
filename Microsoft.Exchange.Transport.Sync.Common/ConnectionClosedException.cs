using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionClosedException : NonPromotableTransientException
	{
		// Token: 0x06000235 RID: 565 RVA: 0x000065CF File Offset: 0x000047CF
		public ConnectionClosedException() : base(Strings.ConnectionClosedException)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000065DC File Offset: 0x000047DC
		public ConnectionClosedException(Exception innerException) : base(Strings.ConnectionClosedException, innerException)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000065EA File Offset: 0x000047EA
		protected ConnectionClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000065F4 File Offset: 0x000047F4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
