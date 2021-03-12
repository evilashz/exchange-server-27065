using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B3 RID: 4275
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainAlreadyFederatedException : FederationException
	{
		// Token: 0x0600B26F RID: 45679 RVA: 0x00299B26 File Offset: 0x00297D26
		public DomainAlreadyFederatedException(string domain) : base(Strings.ErrorDomainAlreadyFederated(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B270 RID: 45680 RVA: 0x00299B3B File Offset: 0x00297D3B
		public DomainAlreadyFederatedException(string domain, Exception innerException) : base(Strings.ErrorDomainAlreadyFederated(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B271 RID: 45681 RVA: 0x00299B51 File Offset: 0x00297D51
		protected DomainAlreadyFederatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B272 RID: 45682 RVA: 0x00299B7B File Offset: 0x00297D7B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C0 RID: 14528
		// (get) Token: 0x0600B273 RID: 45683 RVA: 0x00299B96 File Offset: 0x00297D96
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04006226 RID: 25126
		private readonly string domain;
	}
}
