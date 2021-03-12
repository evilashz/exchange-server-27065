using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F22 RID: 3874
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthTestUserOnIncorrectServerException : LocalizedException
	{
		// Token: 0x0600AA99 RID: 43673 RVA: 0x0028DA54 File Offset: 0x0028BC54
		public CasHealthTestUserOnIncorrectServerException(string domain, string userName, string foundOn, string shouldBeOn) : base(Strings.CasHealthTestUserOnIncorrectServer(domain, userName, foundOn, shouldBeOn))
		{
			this.domain = domain;
			this.userName = userName;
			this.foundOn = foundOn;
			this.shouldBeOn = shouldBeOn;
		}

		// Token: 0x0600AA9A RID: 43674 RVA: 0x0028DA83 File Offset: 0x0028BC83
		public CasHealthTestUserOnIncorrectServerException(string domain, string userName, string foundOn, string shouldBeOn, Exception innerException) : base(Strings.CasHealthTestUserOnIncorrectServer(domain, userName, foundOn, shouldBeOn), innerException)
		{
			this.domain = domain;
			this.userName = userName;
			this.foundOn = foundOn;
			this.shouldBeOn = shouldBeOn;
		}

		// Token: 0x0600AA9B RID: 43675 RVA: 0x0028DAB4 File Offset: 0x0028BCB4
		protected CasHealthTestUserOnIncorrectServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.foundOn = (string)info.GetValue("foundOn", typeof(string));
			this.shouldBeOn = (string)info.GetValue("shouldBeOn", typeof(string));
		}

		// Token: 0x0600AA9C RID: 43676 RVA: 0x0028DB4C File Offset: 0x0028BD4C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("userName", this.userName);
			info.AddValue("foundOn", this.foundOn);
			info.AddValue("shouldBeOn", this.shouldBeOn);
		}

		// Token: 0x1700372E RID: 14126
		// (get) Token: 0x0600AA9D RID: 43677 RVA: 0x0028DBA5 File Offset: 0x0028BDA5
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x1700372F RID: 14127
		// (get) Token: 0x0600AA9E RID: 43678 RVA: 0x0028DBAD File Offset: 0x0028BDAD
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17003730 RID: 14128
		// (get) Token: 0x0600AA9F RID: 43679 RVA: 0x0028DBB5 File Offset: 0x0028BDB5
		public string FoundOn
		{
			get
			{
				return this.foundOn;
			}
		}

		// Token: 0x17003731 RID: 14129
		// (get) Token: 0x0600AAA0 RID: 43680 RVA: 0x0028DBBD File Offset: 0x0028BDBD
		public string ShouldBeOn
		{
			get
			{
				return this.shouldBeOn;
			}
		}

		// Token: 0x04006094 RID: 24724
		private readonly string domain;

		// Token: 0x04006095 RID: 24725
		private readonly string userName;

		// Token: 0x04006096 RID: 24726
		private readonly string foundOn;

		// Token: 0x04006097 RID: 24727
		private readonly string shouldBeOn;
	}
}
