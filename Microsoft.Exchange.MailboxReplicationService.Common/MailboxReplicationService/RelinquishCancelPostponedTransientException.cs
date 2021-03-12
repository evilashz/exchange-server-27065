using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000318 RID: 792
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishCancelPostponedTransientException : RelinquishJobTransientException
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x000510F4 File Offset: 0x0004F2F4
		public RelinquishCancelPostponedTransientException(DateTime removeAfter) : base(MrsStrings.JobHasBeenRelinquishedDueToCancelPostponed(removeAfter))
		{
			this.removeAfter = removeAfter;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00051109 File Offset: 0x0004F309
		public RelinquishCancelPostponedTransientException(DateTime removeAfter, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToCancelPostponed(removeAfter), innerException)
		{
			this.removeAfter = removeAfter;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0005111F File Offset: 0x0004F31F
		protected RelinquishCancelPostponedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.removeAfter = (DateTime)info.GetValue("removeAfter", typeof(DateTime));
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00051149 File Offset: 0x0004F349
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("removeAfter", this.removeAfter);
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x00051164 File Offset: 0x0004F364
		public DateTime RemoveAfter
		{
			get
			{
				return this.removeAfter;
			}
		}

		// Token: 0x04001016 RID: 4118
		private readonly DateTime removeAfter;
	}
}
