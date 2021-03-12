using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000387 RID: 903
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderCreateFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x000548B2 File Offset: 0x00052AB2
		public EasFolderCreateFailedTransientException(string errorMessage) : base(MrsStrings.EasFolderCreateFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000548C7 File Offset: 0x00052AC7
		public EasFolderCreateFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderCreateFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000548DD File Offset: 0x00052ADD
		protected EasFolderCreateFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00054907 File Offset: 0x00052B07
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x00054922 File Offset: 0x00052B22
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108C RID: 4236
		private readonly string errorMessage;
	}
}
