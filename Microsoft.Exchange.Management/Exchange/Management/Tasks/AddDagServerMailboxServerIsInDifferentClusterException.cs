using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107E RID: 4222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddDagServerMailboxServerIsInDifferentClusterException : LocalizedException
	{
		// Token: 0x0600B15D RID: 45405 RVA: 0x00297EC9 File Offset: 0x002960C9
		public AddDagServerMailboxServerIsInDifferentClusterException(string mailboxServer, string thisClusterName, string otherClusterName) : base(Strings.AddDagServerMailboxServerIsInDifferentClusterException(mailboxServer, thisClusterName, otherClusterName))
		{
			this.mailboxServer = mailboxServer;
			this.thisClusterName = thisClusterName;
			this.otherClusterName = otherClusterName;
		}

		// Token: 0x0600B15E RID: 45406 RVA: 0x00297EEE File Offset: 0x002960EE
		public AddDagServerMailboxServerIsInDifferentClusterException(string mailboxServer, string thisClusterName, string otherClusterName, Exception innerException) : base(Strings.AddDagServerMailboxServerIsInDifferentClusterException(mailboxServer, thisClusterName, otherClusterName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.thisClusterName = thisClusterName;
			this.otherClusterName = otherClusterName;
		}

		// Token: 0x0600B15F RID: 45407 RVA: 0x00297F18 File Offset: 0x00296118
		protected AddDagServerMailboxServerIsInDifferentClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.thisClusterName = (string)info.GetValue("thisClusterName", typeof(string));
			this.otherClusterName = (string)info.GetValue("otherClusterName", typeof(string));
		}

		// Token: 0x0600B160 RID: 45408 RVA: 0x00297F8D File Offset: 0x0029618D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("thisClusterName", this.thisClusterName);
			info.AddValue("otherClusterName", this.otherClusterName);
		}

		// Token: 0x17003882 RID: 14466
		// (get) Token: 0x0600B161 RID: 45409 RVA: 0x00297FCA File Offset: 0x002961CA
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x17003883 RID: 14467
		// (get) Token: 0x0600B162 RID: 45410 RVA: 0x00297FD2 File Offset: 0x002961D2
		public string ThisClusterName
		{
			get
			{
				return this.thisClusterName;
			}
		}

		// Token: 0x17003884 RID: 14468
		// (get) Token: 0x0600B163 RID: 45411 RVA: 0x00297FDA File Offset: 0x002961DA
		public string OtherClusterName
		{
			get
			{
				return this.otherClusterName;
			}
		}

		// Token: 0x040061E8 RID: 25064
		private readonly string mailboxServer;

		// Token: 0x040061E9 RID: 25065
		private readonly string thisClusterName;

		// Token: 0x040061EA RID: 25066
		private readonly string otherClusterName;
	}
}
