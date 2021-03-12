using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000381 RID: 897
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasCountFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002741 RID: 10049 RVA: 0x000545E2 File Offset: 0x000527E2
		public EasCountFailedTransientException(string errorMessage) : base(MrsStrings.EasCountFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000545F7 File Offset: 0x000527F7
		public EasCountFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasCountFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0005460D File Offset: 0x0005280D
		protected EasCountFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00054637 File Offset: 0x00052837
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00054652 File Offset: 0x00052852
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001086 RID: 4230
		private readonly string errorMessage;
	}
}
