using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001111 RID: 4369
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationPartitionMailboxAmbiguousException : LocalizedException
	{
		// Token: 0x0600B444 RID: 46148 RVA: 0x0029C862 File Offset: 0x0029AA62
		public MigrationPartitionMailboxAmbiguousException() : base(Strings.MigrationPartitionMailboxAmbiguous)
		{
		}

		// Token: 0x0600B445 RID: 46149 RVA: 0x0029C86F File Offset: 0x0029AA6F
		public MigrationPartitionMailboxAmbiguousException(Exception innerException) : base(Strings.MigrationPartitionMailboxAmbiguous, innerException)
		{
		}

		// Token: 0x0600B446 RID: 46150 RVA: 0x0029C87D File Offset: 0x0029AA7D
		protected MigrationPartitionMailboxAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B447 RID: 46151 RVA: 0x0029C887 File Offset: 0x0029AA87
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
