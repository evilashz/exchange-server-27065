using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037B RID: 891
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderSyncFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002723 RID: 10019 RVA: 0x00054312 File Offset: 0x00052512
		public EasFolderSyncFailedTransientException(string errorMessage) : base(MrsStrings.EasFolderSyncFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x00054327 File Offset: 0x00052527
		public EasFolderSyncFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderSyncFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0005433D File Offset: 0x0005253D
		protected EasFolderSyncFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00054367 File Offset: 0x00052567
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x00054382 File Offset: 0x00052582
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001080 RID: 4224
		private readonly string errorMessage;
	}
}
