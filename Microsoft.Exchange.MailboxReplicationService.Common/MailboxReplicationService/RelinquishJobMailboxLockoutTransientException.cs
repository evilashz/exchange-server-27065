using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000312 RID: 786
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobMailboxLockoutTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x00050EFA File Offset: 0x0004F0FA
		public RelinquishJobMailboxLockoutTransientException(DateTime pickupTime) : base(MrsStrings.JobHasBeenRelinquishedDueToMailboxLockout(pickupTime))
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00050F0F File Offset: 0x0004F10F
		public RelinquishJobMailboxLockoutTransientException(DateTime pickupTime, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToMailboxLockout(pickupTime), innerException)
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00050F25 File Offset: 0x0004F125
		protected RelinquishJobMailboxLockoutTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pickupTime = (DateTime)info.GetValue("pickupTime", typeof(DateTime));
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00050F4F File Offset: 0x0004F14F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pickupTime", this.pickupTime);
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00050F6A File Offset: 0x0004F16A
		public DateTime PickupTime
		{
			get
			{
				return this.pickupTime;
			}
		}

		// Token: 0x04001013 RID: 4115
		private readonly DateTime pickupTime;
	}
}
