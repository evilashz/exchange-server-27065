using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BC RID: 4284
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcceptedDomainsWithSubdomainsException : FederationException
	{
		// Token: 0x0600B298 RID: 45720 RVA: 0x00299E3A File Offset: 0x0029803A
		public AcceptedDomainsWithSubdomainsException(string domain) : base(Strings.ErrorAcceptedDomainsWithSubdomains(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B299 RID: 45721 RVA: 0x00299E4F File Offset: 0x0029804F
		public AcceptedDomainsWithSubdomainsException(string domain, Exception innerException) : base(Strings.ErrorAcceptedDomainsWithSubdomains(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B29A RID: 45722 RVA: 0x00299E65 File Offset: 0x00298065
		protected AcceptedDomainsWithSubdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B29B RID: 45723 RVA: 0x00299E8F File Offset: 0x0029808F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C5 RID: 14533
		// (get) Token: 0x0600B29C RID: 45724 RVA: 0x00299EAA File Offset: 0x002980AA
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x0400622B RID: 25131
		private readonly string domain;
	}
}
