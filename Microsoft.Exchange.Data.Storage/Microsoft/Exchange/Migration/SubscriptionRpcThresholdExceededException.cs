using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000186 RID: 390
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionRpcThresholdExceededException : MigrationPermanentException
	{
		// Token: 0x060016F4 RID: 5876 RVA: 0x0006FFC1 File Offset: 0x0006E1C1
		public SubscriptionRpcThresholdExceededException() : base(Strings.SubscriptionRpcThresholdExceeded)
		{
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0006FFCE File Offset: 0x0006E1CE
		public SubscriptionRpcThresholdExceededException(Exception innerException) : base(Strings.SubscriptionRpcThresholdExceeded, innerException)
		{
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0006FFDC File Offset: 0x0006E1DC
		protected SubscriptionRpcThresholdExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0006FFE6 File Offset: 0x0006E1E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
