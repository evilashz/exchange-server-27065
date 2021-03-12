using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMailboxStatsInBatchFailureException : LocalizedException
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000920F File Offset: 0x0000740F
		public UploadMailboxStatsInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMailboxStatsInBatch)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000921C File Offset: 0x0000741C
		public UploadMailboxStatsInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMailboxStatsInBatch, innerException)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000922A File Offset: 0x0000742A
		protected UploadMailboxStatsInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009234 File Offset: 0x00007434
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
