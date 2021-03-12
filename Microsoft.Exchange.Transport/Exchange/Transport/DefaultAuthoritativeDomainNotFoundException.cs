using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200057F RID: 1407
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultAuthoritativeDomainNotFoundException : ExchangeConfigurationException
	{
		// Token: 0x06004131 RID: 16689 RVA: 0x0011A64E File Offset: 0x0011884E
		public DefaultAuthoritativeDomainNotFoundException(OrganizationId orgId) : base(Strings.DefaultAuthoritativeDomainNotFound(orgId))
		{
			this.orgId = orgId;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x0011A663 File Offset: 0x00118863
		public DefaultAuthoritativeDomainNotFoundException(OrganizationId orgId, Exception innerException) : base(Strings.DefaultAuthoritativeDomainNotFound(orgId), innerException)
		{
			this.orgId = orgId;
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x0011A679 File Offset: 0x00118879
		protected DefaultAuthoritativeDomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (OrganizationId)info.GetValue("orgId", typeof(OrganizationId));
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x0011A6A3 File Offset: 0x001188A3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x0011A6BE File Offset: 0x001188BE
		public OrganizationId OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x0400253B RID: 9531
		private readonly OrganizationId orgId;
	}
}
