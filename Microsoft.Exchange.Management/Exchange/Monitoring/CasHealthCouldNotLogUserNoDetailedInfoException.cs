using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0F RID: 3855
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotLogUserNoDetailedInfoException : LocalizedException
	{
		// Token: 0x0600AA38 RID: 43576 RVA: 0x0028D085 File Offset: 0x0028B285
		public CasHealthCouldNotLogUserNoDetailedInfoException(string userName, string mailboxServerName, string scriptName) : base(Strings.CasHealthCouldNotLogUserNoDetailedInfo(userName, mailboxServerName, scriptName))
		{
			this.userName = userName;
			this.mailboxServerName = mailboxServerName;
			this.scriptName = scriptName;
		}

		// Token: 0x0600AA39 RID: 43577 RVA: 0x0028D0AA File Offset: 0x0028B2AA
		public CasHealthCouldNotLogUserNoDetailedInfoException(string userName, string mailboxServerName, string scriptName, Exception innerException) : base(Strings.CasHealthCouldNotLogUserNoDetailedInfo(userName, mailboxServerName, scriptName), innerException)
		{
			this.userName = userName;
			this.mailboxServerName = mailboxServerName;
			this.scriptName = scriptName;
		}

		// Token: 0x0600AA3A RID: 43578 RVA: 0x0028D0D4 File Offset: 0x0028B2D4
		protected CasHealthCouldNotLogUserNoDetailedInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.mailboxServerName = (string)info.GetValue("mailboxServerName", typeof(string));
			this.scriptName = (string)info.GetValue("scriptName", typeof(string));
		}

		// Token: 0x0600AA3B RID: 43579 RVA: 0x0028D149 File Offset: 0x0028B349
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
			info.AddValue("mailboxServerName", this.mailboxServerName);
			info.AddValue("scriptName", this.scriptName);
		}

		// Token: 0x17003719 RID: 14105
		// (get) Token: 0x0600AA3C RID: 43580 RVA: 0x0028D186 File Offset: 0x0028B386
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x1700371A RID: 14106
		// (get) Token: 0x0600AA3D RID: 43581 RVA: 0x0028D18E File Offset: 0x0028B38E
		public string MailboxServerName
		{
			get
			{
				return this.mailboxServerName;
			}
		}

		// Token: 0x1700371B RID: 14107
		// (get) Token: 0x0600AA3E RID: 43582 RVA: 0x0028D196 File Offset: 0x0028B396
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x0400607F RID: 24703
		private readonly string userName;

		// Token: 0x04006080 RID: 24704
		private readonly string mailboxServerName;

		// Token: 0x04006081 RID: 24705
		private readonly string scriptName;
	}
}
