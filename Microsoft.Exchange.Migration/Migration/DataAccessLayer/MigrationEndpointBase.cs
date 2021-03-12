using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x0200008E RID: 142
	internal abstract class MigrationEndpointBase : MigrationMessagePersistableBase, ISubscriptionSettings, IMigrationSerializable
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00023C94 File Offset: 0x00021E94
		protected MigrationEndpointBase()
		{
			this.EndpointType = MigrationType.None;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00023CA3 File Offset: 0x00021EA3
		protected MigrationEndpointBase(MigrationEndpoint endpoint)
		{
			MigrationUtil.ThrowOnNullArgument(endpoint.Identity, "endpoint.Identity");
			this.InitializeFromPresentationObject(endpoint);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00023CC2 File Offset: 0x00021EC2
		protected MigrationEndpointBase(MigrationType endpointType)
		{
			if (endpointType == MigrationType.None)
			{
				throw new ArgumentException("MigrationType cannot be 'None'", "endpointType");
			}
			this.EndpointType = endpointType;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00023CE4 File Offset: 0x00021EE4
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00023CEC File Offset: 0x00021EEC
		public MigrationEndpointId Identity { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00023CF5 File Offset: 0x00021EF5
		public override long MinimumSupportedVersion
		{
			get
			{
				return 4L;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00023CF9 File Offset: 0x00021EF9
		public override long MaximumSupportedVersion
		{
			get
			{
				return 5L;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00023CFD File Offset: 0x00021EFD
		public override long CurrentSupportedVersion
		{
			get
			{
				return this.MaximumSupportedVersion;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00023D05 File Offset: 0x00021F05
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00023D0D File Offset: 0x00021F0D
		public MigrationType EndpointType { get; private set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00023D18 File Offset: 0x00021F18
		public override PropertyDefinition[] InitializationPropertyDefinitions
		{
			get
			{
				return MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					new StorePropertyDefinition[]
					{
						MigrationEndpointMessageSchema.MigrationEndpointName,
						MigrationEndpointMessageSchema.MigrationEndpointGuid,
						MigrationEndpointMessageSchema.MigrationEndpointType
					},
					base.InitializationPropertyDefinitions
				});
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00023D60 File Offset: 0x00021F60
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				PropertyDefinition[] array = new StorePropertyDefinition[]
				{
					MigrationEndpointMessageSchema.LastModifiedTime,
					MigrationEndpointMessageSchema.RemoteHostName
				};
				return MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					base.PropertyDefinitions,
					BasicMigrationSlotProvider.PropertyDefinition,
					array
				});
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00023DA8 File Offset: 0x00021FA8
		public Guid Guid
		{
			get
			{
				if (this.Identity == null)
				{
					return Guid.Empty;
				}
				return this.Identity.Guid;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00023DC3 File Offset: 0x00021FC3
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00023DCB File Offset: 0x00021FCB
		public BasicMigrationSlotProvider SlotProvider { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000822 RID: 2082
		public abstract ConnectionSettingsBase ConnectionSettings { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000823 RID: 2083
		public abstract MigrationType PreferredMigrationType { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00023DD4 File Offset: 0x00021FD4
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00023DDC File Offset: 0x00021FDC
		public virtual Fqdn RemoteServer { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00023DE5 File Offset: 0x00021FE5
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00023DF7 File Offset: 0x00021FF7
		public string EncryptedPassword
		{
			get
			{
				return base.ExtendedProperties.Get<string>("EncryptedPassword");
			}
			private set
			{
				base.ExtendedProperties.Set<string>("EncryptedPassword", value);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00023E0A File Offset: 0x0002200A
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00023E1C File Offset: 0x0002201C
		public string Username
		{
			get
			{
				return base.ExtendedProperties.Get<string>("Username");
			}
			private set
			{
				base.ExtendedProperties.Set<string>("Username", value);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00023E2F File Offset: 0x0002202F
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00023E41 File Offset: 0x00022041
		public string Domain
		{
			get
			{
				return base.ExtendedProperties.Get<string>("Domain");
			}
			private set
			{
				base.ExtendedProperties.Set<string>("Domain", value);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00023E54 File Offset: 0x00022054
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00023E5C File Offset: 0x0002205C
		public ExDateTime LastModifiedTime { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00023E65 File Offset: 0x00022065
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00023E80 File Offset: 0x00022080
		public virtual PSCredential Credentials
		{
			get
			{
				return MigrationEndpointBase.BuildPSCredentials(this.Domain, this.Username, this.Password);
			}
			set
			{
				if (value != null)
				{
					ICryptoAdapter cryptoAdapter = MigrationServiceFactory.Instance.GetCryptoAdapter();
					string encryptedPassword;
					Exception ex;
					if (!cryptoAdapter.TrySecureStringToEncryptedString(value.Password, out encryptedPassword, out ex))
					{
						throw new CouldNotEncryptPasswordException(value.UserName);
					}
					this.EncryptedPassword = encryptedPassword;
					if (string.IsNullOrEmpty(value.UserName) || SmtpAddress.IsValidSmtpAddress(value.UserName))
					{
						this.Username = value.UserName;
						this.Domain = null;
					}
					else
					{
						NetworkCredential networkCredential = null;
						try
						{
							networkCredential = value.GetNetworkCredential();
						}
						catch (ArgumentException innerException)
						{
							throw new CouldNotCreateCredentialsPermanentException(this.Username, innerException);
						}
						this.Domain = networkCredential.Domain;
						this.Username = networkCredential.UserName;
					}
					if (MigrationUtil.HasUnicodeCharacters(this.Username) || MigrationUtil.HasUnicodeCharacters(this.Domain) || MigrationUtil.HasUnicodeCharacters(value.Password.AsUnsecureString()))
					{
						throw new CannotSpecifyUnicodeInCredentialsException();
					}
				}
				else
				{
					this.Username = null;
					this.EncryptedPassword = null;
					this.Domain = null;
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00023F7C File Offset: 0x0002217C
		public SecureString Password
		{
			get
			{
				ICryptoAdapter cryptoAdapter = MigrationServiceFactory.Instance.GetCryptoAdapter();
				string encryptedPassword = this.EncryptedPassword;
				if (string.IsNullOrEmpty(encryptedPassword))
				{
					return null;
				}
				SecureString result;
				Exception innerException;
				if (!cryptoAdapter.TryEncryptedStringToSecureString(this.EncryptedPassword, out result, out innerException))
				{
					throw new CouldNotEncryptPasswordException(this.Username, innerException);
				}
				return result;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00023FC8 File Offset: 0x000221C8
		public NetworkCredential NetworkCredentials
		{
			get
			{
				return CommonUtils.GetNetworkCredential(this.Credentials, null);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00023FE9 File Offset: 0x000221E9
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x00024001 File Offset: 0x00022201
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return base.ExtendedProperties.Get<AuthenticationMethod>("AuthenticationMethod", this.DefaultAuthenticationMethod);
			}
			set
			{
				this.ValidateAuthenticationMethod(value);
				base.ExtendedProperties.Set<AuthenticationMethod>("AuthenticationMethod", value);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0002401B File Offset: 0x0002221B
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x00024023 File Offset: 0x00022223
		internal AutodiscoverClientResponse AutodiscoverResponse { get; private set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0002402C File Offset: 0x0002222C
		protected virtual IEnumerable<AuthenticationMethod> SupportedAuthenticationMethods
		{
			get
			{
				AuthenticationMethod[] array = new AuthenticationMethod[2];
				array[0] = AuthenticationMethod.Ntlm;
				return array;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00024045 File Offset: 0x00022245
		protected AuthenticationMethod DefaultAuthenticationMethod
		{
			get
			{
				return AuthenticationMethod.Basic;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00024048 File Offset: 0x00022248
		public static implicit operator MigrationEndpointBase(MigrationEndpoint endpoint)
		{
			return MigrationEndpointBase.CreateFrom(endpoint);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00024050 File Offset: 0x00022250
		public static implicit operator MigrationEndpoint(MigrationEndpointBase endpoint)
		{
			return endpoint.ToMigrationEndpoint();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00024058 File Offset: 0x00022258
		public static PSCredential BuildPSCredentials(string domain, string username, SecureString password)
		{
			if (password == null)
			{
				return null;
			}
			string userName;
			if (string.IsNullOrEmpty(username) || SmtpAddress.IsValidSmtpAddress(username))
			{
				userName = username;
			}
			else if (domain == null)
			{
				userName = username;
			}
			else
			{
				userName = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
				{
					domain,
					username
				});
			}
			return new PSCredential(userName, password);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000240AC File Offset: 0x000222AC
		public virtual void InitializeFromAutoDiscover(SmtpAddress emailAddress, PSCredential credentials)
		{
			MigrationUtil.ThrowOnNullArgument(emailAddress, "emailAddress");
			MigrationUtil.ThrowOnNullArgument(credentials, "credentials");
			IMigrationAutodiscoverClient autodiscoverClient = MigrationServiceFactory.Instance.GetAutodiscoverClient();
			MigrationUtil.AssertOrThrow(autodiscoverClient != null, "MigrationServiceFactory should never return a null AutodClient", new object[0]);
			string emailAddress2 = emailAddress.ToString();
			string encryptedPassword;
			Exception ex;
			if (!MigrationServiceFactory.Instance.GetCryptoAdapter().TrySecureStringToEncryptedString(credentials.Password, out encryptedPassword, out ex))
			{
				throw new CouldNotEncryptPasswordException(credentials.UserName);
			}
			this.Credentials = credentials;
			NetworkCredential networkCredentials = this.NetworkCredentials;
			AutodiscoverClientResponse userSettings = autodiscoverClient.GetUserSettings(networkCredentials.UserName, encryptedPassword, networkCredentials.Domain, emailAddress2);
			switch (userSettings.Status)
			{
			case AutodiscoverClientStatus.NoError:
				this.ApplyAutodiscoverSettings(userSettings);
				this.AutodiscoverResponse = userSettings;
				return;
			case AutodiscoverClientStatus.ConfigurationError:
			{
				AutoDiscoverFailedConfigurationErrorException ex2 = new AutoDiscoverFailedConfigurationErrorException(userSettings.ErrorMessage);
				ex2.Data["AutoDiscoverResponseMessage"] = userSettings.ErrorMessage;
				ex2.Data["AutoDiscoverResponseErrorDetail"] = userSettings.ErrorDetail;
				throw ex2;
			}
			case AutodiscoverClientStatus.InternalError:
			{
				AutoDiscoverFailedInternalErrorException ex3 = new AutoDiscoverFailedInternalErrorException(userSettings.ErrorMessage);
				ex3.Data["AutoDiscoverResponseMessage"] = userSettings.ErrorMessage;
				ex3.Data["AutoDiscoverResponseErrorDetail"] = userSettings.ErrorDetail;
				throw ex3;
			}
			default:
				return;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00024210 File Offset: 0x00022410
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[StoreObjectSchema.ItemClass] = "IPM.MS-Exchange.MigrationEndpoint";
			message[MigrationEndpointMessageSchema.MigrationEndpointType] = this.EndpointType;
			message[MigrationEndpointMessageSchema.MigrationEndpointName] = this.Identity.Id;
			message[MigrationEndpointMessageSchema.MigrationEndpointGuid] = this.Guid;
			if (this.RemoteServer != null)
			{
				message[MigrationEndpointMessageSchema.RemoteHostName] = this.RemoteServer.ToString();
			}
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationEndpointMessageSchema.LastModifiedTime, (this.LastModifiedTime == ExDateTime.MinValue) ? null : new ExDateTime?(this.LastModifiedTime));
			this.SlotProvider.WriteToMessageItem(message);
			base.WriteToMessageItem(message, loaded);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000242D4 File Offset: 0x000224D4
		public MigrationEndpoint ToMigrationEndpoint()
		{
			MigrationEndpoint migrationEndpoint = new MigrationEndpoint();
			migrationEndpoint.Identity = this.Identity;
			migrationEndpoint.MaxConcurrentMigrations = this.SlotProvider.MaximumConcurrentMigrations;
			migrationEndpoint.MaxConcurrentIncrementalSyncs = this.SlotProvider.MaximumConcurrentIncrementalSyncs;
			migrationEndpoint.EndpointType = this.EndpointType;
			migrationEndpoint.LastModifiedTime = (DateTime)this.LastModifiedTime;
			migrationEndpoint.ActiveMigrationCount = new int?(this.SlotProvider.ActiveMigrationCount);
			if (this.EndpointType != MigrationType.IMAP)
			{
				migrationEndpoint.ActiveIncrementalSyncCount = new int?(this.SlotProvider.ActiveIncrementalSyncCount);
			}
			this.ApplyAdditionalProperties(migrationEndpoint);
			return migrationEndpoint;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00024370 File Offset: 0x00022570
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.SlotProvider = BasicMigrationSlotProvider.FromMessageItem(this.Guid, message);
			this.RemoteServer = MigrationHelper.GetFqdnProperty(message, MigrationEndpointMessageSchema.RemoteHostName, true);
			this.LastModifiedTime = MigrationHelper.GetExDateTimePropertyOrDefault(message, MigrationEndpointMessageSchema.LastModifiedTime, ExDateTime.MinValue);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000243BE File Offset: 0x000225BE
		public virtual void VerifyConnectivity()
		{
			throw new NotImplementedException(string.Format("Verify connectivity not implemented for endpoints of type {0}", this.EndpointType));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000243DA File Offset: 0x000225DA
		public virtual NspiMigrationDataReader GetNspiDataReader(MigrationJob job = null)
		{
			throw new NspiNotSupportedForEndpointTypeException(this.EndpointType);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000243E8 File Offset: 0x000225E8
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationEndpoint");
			xelement.Add(new XElement("Type", this.EndpointType));
			xelement.Add(new XElement("Name", this.Identity.Id));
			xelement.Add(new XElement("GUID", this.Guid));
			xelement.Add(new XElement("LastModifiedTime", this.LastModifiedTime));
			xelement.Add(new XElement("RemoteServer", this.RemoteServer));
			if (this.SlotProvider != null)
			{
				xelement.Add(this.SlotProvider.GetDiagnosticInfo(dataProvider, argument));
			}
			this.AddDiagnosticInfoToElement(dataProvider, xelement, argument);
			return base.GetDiagnosticInfo(dataProvider, argument, xelement);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000244D0 File Offset: 0x000226D0
		internal static void Create(IMigrationDataProvider migrationDataProvider, MigrationEndpointBase dataObject)
		{
			if (dataObject.Identity == null)
			{
				throw new ArgumentException("Data object must have an identity assigned before being persisted.", "dataObject");
			}
			using (IMigrationDataProvider providerForFolder = migrationDataProvider.GetProviderForFolder(MigrationFolderName.Settings))
			{
				IEnumerable<MigrationEndpointBase> source = MigrationEndpointBase.Get(dataObject.Identity, migrationDataProvider, false);
				if (source.Any<MigrationEndpointBase>())
				{
					throw new MigrationEndpointDuplicatedException(dataObject.Identity.Id);
				}
				MigrationEndpointBase.VerifyConcurrencyLimits(migrationDataProvider, dataObject);
				dataObject.Identity = new MigrationEndpointId(dataObject.Identity.Id, Guid.NewGuid());
				dataObject.CreateInStore(providerForFolder, null);
			}
			MigrationEndpointLog.LogStatusEvent(dataObject.ToMigrationEndpoint(), MigrationEndpointLog.EndpointState.Created);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00024578 File Offset: 0x00022778
		internal static MigrationEndpointBase CreateFrom(MigrationEndpoint presentationObject)
		{
			if (presentationObject == null)
			{
				return null;
			}
			MigrationType endpointType = presentationObject.EndpointType;
			if (endpointType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (endpointType == MigrationType.IMAP)
				{
					return new ImapEndpoint(presentationObject);
				}
				if (endpointType == MigrationType.ExchangeOutlookAnywhere)
				{
					return new ExchangeOutlookAnywhereEndpoint(presentationObject);
				}
			}
			else
			{
				if (endpointType == MigrationType.ExchangeRemoteMove)
				{
					return new ExchangeRemoteMoveEndpoint(presentationObject);
				}
				if (endpointType == MigrationType.PSTImport)
				{
					return new PSTImportEndpoint(presentationObject);
				}
				if (endpointType == MigrationType.PublicFolder)
				{
					return new PublicFolderEndpoint(presentationObject);
				}
			}
			throw new ArgumentException("Endpoint doesn't have a supported type.", "presentationObject");
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00024654 File Offset: 0x00022854
		internal static IEnumerable<MigrationEndpointBase> Get(QueryFilter filter, IMigrationDataProvider dataProvider, bool ignoreNotFoundErrors)
		{
			MigrationUtil.ThrowOnNullArgument(filter, "filter");
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			return MigrationEndpointBase.Get(dataProvider, ignoreNotFoundErrors, (IMigrationDataProvider endpointDataProvider) => endpointDataProvider.FindMessageIds(filter, null, new SortBy[]
			{
				new SortBy(MigrationEndpointMessageSchema.MigrationEndpointType, SortOrder.Ascending),
				new SortBy(MigrationEndpointMessageSchema.MigrationEndpointName, SortOrder.Ascending)
			}, (IDictionary<PropertyDefinition, object> row) => MigrationRowSelectorResult.AcceptRow, null));
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000246EC File Offset: 0x000228EC
		internal static IEnumerable<MigrationEndpointBase> Get(MigrationEqualityFilter primaryFilter, IMigrationDataProvider dataProvider, bool ignoreNotFoundErrors)
		{
			MigrationUtil.ThrowOnNullArgument(primaryFilter, "primaryFilter");
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			return MigrationEndpointBase.Get(dataProvider, ignoreNotFoundErrors, (IMigrationDataProvider endpointDataProvider) => endpointDataProvider.FindMessageIds(primaryFilter, null, null, (IDictionary<PropertyDefinition, object> row) => MigrationRowSelectorResult.AcceptRow, null));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00024734 File Offset: 0x00022934
		internal static IEnumerable<MigrationEndpointBase> Get(MigrationEndpointId migrationEndpointId, IMigrationDataProvider dataProvider, bool ignoreNotFoundErrors)
		{
			QueryFilter filter = (migrationEndpointId ?? MigrationEndpointId.Any).GetFilter();
			return MigrationEndpointBase.Get(filter, dataProvider, ignoreNotFoundErrors);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002475C File Offset: 0x0002295C
		internal static MigrationEndpointBase Get(Guid endpointId, IMigrationDataProvider dataProvider)
		{
			List<MigrationEndpointBase> source = MigrationEndpointBase.Get(new MigrationEndpointId(string.Empty, endpointId), dataProvider, false).ToList<MigrationEndpointBase>();
			return source.FirstOrDefault<MigrationEndpointBase>();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00024787 File Offset: 0x00022987
		internal static IEnumerable<MigrationEndpointBase> Get(MigrationType endpointType, IMigrationDataProvider dataProvider)
		{
			return MigrationEndpointBase.Get(MigrationEndpointDataProvider.GetFilterFromEndpointType(endpointType), dataProvider, true);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00024798 File Offset: 0x00022998
		internal static MigrationType GetMigrationType(MigrationEndpoint sourceEndpoint, MigrationEndpoint targetEndpoint)
		{
			MigrationEndpointBase migrationEndpointBase = MigrationEndpointBase.CreateFrom(sourceEndpoint);
			MigrationEndpointBase migrationEndpointBase2 = MigrationEndpointBase.CreateFrom(targetEndpoint);
			if (migrationEndpointBase2 == null)
			{
				if (migrationEndpointBase != null)
				{
					return migrationEndpointBase.PreferredMigrationType;
				}
				return MigrationType.ExchangeLocalMove;
			}
			else
			{
				if (migrationEndpointBase == null)
				{
					return migrationEndpointBase2.PreferredMigrationType;
				}
				return migrationEndpointBase.ComputePreferredProtocol(migrationEndpointBase2);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000247E0 File Offset: 0x000229E0
		internal static void Delete(MigrationEndpointId endpointId, IMigrationDataProvider dataProvider)
		{
			using (IMigrationDataProvider providerForFolder = dataProvider.GetProviderForFolder(MigrationFolderName.Settings))
			{
				string[] array = (from job in MigrationJob.GetByEndpoint(dataProvider, endpointId)
				select job.JobName).ToArray<string>();
				if (array.Length > 0)
				{
					string batches = string.Join(", ", array);
					throw new CannotRemoveEndpointWithAssociatedBatchesException(endpointId.ToString(), batches);
				}
				try
				{
					MigrationEndpoint migrationEndpoint = MigrationEndpointBase.Get(endpointId.Guid, dataProvider).ToMigrationEndpoint();
					migrationEndpoint.LastModifiedTime = (DateTime)ExDateTime.UtcNow;
					MigrationEndpointLog.LogStatusEvent(migrationEndpoint, MigrationEndpointLog.EndpointState.Deleted);
				}
				catch (ObjectNotFoundException)
				{
				}
				PropertyDefinition[] properties = new StorePropertyDefinition[]
				{
					MigrationEndpointMessageSchema.MigrationEndpointType
				};
				IEnumerable<StoreObjectId> enumerable = providerForFolder.FindMessageIds(endpointId.GetFilter(), properties, new SortBy[]
				{
					new SortBy(MigrationEndpointMessageSchema.MigrationEndpointName, SortOrder.Ascending)
				}, (IDictionary<PropertyDefinition, object> row) => MigrationRowSelectorResult.AcceptRow, null);
				foreach (StoreObjectId messageId in enumerable)
				{
					providerForFolder.RemoveMessage(messageId);
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00024964 File Offset: 0x00022B64
		internal static void UpdateEndpoint(MigrationEndpoint presentationObject, IMigrationDataProvider dataProvider)
		{
			using (IMigrationDataProvider providerForFolder = dataProvider.GetProviderForFolder(MigrationFolderName.Settings))
			{
				MigrationEndpointBase migrationEndpointBase = MigrationEndpointBase.Get(presentationObject.Identity, dataProvider, false).FirstOrDefault<MigrationEndpointBase>();
				if (migrationEndpointBase == null)
				{
					throw new ArgumentException("Endpoint to update must be persisted already.");
				}
				migrationEndpointBase.InitializeFromPresentationObject(presentationObject);
				MigrationEndpointBase.VerifyConcurrencyLimits(dataProvider, migrationEndpointBase);
				using (IMigrationMessageItem migrationMessageItem = migrationEndpointBase.FindMessageItem(providerForFolder, migrationEndpointBase.PropertyDefinitions))
				{
					migrationMessageItem.OpenAsReadWrite();
					migrationEndpointBase.WriteToMessageItem(migrationMessageItem, true);
					migrationMessageItem.Save(SaveMode.NoConflictResolution);
				}
				MigrationEndpointLog.LogStatusEvent(presentationObject, MigrationEndpointLog.EndpointState.Updated);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00024A08 File Offset: 0x00022C08
		internal virtual MigrationType ComputePreferredProtocol(MigrationEndpointBase targetEndpointData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00024A0F File Offset: 0x00022C0F
		protected virtual void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00024A11 File Offset: 0x00022C11
		protected virtual void ApplyAutodiscoverSettings(AutodiscoverClientResponse response)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00024A18 File Offset: 0x00022C18
		protected virtual void ValidateAuthenticationMethod(AuthenticationMethod authenticationMethod)
		{
			if (!this.SupportedAuthenticationMethods.Contains(authenticationMethod))
			{
				string validValues = string.Join<AuthenticationMethod>(",", this.SupportedAuthenticationMethods);
				throw new AuthenticationMethodNotSupportedException(authenticationMethod.ToString(), this.PreferredMigrationType.ToString(), validValues);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00024A66 File Offset: 0x00022C66
		protected virtual void ApplyAdditionalProperties(MigrationEndpoint presentationObject)
		{
			presentationObject.RemoteServer = this.RemoteServer;
			presentationObject.Credentials = this.Credentials;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00024A80 File Offset: 0x00022C80
		protected override bool InitializeFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.InitializeFromMessageItem(message))
			{
				return false;
			}
			Guid guidProperty = MigrationHelper.GetGuidProperty(message, MigrationEndpointMessageSchema.MigrationEndpointGuid, true);
			this.Identity = new MigrationEndpointId((string)message[MigrationEndpointMessageSchema.MigrationEndpointName], guidProperty);
			MigrationType valueOrDefault = message.GetValueOrDefault<MigrationType>(MigrationEndpointMessageSchema.MigrationEndpointType, MigrationType.None);
			if (valueOrDefault != this.EndpointType)
			{
				throw new UnexpectedMigrationTypeException(valueOrDefault.ToString(), this.EndpointType.ToString());
			}
			return true;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00024AFC File Offset: 0x00022CFC
		protected virtual void InitializeFromPresentationObject(MigrationEndpoint endpoint)
		{
			this.SlotProvider = BasicMigrationSlotProvider.Get(this.Guid, endpoint.MaxConcurrentMigrations, endpoint.MaxConcurrentIncrementalSyncs);
			this.EndpointType = endpoint.EndpointType;
			this.Identity = endpoint.Identity;
			this.RemoteServer = endpoint.RemoteServer;
			this.Credentials = endpoint.Credentials;
			this.AuthenticationMethod = (endpoint.Authentication ?? this.DefaultAuthenticationMethod);
			this.LastModifiedTime = (ExDateTime)endpoint.LastModifiedTime;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00024E90 File Offset: 0x00023090
		private static IEnumerable<MigrationEndpointBase> Get(IMigrationDataProvider dataProvider, bool ignoreNotFoundErrors, Func<IMigrationDataProvider, IEnumerable<StoreObjectId>> messageIdFinder)
		{
			MigrationUtil.ThrowOnNullArgument(messageIdFinder, "messageIdFinder");
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			using (IMigrationDataProvider endpointDataProvider = dataProvider.GetProviderForFolder(MigrationFolderName.Settings))
			{
				IEnumerable<StoreObjectId> messageIds = messageIdFinder(endpointDataProvider);
				PropertyDefinition[] propertiesToLoad = new StorePropertyDefinition[]
				{
					MigrationEndpointMessageSchema.MigrationEndpointType,
					MigrationBatchMessageSchema.MigrationVersion
				};
				foreach (StoreObjectId endpointMessageId in messageIds)
				{
					MigrationEndpointBase endpoint = null;
					try
					{
						using (IMigrationMessageItem migrationMessageItem = endpointDataProvider.FindMessage(endpointMessageId, propertiesToLoad))
						{
							endpoint = MigrationEndpointBase.CreateEndpointFromMessage(endpointDataProvider, migrationMessageItem);
						}
					}
					catch (ObjectNotFoundException exception)
					{
						if (!ignoreNotFoundErrors)
						{
							throw;
						}
						MigrationLogger.Log(MigrationEventType.Error, exception, "Encountered an object not found exception when loading a MigrationEndpoint from a message - likely due to a race condition.", new object[0]);
					}
					if (endpoint != null)
					{
						yield return endpoint;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00024EBC File Offset: 0x000230BC
		private static MigrationEndpointBase CreateEndpointFromMessage(IMigrationDataProvider endpointDataProvider, IMigrationMessageItem message)
		{
			MigrationType valueOrDefault = message.GetValueOrDefault<MigrationType>(MigrationEndpointMessageSchema.MigrationEndpointType, MigrationType.None);
			MigrationType migrationType = valueOrDefault;
			MigrationEndpointBase migrationEndpointBase;
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType == MigrationType.IMAP)
				{
					migrationEndpointBase = new ImapEndpoint();
					goto IL_6F;
				}
				if (migrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					migrationEndpointBase = new ExchangeOutlookAnywhereEndpoint();
					goto IL_6F;
				}
			}
			else
			{
				if (migrationType == MigrationType.ExchangeRemoteMove)
				{
					migrationEndpointBase = new ExchangeRemoteMoveEndpoint();
					goto IL_6F;
				}
				if (migrationType == MigrationType.PSTImport)
				{
					migrationEndpointBase = new PSTImportEndpoint();
					goto IL_6F;
				}
				if (migrationType == MigrationType.PublicFolder)
				{
					migrationEndpointBase = new PublicFolderEndpoint();
					goto IL_6F;
				}
			}
			throw new MigrationDataCorruptionException("Invalid endpoint type: " + valueOrDefault);
			IL_6F:
			if (!migrationEndpointBase.TryLoad(endpointDataProvider, message.Id))
			{
				throw new CouldNotLoadMigrationPersistedItemTransientException(message.Id.ToHexEntryId());
			}
			return migrationEndpointBase;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00024F5C File Offset: 0x0002315C
		private static void VerifyConcurrencyLimits(IMigrationDataProvider dataProvider, MigrationEndpointBase incomingEndpoint)
		{
			MigrationSession migrationSession = MigrationSession.Get(dataProvider, false);
			Unlimited<int> maxConcurrentMigrations = migrationSession.MaxConcurrentMigrations;
			if (maxConcurrentMigrations.IsUnlimited)
			{
				return;
			}
			Unlimited<int> value = incomingEndpoint.SlotProvider.MaximumConcurrentMigrations;
			if (value.IsUnlimited)
			{
				throw new MaximumConcurrentMigrationLimitExceededException(value.ToString(), maxConcurrentMigrations.ToString(), incomingEndpoint.EndpointType.ToString());
			}
			foreach (MigrationEndpointBase migrationEndpointBase in MigrationEndpointBase.Get(incomingEndpoint.EndpointType, dataProvider))
			{
				if (!migrationEndpointBase.Identity.Equals(incomingEndpoint.Identity))
				{
					if (migrationEndpointBase.SlotProvider.MaximumConcurrentMigrations.IsUnlimited)
					{
						value = Unlimited<int>.UnlimitedValue;
						break;
					}
					value += migrationEndpointBase.SlotProvider.MaximumConcurrentMigrations;
				}
			}
			if (value > maxConcurrentMigrations)
			{
				throw new MaximumConcurrentMigrationLimitExceededException(value.ToString(), maxConcurrentMigrations.ToString(), incomingEndpoint.EndpointType.ToString());
			}
		}

		// Token: 0x0400034C RID: 844
		private const long MinimumEndpointVersion = 4L;

		// Token: 0x0400034D RID: 845
		private const long MigrationTypeEndpointVersion = 5L;

		// Token: 0x0400034E RID: 846
		private const long MaximumEndpointVersion = 5L;
	}
}
