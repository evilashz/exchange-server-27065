using System;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B3F RID: 2879
	[Cmdlet("New", "SendConnector", SupportsShouldProcess = true, DefaultParameterSetName = "AddressSpaces")]
	public sealed class NewSendConnector : NewSystemConfigurationObjectTask<SmtpSendConnectorConfig>
	{
		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x001AE520 File Offset: 0x001AC720
		// (set) Token: 0x06006851 RID: 26705 RVA: 0x001AE528 File Offset: 0x001AC728
		[Parameter(Mandatory = false)]
		public NewSendConnector.UsageType Usage
		{
			internal get
			{
				return this.usage;
			}
			set
			{
				this.usage = value;
				this.usageSetCount++;
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x06006852 RID: 26706 RVA: 0x001AE53F File Offset: 0x001AC73F
		// (set) Token: 0x06006853 RID: 26707 RVA: 0x001AE54F File Offset: 0x001AC74F
		[Parameter(Mandatory = false)]
		public SwitchParameter Internet
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewSendConnector.UsageType.Internet);
			}
			set
			{
				this.Usage = NewSendConnector.UsageType.Internet;
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x001AE558 File Offset: 0x001AC758
		// (set) Token: 0x06006855 RID: 26709 RVA: 0x001AE568 File Offset: 0x001AC768
		[Parameter(Mandatory = false)]
		public SwitchParameter Internal
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewSendConnector.UsageType.Internal);
			}
			set
			{
				this.Usage = NewSendConnector.UsageType.Internal;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06006856 RID: 26710 RVA: 0x001AE571 File Offset: 0x001AC771
		// (set) Token: 0x06006857 RID: 26711 RVA: 0x001AE581 File Offset: 0x001AC781
		[Parameter(Mandatory = false)]
		public SwitchParameter Partner
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewSendConnector.UsageType.Partner);
			}
			set
			{
				this.Usage = NewSendConnector.UsageType.Partner;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06006858 RID: 26712 RVA: 0x001AE58A File Offset: 0x001AC78A
		// (set) Token: 0x06006859 RID: 26713 RVA: 0x001AE59A File Offset: 0x001AC79A
		[Parameter(Mandatory = false)]
		public SwitchParameter Custom
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewSendConnector.UsageType.Custom);
			}
			set
			{
				this.Usage = NewSendConnector.UsageType.Custom;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x001AE5A3 File Offset: 0x001AC7A3
		// (set) Token: 0x0600685B RID: 26715 RVA: 0x001AE5AB File Offset: 0x001AC7AB
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			internal get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x0600685C RID: 26716 RVA: 0x001AE5B4 File Offset: 0x001AC7B4
		// (set) Token: 0x0600685D RID: 26717 RVA: 0x001AE5C1 File Offset: 0x001AC7C1
		[Parameter(ParameterSetName = "AddressSpaces", Mandatory = true)]
		public MultiValuedProperty<AddressSpace> AddressSpaces
		{
			get
			{
				return this.DataObject.AddressSpaces;
			}
			set
			{
				this.DataObject.AddressSpaces = value;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x0600685E RID: 26718 RVA: 0x001AE5CF File Offset: 0x001AC7CF
		// (set) Token: 0x0600685F RID: 26719 RVA: 0x001AE5DC File Offset: 0x001AC7DC
		[Parameter(Mandatory = false)]
		public bool DomainSecureEnabled
		{
			get
			{
				return this.DataObject.DomainSecureEnabled;
			}
			set
			{
				this.DataObject.DomainSecureEnabled = value;
				this.isDomainSecureEnabledSet = true;
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06006860 RID: 26720 RVA: 0x001AE5F1 File Offset: 0x001AC7F1
		// (set) Token: 0x06006861 RID: 26721 RVA: 0x001AE5FE File Offset: 0x001AC7FE
		[Parameter]
		public bool DNSRoutingEnabled
		{
			get
			{
				return this.DataObject.DNSRoutingEnabled;
			}
			set
			{
				this.DataObject.DNSRoutingEnabled = value;
				this.isDnsRoutingEnabledSet = true;
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06006862 RID: 26722 RVA: 0x001AE613 File Offset: 0x001AC813
		// (set) Token: 0x06006863 RID: 26723 RVA: 0x001AE620 File Offset: 0x001AC820
		[Parameter]
		public MultiValuedProperty<SmartHost> SmartHosts
		{
			get
			{
				return this.DataObject.SmartHosts;
			}
			set
			{
				this.DataObject.SmartHosts = value;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06006864 RID: 26724 RVA: 0x001AE62E File Offset: 0x001AC82E
		// (set) Token: 0x06006865 RID: 26725 RVA: 0x001AE63B File Offset: 0x001AC83B
		[Parameter]
		public int Port
		{
			get
			{
				return this.DataObject.Port;
			}
			set
			{
				this.DataObject.Port = value;
			}
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06006866 RID: 26726 RVA: 0x001AE649 File Offset: 0x001AC849
		// (set) Token: 0x06006867 RID: 26727 RVA: 0x001AE656 File Offset: 0x001AC856
		[Parameter]
		public EnhancedTimeSpan ConnectionInactivityTimeOut
		{
			get
			{
				return this.DataObject.ConnectionInactivityTimeOut;
			}
			set
			{
				this.DataObject.ConnectionInactivityTimeOut = value;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x001AE664 File Offset: 0x001AC864
		// (set) Token: 0x06006869 RID: 26729 RVA: 0x001AE671 File Offset: 0x001AC871
		[Parameter]
		public Unlimited<ByteQuantifiedSize> MaxMessageSize
		{
			get
			{
				return this.DataObject.MaxMessageSize;
			}
			set
			{
				this.DataObject.MaxMessageSize = value;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x0600686A RID: 26730 RVA: 0x001AE67F File Offset: 0x001AC87F
		// (set) Token: 0x0600686B RID: 26731 RVA: 0x001AE68C File Offset: 0x001AC88C
		[Parameter(Mandatory = false)]
		public Fqdn Fqdn
		{
			get
			{
				return this.DataObject.Fqdn;
			}
			set
			{
				this.DataObject.Fqdn = value;
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x0600686C RID: 26732 RVA: 0x001AE69A File Offset: 0x001AC89A
		// (set) Token: 0x0600686D RID: 26733 RVA: 0x001AE6A7 File Offset: 0x001AC8A7
		[Parameter(Mandatory = false)]
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return this.DataObject.TlsCertificateName;
			}
			set
			{
				this.DataObject.TlsCertificateName = value;
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x0600686E RID: 26734 RVA: 0x001AE6B5 File Offset: 0x001AC8B5
		// (set) Token: 0x0600686F RID: 26735 RVA: 0x001AE6C2 File Offset: 0x001AC8C2
		[Parameter]
		public bool ForceHELO
		{
			get
			{
				return this.DataObject.ForceHELO;
			}
			set
			{
				this.DataObject.ForceHELO = value;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x06006870 RID: 26736 RVA: 0x001AE6D0 File Offset: 0x001AC8D0
		// (set) Token: 0x06006871 RID: 26737 RVA: 0x001AE6DD File Offset: 0x001AC8DD
		[Parameter]
		public bool FrontendProxyEnabled
		{
			get
			{
				return this.DataObject.FrontendProxyEnabled;
			}
			set
			{
				this.DataObject.FrontendProxyEnabled = value;
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x06006872 RID: 26738 RVA: 0x001AE6EB File Offset: 0x001AC8EB
		// (set) Token: 0x06006873 RID: 26739 RVA: 0x001AE6F8 File Offset: 0x001AC8F8
		[Parameter]
		public bool IgnoreSTARTTLS
		{
			get
			{
				return this.DataObject.IgnoreSTARTTLS;
			}
			set
			{
				this.DataObject.IgnoreSTARTTLS = value;
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x001AE706 File Offset: 0x001AC906
		// (set) Token: 0x06006875 RID: 26741 RVA: 0x001AE713 File Offset: 0x001AC913
		[Parameter]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return this.DataObject.CloudServicesMailEnabled;
			}
			set
			{
				this.DataObject.CloudServicesMailEnabled = value;
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x001AE721 File Offset: 0x001AC921
		// (set) Token: 0x06006877 RID: 26743 RVA: 0x001AE72E File Offset: 0x001AC92E
		[Parameter]
		public bool RequireOorg
		{
			get
			{
				return this.DataObject.RequireOorg;
			}
			set
			{
				this.DataObject.RequireOorg = value;
			}
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x001AE73C File Offset: 0x001AC93C
		// (set) Token: 0x06006879 RID: 26745 RVA: 0x001AE749 File Offset: 0x001AC949
		[Parameter]
		public bool RequireTLS
		{
			get
			{
				return this.DataObject.RequireTLS;
			}
			set
			{
				this.DataObject.RequireTLS = value;
			}
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x001AE757 File Offset: 0x001AC957
		// (set) Token: 0x0600687B RID: 26747 RVA: 0x001AE764 File Offset: 0x001AC964
		[Parameter]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x001AE772 File Offset: 0x001AC972
		// (set) Token: 0x0600687D RID: 26749 RVA: 0x001AE77F File Offset: 0x001AC97F
		[Parameter]
		public ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				return this.DataObject.ProtocolLoggingLevel;
			}
			set
			{
				this.DataObject.ProtocolLoggingLevel = value;
			}
		}

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x001AE78D File Offset: 0x001AC98D
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x001AE79A File Offset: 0x001AC99A
		[Parameter]
		public SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
		{
			get
			{
				return this.DataObject.SmartHostAuthMechanism;
			}
			set
			{
				this.DataObject.SmartHostAuthMechanism = value;
			}
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x001AE7A8 File Offset: 0x001AC9A8
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x001AE7B5 File Offset: 0x001AC9B5
		[Parameter]
		public bool UseExternalDNSServersEnabled
		{
			get
			{
				return this.DataObject.UseExternalDNSServersEnabled;
			}
			set
			{
				this.DataObject.UseExternalDNSServersEnabled = value;
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x001AE7C3 File Offset: 0x001AC9C3
		// (set) Token: 0x06006883 RID: 26755 RVA: 0x001AE7D0 File Offset: 0x001AC9D0
		[Parameter]
		public string Comment
		{
			get
			{
				return this.DataObject.Comment;
			}
			set
			{
				this.DataObject.Comment = value;
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x001AE7DE File Offset: 0x001AC9DE
		// (set) Token: 0x06006885 RID: 26757 RVA: 0x001AE7EB File Offset: 0x001AC9EB
		[Parameter]
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.DataObject.SourceIPAddress;
			}
			set
			{
				this.DataObject.SourceIPAddress = value;
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x001AE7F9 File Offset: 0x001AC9F9
		// (set) Token: 0x06006887 RID: 26759 RVA: 0x001AE806 File Offset: 0x001ACA06
		[Parameter]
		public int SmtpMaxMessagesPerConnection
		{
			get
			{
				return this.DataObject.SmtpMaxMessagesPerConnection;
			}
			set
			{
				this.DataObject.SmtpMaxMessagesPerConnection = value;
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06006888 RID: 26760 RVA: 0x001AE814 File Offset: 0x001ACA14
		// (set) Token: 0x06006889 RID: 26761 RVA: 0x001AE821 File Offset: 0x001ACA21
		[Parameter]
		public PSCredential AuthenticationCredential
		{
			get
			{
				return this.DataObject.AuthenticationCredential;
			}
			set
			{
				this.DataObject.AuthenticationCredential = value;
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x0600688A RID: 26762 RVA: 0x001AE82F File Offset: 0x001ACA2F
		// (set) Token: 0x0600688B RID: 26763 RVA: 0x001AE846 File Offset: 0x001ACA46
		[Parameter]
		public MultiValuedProperty<ServerIdParameter> SourceTransportServers
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["SourceTransportServers"];
			}
			set
			{
				base.Fields["SourceTransportServers"] = value;
			}
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x0600688C RID: 26764 RVA: 0x001AE859 File Offset: 0x001ACA59
		// (set) Token: 0x0600688D RID: 26765 RVA: 0x001AE866 File Offset: 0x001ACA66
		[Parameter(ParameterSetName = "AddressSpaces", Mandatory = false)]
		public bool IsScopedConnector
		{
			get
			{
				return this.DataObject.IsScopedConnector;
			}
			set
			{
				this.DataObject.IsScopedConnector = value;
			}
		}

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x001AE874 File Offset: 0x001ACA74
		// (set) Token: 0x0600688F RID: 26767 RVA: 0x001AE881 File Offset: 0x001ACA81
		[Parameter(Mandatory = false)]
		public SmtpDomainWithSubdomains TlsDomain
		{
			get
			{
				return this.DataObject.TlsDomain;
			}
			set
			{
				this.DataObject.TlsDomain = value;
			}
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x06006890 RID: 26768 RVA: 0x001AE88F File Offset: 0x001ACA8F
		// (set) Token: 0x06006891 RID: 26769 RVA: 0x001AE89C File Offset: 0x001ACA9C
		[Parameter(Mandatory = false)]
		public TlsAuthLevel? TlsAuthLevel
		{
			get
			{
				return this.DataObject.TlsAuthLevel;
			}
			set
			{
				this.DataObject.TlsAuthLevel = value;
			}
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x001AE8AA File Offset: 0x001ACAAA
		// (set) Token: 0x06006893 RID: 26771 RVA: 0x001AE8B7 File Offset: 0x001ACAB7
		[Parameter(Mandatory = false)]
		public ErrorPolicies ErrorPolicies
		{
			get
			{
				return this.DataObject.ErrorPolicies;
			}
			set
			{
				this.DataObject.ErrorPolicies = value;
			}
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06006894 RID: 26772 RVA: 0x001AE8C5 File Offset: 0x001ACAC5
		private bool IsUsageSet
		{
			get
			{
				if (this.usageSetCount > 1)
				{
					throw new ApplicationException("usage parameters should be checked and tasks execution ended before using this method.");
				}
				return this.usageSetCount > 0;
			}
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x001AE8E4 File Offset: 0x001ACAE4
		internal static MultiValuedProperty<ADObjectId> GetDefaultSourceTransportServers()
		{
			Server server = null;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 516, "GetDefaultSourceTransportServers", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\NewSendConnector.cs");
			try
			{
				server = topologyConfigurationSession.FindLocalServer();
			}
			catch (ComputerNameNotCurrentlyAvailableException)
			{
			}
			catch (LocalServerNotFoundException)
			{
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			if (server != null && server.IsHubTransportServer)
			{
				multiValuedProperty.Add(server.Id);
			}
			return multiValuedProperty;
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06006896 RID: 26774 RVA: 0x001AE95C File Offset: 0x001ACB5C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSendConnectorAddressSpaces(base.Name, base.FormatMultiValuedProperty(this.AddressSpaces));
			}
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x001AE978 File Offset: 0x001ACB78
		internal static LocalizedException CrossObjectValidate(SmtpSendConnectorConfig connector, IConfigurationSession session, Server localServer, Task task, out bool multiSiteConnector)
		{
			bool flag;
			LocalizedException ex = NewSendConnector.ValidateSourceServers(connector, session, localServer, task, out flag, out multiSiteConnector);
			if (ex != null)
			{
				return ex;
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(connector.AddressSpaces))
			{
				foreach (AddressSpace addressSpace in connector.AddressSpaces)
				{
					if (!addressSpace.IsSmtpType)
					{
						if (flag)
						{
							return new SendConnectorNonSmtpAddressSpaceOnEdgeException(addressSpace.ToString());
						}
						if (connector.DNSRoutingEnabled)
						{
							return new SendConnectorNonSmtpAddressSpaceOnDNSConnectorException(addressSpace.ToString());
						}
					}
					if (addressSpace.Address != null && addressSpace.Address.Equals("--", StringComparison.InvariantCulture))
					{
						if (!flag)
						{
							return new SendConnectorDashdashAddressSpaceNotOffEdgeException();
						}
						if (connector.AddressSpaces.Count > 1)
						{
							return new SendConnectorDashdashAddressSpaceNotUniqueException();
						}
					}
				}
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(connector.SmartHosts))
			{
				foreach (SmartHost smartHost in connector.SmartHosts)
				{
					if (smartHost.Domain != null && smartHost.Domain.ToString().Equals("--", StringComparison.InvariantCulture))
					{
						if (!flag)
						{
							return new SendConnectorDashdashSmarthostNotOffEdgeException();
						}
						if (connector.SmartHosts.Count > 1)
						{
							return new SendConnectorDashdashSmarthostNotUniqueException();
						}
					}
				}
			}
			if (flag && connector.FrontendProxyEnabled)
			{
				return new SendConnectorProxyEnabledOnEdgeException();
			}
			if (localServer != null && localServer.IsEdgeServer)
			{
				ex = NewSendConnector.ValidateSourceIPAddress(connector);
				if (ex != null)
				{
					return ex;
				}
			}
			return null;
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x001AEB24 File Offset: 0x001ACD24
		internal static Exception CheckDNSAndSmartHostParameters(SmtpSendConnectorConfig sendConnector)
		{
			if (sendConnector.IsModified(SmtpSendConnectorConfigSchema.SmartHostsString) && !string.IsNullOrEmpty(sendConnector.SmartHostsString))
			{
				return new InvalidOperationException(Strings.DNSRoutingEnabledMustNotBeSpecified);
			}
			return null;
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x001AEB51 File Offset: 0x001ACD51
		internal static Exception CheckDNSAndSmartHostAuthMechanismParameters(SmtpSendConnectorConfig sendConnector)
		{
			if (sendConnector.SmartHostAuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.None)
			{
				return null;
			}
			if (sendConnector.IsModified(SmtpSendConnectorConfigSchema.SmartHostAuthMechanism) && !sendConnector.IsModified(SmtpSendConnectorConfigSchema.DNSRoutingEnabled))
			{
				return new InvalidOperationException(Strings.DNSRoutingEnabledMustBeSpecifiedForAuthMechanism);
			}
			return null;
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x001AEB88 File Offset: 0x001ACD88
		internal static Exception CheckTLSParameters(SmtpSendConnectorConfig sendConnector)
		{
			if (sendConnector.RequireTLS && sendConnector.IgnoreSTARTTLS)
			{
				return new InvalidOperationException(Strings.IgnoreRequireTLSConflict);
			}
			return null;
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x001AEBAB File Offset: 0x001ACDAB
		internal static void ClearSmartHostsListIfNecessary(SmtpSendConnectorConfig sendConnector)
		{
			if (sendConnector.DNSRoutingEnabled && sendConnector.SmartHosts != null && sendConnector.SmartHosts.Count != 0)
			{
				sendConnector.SmartHosts = null;
			}
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x001AEBD1 File Offset: 0x001ACDD1
		internal static void SetSmartHostAuthMechanismIfNecessary(SmtpSendConnectorConfig sendConnector)
		{
			if (sendConnector.DNSRoutingEnabled && sendConnector.SmartHostAuthMechanism != SmtpSendConnectorConfig.AuthMechanisms.None)
			{
				sendConnector.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.None;
				sendConnector.AuthenticationCredential = null;
			}
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x001AEBF4 File Offset: 0x001ACDF4
		private RawSecurityDescriptor ConfigureDefaultSecurityDescriptor(RawSecurityDescriptor originalSecurityDescriptor)
		{
			PrincipalPermissionList defaultPermission = NewSendConnector.GetDefaultPermission(this.isHubTransportServer);
			return defaultPermission.AddExtendedRightsToSecurityDescriptor(originalSecurityDescriptor);
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x001AEC44 File Offset: 0x001ACE44
		protected override IConfigurable PrepareDataObject()
		{
			SmtpSendConnectorConfig smtpSendConnectorConfig = (SmtpSendConnectorConfig)base.PrepareDataObject();
			try
			{
				this.localServer = ((ITopologyConfigurationSession)base.DataSession).ReadLocalServer();
			}
			catch (TransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ResourceUnavailable, this.DataObject);
			}
			this.isHubTransportServer = (this.localServer != null && this.localServer.IsHubTransportServer);
			bool isEdgeConnector = this.localServer != null && this.localServer.IsEdgeServer;
			if (this.SourceTransportServers != null)
			{
				smtpSendConnectorConfig.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, delegate(IConfigurable configObject)
				{
					Server server = (Server)configObject;
					isEdgeConnector |= server.IsEdgeServer;
					return server;
				});
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = this.DataObject.SourceTransportServers;
			if (this.localServer != null && this.localServer.IsHubTransportServer && (multiValuedProperty == null || multiValuedProperty.Count == 0))
			{
				multiValuedProperty = new MultiValuedProperty<ADObjectId>(false, SendConnectorSchema.SourceTransportServers, new ADObjectId[]
				{
					this.localServer.Id
				});
				this.DataObject.SourceTransportServers = multiValuedProperty;
			}
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				ManageSendConnectors.SetConnectorHomeMta(this.DataObject, (IConfigurationSession)base.DataSession);
			}
			if (!this.DataObject.IsModified(SendConnectorSchema.MaxMessageSize))
			{
				if (this.IsUsageSet && this.usage == NewSendConnector.UsageType.Internal)
				{
					this.MaxMessageSize = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				}
				else
				{
					this.MaxMessageSize = ByteQuantifiedSize.FromMB(35UL);
				}
			}
			if (!this.DataObject.IsModified(SmtpSendConnectorConfigSchema.UseExternalDNSServersEnabled) && isEdgeConnector)
			{
				this.UseExternalDNSServersEnabled = true;
			}
			ManageSendConnectors.SetConnectorId(smtpSendConnectorConfig, ((ITopologyConfigurationSession)base.DataSession).GetRoutingGroupId());
			return smtpSendConnectorConfig;
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x001AEE28 File Offset: 0x001AD028
		protected override void InternalProcessRecord()
		{
			SmtpSendConnectorConfig dataObject = this.DataObject;
			if (!TopologyProvider.IsAdamTopology() && !dataObject.Enabled && !this.force && !base.ShouldContinue(Strings.ConfirmationMessageDisableSendConnector))
			{
				return;
			}
			Exception ex = null;
			if (dataObject.DNSRoutingEnabled)
			{
				ex = NewSendConnector.CheckDNSAndSmartHostParameters(this.DataObject);
				if (ex == null)
				{
					ex = NewSendConnector.CheckDNSAndSmartHostAuthMechanismParameters(this.DataObject);
				}
			}
			if (ex == null)
			{
				ex = NewSendConnector.CheckTLSParameters(this.DataObject);
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject.Identity);
				return;
			}
			NewSendConnector.ClearSmartHostsListIfNecessary(this.DataObject);
			NewSendConnector.SetSmartHostAuthMechanismIfNecessary(this.DataObject);
			if (dataObject.IsScopedConnector)
			{
				ManageSendConnectors.AdjustAddressSpaces(dataObject);
			}
			base.InternalProcessRecord();
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			SmtpSendConnectorConfig smtpSendConnectorConfig = null;
			try
			{
				smtpSendConnectorConfig = topologyConfigurationSession.Read<SmtpSendConnectorConfig>(this.DataObject.OriginalId);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.DataObject);
				return;
			}
			RawSecurityDescriptor originalSecurityDescriptor = smtpSendConnectorConfig.ReadSecurityDescriptor();
			RawSecurityDescriptor rawSecurityDescriptor = null;
			try
			{
				rawSecurityDescriptor = this.ConfigureDefaultSecurityDescriptor(originalSecurityDescriptor);
			}
			catch (LocalizedException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, this.DataObject);
				return;
			}
			if (rawSecurityDescriptor != null)
			{
				topologyConfigurationSession.SaveSecurityDescriptor(this.DataObject.OriginalId, rawSecurityDescriptor);
			}
			if (!TopologyProvider.IsAdamTopology())
			{
				ManageSendConnectors.UpdateGwartLastModified(topologyConfigurationSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.ThrowTerminatingError));
			}
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x001AEF94 File Offset: 0x001AD194
		protected override void InternalValidate()
		{
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			if (this.usageSetCount > 1)
			{
				base.WriteError(new NewSendConnectorIncorrectUsageParametersException(), ErrorCategory.InvalidOperation, null);
			}
			if (this.IsUsageSet && this.usage == NewSendConnector.UsageType.Partner)
			{
				if (!this.isDomainSecureEnabledSet)
				{
					this.DataObject.DomainSecureEnabled = true;
				}
				if (!this.isDnsRoutingEnabledSet)
				{
					this.DataObject.DNSRoutingEnabled = true;
				}
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			bool flag;
			LocalizedException ex = NewSendConnector.CrossObjectValidate(this.DataObject, (IConfigurationSession)base.DataSession, this.localServer, this, out flag);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject);
				return;
			}
			if (flag)
			{
				this.WriteWarning(Strings.WarningMultiSiteSourceServers);
			}
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x001AF05C File Offset: 0x001AD25C
		private static LocalizedException ValidateSourceServers(SmtpSendConnectorConfig connector, IConfigurationSession session, Server localServer, Task task, out bool edgeConnector, out bool multiSiteConnector)
		{
			edgeConnector = false;
			multiSiteConnector = false;
			MultiValuedProperty<ADObjectId> sourceTransportServers = connector.SourceTransportServers;
			if (localServer == null || !localServer.IsEdgeServer)
			{
				ADObjectId sourceRoutingGroup = connector.SourceRoutingGroup;
				return ManageSendConnectors.ValidateTransportServers(session, connector, ref sourceRoutingGroup, true, true, task, out edgeConnector, out multiSiteConnector);
			}
			if (sourceTransportServers != null && sourceTransportServers.Count > 0)
			{
				return new SendConnectorSourceServersSetForEdgeException();
			}
			edgeConnector = true;
			return null;
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x001AF0B4 File Offset: 0x001AD2B4
		private static LocalizedException ValidateSourceIPAddress(SmtpSendConnectorConfig connector)
		{
			IPAddress sourceIPAddress = connector.SourceIPAddress;
			if (sourceIPAddress.AddressFamily != AddressFamily.InterNetwork && sourceIPAddress.AddressFamily != AddressFamily.InterNetworkV6)
			{
				return new SendConnectorInvalidSourceIPAddressException();
			}
			if (sourceIPAddress.Equals(IPAddress.Any) || sourceIPAddress.Equals(IPAddress.IPv6Any) || IPAddress.IsLoopback(sourceIPAddress))
			{
				return null;
			}
			bool flag = false;
			IPHostEntry hostEntry = Dns.GetHostEntry(string.Empty);
			for (int i = 0; i < hostEntry.AddressList.Length; i++)
			{
				if (sourceIPAddress.Equals(hostEntry.AddressList[i]))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return new SendConnectorInvalidSourceIPAddressException();
			}
			return null;
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x001AF148 File Offset: 0x001AD348
		internal static PrincipalPermissionList GetDefaultPermission(bool isHubTransportServer)
		{
			PrincipalPermissionList principalPermissionList = new PrincipalPermissionList(5);
			principalPermissionList.Add(new SecurityIdentifier(WellKnownSidType.AnonymousSid, null), Permission.SendRoutingHeaders);
			principalPermissionList.Add(WellKnownSids.PartnerServers, Permission.SendRoutingHeaders);
			principalPermissionList.Add(WellKnownSids.LegacyExchangeServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders);
			principalPermissionList.Add(WellKnownSids.HubTransportServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			principalPermissionList.Add(WellKnownSids.EdgeTransportServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			principalPermissionList.Add(WellKnownSids.ExternallySecuredServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders);
			if (isHubTransportServer)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1215, "GetDefaultPermission", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\NewSendConnector.cs");
				IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1219, "GetDefaultPermission", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\NewSendConnector.cs");
				configurationSession.UseConfigNC = false;
				principalPermissionList.Add(NewEdgeSubscription.GetSidForExchangeKnownGuid(tenantOrRootOrgRecipientSession, WellKnownGuid.ExSWkGuid, configurationSession.ConfigurationNamingContext.DistinguishedName), Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			}
			return principalPermissionList;
		}

		// Token: 0x04003685 RID: 13957
		private const string AddressSpacesParameterSetName = "AddressSpaces";

		// Token: 0x04003686 RID: 13958
		private const Permission FullSendPermission = Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow;

		// Token: 0x04003687 RID: 13959
		private Server localServer;

		// Token: 0x04003688 RID: 13960
		private bool isHubTransportServer;

		// Token: 0x04003689 RID: 13961
		private NewSendConnector.UsageType usage;

		// Token: 0x0400368A RID: 13962
		private int usageSetCount;

		// Token: 0x0400368B RID: 13963
		private bool isDomainSecureEnabledSet;

		// Token: 0x0400368C RID: 13964
		private bool isDnsRoutingEnabledSet;

		// Token: 0x0400368D RID: 13965
		private SwitchParameter force;

		// Token: 0x02000B40 RID: 2880
		public enum UsageType
		{
			// Token: 0x0400368F RID: 13967
			[LocDescription(Strings.IDs.UsageTypeCustom)]
			Custom,
			// Token: 0x04003690 RID: 13968
			[LocDescription(Strings.IDs.UsageTypeInternal)]
			Internal,
			// Token: 0x04003691 RID: 13969
			[LocDescription(Strings.IDs.UsageTypeInternet)]
			Internet,
			// Token: 0x04003692 RID: 13970
			[LocDescription(Strings.IDs.UsageTypePartner)]
			Partner
		}
	}
}
