using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000319 RID: 793
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobOfflineTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002532 RID: 9522 RVA: 0x0005116C File Offset: 0x0004F36C
		public RelinquishJobOfflineTransientException(DateTime pickupTime) : base(MrsStrings.JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove(pickupTime))
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00051181 File Offset: 0x0004F381
		public RelinquishJobOfflineTransientException(DateTime pickupTime, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove(pickupTime), innerException)
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00051197 File Offset: 0x0004F397
		protected RelinquishJobOfflineTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pickupTime = (DateTime)info.GetValue("pickupTime", typeof(DateTime));
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000511C1 File Offset: 0x0004F3C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pickupTime", this.pickupTime);
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x000511DC File Offset: 0x0004F3DC
		public DateTime PickupTime
		{
			get
			{
				return this.pickupTime;
			}
		}

		// Token: 0x04001017 RID: 4119
		private readonly DateTime pickupTime;
	}
}
