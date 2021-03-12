using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F59 RID: 3929
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetTargetDeliveryDomainOnWildCardDomainsException : LocalizedException
	{
		// Token: 0x0600ABB3 RID: 43955 RVA: 0x0028F6D0 File Offset: 0x0028D8D0
		public CannotSetTargetDeliveryDomainOnWildCardDomainsException(string domain) : base(Strings.CannotSetTargetDeliveryDomainOnWildCardDomains(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABB4 RID: 43956 RVA: 0x0028F6E5 File Offset: 0x0028D8E5
		public CannotSetTargetDeliveryDomainOnWildCardDomainsException(string domain, Exception innerException) : base(Strings.CannotSetTargetDeliveryDomainOnWildCardDomains(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABB5 RID: 43957 RVA: 0x0028F6FB File Offset: 0x0028D8FB
		protected CannotSetTargetDeliveryDomainOnWildCardDomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABB6 RID: 43958 RVA: 0x0028F725 File Offset: 0x0028D925
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700376C RID: 14188
		// (get) Token: 0x0600ABB7 RID: 43959 RVA: 0x0028F740 File Offset: 0x0028D940
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060D2 RID: 24786
		private readonly string domain;
	}
}
