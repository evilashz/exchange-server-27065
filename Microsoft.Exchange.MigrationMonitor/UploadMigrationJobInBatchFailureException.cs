using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMigrationJobInBatchFailureException : LocalizedException
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00009387 File Offset: 0x00007587
		public UploadMigrationJobInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMigrationJobDataInBatch)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009394 File Offset: 0x00007594
		public UploadMigrationJobInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMigrationJobDataInBatch, innerException)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000093A2 File Offset: 0x000075A2
		protected UploadMigrationJobInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000093AC File Offset: 0x000075AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
