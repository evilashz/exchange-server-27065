using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BA RID: 4282
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainNameNotAcceptedDomainException : FederationException
	{
		// Token: 0x0600B28F RID: 45711 RVA: 0x00299D93 File Offset: 0x00297F93
		public DomainNameNotAcceptedDomainException(string domain) : base(Strings.ErrorDomainNameNotAcceptedDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B290 RID: 45712 RVA: 0x00299DA8 File Offset: 0x00297FA8
		public DomainNameNotAcceptedDomainException(string domain, Exception innerException) : base(Strings.ErrorDomainNameNotAcceptedDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B291 RID: 45713 RVA: 0x00299DBE File Offset: 0x00297FBE
		protected DomainNameNotAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B292 RID: 45714 RVA: 0x00299DE8 File Offset: 0x00297FE8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C4 RID: 14532
		// (get) Token: 0x0600B293 RID: 45715 RVA: 0x00299E03 File Offset: 0x00298003
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x0400622A RID: 25130
		private readonly string domain;
	}
}
