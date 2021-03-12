using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014E RID: 334
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationMailboxNotFoundException : MigrationPermanentException
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x0006EB19 File Offset: 0x0006CD19
		public MigrationMailboxNotFoundException() : base(Strings.ErrorMigrationMailboxMissingOrInvalid)
		{
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0006EB26 File Offset: 0x0006CD26
		public MigrationMailboxNotFoundException(Exception innerException) : base(Strings.ErrorMigrationMailboxMissingOrInvalid, innerException)
		{
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0006EB34 File Offset: 0x0006CD34
		protected MigrationMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0006EB3E File Offset: 0x0006CD3E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
