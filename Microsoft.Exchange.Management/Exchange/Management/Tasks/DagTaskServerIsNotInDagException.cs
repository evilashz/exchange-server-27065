using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107D RID: 4221
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerIsNotInDagException : LocalizedException
	{
		// Token: 0x0600B157 RID: 45399 RVA: 0x00297DFD File Offset: 0x00295FFD
		public DagTaskServerIsNotInDagException(string mailboxServer, string dagName) : base(Strings.DagTaskServerIsNotInDagException(mailboxServer, dagName))
		{
			this.mailboxServer = mailboxServer;
			this.dagName = dagName;
		}

		// Token: 0x0600B158 RID: 45400 RVA: 0x00297E1A File Offset: 0x0029601A
		public DagTaskServerIsNotInDagException(string mailboxServer, string dagName, Exception innerException) : base(Strings.DagTaskServerIsNotInDagException(mailboxServer, dagName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.dagName = dagName;
		}

		// Token: 0x0600B159 RID: 45401 RVA: 0x00297E38 File Offset: 0x00296038
		protected DagTaskServerIsNotInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B15A RID: 45402 RVA: 0x00297E8D File Offset: 0x0029608D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003880 RID: 14464
		// (get) Token: 0x0600B15B RID: 45403 RVA: 0x00297EB9 File Offset: 0x002960B9
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x17003881 RID: 14465
		// (get) Token: 0x0600B15C RID: 45404 RVA: 0x00297EC1 File Offset: 0x002960C1
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061E6 RID: 25062
		private readonly string mailboxServer;

		// Token: 0x040061E7 RID: 25063
		private readonly string dagName;
	}
}
