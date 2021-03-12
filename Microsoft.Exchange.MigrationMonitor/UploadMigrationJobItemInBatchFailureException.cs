using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMigrationJobItemInBatchFailureException : LocalizedException
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000093B6 File Offset: 0x000075B6
		public UploadMigrationJobItemInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMigrationJobItemDataInBatch)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000093C3 File Offset: 0x000075C3
		public UploadMigrationJobItemInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMigrationJobItemDataInBatch, innerException)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000093D1 File Offset: 0x000075D1
		protected UploadMigrationJobItemInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000093DB File Offset: 0x000075DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
