using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMigrationEndpointInBatchFailureException : LocalizedException
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00009533 File Offset: 0x00007733
		public UploadMigrationEndpointInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMigrationEndpointDataInBatch)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009540 File Offset: 0x00007740
		public UploadMigrationEndpointInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMigrationEndpointDataInBatch, innerException)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000954E File Offset: 0x0000774E
		protected UploadMigrationEndpointInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009558 File Offset: 0x00007758
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
