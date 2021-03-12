using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004EE RID: 1262
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbHTServiceShuttingDownException : DatabaseHealthTrackerException
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x000C3A79 File Offset: 0x000C1C79
		public DbHTServiceShuttingDownException() : base(ReplayStrings.DbHTServiceShuttingDownException)
		{
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000C3A8B File Offset: 0x000C1C8B
		public DbHTServiceShuttingDownException(Exception innerException) : base(ReplayStrings.DbHTServiceShuttingDownException, innerException)
		{
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000C3A9E File Offset: 0x000C1C9E
		protected DbHTServiceShuttingDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000C3AA8 File Offset: 0x000C1CA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
