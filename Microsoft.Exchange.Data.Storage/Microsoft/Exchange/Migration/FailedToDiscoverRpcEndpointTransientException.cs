using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018B RID: 395
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToDiscoverRpcEndpointTransientException : FailedToDiscoverNspiServerTransientException
	{
		// Token: 0x06001709 RID: 5897 RVA: 0x000700ED File Offset: 0x0006E2ED
		public FailedToDiscoverRpcEndpointTransientException() : base(Strings.MigrationExchangeRpcConnectionFailure)
		{
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000700FA File Offset: 0x0006E2FA
		public FailedToDiscoverRpcEndpointTransientException(Exception innerException) : base(Strings.MigrationExchangeRpcConnectionFailure, innerException)
		{
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00070108 File Offset: 0x0006E308
		protected FailedToDiscoverRpcEndpointTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00070112 File Offset: 0x0006E312
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
