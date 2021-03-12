using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadWLMResourceStatsLogInBatchFailureException : LocalizedException
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00009358 File Offset: 0x00007558
		public UploadWLMResourceStatsLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadWLMResourceStatsLogInBatch)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009365 File Offset: 0x00007565
		public UploadWLMResourceStatsLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadWLMResourceStatsLogInBatch, innerException)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009373 File Offset: 0x00007573
		protected UploadWLMResourceStatsLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000937D File Offset: 0x0000757D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
