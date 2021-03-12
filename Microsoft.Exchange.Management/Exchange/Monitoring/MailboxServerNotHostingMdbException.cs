using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F03 RID: 3843
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxServerNotHostingMdbException : LocalizedException
	{
		// Token: 0x0600A9F8 RID: 43512 RVA: 0x0028C982 File Offset: 0x0028AB82
		public MailboxServerNotHostingMdbException(string mailboxServer) : base(Strings.MailboxServerNotHostingMdbException(mailboxServer))
		{
			this.mailboxServer = mailboxServer;
		}

		// Token: 0x0600A9F9 RID: 43513 RVA: 0x0028C997 File Offset: 0x0028AB97
		public MailboxServerNotHostingMdbException(string mailboxServer, Exception innerException) : base(Strings.MailboxServerNotHostingMdbException(mailboxServer), innerException)
		{
			this.mailboxServer = mailboxServer;
		}

		// Token: 0x0600A9FA RID: 43514 RVA: 0x0028C9AD File Offset: 0x0028ABAD
		protected MailboxServerNotHostingMdbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
		}

		// Token: 0x0600A9FB RID: 43515 RVA: 0x0028C9D7 File Offset: 0x0028ABD7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
		}

		// Token: 0x17003709 RID: 14089
		// (get) Token: 0x0600A9FC RID: 43516 RVA: 0x0028C9F2 File Offset: 0x0028ABF2
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x0400606F RID: 24687
		private readonly string mailboxServer;
	}
}
