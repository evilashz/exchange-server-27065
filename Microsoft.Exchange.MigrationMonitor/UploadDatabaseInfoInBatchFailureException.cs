using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadDatabaseInfoInBatchFailureException : LocalizedException
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000923E File Offset: 0x0000743E
		public UploadDatabaseInfoInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadDatabaseInfoInBatch)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000924B File Offset: 0x0000744B
		public UploadDatabaseInfoInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadDatabaseInfoInBatch, innerException)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009259 File Offset: 0x00007459
		protected UploadDatabaseInfoInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009263 File Offset: 0x00007463
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
