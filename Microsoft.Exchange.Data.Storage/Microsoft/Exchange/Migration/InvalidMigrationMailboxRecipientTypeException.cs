using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidMigrationMailboxRecipientTypeException : MigrationPermanentException
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x0006EB77 File Offset: 0x0006CD77
		public InvalidMigrationMailboxRecipientTypeException() : base(Strings.ErrorMigrationMailboxMissingOrInvalid)
		{
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0006EB84 File Offset: 0x0006CD84
		public InvalidMigrationMailboxRecipientTypeException(Exception innerException) : base(Strings.ErrorMigrationMailboxMissingOrInvalid, innerException)
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0006EB92 File Offset: 0x0006CD92
		protected InvalidMigrationMailboxRecipientTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0006EB9C File Offset: 0x0006CD9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
