using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0E RID: 3854
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotLogUserException : LocalizedException
	{
		// Token: 0x0600AA30 RID: 43568 RVA: 0x0028CF11 File Offset: 0x0028B111
		public CasHealthCouldNotLogUserException(string userName, string mailboxServerName, string scriptName, string errorString) : base(Strings.CasHealthCouldNotLogUser(userName, mailboxServerName, scriptName, errorString))
		{
			this.userName = userName;
			this.mailboxServerName = mailboxServerName;
			this.scriptName = scriptName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA31 RID: 43569 RVA: 0x0028CF40 File Offset: 0x0028B140
		public CasHealthCouldNotLogUserException(string userName, string mailboxServerName, string scriptName, string errorString, Exception innerException) : base(Strings.CasHealthCouldNotLogUser(userName, mailboxServerName, scriptName, errorString), innerException)
		{
			this.userName = userName;
			this.mailboxServerName = mailboxServerName;
			this.scriptName = scriptName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA32 RID: 43570 RVA: 0x0028CF74 File Offset: 0x0028B174
		protected CasHealthCouldNotLogUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.mailboxServerName = (string)info.GetValue("mailboxServerName", typeof(string));
			this.scriptName = (string)info.GetValue("scriptName", typeof(string));
			this.errorString = (string)info.GetValue("errorString", typeof(string));
		}

		// Token: 0x0600AA33 RID: 43571 RVA: 0x0028D00C File Offset: 0x0028B20C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
			info.AddValue("mailboxServerName", this.mailboxServerName);
			info.AddValue("scriptName", this.scriptName);
			info.AddValue("errorString", this.errorString);
		}

		// Token: 0x17003715 RID: 14101
		// (get) Token: 0x0600AA34 RID: 43572 RVA: 0x0028D065 File Offset: 0x0028B265
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17003716 RID: 14102
		// (get) Token: 0x0600AA35 RID: 43573 RVA: 0x0028D06D File Offset: 0x0028B26D
		public string MailboxServerName
		{
			get
			{
				return this.mailboxServerName;
			}
		}

		// Token: 0x17003717 RID: 14103
		// (get) Token: 0x0600AA36 RID: 43574 RVA: 0x0028D075 File Offset: 0x0028B275
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x17003718 RID: 14104
		// (get) Token: 0x0600AA37 RID: 43575 RVA: 0x0028D07D File Offset: 0x0028B27D
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		// Token: 0x0400607B RID: 24699
		private readonly string userName;

		// Token: 0x0400607C RID: 24700
		private readonly string mailboxServerName;

		// Token: 0x0400607D RID: 24701
		private readonly string scriptName;

		// Token: 0x0400607E RID: 24702
		private readonly string errorString;
	}
}
