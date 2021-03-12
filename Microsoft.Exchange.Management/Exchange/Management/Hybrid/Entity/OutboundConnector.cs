using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008F8 RID: 2296
	internal class OutboundConnector : IOutboundConnector, IEntity<IOutboundConnector>
	{
		// Token: 0x06005167 RID: 20839 RVA: 0x0015226C File Offset: 0x0015046C
		public OutboundConnector()
		{
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00152274 File Offset: 0x00150474
		public OutboundConnector(TenantOutboundConnector oc)
		{
			this.Identity = (ADObjectId)oc.Identity;
			this.Name = oc.Name;
			this.TlsDomain = TaskCommon.ToStringOrNull(oc.TlsDomain);
			this.ConnectorType = oc.ConnectorType;
			this.ConnectorSource = oc.ConnectorSource;
			this.RecipientDomains = oc.RecipientDomains;
			this.SmartHosts = oc.SmartHosts;
			this.TlsSettings = oc.TlsSettings;
			this.CloudServicesMailEnabled = oc.CloudServicesMailEnabled;
			this.RouteAllMessagesViaOnPremises = oc.RouteAllMessagesViaOnPremises;
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x0015230C File Offset: 0x0015050C
		public OutboundConnector(string name, string tlsDomain, MultiValuedProperty<SmtpDomainWithSubdomains> recipientDomains, MultiValuedProperty<SmartHost> smartHosts, bool routeAllMessagesViaOnPremises)
		{
			this.Name = name;
			this.TlsDomain = tlsDomain;
			this.ConnectorType = TenantConnectorType.OnPremises;
			this.RecipientDomains = recipientDomains;
			this.SmartHosts = smartHosts;
			this.TlsSettings = new TlsAuthLevel?(TlsAuthLevel.DomainValidation);
			this.CloudServicesMailEnabled = true;
			this.RouteAllMessagesViaOnPremises = routeAllMessagesViaOnPremises;
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x0015235E File Offset: 0x0015055E
		// (set) Token: 0x0600516B RID: 20843 RVA: 0x00152366 File Offset: 0x00150566
		public ADObjectId Identity { get; set; }

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0015236F File Offset: 0x0015056F
		// (set) Token: 0x0600516D RID: 20845 RVA: 0x00152377 File Offset: 0x00150577
		public string Name { get; set; }

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x0600516E RID: 20846 RVA: 0x00152380 File Offset: 0x00150580
		// (set) Token: 0x0600516F RID: 20847 RVA: 0x00152388 File Offset: 0x00150588
		public string TlsDomain { get; set; }

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x06005170 RID: 20848 RVA: 0x00152391 File Offset: 0x00150591
		// (set) Token: 0x06005171 RID: 20849 RVA: 0x00152399 File Offset: 0x00150599
		public TenantConnectorType ConnectorType { get; set; }

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06005172 RID: 20850 RVA: 0x001523A2 File Offset: 0x001505A2
		// (set) Token: 0x06005173 RID: 20851 RVA: 0x001523AA File Offset: 0x001505AA
		public TenantConnectorSource ConnectorSource { get; set; }

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x001523B3 File Offset: 0x001505B3
		// (set) Token: 0x06005175 RID: 20853 RVA: 0x001523BB File Offset: 0x001505BB
		public MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains { get; set; }

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x001523C4 File Offset: 0x001505C4
		// (set) Token: 0x06005177 RID: 20855 RVA: 0x001523CC File Offset: 0x001505CC
		public MultiValuedProperty<SmartHost> SmartHosts { get; set; }

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x001523D5 File Offset: 0x001505D5
		// (set) Token: 0x06005179 RID: 20857 RVA: 0x001523DD File Offset: 0x001505DD
		public TlsAuthLevel? TlsSettings { get; set; }

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x001523E6 File Offset: 0x001505E6
		// (set) Token: 0x0600517B RID: 20859 RVA: 0x001523EE File Offset: 0x001505EE
		public bool CloudServicesMailEnabled { get; set; }

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x001523F7 File Offset: 0x001505F7
		// (set) Token: 0x0600517D RID: 20861 RVA: 0x001523FF File Offset: 0x001505FF
		public bool RouteAllMessagesViaOnPremises { get; set; }

		// Token: 0x0600517E RID: 20862 RVA: 0x00152408 File Offset: 0x00150608
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return "<New>";
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00152424 File Offset: 0x00150624
		public bool Equals(IOutboundConnector obj)
		{
			return this.TlsSettings == obj.TlsSettings && this.ConnectorType == obj.ConnectorType && this.CloudServicesMailEnabled == obj.CloudServicesMailEnabled && this.RouteAllMessagesViaOnPremises == obj.RouteAllMessagesViaOnPremises && TaskCommon.ContainsSame<SmartHost>(this.SmartHosts, obj.SmartHosts) && string.Equals(this.Name, obj.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.TlsDomain, obj.TlsDomain, StringComparison.InvariantCultureIgnoreCase) && TaskCommon.ContainsSame<SmtpDomainWithSubdomains>(this.RecipientDomains, obj.RecipientDomains);
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x001524DC File Offset: 0x001506DC
		public IOutboundConnector Clone(ADObjectId identity)
		{
			OutboundConnector outboundConnector = new OutboundConnector();
			outboundConnector.UpdateFrom(this);
			outboundConnector.Identity = identity;
			return outboundConnector;
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00152500 File Offset: 0x00150700
		public void UpdateFrom(IOutboundConnector obj)
		{
			this.Name = obj.Name;
			this.TlsDomain = obj.TlsDomain;
			this.ConnectorType = obj.ConnectorType;
			this.ConnectorSource = obj.ConnectorSource;
			this.RecipientDomains = obj.RecipientDomains;
			this.SmartHosts = obj.SmartHosts;
			this.TlsSettings = obj.TlsSettings;
			this.CloudServicesMailEnabled = obj.CloudServicesMailEnabled;
			this.RouteAllMessagesViaOnPremises = obj.RouteAllMessagesViaOnPremises;
		}
	}
}
