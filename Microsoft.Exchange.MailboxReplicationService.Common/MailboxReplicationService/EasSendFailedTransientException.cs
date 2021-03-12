using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000385 RID: 901
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasSendFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x000547C2 File Offset: 0x000529C2
		public EasSendFailedTransientException(string errorMessage) : base(MrsStrings.EasSendFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000547D7 File Offset: 0x000529D7
		public EasSendFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasSendFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000547ED File Offset: 0x000529ED
		protected EasSendFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00054817 File Offset: 0x00052A17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00054832 File Offset: 0x00052A32
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108A RID: 4234
		private readonly string errorMessage;
	}
}
