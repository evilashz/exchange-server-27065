using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CasServerNotSupportEsoException : LocalizedException
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x0004B571 File Offset: 0x00049771
		public CasServerNotSupportEsoException(string userName, string currentSite, string mailboxSite) : base(Strings.CasServerNotSupportEso(userName, currentSite, mailboxSite))
		{
			this.userName = userName;
			this.currentSite = currentSite;
			this.mailboxSite = mailboxSite;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0004B596 File Offset: 0x00049796
		public CasServerNotSupportEsoException(string userName, string currentSite, string mailboxSite, Exception innerException) : base(Strings.CasServerNotSupportEso(userName, currentSite, mailboxSite), innerException)
		{
			this.userName = userName;
			this.currentSite = currentSite;
			this.mailboxSite = mailboxSite;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0004B5C0 File Offset: 0x000497C0
		protected CasServerNotSupportEsoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.currentSite = (string)info.GetValue("currentSite", typeof(string));
			this.mailboxSite = (string)info.GetValue("mailboxSite", typeof(string));
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0004B635 File Offset: 0x00049835
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
			info.AddValue("currentSite", this.currentSite);
			info.AddValue("mailboxSite", this.mailboxSite);
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0004B672 File Offset: 0x00049872
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0004B67A File Offset: 0x0004987A
		public string CurrentSite
		{
			get
			{
				return this.currentSite;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0004B682 File Offset: 0x00049882
		public string MailboxSite
		{
			get
			{
				return this.mailboxSite;
			}
		}

		// Token: 0x0400184F RID: 6223
		private readonly string userName;

		// Token: 0x04001850 RID: 6224
		private readonly string currentSite;

		// Token: 0x04001851 RID: 6225
		private readonly string mailboxSite;
	}
}
