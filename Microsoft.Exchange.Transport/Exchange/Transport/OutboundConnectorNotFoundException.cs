using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000580 RID: 1408
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OutboundConnectorNotFoundException : ExchangeConfigurationException
	{
		// Token: 0x06004136 RID: 16694 RVA: 0x0011A6C6 File Offset: 0x001188C6
		public OutboundConnectorNotFoundException(string name, OrganizationId orgId) : base(Strings.OutboundConnectorNotFound(name, orgId))
		{
			this.name = name;
			this.orgId = orgId;
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x0011A6E3 File Offset: 0x001188E3
		public OutboundConnectorNotFoundException(string name, OrganizationId orgId, Exception innerException) : base(Strings.OutboundConnectorNotFound(name, orgId), innerException)
		{
			this.name = name;
			this.orgId = orgId;
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x0011A704 File Offset: 0x00118904
		protected OutboundConnectorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.orgId = (OrganizationId)info.GetValue("orgId", typeof(OrganizationId));
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0011A759 File Offset: 0x00118959
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x0011A785 File Offset: 0x00118985
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x0011A78D File Offset: 0x0011898D
		public OrganizationId OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x0400253C RID: 9532
		private readonly string name;

		// Token: 0x0400253D RID: 9533
		private readonly OrganizationId orgId;
	}
}
