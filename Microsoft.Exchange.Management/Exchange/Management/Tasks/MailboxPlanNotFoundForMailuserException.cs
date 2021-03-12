using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8D RID: 3725
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxPlanNotFoundForMailuserException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A799 RID: 42905 RVA: 0x00288B69 File Offset: 0x00286D69
		public MailboxPlanNotFoundForMailuserException(string user) : base(Strings.ErrorCouldNotLocateMailboxPlanForMailUser(user))
		{
			this.user = user;
		}

		// Token: 0x0600A79A RID: 42906 RVA: 0x00288B7E File Offset: 0x00286D7E
		public MailboxPlanNotFoundForMailuserException(string user, Exception innerException) : base(Strings.ErrorCouldNotLocateMailboxPlanForMailUser(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600A79B RID: 42907 RVA: 0x00288B94 File Offset: 0x00286D94
		protected MailboxPlanNotFoundForMailuserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x0600A79C RID: 42908 RVA: 0x00288BBE File Offset: 0x00286DBE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17003682 RID: 13954
		// (get) Token: 0x0600A79D RID: 42909 RVA: 0x00288BD9 File Offset: 0x00286DD9
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04005FE8 RID: 24552
		private readonly string user;
	}
}
