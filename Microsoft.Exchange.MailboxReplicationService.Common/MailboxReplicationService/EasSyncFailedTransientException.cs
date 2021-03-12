using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037D RID: 893
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasSyncFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600272D RID: 10029 RVA: 0x00054402 File Offset: 0x00052602
		public EasSyncFailedTransientException(string errorMessage) : base(MrsStrings.EasSyncFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00054417 File Offset: 0x00052617
		public EasSyncFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasSyncFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0005442D File Offset: 0x0005262D
		protected EasSyncFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00054457 File Offset: 0x00052657
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x00054472 File Offset: 0x00052672
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001082 RID: 4226
		private readonly string errorMessage;
	}
}
