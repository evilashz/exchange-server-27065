using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200112C RID: 4396
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationNewBatchUsersShareBatchException : LocalizedException
	{
		// Token: 0x0600B4CB RID: 46283 RVA: 0x0029D57F File Offset: 0x0029B77F
		public MigrationNewBatchUsersShareBatchException() : base(Strings.MigrationNewBatchUsersShareBatch)
		{
		}

		// Token: 0x0600B4CC RID: 46284 RVA: 0x0029D58C File Offset: 0x0029B78C
		public MigrationNewBatchUsersShareBatchException(Exception innerException) : base(Strings.MigrationNewBatchUsersShareBatch, innerException)
		{
		}

		// Token: 0x0600B4CD RID: 46285 RVA: 0x0029D59A File Offset: 0x0029B79A
		protected MigrationNewBatchUsersShareBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4CE RID: 46286 RVA: 0x0029D5A4 File Offset: 0x0029B7A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
