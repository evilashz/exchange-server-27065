using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008EF RID: 2287
	internal class InboundConnector : IInboundConnector, IEntity<IInboundConnector>
	{
		// Token: 0x06005102 RID: 20738 RVA: 0x00151AE1 File Offset: 0x0014FCE1
		public InboundConnector()
		{
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x00151AEC File Offset: 0x0014FCEC
		public InboundConnector(TenantInboundConnector ic)
		{
			this.Identity = (ADObjectId)ic.Identity;
			this.Name = ic.Name;
			this.TLSSenderCertificateName = TaskCommon.ToStringOrNull(ic.TlsSenderCertificateName);
			this.ConnectorType = ic.ConnectorType;
			this.ConnectorSource = ic.ConnectorSource;
			this.SenderDomains = ic.SenderDomains;
			this.RequireTls = ic.RequireTls;
			this.CloudServicesMailEnabled = ic.CloudServicesMailEnabled;
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x00151B6C File Offset: 0x0014FD6C
		public InboundConnector(string name, SmtpX509Identifier tlsSenderCertificateName)
		{
			MultiValuedProperty<AddressSpace> multiValuedProperty = new MultiValuedProperty<AddressSpace>();
			multiValuedProperty.Add(new AddressSpace("*"));
			this.Name = name;
			this.TLSSenderCertificateName = TaskCommon.ToStringOrNull(tlsSenderCertificateName);
			this.ConnectorType = TenantConnectorType.OnPremises;
			this.SenderDomains = multiValuedProperty;
			this.RequireTls = true;
			this.CloudServicesMailEnabled = true;
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x00151BC4 File Offset: 0x0014FDC4
		// (set) Token: 0x06005106 RID: 20742 RVA: 0x00151BCC File Offset: 0x0014FDCC
		public ADObjectId Identity { get; set; }

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x00151BD5 File Offset: 0x0014FDD5
		// (set) Token: 0x06005108 RID: 20744 RVA: 0x00151BDD File Offset: 0x0014FDDD
		public string Name { get; set; }

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x00151BE6 File Offset: 0x0014FDE6
		// (set) Token: 0x0600510A RID: 20746 RVA: 0x00151BEE File Offset: 0x0014FDEE
		public string TLSSenderCertificateName { get; set; }

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x00151BF7 File Offset: 0x0014FDF7
		// (set) Token: 0x0600510C RID: 20748 RVA: 0x00151BFF File Offset: 0x0014FDFF
		public TenantConnectorType ConnectorType { get; set; }

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x0600510D RID: 20749 RVA: 0x00151C08 File Offset: 0x0014FE08
		// (set) Token: 0x0600510E RID: 20750 RVA: 0x00151C10 File Offset: 0x0014FE10
		public TenantConnectorSource ConnectorSource { get; set; }

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x0600510F RID: 20751 RVA: 0x00151C19 File Offset: 0x0014FE19
		// (set) Token: 0x06005110 RID: 20752 RVA: 0x00151C21 File Offset: 0x0014FE21
		public MultiValuedProperty<AddressSpace> SenderDomains { get; set; }

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x00151C2A File Offset: 0x0014FE2A
		// (set) Token: 0x06005112 RID: 20754 RVA: 0x00151C32 File Offset: 0x0014FE32
		public bool RequireTls { get; set; }

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x00151C3B File Offset: 0x0014FE3B
		// (set) Token: 0x06005114 RID: 20756 RVA: 0x00151C43 File Offset: 0x0014FE43
		public bool CloudServicesMailEnabled { get; set; }

		// Token: 0x06005115 RID: 20757 RVA: 0x00151C4C File Offset: 0x0014FE4C
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return "<New>";
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x00151C68 File Offset: 0x0014FE68
		public bool Equals(IInboundConnector obj)
		{
			return this.RequireTls == obj.RequireTls && this.ConnectorType == obj.ConnectorType && this.CloudServicesMailEnabled == obj.CloudServicesMailEnabled && string.Equals(this.Name, obj.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.TLSSenderCertificateName, obj.TLSSenderCertificateName, StringComparison.InvariantCultureIgnoreCase) && TaskCommon.ContainsSame<AddressSpace>(this.SenderDomains, obj.SenderDomains);
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00151CDC File Offset: 0x0014FEDC
		public IInboundConnector Clone(ADObjectId identity)
		{
			InboundConnector inboundConnector = new InboundConnector();
			inboundConnector.UpdateFrom(this);
			inboundConnector.Identity = identity;
			return inboundConnector;
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x00151D00 File Offset: 0x0014FF00
		public void UpdateFrom(IInboundConnector obj)
		{
			this.Name = obj.Name;
			this.TLSSenderCertificateName = obj.TLSSenderCertificateName;
			this.ConnectorType = obj.ConnectorType;
			this.ConnectorSource = obj.ConnectorSource;
			this.SenderDomains = obj.SenderDomains;
			this.RequireTls = obj.RequireTls;
			this.CloudServicesMailEnabled = obj.CloudServicesMailEnabled;
		}
	}
}
