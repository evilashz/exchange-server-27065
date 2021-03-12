using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001199 RID: 4505
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateIntraOrganizationConnectorDomainException : LocalizedException
	{
		// Token: 0x0600B6F3 RID: 46835 RVA: 0x002A0BD3 File Offset: 0x0029EDD3
		public DuplicateIntraOrganizationConnectorDomainException(string domain) : base(Strings.DuplicateIntraOrganizationConnectorDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B6F4 RID: 46836 RVA: 0x002A0BE8 File Offset: 0x0029EDE8
		public DuplicateIntraOrganizationConnectorDomainException(string domain, Exception innerException) : base(Strings.DuplicateIntraOrganizationConnectorDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B6F5 RID: 46837 RVA: 0x002A0BFE File Offset: 0x0029EDFE
		protected DuplicateIntraOrganizationConnectorDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B6F6 RID: 46838 RVA: 0x002A0C28 File Offset: 0x0029EE28
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170039AC RID: 14764
		// (get) Token: 0x0600B6F7 RID: 46839 RVA: 0x002A0C43 File Offset: 0x0029EE43
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04006312 RID: 25362
		private readonly string domain;
	}
}
