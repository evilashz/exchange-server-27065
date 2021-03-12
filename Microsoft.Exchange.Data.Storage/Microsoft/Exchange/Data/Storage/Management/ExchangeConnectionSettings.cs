using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A15 RID: 2581
	[Serializable]
	public class ExchangeConnectionSettings : ConnectionSettingsBase
	{
		// Token: 0x06005ED5 RID: 24277 RVA: 0x0019076F File Offset: 0x0018E96F
		public ExchangeConnectionSettings()
		{
			this[SimpleProviderObjectSchema.Identity] = MigrationBatchId.Any;
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x00190788 File Offset: 0x0018E988
		private ExchangeConnectionSettings(SmtpAddress incomingEmailAddress, string userName, string domain, string encryptedPassword, bool useAutodiscovery, string incomingRPCProxyServer, string incomingExchangeServer, AuthenticationMethod incomingAuthentication, bool hasAdminPrivilege, bool hasMrsProxy) : this()
		{
			this.IncomingUserName = userName;
			this.IncomingDomain = domain;
			this.EncryptedIncomingPassword = encryptedPassword;
			this.HasAutodiscovery = useAutodiscovery;
			this.IncomingEmailAddress = incomingEmailAddress;
			this.IncomingRPCProxyServer = incomingRPCProxyServer;
			this.IncomingExchangeServer = incomingExchangeServer;
			this.IncomingAuthentication = incomingAuthentication;
			this.HasAdminPrivilege = hasAdminPrivilege;
			this.HasMrsProxy = hasMrsProxy;
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x001907E8 File Offset: 0x0018E9E8
		public static ExchangeConnectionSettings Create(string userName, string domain, string encryptedPassword, string incomingRpcProxyServer, string incomingExchangeServer, AuthenticationMethod incomingAuthentication, bool hasAdminPrivilege)
		{
			return ExchangeConnectionSettings.Create(userName, domain, encryptedPassword, incomingRpcProxyServer, incomingExchangeServer, incomingAuthentication, hasAdminPrivilege, false);
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x001907FC File Offset: 0x0018E9FC
		public static ExchangeConnectionSettings Create(string userName, string domain, string encryptedPassword, string incomingRpcProxyServer, string incomingExchangeServer, AuthenticationMethod incomingAuthentication, bool hasAdminPrivilege, bool hasMrsProxy)
		{
			return new ExchangeConnectionSettings(SmtpAddress.Empty, userName, domain, encryptedPassword, false, incomingRpcProxyServer, incomingExchangeServer, incomingAuthentication, hasAdminPrivilege, hasMrsProxy);
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x00190820 File Offset: 0x0018EA20
		public static ExchangeConnectionSettings Create(string userName, string domain, string encryptedPassword, SmtpAddress incomingEmailAddress, string incomingRpcProxyServer, string incomingExchangeServer, AuthenticationMethod incomingAuthentication, bool hasAdminPrivilege)
		{
			return new ExchangeConnectionSettings(incomingEmailAddress, userName, domain, encryptedPassword, true, incomingRpcProxyServer, incomingExchangeServer, incomingAuthentication, hasAdminPrivilege, false);
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x00190840 File Offset: 0x0018EA40
		public static ExchangeConnectionSettings Create(string userName, string domain, string encryptedPassword, string incomingRpcProxyServer, string sourceMailboxLegDn, string publicFolderDatabaseServerLegacyDN, AuthenticationMethod incomingAuthentication)
		{
			return new ExchangeConnectionSettings(SmtpAddress.Empty, userName, domain, encryptedPassword, false, incomingRpcProxyServer, null, incomingAuthentication, true, false)
			{
				SourceMailboxLegDn = sourceMailboxLegDn,
				PublicFolderDatabaseServerLegacyDN = publicFolderDatabaseServerLegacyDN
			};
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x00190875 File Offset: 0x0018EA75
		public override MigrationType Type
		{
			get
			{
				if (this.HasMrsProxy)
				{
					return MigrationType.ExchangeRemoteMove;
				}
				if (!string.IsNullOrEmpty(this.PublicFolderDatabaseServerLegacyDN))
				{
					return MigrationType.PublicFolder;
				}
				return MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x00190896 File Offset: 0x0018EA96
		// (set) Token: 0x06005EDD RID: 24285 RVA: 0x001908A8 File Offset: 0x0018EAA8
		public bool HasAdminPrivilege
		{
			get
			{
				return (bool)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasAdminPrivilege];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasAdminPrivilege] = value;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x001908BB File Offset: 0x0018EABB
		// (set) Token: 0x06005EDF RID: 24287 RVA: 0x001908CD File Offset: 0x0018EACD
		public bool HasAutodiscovery
		{
			get
			{
				return (bool)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasAutodiscovery];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasAutodiscovery] = value;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06005EE0 RID: 24288 RVA: 0x001908E0 File Offset: 0x0018EAE0
		// (set) Token: 0x06005EE1 RID: 24289 RVA: 0x001908F2 File Offset: 0x0018EAF2
		public string IncomingRPCProxyServer
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingRPCProxyServer];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingRPCProxyServer] = value;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06005EE2 RID: 24290 RVA: 0x00190900 File Offset: 0x0018EB00
		// (set) Token: 0x06005EE3 RID: 24291 RVA: 0x00190912 File Offset: 0x0018EB12
		public string IncomingExchangeServer
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingExchangeServer];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingExchangeServer] = value;
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06005EE4 RID: 24292 RVA: 0x00190920 File Offset: 0x0018EB20
		// (set) Token: 0x06005EE5 RID: 24293 RVA: 0x00190932 File Offset: 0x0018EB32
		public string IncomingNSPIServer
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingNSPIServer];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingNSPIServer] = value;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06005EE6 RID: 24294 RVA: 0x00190940 File Offset: 0x0018EB40
		// (set) Token: 0x06005EE7 RID: 24295 RVA: 0x00190952 File Offset: 0x0018EB52
		public string IncomingDomain
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingDomain];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingDomain] = value;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x00190960 File Offset: 0x0018EB60
		// (set) Token: 0x06005EE9 RID: 24297 RVA: 0x00190972 File Offset: 0x0018EB72
		public string IncomingUserName
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingUserName];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingUserName] = value;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06005EEA RID: 24298 RVA: 0x00190980 File Offset: 0x0018EB80
		// (set) Token: 0x06005EEB RID: 24299 RVA: 0x00190992 File Offset: 0x0018EB92
		public string EncryptedIncomingPassword
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.EncryptedIncomingPassword];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.EncryptedIncomingPassword] = value;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x001909A0 File Offset: 0x0018EBA0
		// (set) Token: 0x06005EED RID: 24301 RVA: 0x001909B2 File Offset: 0x0018EBB2
		public string AutodiscoverUrl
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.AutodiscoverUrl];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.AutodiscoverUrl] = value;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x001909C0 File Offset: 0x0018EBC0
		// (set) Token: 0x06005EEF RID: 24303 RVA: 0x001909D2 File Offset: 0x0018EBD2
		public SmtpAddress IncomingEmailAddress
		{
			get
			{
				return (SmtpAddress)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingEmailAddress];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingEmailAddress] = value;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x001909E5 File Offset: 0x0018EBE5
		// (set) Token: 0x06005EF1 RID: 24305 RVA: 0x001909F7 File Offset: 0x0018EBF7
		public AuthenticationMethod IncomingAuthentication
		{
			get
			{
				return (AuthenticationMethod)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingAuthentication];
			}
			set
			{
				if (value != AuthenticationMethod.Basic && value != AuthenticationMethod.Ntlm)
				{
					throw new MigrationPermanentException(ServerStrings.MigrationJobConnectionSettingsInvalid("IncomingAuthentication", value.ToString()));
				}
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.IncomingAuthentication] = value;
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x00190A2C File Offset: 0x0018EC2C
		// (set) Token: 0x06005EF3 RID: 24307 RVA: 0x00190A3E File Offset: 0x0018EC3E
		public string ServerVersion
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.ServerVersion];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.ServerVersion] = value;
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00190A4C File Offset: 0x0018EC4C
		// (set) Token: 0x06005EF5 RID: 24309 RVA: 0x00190A5E File Offset: 0x0018EC5E
		public string SourceMailboxLegDn
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.SourceMailboxLegDn];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.SourceMailboxLegDn] = value;
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x00190A6C File Offset: 0x0018EC6C
		// (set) Token: 0x06005EF7 RID: 24311 RVA: 0x00190A7E File Offset: 0x0018EC7E
		public string PublicFolderDatabaseServerLegacyDN
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.PublicFolderDatabaseServerLegacyDN];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.PublicFolderDatabaseServerLegacyDN] = value;
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06005EF8 RID: 24312 RVA: 0x00190A8C File Offset: 0x0018EC8C
		// (set) Token: 0x06005EF9 RID: 24313 RVA: 0x00190A9E File Offset: 0x0018EC9E
		public string TargetDomainName
		{
			get
			{
				return (string)this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.TargetDomainName];
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.TargetDomainName] = value;
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x00190AAC File Offset: 0x0018ECAC
		// (set) Token: 0x06005EFB RID: 24315 RVA: 0x00190AC8 File Offset: 0x0018ECC8
		public bool HasMrsProxy
		{
			get
			{
				return (bool)(this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasMrsProxy] ?? false);
			}
			set
			{
				this[ExchangeConnectionSettings.ExchangeConnectionSettingsSchema.HasMrsProxy] = value;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x00190ADB File Offset: 0x0018ECDB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExchangeConnectionSettings.schema;
			}
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x00190AE4 File Offset: 0x0018ECE4
		public new static implicit operator ExchangeConnectionSettings(string xml)
		{
			ExchangeConnectionSettings result;
			try
			{
				ExchangeConnectionSettings exchangeConnectionSettings = MigrationXmlSerializer.Deserialize<ExchangeConnectionSettings>(xml);
				result = exchangeConnectionSettings;
			}
			catch (MigrationDataCorruptionException ex)
			{
				throw new CouldNotDeserializeConnectionSettingsException(ex.InnerException);
			}
			return result;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x00190B1C File Offset: 0x0018ED1C
		public override ConnectionSettingsBase CloneForPresentation()
		{
			return new ExchangeConnectionSettings
			{
				HasAdminPrivilege = this.HasAdminPrivilege,
				HasAutodiscovery = this.HasAutodiscovery,
				AutodiscoverUrl = this.AutodiscoverUrl,
				IncomingEmailAddress = this.IncomingEmailAddress,
				IncomingRPCProxyServer = this.IncomingRPCProxyServer,
				IncomingExchangeServer = this.IncomingExchangeServer,
				IncomingNSPIServer = this.IncomingNSPIServer,
				IncomingDomain = this.IncomingDomain,
				IncomingUserName = this.IncomingUserName,
				EncryptedIncomingPassword = this.EncryptedIncomingPassword,
				IncomingAuthentication = this.IncomingAuthentication,
				ServerVersion = this.ServerVersion,
				TargetDomainName = this.TargetDomainName,
				HasMrsProxy = this.HasMrsProxy,
				SourceMailboxLegDn = this.SourceMailboxLegDn,
				PublicFolderDatabaseServerLegacyDN = this.PublicFolderDatabaseServerLegacyDN
			};
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x00190BF0 File Offset: 0x0018EDF0
		public override void ReadXml(XmlReader reader)
		{
			if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "ExchangeConnectionSettings")
			{
				this.HasAdminPrivilege = bool.Parse(reader["HasAdminPrivilege"]);
				this.HasAutodiscovery = bool.Parse(reader["HasAutodiscovery"]);
				this.HasMrsProxy = bool.Parse(reader["HasMrsProxy"] ?? false.ToString(CultureInfo.InvariantCulture));
				this.AutodiscoverUrl = reader["AutodiscoverUrl"];
				this.IncomingEmailAddress = new SmtpAddress(reader["IncomingEmailAddress"]);
				this.IncomingRPCProxyServer = reader["IncomingRPCProxyServer"];
				this.IncomingExchangeServer = reader["IncomingExchangeServer"];
				this.IncomingNSPIServer = reader["IncomingNSPIServer"];
				this.IncomingDomain = reader["IncomingDomain"];
				this.IncomingUserName = reader["IncomingUserName"];
				this.EncryptedIncomingPassword = reader["EncryptedIncomingPassword"];
				this.IncomingAuthentication = (AuthenticationMethod)Enum.Parse(typeof(AuthenticationMethod), reader["IncomingAuthentication"]);
				this.SourceMailboxLegDn = reader["SourceMailboxLegDn"];
				this.PublicFolderDatabaseServerLegacyDN = reader["PublicFolderDatabaseServerLegacyDN"];
				this.ServerVersion = reader["ServerVersion"];
				this.TargetDomainName = reader["TargetDomainName"];
			}
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x00190D68 File Offset: 0x0018EF68
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ExchangeConnectionSettings");
			writer.WriteAttributeString("HasAdminPrivilege", this.HasAdminPrivilege.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("HasAutodiscovery", this.HasAutodiscovery.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("HasMrsProxy", this.HasMrsProxy.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("AutodiscoverUrl", this.AutodiscoverUrl);
			writer.WriteAttributeString("IncomingEmailAddress", this.IncomingEmailAddress.ToString());
			writer.WriteAttributeString("IncomingRPCProxyServer", this.IncomingRPCProxyServer);
			writer.WriteAttributeString("IncomingExchangeServer", this.IncomingExchangeServer);
			writer.WriteAttributeString("IncomingNSPIServer", this.IncomingNSPIServer);
			writer.WriteAttributeString("IncomingDomain", this.IncomingDomain);
			writer.WriteAttributeString("IncomingUserName", this.IncomingUserName);
			writer.WriteAttributeString("EncryptedIncomingPassword", this.EncryptedIncomingPassword);
			writer.WriteAttributeString("IncomingAuthentication", this.IncomingAuthentication.ToString());
			writer.WriteAttributeString("ServerVersion", this.ServerVersion);
			writer.WriteAttributeString("TargetDomainName", this.TargetDomainName);
			writer.WriteAttributeString("SourceMailboxLegDn", this.SourceMailboxLegDn);
			writer.WriteAttributeString("PublicFolderDatabaseServerLegacyDN", this.PublicFolderDatabaseServerLegacyDN);
			writer.WriteEndElement();
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x00190ED8 File Offset: 0x0018F0D8
		public override bool Equals(object obj)
		{
			ExchangeConnectionSettings exchangeConnectionSettings = obj as ExchangeConnectionSettings;
			if (exchangeConnectionSettings == null)
			{
				return false;
			}
			bool flag = this.HasMrsProxy == exchangeConnectionSettings.HasMrsProxy;
			flag = (flag && this.IncomingAuthentication == exchangeConnectionSettings.IncomingAuthentication);
			flag = (flag && this.HasAutodiscovery == exchangeConnectionSettings.HasAutodiscovery);
			flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.AutodiscoverUrl, exchangeConnectionSettings.AutodiscoverUrl));
			flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.IncomingDomain, exchangeConnectionSettings.IncomingDomain));
			flag = (flag && this.IncomingEmailAddress.Equals(exchangeConnectionSettings.IncomingEmailAddress));
			if (!this.HasMrsProxy)
			{
				flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.IncomingExchangeServer, exchangeConnectionSettings.IncomingExchangeServer));
				flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.IncomingNSPIServer, exchangeConnectionSettings.IncomingNSPIServer));
				flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.SourceMailboxLegDn, exchangeConnectionSettings.SourceMailboxLegDn));
				flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.PublicFolderDatabaseServerLegacyDN, exchangeConnectionSettings.PublicFolderDatabaseServerLegacyDN));
			}
			flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.IncomingRPCProxyServer, exchangeConnectionSettings.IncomingRPCProxyServer));
			flag = (flag && ExchangeConnectionSettings.AreStringEqual(this.IncomingUserName, exchangeConnectionSettings.IncomingUserName));
			return flag && ExchangeConnectionSettings.AreStringEqual(this.TargetDomainName, exchangeConnectionSettings.TargetDomainName);
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x00191028 File Offset: 0x0018F228
		public override int GetHashCode()
		{
			int num = 31;
			num ^= (this.HasMrsProxy ? 47 : 0);
			num ^= (this.HasAutodiscovery ? 109 : 0);
			if (!this.HasMrsProxy)
			{
				num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingExchangeServer);
				num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingNSPIServer);
				num ^= ExchangeConnectionSettings.SafeGetHashCode(this.SourceMailboxLegDn);
			}
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingAuthentication);
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.AutodiscoverUrl);
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingDomain);
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingEmailAddress);
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingRPCProxyServer);
			num ^= ExchangeConnectionSettings.SafeGetHashCode(this.IncomingUserName);
			return num ^ ExchangeConnectionSettings.SafeGetHashCode(this.TargetDomainName);
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001910F7 File Offset: 0x0018F2F7
		private static bool AreStringEqual(string first, string second)
		{
			return (string.IsNullOrEmpty(first) && string.IsNullOrEmpty(second)) || StringComparer.OrdinalIgnoreCase.Equals(first, second);
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00191117 File Offset: 0x0018F317
		private static int SafeGetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x040034D4 RID: 13524
		private const string RootSerializedTag = "ExchangeConnectionSettings";

		// Token: 0x040034D5 RID: 13525
		private static ObjectSchema schema = ObjectSchema.GetInstance<ExchangeConnectionSettings.ExchangeConnectionSettingsSchema>();

		// Token: 0x02000A16 RID: 2582
		private class ExchangeConnectionSettingsSchema : SimpleProviderObjectSchema
		{
			// Token: 0x040034D6 RID: 13526
			public static readonly SimpleProviderPropertyDefinition HasAdminPrivilege = new SimpleProviderPropertyDefinition("HasAdminPrivilege", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034D7 RID: 13527
			public static readonly SimpleProviderPropertyDefinition HasAutodiscovery = new SimpleProviderPropertyDefinition("HasAutodiscovery", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034D8 RID: 13528
			public static readonly SimpleProviderPropertyDefinition AutodiscoverUrl = new SimpleProviderPropertyDefinition("AutodiscoverUrl", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034D9 RID: 13529
			public static readonly SimpleProviderPropertyDefinition IncomingEmailAddress = new SimpleProviderPropertyDefinition("IncomingEmailAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.TaskPopulated, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DA RID: 13530
			public static readonly SimpleProviderPropertyDefinition IncomingRPCProxyServer = new SimpleProviderPropertyDefinition("IncomingRPCProxyServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DB RID: 13531
			public static readonly SimpleProviderPropertyDefinition IncomingExchangeServer = new SimpleProviderPropertyDefinition("IncomingExchangeServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DC RID: 13532
			public static readonly SimpleProviderPropertyDefinition IncomingNSPIServer = new SimpleProviderPropertyDefinition("IncomingNSPIServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DD RID: 13533
			public static readonly SimpleProviderPropertyDefinition IncomingDomain = new SimpleProviderPropertyDefinition("IncomingDomain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DE RID: 13534
			public static readonly SimpleProviderPropertyDefinition IncomingUserName = new SimpleProviderPropertyDefinition("IncomingUserName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034DF RID: 13535
			public static readonly SimpleProviderPropertyDefinition EncryptedIncomingPassword = new SimpleProviderPropertyDefinition("EncryptedIncomingPassword", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E0 RID: 13536
			public static readonly SimpleProviderPropertyDefinition IncomingAuthentication = new SimpleProviderPropertyDefinition("IncomingAuthentication", ExchangeObjectVersion.Exchange2010, typeof(AuthenticationMethod), PropertyDefinitionFlags.TaskPopulated, AuthenticationMethod.Basic, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E1 RID: 13537
			public static readonly SimpleProviderPropertyDefinition ServerVersion = new SimpleProviderPropertyDefinition("ServerVersion", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E2 RID: 13538
			public static readonly SimpleProviderPropertyDefinition SourceMailboxLegDn = new SimpleProviderPropertyDefinition("SourceMailboxLegDn", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E3 RID: 13539
			public static readonly SimpleProviderPropertyDefinition PublicFolderDatabaseServerLegacyDN = new SimpleProviderPropertyDefinition("PublicFolderDatabaseServerLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E4 RID: 13540
			public static readonly SimpleProviderPropertyDefinition TargetDomainName = new SimpleProviderPropertyDefinition("TargetDomainName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E5 RID: 13541
			public static readonly SimpleProviderPropertyDefinition HasMrsProxy = new SimpleProviderPropertyDefinition("HasMrsProxy", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
