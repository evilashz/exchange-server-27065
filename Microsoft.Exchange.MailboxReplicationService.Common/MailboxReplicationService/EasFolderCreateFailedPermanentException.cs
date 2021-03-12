using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000388 RID: 904
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderCreateFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x0005492A File Offset: 0x00052B2A
		public EasFolderCreateFailedPermanentException(string errorMessage) : base(MrsStrings.EasFolderCreateFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0005493F File Offset: 0x00052B3F
		public EasFolderCreateFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderCreateFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00054955 File Offset: 0x00052B55
		protected EasFolderCreateFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0005497F File Offset: 0x00052B7F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x0005499A File Offset: 0x00052B9A
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108D RID: 4237
		private readonly string errorMessage;
	}
}
