using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BF RID: 4287
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReserveDomainException : FederationException
	{
		// Token: 0x0600B2A7 RID: 45735 RVA: 0x00299FA2 File Offset: 0x002981A2
		public UnableToReserveDomainException(string domain, string appId, string errdetails) : base(Strings.ErrorUnableToReserveDomain(domain, appId, errdetails))
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2A8 RID: 45736 RVA: 0x00299FC7 File Offset: 0x002981C7
		public UnableToReserveDomainException(string domain, string appId, string errdetails, Exception innerException) : base(Strings.ErrorUnableToReserveDomain(domain, appId, errdetails), innerException)
		{
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2A9 RID: 45737 RVA: 0x00299FF0 File Offset: 0x002981F0
		protected UnableToReserveDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.errdetails = (string)info.GetValue("errdetails", typeof(string));
		}

		// Token: 0x0600B2AA RID: 45738 RVA: 0x0029A065 File Offset: 0x00298265
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("appId", this.appId);
			info.AddValue("errdetails", this.errdetails);
		}

		// Token: 0x170038C8 RID: 14536
		// (get) Token: 0x0600B2AB RID: 45739 RVA: 0x0029A0A2 File Offset: 0x002982A2
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170038C9 RID: 14537
		// (get) Token: 0x0600B2AC RID: 45740 RVA: 0x0029A0AA File Offset: 0x002982AA
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170038CA RID: 14538
		// (get) Token: 0x0600B2AD RID: 45741 RVA: 0x0029A0B2 File Offset: 0x002982B2
		public string Errdetails
		{
			get
			{
				return this.errdetails;
			}
		}

		// Token: 0x0400622E RID: 25134
		private readonly string domain;

		// Token: 0x0400622F RID: 25135
		private readonly string appId;

		// Token: 0x04006230 RID: 25136
		private readonly string errdetails;
	}
}
