using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadDrumTestingResultLogInBatchFailureException : LocalizedException
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x000092CB File Offset: 0x000074CB
		public UploadDrumTestingResultLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMrsDrumTestingLogInBatch)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000092D8 File Offset: 0x000074D8
		public UploadDrumTestingResultLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMrsDrumTestingLogInBatch, innerException)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000092E6 File Offset: 0x000074E6
		protected UploadDrumTestingResultLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000092F0 File Offset: 0x000074F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
