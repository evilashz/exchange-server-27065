using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000877 RID: 2167
	[Serializable]
	public sealed class HybridMailflowConfiguration : ConfigurableObject
	{
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x00137691 File Offset: 0x00135891
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return HybridMailflowConfiguration.schema;
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x00137698 File Offset: 0x00135898
		// (set) Token: 0x06004B15 RID: 19221 RVA: 0x001376A0 File Offset: 0x001358A0
		[Parameter]
		public List<SmtpDomainWithSubdomains> OutboundDomains
		{
			get
			{
				return this.myOutboundDomains;
			}
			set
			{
				this.myOutboundDomains = value;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x001376A9 File Offset: 0x001358A9
		// (set) Token: 0x06004B17 RID: 19223 RVA: 0x001376B1 File Offset: 0x001358B1
		[Parameter]
		public List<IPRange> InboundIPs
		{
			get
			{
				return this.myInboundIPs;
			}
			set
			{
				this.myInboundIPs = value;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x001376BA File Offset: 0x001358BA
		// (set) Token: 0x06004B19 RID: 19225 RVA: 0x001376C2 File Offset: 0x001358C2
		[Parameter]
		public Fqdn OnPremisesFQDN
		{
			get
			{
				return this.myOnPremisesFQDN;
			}
			set
			{
				this.myOnPremisesFQDN = value;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x001376CB File Offset: 0x001358CB
		// (set) Token: 0x06004B1B RID: 19227 RVA: 0x001376D3 File Offset: 0x001358D3
		[Parameter]
		public string CertificateSubject
		{
			get
			{
				return this.myCertificateSubject;
			}
			set
			{
				this.myCertificateSubject = value;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x001376DC File Offset: 0x001358DC
		// (set) Token: 0x06004B1D RID: 19229 RVA: 0x001376E4 File Offset: 0x001358E4
		[Parameter]
		public bool? SecureMailEnabled
		{
			get
			{
				return this.mySecureMailEnabled;
			}
			set
			{
				this.mySecureMailEnabled = value;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x001376ED File Offset: 0x001358ED
		// (set) Token: 0x06004B1F RID: 19231 RVA: 0x001376F5 File Offset: 0x001358F5
		[Parameter]
		public bool? CentralizedTransportEnabled
		{
			get
			{
				return this.myCentralizedTransportEnabled;
			}
			set
			{
				this.myCentralizedTransportEnabled = value;
			}
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x001376FE File Offset: 0x001358FE
		internal HybridMailflowConfiguration() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x0013770B File Offset: 0x0013590B
		internal HybridMailflowConfiguration(List<SmtpDomainWithSubdomains> outboundDomains, List<IPRange> inboundIPs, Fqdn onPremisesFQDN, string certificateSubject, bool? secureMailEnabled, bool? centralizedTransportEnabled) : base(new SimpleProviderPropertyBag())
		{
			this.OutboundDomains = outboundDomains;
			this.InboundIPs = inboundIPs;
			this.OnPremisesFQDN = onPremisesFQDN;
			this.CertificateSubject = certificateSubject;
			this.SecureMailEnabled = secureMailEnabled;
			this.CentralizedTransportEnabled = centralizedTransportEnabled;
		}

		// Token: 0x04002D11 RID: 11537
		private const string MostDerivedClass = "msHybridMailflowEhfConfiguration";

		// Token: 0x04002D12 RID: 11538
		private static HybridMailflowConfigurationSchema schema = ObjectSchema.GetInstance<HybridMailflowConfigurationSchema>();

		// Token: 0x04002D13 RID: 11539
		private List<SmtpDomainWithSubdomains> myOutboundDomains;

		// Token: 0x04002D14 RID: 11540
		private List<IPRange> myInboundIPs;

		// Token: 0x04002D15 RID: 11541
		private Fqdn myOnPremisesFQDN;

		// Token: 0x04002D16 RID: 11542
		private string myCertificateSubject;

		// Token: 0x04002D17 RID: 11543
		private bool? mySecureMailEnabled;

		// Token: 0x04002D18 RID: 11544
		private bool? myCentralizedTransportEnabled;
	}
}
