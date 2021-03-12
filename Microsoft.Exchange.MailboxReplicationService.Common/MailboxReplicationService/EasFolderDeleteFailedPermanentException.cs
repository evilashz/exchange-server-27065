using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038A RID: 906
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFolderDeleteFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600276E RID: 10094 RVA: 0x00054A1A File Offset: 0x00052C1A
		public EasFolderDeleteFailedPermanentException(string errorMessage) : base(MrsStrings.EasFolderDeleteFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00054A2F File Offset: 0x00052C2F
		public EasFolderDeleteFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasFolderDeleteFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00054A45 File Offset: 0x00052C45
		protected EasFolderDeleteFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00054A6F File Offset: 0x00052C6F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x00054A8A File Offset: 0x00052C8A
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108F RID: 4239
		private readonly string errorMessage;
	}
}
