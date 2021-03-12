using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1C RID: 3868
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotLogUserForAutodiscoverException : LocalizedException
	{
		// Token: 0x0600AA7B RID: 43643 RVA: 0x0028D778 File Offset: 0x0028B978
		public CasHealthCouldNotLogUserForAutodiscoverException(string domain, string userName, string additionalInfo) : base(Strings.CasHealthCouldNotLogUserForAutodiscover(domain, userName, additionalInfo))
		{
			this.domain = domain;
			this.userName = userName;
			this.additionalInfo = additionalInfo;
		}

		// Token: 0x0600AA7C RID: 43644 RVA: 0x0028D79D File Offset: 0x0028B99D
		public CasHealthCouldNotLogUserForAutodiscoverException(string domain, string userName, string additionalInfo, Exception innerException) : base(Strings.CasHealthCouldNotLogUserForAutodiscover(domain, userName, additionalInfo), innerException)
		{
			this.domain = domain;
			this.userName = userName;
			this.additionalInfo = additionalInfo;
		}

		// Token: 0x0600AA7D RID: 43645 RVA: 0x0028D7C4 File Offset: 0x0028B9C4
		protected CasHealthCouldNotLogUserForAutodiscoverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.additionalInfo = (string)info.GetValue("additionalInfo", typeof(string));
		}

		// Token: 0x0600AA7E RID: 43646 RVA: 0x0028D839 File Offset: 0x0028BA39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("userName", this.userName);
			info.AddValue("additionalInfo", this.additionalInfo);
		}

		// Token: 0x17003728 RID: 14120
		// (get) Token: 0x0600AA7F RID: 43647 RVA: 0x0028D876 File Offset: 0x0028BA76
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17003729 RID: 14121
		// (get) Token: 0x0600AA80 RID: 43648 RVA: 0x0028D87E File Offset: 0x0028BA7E
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x1700372A RID: 14122
		// (get) Token: 0x0600AA81 RID: 43649 RVA: 0x0028D886 File Offset: 0x0028BA86
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfo;
			}
		}

		// Token: 0x0400608E RID: 24718
		private readonly string domain;

		// Token: 0x0400608F RID: 24719
		private readonly string userName;

		// Token: 0x04006090 RID: 24720
		private readonly string additionalInfo;
	}
}
