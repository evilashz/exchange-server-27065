using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000517 RID: 1303
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseCopyLayoutTableNullException : DatabaseCopyLayoutException
	{
		// Token: 0x06002F89 RID: 12169 RVA: 0x000C5BDD File Offset: 0x000C3DDD
		public DatabaseCopyLayoutTableNullException() : base(ReplayStrings.DatabaseCopyLayoutTableNullException)
		{
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000C5BEF File Offset: 0x000C3DEF
		public DatabaseCopyLayoutTableNullException(Exception innerException) : base(ReplayStrings.DatabaseCopyLayoutTableNullException, innerException)
		{
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000C5C02 File Offset: 0x000C3E02
		protected DatabaseCopyLayoutTableNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000C5C0C File Offset: 0x000C3E0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
