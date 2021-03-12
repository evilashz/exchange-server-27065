using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037C RID: 892
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderSyncFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002728 RID: 10024 RVA: 0x0005438A File Offset: 0x0005258A
		public EasFolderSyncFailedPermanentException(string errorMessage) : base(MrsStrings.EasFolderSyncFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0005439F File Offset: 0x0005259F
		public EasFolderSyncFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderSyncFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000543B5 File Offset: 0x000525B5
		protected EasFolderSyncFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000543DF File Offset: 0x000525DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000543FA File Offset: 0x000525FA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001081 RID: 4225
		private readonly string errorMessage;
	}
}
