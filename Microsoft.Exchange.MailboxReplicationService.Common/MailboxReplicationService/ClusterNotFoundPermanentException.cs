using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A5 RID: 677
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x0004DB07 File Offset: 0x0004BD07
		public ClusterNotFoundPermanentException() : base(MrsStrings.ClusterNotFound)
		{
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0004DB14 File Offset: 0x0004BD14
		public ClusterNotFoundPermanentException(Exception innerException) : base(MrsStrings.ClusterNotFound, innerException)
		{
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0004DB22 File Offset: 0x0004BD22
		protected ClusterNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0004DB2C File Offset: 0x0004BD2C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
