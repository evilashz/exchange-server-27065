using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107A RID: 4218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerMailboxServerIsInDifferentDagException : LocalizedException
	{
		// Token: 0x0600B147 RID: 45383 RVA: 0x00297C3E File Offset: 0x00295E3E
		public DagTaskServerMailboxServerIsInDifferentDagException(string mailboxServer, string otherDagName) : base(Strings.DagTaskServerMailboxServerIsInDifferentDagException(mailboxServer, otherDagName))
		{
			this.mailboxServer = mailboxServer;
			this.otherDagName = otherDagName;
		}

		// Token: 0x0600B148 RID: 45384 RVA: 0x00297C5B File Offset: 0x00295E5B
		public DagTaskServerMailboxServerIsInDifferentDagException(string mailboxServer, string otherDagName, Exception innerException) : base(Strings.DagTaskServerMailboxServerIsInDifferentDagException(mailboxServer, otherDagName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.otherDagName = otherDagName;
		}

		// Token: 0x0600B149 RID: 45385 RVA: 0x00297C7C File Offset: 0x00295E7C
		protected DagTaskServerMailboxServerIsInDifferentDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.otherDagName = (string)info.GetValue("otherDagName", typeof(string));
		}

		// Token: 0x0600B14A RID: 45386 RVA: 0x00297CD1 File Offset: 0x00295ED1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("otherDagName", this.otherDagName);
		}

		// Token: 0x1700387C RID: 14460
		// (get) Token: 0x0600B14B RID: 45387 RVA: 0x00297CFD File Offset: 0x00295EFD
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x1700387D RID: 14461
		// (get) Token: 0x0600B14C RID: 45388 RVA: 0x00297D05 File Offset: 0x00295F05
		public string OtherDagName
		{
			get
			{
				return this.otherDagName;
			}
		}

		// Token: 0x040061E2 RID: 25058
		private readonly string mailboxServer;

		// Token: 0x040061E3 RID: 25059
		private readonly string otherDagName;
	}
}
