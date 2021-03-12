using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000389 RID: 905
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderDeleteFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002769 RID: 10089 RVA: 0x000549A2 File Offset: 0x00052BA2
		public EasFolderDeleteFailedTransientException(string errorMessage) : base(MrsStrings.EasFolderDeleteFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000549B7 File Offset: 0x00052BB7
		public EasFolderDeleteFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderDeleteFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000549CD File Offset: 0x00052BCD
		protected EasFolderDeleteFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000549F7 File Offset: 0x00052BF7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00054A12 File Offset: 0x00052C12
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108E RID: 4238
		private readonly string errorMessage;
	}
}
