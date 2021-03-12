using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000315 RID: 789
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobDGTimeoutTransientException : RelinquishJobTransientException
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		public RelinquishJobDGTimeoutTransientException(DateTime pickupTime) : base(MrsStrings.JobHasBeenRelinquishedDueToDataGuaranteeTimeout(pickupTime))
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00050FE5 File Offset: 0x0004F1E5
		public RelinquishJobDGTimeoutTransientException(DateTime pickupTime, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToDataGuaranteeTimeout(pickupTime), innerException)
		{
			this.pickupTime = pickupTime;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00050FFB File Offset: 0x0004F1FB
		protected RelinquishJobDGTimeoutTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pickupTime = (DateTime)info.GetValue("pickupTime", typeof(DateTime));
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00051025 File Offset: 0x0004F225
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pickupTime", this.pickupTime);
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00051040 File Offset: 0x0004F240
		public DateTime PickupTime
		{
			get
			{
				return this.pickupTime;
			}
		}

		// Token: 0x04001014 RID: 4116
		private readonly DateTime pickupTime;
	}
}
