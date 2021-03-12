using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000141 RID: 321
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationLocalDatabasesNotFoundException : MigrationPermanentException
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x0006E53D File Offset: 0x0006C73D
		public MigrationLocalDatabasesNotFoundException() : base(Strings.MigrationLocalDatabasesNotFound)
		{
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0006E54A File Offset: 0x0006C74A
		public MigrationLocalDatabasesNotFoundException(Exception innerException) : base(Strings.MigrationLocalDatabasesNotFound, innerException)
		{
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0006E558 File Offset: 0x0006C758
		protected MigrationLocalDatabasesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0006E562 File Offset: 0x0006C762
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
