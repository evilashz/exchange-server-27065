using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B7 RID: 1207
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogCopierFailedToGetSuspendLockException : TransientException
	{
		// Token: 0x06002D61 RID: 11617 RVA: 0x000C14D1 File Offset: 0x000BF6D1
		public LogCopierFailedToGetSuspendLockException() : base(ReplayStrings.LogCopierFailedToGetSuspendLock)
		{
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000C14DE File Offset: 0x000BF6DE
		public LogCopierFailedToGetSuspendLockException(Exception innerException) : base(ReplayStrings.LogCopierFailedToGetSuspendLock, innerException)
		{
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000C14EC File Offset: 0x000BF6EC
		protected LogCopierFailedToGetSuspendLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000C14F6 File Offset: 0x000BF6F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
