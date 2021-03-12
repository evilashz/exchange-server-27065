using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018D RID: 397
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationUnknownException : MigrationPermanentException
	{
		// Token: 0x06001712 RID: 5906 RVA: 0x00070194 File Offset: 0x0006E394
		public MigrationUnknownException() : base(Strings.UnknownMigrationError)
		{
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000701A1 File Offset: 0x0006E3A1
		public MigrationUnknownException(Exception innerException) : base(Strings.UnknownMigrationError, innerException)
		{
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000701AF File Offset: 0x0006E3AF
		protected MigrationUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000701B9 File Offset: 0x0006E3B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
