using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000382 RID: 898
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasCountFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002746 RID: 10054 RVA: 0x0005465A File Offset: 0x0005285A
		public EasCountFailedPermanentException(string errorMessage) : base(MrsStrings.EasCountFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0005466F File Offset: 0x0005286F
		public EasCountFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasCountFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00054685 File Offset: 0x00052885
		protected EasCountFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000546AF File Offset: 0x000528AF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000546CA File Offset: 0x000528CA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001087 RID: 4231
		private readonly string errorMessage;
	}
}
