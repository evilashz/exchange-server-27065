using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000339 RID: 825
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsIntegAttemptsExceededTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025D8 RID: 9688 RVA: 0x000522F0 File Offset: 0x000504F0
		public IsIntegAttemptsExceededTransientException(short attempts) : base(MrsStrings.IsIntegAttemptsExceededError(attempts))
		{
			this.attempts = attempts;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x00052305 File Offset: 0x00050505
		public IsIntegAttemptsExceededTransientException(short attempts, Exception innerException) : base(MrsStrings.IsIntegAttemptsExceededError(attempts), innerException)
		{
			this.attempts = attempts;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0005231B File Offset: 0x0005051B
		protected IsIntegAttemptsExceededTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.attempts = (short)info.GetValue("attempts", typeof(short));
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00052345 File Offset: 0x00050545
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("attempts", this.attempts);
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00052360 File Offset: 0x00050560
		public short Attempts
		{
			get
			{
				return this.attempts;
			}
		}

		// Token: 0x0400103D RID: 4157
		private readonly short attempts;
	}
}
