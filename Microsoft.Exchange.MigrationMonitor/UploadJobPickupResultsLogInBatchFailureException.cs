using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadJobPickupResultsLogInBatchFailureException : LocalizedException
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x000092FA File Offset: 0x000074FA
		public UploadJobPickupResultsLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadJobPickupResultsLogInBatch)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009307 File Offset: 0x00007507
		public UploadJobPickupResultsLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadJobPickupResultsLogInBatch, innerException)
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009315 File Offset: 0x00007515
		protected UploadJobPickupResultsLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000931F File Offset: 0x0000751F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
