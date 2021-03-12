using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1A RID: 3866
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthStorageErrorException : LocalizedException
	{
		// Token: 0x0600AA6F RID: 43631 RVA: 0x0028D5D8 File Offset: 0x0028B7D8
		public CasHealthStorageErrorException(string serverName, string domain, string user, string errorStr) : base(Strings.CasHealthStorageError(serverName, domain, user, errorStr))
		{
			this.serverName = serverName;
			this.domain = domain;
			this.user = user;
			this.errorStr = errorStr;
		}

		// Token: 0x0600AA70 RID: 43632 RVA: 0x0028D607 File Offset: 0x0028B807
		public CasHealthStorageErrorException(string serverName, string domain, string user, string errorStr, Exception innerException) : base(Strings.CasHealthStorageError(serverName, domain, user, errorStr), innerException)
		{
			this.serverName = serverName;
			this.domain = domain;
			this.user = user;
			this.errorStr = errorStr;
		}

		// Token: 0x0600AA71 RID: 43633 RVA: 0x0028D638 File Offset: 0x0028B838
		protected CasHealthStorageErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.user = (string)info.GetValue("user", typeof(string));
			this.errorStr = (string)info.GetValue("errorStr", typeof(string));
		}

		// Token: 0x0600AA72 RID: 43634 RVA: 0x0028D6D0 File Offset: 0x0028B8D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("domain", this.domain);
			info.AddValue("user", this.user);
			info.AddValue("errorStr", this.errorStr);
		}

		// Token: 0x17003724 RID: 14116
		// (get) Token: 0x0600AA73 RID: 43635 RVA: 0x0028D729 File Offset: 0x0028B929
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17003725 RID: 14117
		// (get) Token: 0x0600AA74 RID: 43636 RVA: 0x0028D731 File Offset: 0x0028B931
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17003726 RID: 14118
		// (get) Token: 0x0600AA75 RID: 43637 RVA: 0x0028D739 File Offset: 0x0028B939
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003727 RID: 14119
		// (get) Token: 0x0600AA76 RID: 43638 RVA: 0x0028D741 File Offset: 0x0028B941
		public string ErrorStr
		{
			get
			{
				return this.errorStr;
			}
		}

		// Token: 0x0400608A RID: 24714
		private readonly string serverName;

		// Token: 0x0400608B RID: 24715
		private readonly string domain;

		// Token: 0x0400608C RID: 24716
		private readonly string user;

		// Token: 0x0400608D RID: 24717
		private readonly string errorStr;
	}
}
