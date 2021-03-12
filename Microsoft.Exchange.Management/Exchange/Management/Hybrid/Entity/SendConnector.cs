using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x02000901 RID: 2305
	internal class SendConnector : ISendConnector, IEntity<ISendConnector>
	{
		// Token: 0x060051C0 RID: 20928 RVA: 0x00152A24 File Offset: 0x00150C24
		public SendConnector()
		{
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00152A2C File Offset: 0x00150C2C
		public SendConnector(SmtpSendConnectorConfig sc)
		{
			this.Identity = (ADObjectId)sc.Identity;
			this.Name = sc.Name;
			this.AddressSpaces = sc.AddressSpaces;
			this.SourceTransportServers = sc.SourceTransportServers;
			this.DNSRoutingEnabled = sc.DNSRoutingEnabled;
			this.SmartHosts = sc.SmartHosts;
			this.RequireTLS = sc.RequireTLS;
			this.TlsAuthLevel = sc.TlsAuthLevel;
			this.TlsDomain = TaskCommon.ToStringOrNull(sc.TlsDomain);
			this.ErrorPolicies = sc.ErrorPolicies;
			this.TlsCertificateName = sc.TlsCertificateName;
			this.CloudServicesMailEnabled = sc.CloudServicesMailEnabled;
			this.Fqdn = TaskCommon.ToStringOrNull(sc.Fqdn);
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00152AEC File Offset: 0x00150CEC
		public SendConnector(string name, MultiValuedProperty<AddressSpace> addressSpaces, MultiValuedProperty<ADObjectId> transportServers, string tlsDomain, SmtpX509Identifier tlsCertificateName, bool requireTLS, string fqdn)
		{
			this.Name = name;
			this.AddressSpaces = addressSpaces;
			this.SourceTransportServers = transportServers;
			this.DNSRoutingEnabled = true;
			this.SmartHosts = null;
			this.RequireTLS = requireTLS;
			this.TlsAuthLevel = (requireTLS ? new TlsAuthLevel?(Microsoft.Exchange.Data.TlsAuthLevel.DomainValidation) : null);
			this.TlsDomain = (requireTLS ? tlsDomain : null);
			this.ErrorPolicies = ErrorPolicies.Default;
			this.TlsCertificateName = tlsCertificateName;
			this.CloudServicesMailEnabled = true;
			this.Fqdn = fqdn;
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x00152B72 File Offset: 0x00150D72
		// (set) Token: 0x060051C4 RID: 20932 RVA: 0x00152B7A File Offset: 0x00150D7A
		public ADObjectId Identity { get; set; }

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x00152B83 File Offset: 0x00150D83
		// (set) Token: 0x060051C6 RID: 20934 RVA: 0x00152B8B File Offset: 0x00150D8B
		public string Name { get; set; }

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x060051C7 RID: 20935 RVA: 0x00152B94 File Offset: 0x00150D94
		// (set) Token: 0x060051C8 RID: 20936 RVA: 0x00152B9C File Offset: 0x00150D9C
		public MultiValuedProperty<AddressSpace> AddressSpaces { get; set; }

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x00152BA5 File Offset: 0x00150DA5
		// (set) Token: 0x060051CA RID: 20938 RVA: 0x00152BAD File Offset: 0x00150DAD
		public MultiValuedProperty<ADObjectId> SourceTransportServers { get; set; }

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x00152BB6 File Offset: 0x00150DB6
		// (set) Token: 0x060051CC RID: 20940 RVA: 0x00152BBE File Offset: 0x00150DBE
		public bool DNSRoutingEnabled { get; set; }

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x00152BC7 File Offset: 0x00150DC7
		// (set) Token: 0x060051CE RID: 20942 RVA: 0x00152BCF File Offset: 0x00150DCF
		public MultiValuedProperty<SmartHost> SmartHosts { get; set; }

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x00152BD8 File Offset: 0x00150DD8
		// (set) Token: 0x060051D0 RID: 20944 RVA: 0x00152BE0 File Offset: 0x00150DE0
		public bool RequireTLS { get; set; }

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x00152BE9 File Offset: 0x00150DE9
		// (set) Token: 0x060051D2 RID: 20946 RVA: 0x00152BF1 File Offset: 0x00150DF1
		public TlsAuthLevel? TlsAuthLevel { get; set; }

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x00152BFA File Offset: 0x00150DFA
		// (set) Token: 0x060051D4 RID: 20948 RVA: 0x00152C02 File Offset: 0x00150E02
		public string TlsDomain { get; set; }

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x00152C0B File Offset: 0x00150E0B
		// (set) Token: 0x060051D6 RID: 20950 RVA: 0x00152C13 File Offset: 0x00150E13
		public ErrorPolicies ErrorPolicies { get; set; }

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x00152C1C File Offset: 0x00150E1C
		// (set) Token: 0x060051D8 RID: 20952 RVA: 0x00152C24 File Offset: 0x00150E24
		public SmtpX509Identifier TlsCertificateName { get; set; }

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x00152C2D File Offset: 0x00150E2D
		// (set) Token: 0x060051DA RID: 20954 RVA: 0x00152C35 File Offset: 0x00150E35
		public string Fqdn { get; set; }

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x00152C3E File Offset: 0x00150E3E
		// (set) Token: 0x060051DC RID: 20956 RVA: 0x00152C46 File Offset: 0x00150E46
		public bool CloudServicesMailEnabled { get; set; }

		// Token: 0x060051DD RID: 20957 RVA: 0x00152C4F File Offset: 0x00150E4F
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return "<New>";
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00152C6C File Offset: 0x00150E6C
		public bool Equals(ISendConnector obj)
		{
			return this.CloudServicesMailEnabled == obj.CloudServicesMailEnabled && string.Equals(this.Name, obj.Name, StringComparison.InvariantCultureIgnoreCase) && this.RequireTLS == obj.RequireTLS && this.DNSRoutingEnabled == obj.DNSRoutingEnabled && this.TlsAuthLevel == obj.TlsAuthLevel && this.ErrorPolicies == obj.ErrorPolicies && string.Equals(this.TlsDomain, obj.TlsDomain, StringComparison.InvariantCultureIgnoreCase) && TaskCommon.AreEqual(this.TlsCertificateName, obj.TlsCertificateName) && TaskCommon.ContainsSame<AddressSpace>(this.AddressSpaces, obj.AddressSpaces) && TaskCommon.ContainsSame<SmartHost>(this.SmartHosts, obj.SmartHosts) && TaskCommon.ContainsSame<ADObjectId>(this.SourceTransportServers, obj.SourceTransportServers) && string.Equals(this.Fqdn, obj.Fqdn, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00152D7C File Offset: 0x00150F7C
		public ISendConnector Clone(ADObjectId identity)
		{
			SendConnector sendConnector = new SendConnector();
			sendConnector.UpdateFrom(this);
			sendConnector.Identity = identity;
			return sendConnector;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00152DA0 File Offset: 0x00150FA0
		public void UpdateFrom(ISendConnector obj)
		{
			this.Name = obj.Name;
			this.AddressSpaces = obj.AddressSpaces;
			this.SourceTransportServers = obj.SourceTransportServers;
			this.DNSRoutingEnabled = obj.DNSRoutingEnabled;
			this.SmartHosts = obj.SmartHosts;
			this.RequireTLS = obj.RequireTLS;
			this.TlsAuthLevel = obj.TlsAuthLevel;
			this.TlsDomain = obj.TlsDomain;
			this.ErrorPolicies = obj.ErrorPolicies;
			this.TlsCertificateName = obj.TlsCertificateName;
			this.CloudServicesMailEnabled = obj.CloudServicesMailEnabled;
			this.Fqdn = obj.Fqdn;
		}
	}
}
