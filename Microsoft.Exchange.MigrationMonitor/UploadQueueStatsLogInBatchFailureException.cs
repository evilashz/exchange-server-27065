using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadQueueStatsLogInBatchFailureException : LocalizedException
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00009329 File Offset: 0x00007529
		public UploadQueueStatsLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadQueueStatsLogInBatch)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009336 File Offset: 0x00007536
		public UploadQueueStatsLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadQueueStatsLogInBatch, innerException)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009344 File Offset: 0x00007544
		protected UploadQueueStatsLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000934E File Offset: 0x0000754E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
