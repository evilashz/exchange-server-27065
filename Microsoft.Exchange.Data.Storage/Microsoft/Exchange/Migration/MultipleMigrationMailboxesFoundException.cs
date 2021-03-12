using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleMigrationMailboxesFoundException : MigrationPermanentException
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x0006EB48 File Offset: 0x0006CD48
		public MultipleMigrationMailboxesFoundException() : base(Strings.ErrorMigrationMailboxMissingOrInvalid)
		{
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0006EB55 File Offset: 0x0006CD55
		public MultipleMigrationMailboxesFoundException(Exception innerException) : base(Strings.ErrorMigrationMailboxMissingOrInvalid, innerException)
		{
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0006EB63 File Offset: 0x0006CD63
		protected MultipleMigrationMailboxesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0006EB6D File Offset: 0x0006CD6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
