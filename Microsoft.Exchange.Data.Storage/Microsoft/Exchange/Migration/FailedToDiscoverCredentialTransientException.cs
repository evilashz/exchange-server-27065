using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018A RID: 394
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToDiscoverCredentialTransientException : FailedToDiscoverNspiServerTransientException
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x000700BE File Offset: 0x0006E2BE
		public FailedToDiscoverCredentialTransientException() : base(Strings.MigrationExchangeCredentialFailure)
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000700CB File Offset: 0x0006E2CB
		public FailedToDiscoverCredentialTransientException(Exception innerException) : base(Strings.MigrationExchangeCredentialFailure, innerException)
		{
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000700D9 File Offset: 0x0006E2D9
		protected FailedToDiscoverCredentialTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000700E3 File Offset: 0x0006E2E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
