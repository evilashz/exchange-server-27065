using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C3 RID: 4291
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetDomainStatusFromPartnerSTSException : FederationException
	{
		// Token: 0x0600B2C5 RID: 45765 RVA: 0x0029A4B9 File Offset: 0x002986B9
		public CannotGetDomainStatusFromPartnerSTSException(string domain, string appId, string errdetails) : base(Strings.ErrorCannotGetDomainStatusFromPartnerSTS(domain, appId, errdetails))
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x0029A4DE File Offset: 0x002986DE
		public CannotGetDomainStatusFromPartnerSTSException(string domain, string appId, string errdetails, Exception innerException) : base(Strings.ErrorCannotGetDomainStatusFromPartnerSTS(domain, appId, errdetails), innerException)
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2C7 RID: 45767 RVA: 0x0029A508 File Offset: 0x00298708
		protected CannotGetDomainStatusFromPartnerSTSException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.errdetails = (string)info.GetValue("errdetails", typeof(string));
		}

		// Token: 0x0600B2C8 RID: 45768 RVA: 0x0029A57D File Offset: 0x0029877D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("appId", this.appId);
			info.AddValue("errdetails", this.errdetails);
		}

		// Token: 0x170038D6 RID: 14550
		// (get) Token: 0x0600B2C9 RID: 45769 RVA: 0x0029A5BA File Offset: 0x002987BA
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170038D7 RID: 14551
		// (get) Token: 0x0600B2CA RID: 45770 RVA: 0x0029A5C2 File Offset: 0x002987C2
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170038D8 RID: 14552
		// (get) Token: 0x0600B2CB RID: 45771 RVA: 0x0029A5CA File Offset: 0x002987CA
		public string Errdetails
		{
			get
			{
				return this.errdetails;
			}
		}

		// Token: 0x0400623C RID: 25148
		private readonly string domain;

		// Token: 0x0400623D RID: 25149
		private readonly string appId;

		// Token: 0x0400623E RID: 25150
		private readonly string errdetails;
	}
}
