using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038C RID: 908
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderUpdateFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002778 RID: 10104 RVA: 0x00054B0A File Offset: 0x00052D0A
		public EasFolderUpdateFailedPermanentException(string errorMessage) : base(MrsStrings.EasFolderUpdateFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00054B1F File Offset: 0x00052D1F
		public EasFolderUpdateFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderUpdateFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00054B35 File Offset: 0x00052D35
		protected EasFolderUpdateFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00054B5F File Offset: 0x00052D5F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x00054B7A File Offset: 0x00052D7A
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001091 RID: 4241
		private readonly string errorMessage;
	}
}
