using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001110 RID: 4368
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationPartitionMailboxNotFoundException : LocalizedException
	{
		// Token: 0x0600B440 RID: 46144 RVA: 0x0029C833 File Offset: 0x0029AA33
		public MigrationPartitionMailboxNotFoundException() : base(Strings.MigrationPartitionMailboxNotFound)
		{
		}

		// Token: 0x0600B441 RID: 46145 RVA: 0x0029C840 File Offset: 0x0029AA40
		public MigrationPartitionMailboxNotFoundException(Exception innerException) : base(Strings.MigrationPartitionMailboxNotFound, innerException)
		{
		}

		// Token: 0x0600B442 RID: 46146 RVA: 0x0029C84E File Offset: 0x0029AA4E
		protected MigrationPartitionMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B443 RID: 46147 RVA: 0x0029C858 File Offset: 0x0029AA58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
