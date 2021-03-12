using System;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x0200008F RID: 143
	internal class ExchangeOutlookAnywhereEndpoint : MigrationEndpointBase
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0002508C File Offset: 0x0002328C
		public ExchangeOutlookAnywhereEndpoint(MigrationEndpoint presentationObject) : base(presentationObject)
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00025095 File Offset: 0x00023295
		public ExchangeOutlookAnywhereEndpoint() : base(MigrationType.ExchangeOutlookAnywhere)
		{
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002509E File Offset: 0x0002329E
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000250A6 File Offset: 0x000232A6
		public string ExchangeServer { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000250AF File Offset: 0x000232AF
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000250B7 File Offset: 0x000232B7
		public Fqdn RpcProxyServer
		{
			get
			{
				return this.RemoteServer;
			}
			set
			{
				this.RemoteServer = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000250C0 File Offset: 0x000232C0
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x000250D2 File Offset: 0x000232D2
		public string NspiServer
		{
			get
			{
				return base.ExtendedProperties.Get<string>("NspiServer");
			}
			set
			{
				base.ExtendedProperties.Set<string>("NspiServer", value);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000250E5 File Offset: 0x000232E5
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x000250F7 File Offset: 0x000232F7
		public string EmailAddress
		{
			get
			{
				return base.ExtendedProperties.Get<string>("EmailAddress");
			}
			set
			{
				base.ExtendedProperties.Set<string>("EmailAddress", value);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0002510A File Offset: 0x0002330A
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0002511D File Offset: 0x0002331D
		public bool UseAutoDiscover
		{
			get
			{
				return base.ExtendedProperties.Get<bool>("UseAutoDiscover", false);
			}
			set
			{
				base.ExtendedProperties.Set<bool>("UseAutoDiscover", value);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00025130 File Offset: 0x00023330
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00025143 File Offset: 0x00023343
		public MigrationMailboxPermission MailboxPermission
		{
			get
			{
				return base.ExtendedProperties.Get<MigrationMailboxPermission>("MailboxPermission", MigrationMailboxPermission.Admin);
			}
			set
			{
				base.ExtendedProperties.Set<MigrationMailboxPermission>("MailboxPermission", value);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00025158 File Offset: 0x00023358
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				PropertyDefinition[] second = new StorePropertyDefinition[]
				{
					MigrationEndpointMessageSchema.ExchangeServer
				};
				return base.PropertyDefinitions.Union(second).ToArray<PropertyDefinition>();
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00025188 File Offset: 0x00023388
		public override ConnectionSettingsBase ConnectionSettings
		{
			get
			{
				ExchangeConnectionSettings exchangeConnectionSettings;
				if (this.UseAutoDiscover)
				{
					exchangeConnectionSettings = ExchangeConnectionSettings.Create(base.Username, base.Domain, base.EncryptedPassword, (SmtpAddress)this.EmailAddress, this.RpcProxyServer.ToString(), this.ExchangeServer, base.AuthenticationMethod, this.MailboxPermission == MigrationMailboxPermission.Admin);
				}
				else
				{
					exchangeConnectionSettings = ExchangeConnectionSettings.Create(base.Username, base.Domain, base.EncryptedPassword, this.RpcProxyServer.ToString(), this.ExchangeServer, base.AuthenticationMethod, this.MailboxPermission == MigrationMailboxPermission.Admin);
				}
				if (this.NspiServer != null)
				{
					exchangeConnectionSettings.IncomingNSPIServer = this.NspiServer;
				}
				return exchangeConnectionSettings;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0002522F File Offset: 0x0002342F
		public override MigrationType PreferredMigrationType
		{
			get
			{
				return MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00025234 File Offset: 0x00023434
		public static void ValidateEndpoint(ExchangeOutlookAnywhereEndpoint endpoint)
		{
			if (string.IsNullOrEmpty(endpoint.Username))
			{
				throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsIncomplete("Username"), "empty user name");
			}
			if (endpoint.RpcProxyServer == null)
			{
				throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsIncomplete("RpcProxyServer"), "no rpc proxy server");
			}
			if (endpoint.ExchangeServer == null)
			{
				throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsIncomplete("ExchangeServer"), "no exchange server");
			}
			if (endpoint.NspiServer == null)
			{
				throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsIncomplete("NspiServer"), "no nspi server");
			}
			if (endpoint.UseAutoDiscover && string.IsNullOrEmpty(endpoint.EmailAddress))
			{
				throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsIncomplete("EmailAddress"), "autodiscovery used, but no email");
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000252E4 File Offset: 0x000234E4
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			message[MigrationEndpointMessageSchema.ExchangeServer] = this.ExchangeServer;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000252FF File Offset: 0x000234FF
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.ExchangeServer = (string)message[MigrationEndpointMessageSchema.ExchangeServer];
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00025320 File Offset: 0x00023520
		public override NspiMigrationDataReader GetNspiDataReader(MigrationJob job = null)
		{
			if (string.IsNullOrEmpty(this.NspiServer))
			{
				IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient((job != null) ? job.ReportData : null);
				this.NspiServer = nspiClient.GetNewDSA(this);
			}
			return new NspiMigrationDataReader(this, job);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00025365 File Offset: 0x00023565
		public override void VerifyConnectivity()
		{
			this.GetNspiDataReader(null).Ping();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00025373 File Offset: 0x00023573
		public override void InitializeFromAutoDiscover(SmtpAddress emailAddress, PSCredential credentials)
		{
			base.InitializeFromAutoDiscover(emailAddress, credentials);
			this.EmailAddress = (string)emailAddress;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002538C File Offset: 0x0002358C
		protected override void ApplyAdditionalProperties(MigrationEndpoint presentationObject)
		{
			presentationObject.RpcProxyServer = this.RpcProxyServer;
			presentationObject.ExchangeServer = this.ExchangeServer;
			presentationObject.Credentials = this.Credentials;
			presentationObject.UseAutoDiscover = new bool?(this.UseAutoDiscover);
			presentationObject.MailboxPermission = this.MailboxPermission;
			presentationObject.NspiServer = this.NspiServer;
			presentationObject.Authentication = new AuthenticationMethod?(base.AuthenticationMethod);
			presentationObject.EmailAddress = (SmtpAddress)this.EmailAddress;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00025408 File Offset: 0x00023608
		protected override void ApplyAutodiscoverSettings(AutodiscoverClientResponse response)
		{
			this.RpcProxyServer = new Fqdn(response.RPCProxyServer);
			this.ExchangeServer = response.ExchangeServer;
			base.AuthenticationMethod = (response.AuthenticationMethod ?? base.DefaultAuthenticationMethod);
			this.UseAutoDiscover = true;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00025460 File Offset: 0x00023660
		protected override void InitializeFromPresentationObject(MigrationEndpoint endpoint)
		{
			base.InitializeFromPresentationObject(endpoint);
			this.ExchangeServer = endpoint.ExchangeServer;
			this.RpcProxyServer = endpoint.RpcProxyServer;
			this.NspiServer = endpoint.NspiServer;
			this.UseAutoDiscover = (endpoint.UseAutoDiscover ?? false);
			this.MailboxPermission = endpoint.MailboxPermission;
			this.EmailAddress = (string)endpoint.EmailAddress;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000254D5 File Offset: 0x000236D5
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			base.AddDiagnosticInfoToElement(dataProvider, parent, argument);
			parent.Add(new XElement("ExchangeServer", this.ExchangeServer));
		}
	}
}
