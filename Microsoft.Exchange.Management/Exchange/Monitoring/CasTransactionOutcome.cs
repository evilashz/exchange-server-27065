using System;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000524 RID: 1316
	[Serializable]
	public class CasTransactionOutcome : TransactionOutcomeBase
	{
		// Token: 0x06002F57 RID: 12119 RVA: 0x000BEA24 File Offset: 0x000BCC24
		internal CasTransactionOutcome(string clientAccessServer, string scenarioName, string scenarioDescription, string performanceCounterName, string localSite, bool secureAccess, string userName) : base((!string.IsNullOrEmpty(clientAccessServer)) ? ServerIdParameter.Parse(clientAccessServer).ToString() : string.Empty, scenarioName, scenarioDescription, performanceCounterName, userName)
		{
			this.LocalSite = ((!string.IsNullOrEmpty(localSite)) ? AdSiteIdParameter.Parse(localSite).ToString() : string.Empty);
			this.SecureAccess = secureAccess;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000BEA81 File Offset: 0x000BCC81
		internal CasTransactionOutcome(string clientAccessServer, string scenarioName, string scenarioDescription, string performanceCounterName, string localSite, bool secureAccess, string userName, string virtualDirectoryName, Uri url, VirtualDirectoryUriScope urlType) : this(clientAccessServer, scenarioName, scenarioDescription, performanceCounterName, localSite, secureAccess, userName)
		{
			this.VirtualDirectoryName = virtualDirectoryName;
			this.Url = url;
			this.UrlType = urlType;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000BEAAC File Offset: 0x000BCCAC
		internal CasTransactionOutcome(string clientAccessServer, string scenarioName, string scenarioDescription, string performanceCounterName, string localSite, bool secureAccess, string userName, string virtualDirectoryName, Uri url, VirtualDirectoryUriScope urlType, int port, ProtocolConnectionType connectionType) : this(clientAccessServer, scenarioName, scenarioDescription, performanceCounterName, localSite, secureAccess, userName)
		{
			this.VirtualDirectoryName = virtualDirectoryName;
			this.Url = url;
			this.UrlType = urlType;
			this.Port = port;
			this.ConnectionType = connectionType;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000BEAE8 File Offset: 0x000BCCE8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Strings.CasHealthClientAccessServerName + ": ");
			stringBuilder.Append(((base.ClientAccessServer != null) ? base.ClientAccessServer.ToString() : "") + "\r\n");
			stringBuilder.Append(Strings.CasHealthScenario + ": ");
			stringBuilder.Append(base.Scenario + "\r\n");
			stringBuilder.Append(Strings.CasHealthScenarioDescription + ": ");
			stringBuilder.Append(base.ScenarioDescription + "\r\n");
			stringBuilder.Append(Strings.CasHealthUserNameHeader + ": ");
			stringBuilder.Append((base.UserName ?? "null") + "\r\n");
			stringBuilder.Append(Strings.CasHealthPerformanceCounterName + ": ");
			stringBuilder.Append(base.PerformanceCounterName + "\r\n");
			stringBuilder.Append(Strings.CasHealthResult + ": ");
			stringBuilder.Append(base.Result + "\r\n");
			stringBuilder.Append(Strings.CasHealthSiteName + ": ");
			stringBuilder.Append(this.LocalSite + "\r\n");
			stringBuilder.Append(Strings.CasHealthLatency + ": ");
			stringBuilder.Append(base.Latency + "\r\n");
			stringBuilder.Append(Strings.CasHealthSecureAccess + ": ");
			stringBuilder.Append(this.SecureAccess + "\r\n");
			stringBuilder.Append(Strings.CasHealthConnectionType + ": ");
			stringBuilder.Append(this.ConnectionType + "\r\n");
			stringBuilder.Append(Strings.CasHealthPortnumber + ": ");
			stringBuilder.Append(this.Port + "\r\n");
			stringBuilder.Append(Strings.CasHealthLatencyHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append(base.Latency.TotalMilliseconds);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthOwaVdirNameHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append((this.VirtualDirectoryName != null) ? this.VirtualDirectoryName.ToString() : "null");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthOwaUriHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append((this.Url != null) ? this.Url.ToString() : "null");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthOwaUriScopeHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append(this.UrlType);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthAdditionalInformation + ": \r\n");
			stringBuilder.Append(base.Error + "\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000BEE9D File Offset: 0x000BD09D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000BEEA4 File Offset: 0x000BD0A4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return CasTransactionOutcome.schema;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000BEEAB File Offset: 0x000BD0AB
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x000BEEC2 File Offset: 0x000BD0C2
		public string LocalSite
		{
			get
			{
				return (string)this.propertyBag[CasTransactionOutcomeSchema.LocalSite];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.LocalSite] = value;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000BEED5 File Offset: 0x000BD0D5
		// (set) Token: 0x06002F60 RID: 12128 RVA: 0x000BEEEC File Offset: 0x000BD0EC
		public bool SecureAccess
		{
			get
			{
				return (bool)this.propertyBag[CasTransactionOutcomeSchema.SecureAccess];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.SecureAccess] = value;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x000BEF04 File Offset: 0x000BD104
		// (set) Token: 0x06002F62 RID: 12130 RVA: 0x000BEF1B File Offset: 0x000BD11B
		public string VirtualDirectoryName
		{
			get
			{
				return (string)this.propertyBag[CasTransactionOutcomeSchema.VirtualDirectoryName];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.VirtualDirectoryName] = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000BEF2E File Offset: 0x000BD12E
		// (set) Token: 0x06002F64 RID: 12132 RVA: 0x000BEF45 File Offset: 0x000BD145
		public Uri Url
		{
			get
			{
				return (Uri)this.propertyBag[CasTransactionOutcomeSchema.Url];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.Url] = value;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000BEF58 File Offset: 0x000BD158
		// (set) Token: 0x06002F66 RID: 12134 RVA: 0x000BEF6F File Offset: 0x000BD16F
		public VirtualDirectoryUriScope UrlType
		{
			get
			{
				return (VirtualDirectoryUriScope)this.propertyBag[CasTransactionOutcomeSchema.UrlType];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.UrlType] = value;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000BEF87 File Offset: 0x000BD187
		// (set) Token: 0x06002F68 RID: 12136 RVA: 0x000BEF9E File Offset: 0x000BD19E
		public int Port
		{
			get
			{
				return (int)this.propertyBag[CasTransactionOutcomeSchema.Port];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.Port] = value;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000BEFB6 File Offset: 0x000BD1B6
		// (set) Token: 0x06002F6A RID: 12138 RVA: 0x000BEFCD File Offset: 0x000BD1CD
		public ProtocolConnectionType ConnectionType
		{
			get
			{
				return (ProtocolConnectionType)this.propertyBag[CasTransactionOutcomeSchema.ConnectionType];
			}
			internal set
			{
				this.propertyBag[CasTransactionOutcomeSchema.ConnectionType] = value;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000BEFE8 File Offset: 0x000BD1E8
		public string ClientAccessServerShortName
		{
			get
			{
				string text = base.ClientAccessServer;
				if (!string.IsNullOrEmpty(text))
				{
					if (text.IndexOf('.') > 0)
					{
						text = text.Substring(0, text.IndexOf('.'));
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000BF028 File Offset: 0x000BD228
		public string LocalSiteShortName
		{
			get
			{
				string text = this.LocalSite;
				if (!string.IsNullOrEmpty(text))
				{
					if (text.IndexOf('.') > 0)
					{
						text = text.Substring(0, text.IndexOf('.'));
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x040021E2 RID: 8674
		private static CasTransactionOutcomeSchema schema = ObjectSchema.GetInstance<CasTransactionOutcomeSchema>();
	}
}
