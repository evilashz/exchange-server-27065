using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107B RID: 4219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerMailboxServerAlsoServesAsFswNodeException : LocalizedException
	{
		// Token: 0x0600B14D RID: 45389 RVA: 0x00297D0D File Offset: 0x00295F0D
		public DagTaskServerMailboxServerAlsoServesAsFswNodeException(string mailboxServer) : base(Strings.DagTaskServerMailboxServerAlsoServesAsFswNodeException(mailboxServer))
		{
			this.mailboxServer = mailboxServer;
		}

		// Token: 0x0600B14E RID: 45390 RVA: 0x00297D22 File Offset: 0x00295F22
		public DagTaskServerMailboxServerAlsoServesAsFswNodeException(string mailboxServer, Exception innerException) : base(Strings.DagTaskServerMailboxServerAlsoServesAsFswNodeException(mailboxServer), innerException)
		{
			this.mailboxServer = mailboxServer;
		}

		// Token: 0x0600B14F RID: 45391 RVA: 0x00297D38 File Offset: 0x00295F38
		protected DagTaskServerMailboxServerAlsoServesAsFswNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
		}

		// Token: 0x0600B150 RID: 45392 RVA: 0x00297D62 File Offset: 0x00295F62
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
		}

		// Token: 0x1700387E RID: 14462
		// (get) Token: 0x0600B151 RID: 45393 RVA: 0x00297D7D File Offset: 0x00295F7D
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x040061E4 RID: 25060
		private readonly string mailboxServer;
	}
}
