using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMrsAvailabilityLogInBatchFailureException : LocalizedException
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000929C File Offset: 0x0000749C
		public UploadMrsAvailabilityLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMrsAvailabilityLogInBatch)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000092A9 File Offset: 0x000074A9
		public UploadMrsAvailabilityLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMrsAvailabilityLogInBatch, innerException)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000092B7 File Offset: 0x000074B7
		protected UploadMrsAvailabilityLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000092C1 File Offset: 0x000074C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
