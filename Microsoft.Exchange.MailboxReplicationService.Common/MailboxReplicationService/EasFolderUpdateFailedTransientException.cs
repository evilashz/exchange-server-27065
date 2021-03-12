using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038B RID: 907
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderUpdateFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002773 RID: 10099 RVA: 0x00054A92 File Offset: 0x00052C92
		public EasFolderUpdateFailedTransientException(string errorMessage) : base(MrsStrings.EasFolderUpdateFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00054AA7 File Offset: 0x00052CA7
		public EasFolderUpdateFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderUpdateFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00054ABD File Offset: 0x00052CBD
		protected EasFolderUpdateFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00054AE7 File Offset: 0x00052CE7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00054B02 File Offset: 0x00052D02
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001090 RID: 4240
		private readonly string errorMessage;
	}
}
