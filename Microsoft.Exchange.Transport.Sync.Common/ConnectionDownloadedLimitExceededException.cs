using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000020 RID: 32
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionDownloadedLimitExceededException : NonPromotableTransientException
	{
		// Token: 0x0600014A RID: 330 RVA: 0x000053C6 File Offset: 0x000035C6
		public ConnectionDownloadedLimitExceededException() : base(Strings.ConnectionDownloadedLimitExceededException)
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000053D3 File Offset: 0x000035D3
		public ConnectionDownloadedLimitExceededException(Exception innerException) : base(Strings.ConnectionDownloadedLimitExceededException, innerException)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000053E1 File Offset: 0x000035E1
		protected ConnectionDownloadedLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000053EB File Offset: 0x000035EB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
