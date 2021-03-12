using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018E RID: 398
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationUnknownBatchErrorException : MigrationPermanentException
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x000701C3 File Offset: 0x0006E3C3
		public MigrationUnknownBatchErrorException() : base(Strings.UnknownMigrationBatchError)
		{
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000701D0 File Offset: 0x0006E3D0
		public MigrationUnknownBatchErrorException(Exception innerException) : base(Strings.UnknownMigrationBatchError, innerException)
		{
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000701DE File Offset: 0x0006E3DE
		protected MigrationUnknownBatchErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000701E8 File Offset: 0x0006E3E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
