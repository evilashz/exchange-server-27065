using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C1 RID: 4289
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReleaseDomainException : FederationException
	{
		// Token: 0x0600B2B6 RID: 45750 RVA: 0x0029A22D File Offset: 0x0029842D
		public UnableToReleaseDomainException(string domain, string appId, string errdetails) : base(Strings.ErrorUnableToReleaseDomain(domain, appId, errdetails))
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2B7 RID: 45751 RVA: 0x0029A252 File Offset: 0x00298452
		public UnableToReleaseDomainException(string domain, string appId, string errdetails, Exception innerException) : base(Strings.ErrorUnableToReleaseDomain(domain, appId, errdetails), innerException)
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2B8 RID: 45752 RVA: 0x0029A27C File Offset: 0x0029847C
		protected UnableToReleaseDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.errdetails = (string)info.GetValue("errdetails", typeof(string));
		}

		// Token: 0x0600B2B9 RID: 45753 RVA: 0x0029A2F1 File Offset: 0x002984F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("appId", this.appId);
			info.AddValue("errdetails", this.errdetails);
		}

		// Token: 0x170038CF RID: 14543
		// (get) Token: 0x0600B2BA RID: 45754 RVA: 0x0029A32E File Offset: 0x0029852E
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170038D0 RID: 14544
		// (get) Token: 0x0600B2BB RID: 45755 RVA: 0x0029A336 File Offset: 0x00298536
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170038D1 RID: 14545
		// (get) Token: 0x0600B2BC RID: 45756 RVA: 0x0029A33E File Offset: 0x0029853E
		public string Errdetails
		{
			get
			{
				return this.errdetails;
			}
		}

		// Token: 0x04006235 RID: 25141
		private readonly string domain;

		// Token: 0x04006236 RID: 25142
		private readonly string appId;

		// Token: 0x04006237 RID: 25143
		private readonly string errdetails;
	}
}
