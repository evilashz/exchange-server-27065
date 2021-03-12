using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000310 RID: 784
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobServerBusyTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002508 RID: 9480 RVA: 0x00050DF5 File Offset: 0x0004EFF5
		public RelinquishJobServerBusyTransientException(LocalizedString error, TimeSpan backoffTimeSpan) : base(MrsStrings.JobHasBeenRelinquishedDueToServerBusy(error, backoffTimeSpan))
		{
			this.error = error;
			this.backoffTimeSpan = backoffTimeSpan;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00050E12 File Offset: 0x0004F012
		public RelinquishJobServerBusyTransientException(LocalizedString error, TimeSpan backoffTimeSpan, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToServerBusy(error, backoffTimeSpan), innerException)
		{
			this.error = error;
			this.backoffTimeSpan = backoffTimeSpan;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00050E30 File Offset: 0x0004F030
		protected RelinquishJobServerBusyTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (LocalizedString)info.GetValue("error", typeof(LocalizedString));
			this.backoffTimeSpan = (TimeSpan)info.GetValue("backoffTimeSpan", typeof(TimeSpan));
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00050E85 File Offset: 0x0004F085
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
			info.AddValue("backoffTimeSpan", this.backoffTimeSpan);
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x00050EBB File Offset: 0x0004F0BB
		public LocalizedString Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x00050EC3 File Offset: 0x0004F0C3
		public TimeSpan BackoffTimeSpan
		{
			get
			{
				return this.backoffTimeSpan;
			}
		}

		// Token: 0x04001011 RID: 4113
		private readonly LocalizedString error;

		// Token: 0x04001012 RID: 4114
		private readonly TimeSpan backoffTimeSpan;
	}
}
