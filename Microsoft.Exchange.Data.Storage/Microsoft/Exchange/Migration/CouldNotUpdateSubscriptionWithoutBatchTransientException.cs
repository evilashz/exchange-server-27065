using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotUpdateSubscriptionWithoutBatchTransientException : MigrationTransientException
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x0006EF22 File Offset: 0x0006D122
		public CouldNotUpdateSubscriptionWithoutBatchTransientException() : base(Strings.CouldNotUpdateSubscriptionSettingsWithoutBatch)
		{
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0006EF2F File Offset: 0x0006D12F
		public CouldNotUpdateSubscriptionWithoutBatchTransientException(Exception innerException) : base(Strings.CouldNotUpdateSubscriptionSettingsWithoutBatch, innerException)
		{
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0006EF3D File Offset: 0x0006D13D
		protected CouldNotUpdateSubscriptionWithoutBatchTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0006EF47 File Offset: 0x0006D147
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
