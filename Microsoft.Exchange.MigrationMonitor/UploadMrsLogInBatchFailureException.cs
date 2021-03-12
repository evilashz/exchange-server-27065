using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000041 RID: 65
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UploadMrsLogInBatchFailureException : LocalizedException
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000926D File Offset: 0x0000746D
		public UploadMrsLogInBatchFailureException() : base(MigrationMonitorStrings.ErrorUploadMrsLogInBatch)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000927A File Offset: 0x0000747A
		public UploadMrsLogInBatchFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorUploadMrsLogInBatch, innerException)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009288 File Offset: 0x00007488
		protected UploadMrsLogInBatchFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009292 File Offset: 0x00007492
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
