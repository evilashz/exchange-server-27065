using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProxyCantFindCasServerException : ProxyException
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x0004B333 File Offset: 0x00049533
		public ProxyCantFindCasServerException(string userName, string currentSite, string mailboxSite, string decisionLog) : base(Strings.ProxyCantFindCasServer(userName, currentSite, mailboxSite, decisionLog))
		{
			this.userName = userName;
			this.currentSite = currentSite;
			this.mailboxSite = mailboxSite;
			this.decisionLog = decisionLog;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0004B362 File Offset: 0x00049562
		public ProxyCantFindCasServerException(string userName, string currentSite, string mailboxSite, string decisionLog, Exception innerException) : base(Strings.ProxyCantFindCasServer(userName, currentSite, mailboxSite, decisionLog), innerException)
		{
			this.userName = userName;
			this.currentSite = currentSite;
			this.mailboxSite = mailboxSite;
			this.decisionLog = decisionLog;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0004B394 File Offset: 0x00049594
		protected ProxyCantFindCasServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.currentSite = (string)info.GetValue("currentSite", typeof(string));
			this.mailboxSite = (string)info.GetValue("mailboxSite", typeof(string));
			this.decisionLog = (string)info.GetValue("decisionLog", typeof(string));
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0004B42C File Offset: 0x0004962C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
			info.AddValue("currentSite", this.currentSite);
			info.AddValue("mailboxSite", this.mailboxSite);
			info.AddValue("decisionLog", this.decisionLog);
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0004B485 File Offset: 0x00049685
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0004B48D File Offset: 0x0004968D
		public string CurrentSite
		{
			get
			{
				return this.currentSite;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0004B495 File Offset: 0x00049695
		public string MailboxSite
		{
			get
			{
				return this.mailboxSite;
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0004B49D File Offset: 0x0004969D
		public string DecisionLog
		{
			get
			{
				return this.decisionLog;
			}
		}

		// Token: 0x04001849 RID: 6217
		private readonly string userName;

		// Token: 0x0400184A RID: 6218
		private readonly string currentSite;

		// Token: 0x0400184B RID: 6219
		private readonly string mailboxSite;

		// Token: 0x0400184C RID: 6220
		private readonly string decisionLog;
	}
}
