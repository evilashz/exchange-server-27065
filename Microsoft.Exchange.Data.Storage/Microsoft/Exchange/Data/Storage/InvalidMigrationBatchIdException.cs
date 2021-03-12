using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200010C RID: 268
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidMigrationBatchIdException : MigrationPermanentException
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x00069CAF File Offset: 0x00067EAF
		public InvalidMigrationBatchIdException() : base(ServerStrings.InvalidMigrationBatchId)
		{
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00069CBC File Offset: 0x00067EBC
		public InvalidMigrationBatchIdException(Exception innerException) : base(ServerStrings.InvalidMigrationBatchId, innerException)
		{
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00069CCA File Offset: 0x00067ECA
		protected InvalidMigrationBatchIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00069CD4 File Offset: 0x00067ED4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
