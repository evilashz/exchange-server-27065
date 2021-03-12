using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A2 RID: 1186
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToReadDatabasePage : TransientException
	{
		// Token: 0x06002CE4 RID: 11492 RVA: 0x000C0435 File Offset: 0x000BE635
		public FailedToReadDatabasePage(int error) : base(ReplayStrings.FailedToReadDatabasePage(error))
		{
			this.error = error;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000C044A File Offset: 0x000BE64A
		public FailedToReadDatabasePage(int error, Exception innerException) : base(ReplayStrings.FailedToReadDatabasePage(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000C0460 File Offset: 0x000BE660
		protected FailedToReadDatabasePage(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (int)info.GetValue("error", typeof(int));
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000C048A File Offset: 0x000BE68A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000C04A5 File Offset: 0x000BE6A5
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001503 RID: 5379
		private readonly int error;
	}
}
