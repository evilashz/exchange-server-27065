using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018F RID: 399
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationPoisonCountThresholdExceededException : MigrationPermanentException
	{
		// Token: 0x0600171A RID: 5914 RVA: 0x000701F2 File Offset: 0x0006E3F2
		public MigrationPoisonCountThresholdExceededException() : base(Strings.UnknownMigrationBatchError)
		{
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000701FF File Offset: 0x0006E3FF
		public MigrationPoisonCountThresholdExceededException(Exception innerException) : base(Strings.UnknownMigrationBatchError, innerException)
		{
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0007020D File Offset: 0x0006E40D
		protected MigrationPoisonCountThresholdExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00070217 File Offset: 0x0006E417
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
