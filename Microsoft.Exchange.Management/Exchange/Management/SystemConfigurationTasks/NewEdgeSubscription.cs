using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Cryptography;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009BF RID: 2495
	[Cmdlet("New", "EdgeSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewEdgeSubscription : NewFixedNameSystemConfigurationObjectTask<Server>
	{
		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x060058C5 RID: 22725 RVA: 0x00172816 File Offset: 0x00170A16
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewEdgeSubscription;
			}
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x060058C6 RID: 22726 RVA: 0x0017281D File Offset: 0x00170A1D
		// (set) Token: 0x060058C7 RID: 22727 RVA: 0x00172825 File Offset: 0x00170A25
		[Parameter(Mandatory = false)]
		public TimeSpan AccountExpiryDuration
		{
			get
			{
				return this.accountExpiryDuration;
			}
			set
			{
				this.accountExpiryDuration = value;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x0017282E File Offset: 0x00170A2E
		// (set) Token: 0x060058C9 RID: 22729 RVA: 0x00172836 File Offset: 0x00170A36
		[Parameter(Mandatory = false)]
		public LongPath FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x0017283F File Offset: 0x00170A3F
		// (set) Token: 0x060058CB RID: 22731 RVA: 0x00172847 File Offset: 0x00170A47
		[Parameter(Mandatory = false)]
		public AdSiteIdParameter Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x00172850 File Offset: 0x00170A50
		// (set) Token: 0x060058CD RID: 22733 RVA: 0x00172871 File Offset: 0x00170A71
		[Parameter(Mandatory = false)]
		public bool CreateInternetSendConnector
		{
			get
			{
				return (bool)(base.Fields["CreateInternetSendConnector"] ?? true);
			}
			set
			{
				base.Fields["CreateInternetSendConnector"] = value;
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x00172889 File Offset: 0x00170A89
		// (set) Token: 0x060058CF RID: 22735 RVA: 0x001728AA File Offset: 0x00170AAA
		[Parameter(Mandatory = false)]
		public bool CreateInboundSendConnector
		{
			get
			{
				return (bool)(base.Fields["CreateInboundSendConnector"] ?? true);
			}
			set
			{
				base.Fields["CreateInboundSendConnector"] = value;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x001728C2 File Offset: 0x00170AC2
		// (set) Token: 0x060058D1 RID: 22737 RVA: 0x001728D4 File Offset: 0x00170AD4
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			internal get
			{
				return new SwitchParameter(this.force);
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x060058D2 RID: 22738 RVA: 0x001728DD File Offset: 0x00170ADD
		// (set) Token: 0x060058D3 RID: 22739 RVA: 0x001728F4 File Offset: 0x00170AF4
		[Parameter(Mandatory = false)]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00172908 File Offset: 0x00170B08
		internal static SecurityIdentifier GetSidForExchangeKnownGuid(IRecipientSession session, Guid knownGuid, string containerDN)
		{
			ADGroup adgroup = session.ResolveWellKnownGuid<ADGroup>(knownGuid, containerDN);
			if (adgroup == null)
			{
				throw new ErrorExchangeGroupNotFoundException(knownGuid);
			}
			return adgroup.Sid;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00172930 File Offset: 0x00170B30
		private static ExchangeRelease GetExchangeRelease(int versionNumber, out ServerVersion serverVersion)
		{
			int num = versionNumber >> 22 & 63;
			int num2 = versionNumber >> 16 & 63;
			int build = versionNumber & 32767;
			serverVersion = new ServerVersion(num, num2, build, 0);
			if (Server.Exchange2007MajorVersion == num && num2 == 0)
			{
				return ExchangeRelease.E2007RTM;
			}
			if (Server.Exchange2007MajorVersion == num && num2 > 0 && num2 <= 3)
			{
				return ExchangeRelease.E2007SP1_SP2_SP3_E14_E15;
			}
			if (Server.Exchange2009MajorVersion == num && num2 >= 0)
			{
				return ExchangeRelease.E2007SP1_SP2_SP3_E14_E15;
			}
			if (Server.Exchange2011MajorVersion == num && num2 >= 0)
			{
				return ExchangeRelease.E2007SP1_SP2_SP3_E14_E15;
			}
			return ExchangeRelease.Unknown;
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0017299C File Offset: 0x00170B9C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 362, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\NewEdgeSubscription.cs");
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x001729C0 File Offset: 0x00170BC0
		protected override void InternalValidate()
		{
			if (this.filename != null && !Path.GetExtension(this.filename.ToString()).Equals(".xml", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteError(new ArgumentException(Strings.ErrorSubscriptionFileMustBeXml, "FileName"), ErrorCategory.InvalidData, null);
			}
			if (this.accountExpiryDuration != TimeSpan.MinValue && this.accountExpiryDuration < NewEdgeSubscription.MinimumAccountExpiry)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMinimumAccountExpiry, "AccountExpiryDuration"), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00172A54 File Offset: 0x00170C54
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.configurationSession = (ITopologyConfigurationSession)base.DataSession;
			this.serializer = new EdgeSubscriptionDataSerializer();
			try
			{
				this.serverObject = this.configurationSession.FindLocalServer();
				if (this.serverObject.IsEdgeServer)
				{
					TaskLogger.Trace("Running the task on Edge server " + this.serverObject.Name, new object[0]);
					this.runningLocation = RunningLocation.Edge;
				}
				else if (this.serverObject.IsHubTransportServer)
				{
					TaskLogger.Trace("Running the task on Hub server " + this.serverObject.Name, new object[0]);
					this.runningLocation = RunningLocation.Hub;
				}
				else
				{
					TaskLogger.Trace("Running the task admin console", new object[0]);
					this.runningLocation = RunningLocation.AdminOnly;
				}
			}
			catch (LocalServerNotFoundException)
			{
				TaskLogger.Trace("Running the task admin console", new object[0]);
				this.runningLocation = RunningLocation.AdminOnly;
			}
			if (this.runningLocation == RunningLocation.Edge)
			{
				TaskLogger.Trace("Initiate subscription from Edge side", new object[0]);
				if (this.filename == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ExportFileNameMissing), ErrorCategory.InvalidOperation, null);
				}
				if (this.FileData != null)
				{
					base.WriteError(new InvalidOperationException(Strings.FileDataShouldNotBeSet), ErrorCategory.InvalidOperation, null);
				}
				this.accountExpiryDuration = ((this.accountExpiryDuration == TimeSpan.MinValue) ? NewEdgeSubscription.DefaultBootStrapEdgeSyncAccountExpiry : this.accountExpiryDuration);
				LocalizedString message = this.serverObject.GatewayEdgeSyncSubscribed ? Strings.ConfirmationMessageNewEdgeReSubscriptionExpirationWarning(this.accountExpiryDuration.TotalMinutes.ToString()) : Strings.ConfirmationMessageNewEdgeSubscriptionWarnConfigObjectsWillBeDeleted(this.accountExpiryDuration.TotalMinutes.ToString());
				if (this.force || base.ShouldContinue(message))
				{
					this.InitiateSubscriptionOnEdge();
				}
			}
			else
			{
				if (this.filename != null)
				{
					base.WriteError(new InvalidOperationException(Strings.ExportFileNameShouldNotBeSet), ErrorCategory.InvalidOperation, null);
				}
				if (this.FileData == null)
				{
					base.WriteError(new InvalidOperationException(Strings.FileDataMissing), ErrorCategory.InvalidOperation, null);
				}
				if (this.site == null)
				{
					base.WriteError(new InvalidOperationException(Strings.SiteParameterRequired), ErrorCategory.InvalidOperation, null);
				}
				this.accountExpiryDuration = ((this.accountExpiryDuration == TimeSpan.MinValue) ? NewEdgeSubscription.DefaultEdgeSyncAccountExpiry : this.accountExpiryDuration);
				TaskLogger.Trace("Complete subscription inside the organization", new object[0]);
				this.CompleteSubscription();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x00172CD0 File Offset: 0x00170ED0
		private void InitiateSubscriptionOnEdge()
		{
			this.LoadCertificate();
			this.edgeSubscriptionData.EdgeServerName = this.serverObject.Name;
			this.edgeSubscriptionData.EdgeServerFQDN = this.serverObject.Fqdn;
			this.edgeSubscriptionData.ESRAPassword = AdamUserManagement.GeneratePassword();
			this.edgeSubscriptionData.EffectiveDate = DateTime.UtcNow.Ticks;
			this.edgeSubscriptionData.Duration = this.accountExpiryDuration.Ticks;
			this.edgeSubscriptionData.ServerType = this.serverObject.ServerType;
			this.edgeSubscriptionData.ProductID = this.serverObject.ProductID;
			this.edgeSubscriptionData.VersionNumber = this.serverObject.VersionNumber;
			this.edgeSubscriptionData.SerialNumber = this.serverObject.SerialNumber;
			this.GetAdamPort();
			for (int i = 0; i < 4; i++)
			{
				try
				{
					this.edgeSubscriptionData.ESRAUsername = AdamUserManagement.CreateOrUpdateADAMPrincipal("ESRA." + this.edgeSubscriptionData.EdgeServerName, this.edgeSubscriptionData.ESRAPassword, true, this.accountExpiryDuration);
					break;
				}
				catch (Exception ex)
				{
					ExTraceGlobals.SubscriptionTracer.TraceError<Exception>(0L, "CreateOrUpdateADAMPrincipal failed with {0}", ex);
					if (!(ex is DirectoryOperationException) || i == 3)
					{
						base.WriteError(new InvalidOperationException(Strings.FailToCreateOrUpdateSubscriptionPrincipalOnEdge(ex.Message, ex.StackTrace)), ErrorCategory.InvalidOperation, null);
					}
					Thread.Sleep(1000);
				}
			}
			try
			{
				AdamUserManagement.VerifyADAMPrincipal(this.edgeSubscriptionData.ESRAUsername, this.edgeSubscriptionData.ESRAPassword);
			}
			catch (Exception ex2)
			{
				base.WriteError(new InvalidOperationException(Strings.FailToValidateSubscripionPrincipalOnEdge(this.edgeSubscriptionData.ESRAUsername, ex2.Message)), ErrorCategory.InvalidOperation, null);
			}
			this.GenerateSubscriptionFile();
			this.serverObject.EdgeSyncStatus.Clear();
			this.serverObject.EdgeSyncStatus.Add(DateTime.UtcNow.AddTicks(this.accountExpiryDuration.Ticks).ToString(DateTimeFormatInfo.InvariantInfo));
			if (!this.serverObject.GatewayEdgeSyncSubscribed)
			{
				foreach (SmtpSendConnectorConfig instance in this.configurationSession.FindAllPaged<SmtpSendConnectorConfig>())
				{
					this.configurationSession.Delete(instance);
				}
				this.serverObject.GatewayEdgeSyncSubscribed = true;
			}
			this.configurationSession.Save(this.serverObject);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x00172F6C File Offset: 0x0017116C
		private void InstallCertificateToADAMServiceStore()
		{
			CertificateStore certificateStore = null;
			try
			{
				certificateStore = new CertificateStore(StoreType.Service, "ADAM_MSExchange\\MY");
				certificateStore.Open(OpenFlags.ReadWrite);
				certificateStore.BaseStore.RemoveRange(certificateStore.BaseStore.Certificates);
				certificateStore.BaseStore.Add(this.tlsCertificate);
				ExTraceGlobals.SubscriptionTracer.TraceDebug(0L, "install the certificate into ADAM service store");
			}
			catch (CryptographicException ex)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorAddCertificateOnEdge(ex.Message)), ErrorCategory.InvalidOperation, this.serverObject.Fqdn);
			}
			finally
			{
				if (certificateStore != null)
				{
					certificateStore.Close();
				}
			}
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x00173028 File Offset: 0x00171228
		private void GenerateSubscriptionFile()
		{
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EdgeServerName: {0}", this.edgeSubscriptionData.EdgeServerName);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EdgeServerFQDN: {0}", this.edgeSubscriptionData.EdgeServerFQDN);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "ESRAUsername: {0}", this.edgeSubscriptionData.ESRAUsername);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EffectiveDate: {0}", new DateTime(this.edgeSubscriptionData.EffectiveDate).ToString("u", DateTimeFormatInfo.InvariantInfo));
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Duration: {0}", new TimeSpan(this.edgeSubscriptionData.Duration).ToString());
			ExTraceGlobals.SubscriptionTracer.TraceDebug<int>(0L, "ADAM ssl port: {0}", this.edgeSubscriptionData.AdamSslPort);
			try
			{
				using (FileStream fileStream = new FileStream(this.filename.PathName, this.force ? FileMode.Create : FileMode.CreateNew))
				{
					this.serializer.Serialize(fileStream, this.edgeSubscriptionData);
				}
			}
			catch (UnauthorizedAccessException)
			{
				base.WriteError(new ExportDestinationIsReadonlyException(this.filename.PathName), ErrorCategory.WriteError, null);
			}
			catch (SecurityException)
			{
				base.WriteError(new ExportDestinationAccessDeniedException(this.filename.PathName), ErrorCategory.WriteError, null);
			}
			catch (DirectoryNotFoundException)
			{
				base.WriteError(new ExportDestinationInvalidException(this.filename.PathName), ErrorCategory.WriteError, null);
			}
			catch (PathTooLongException)
			{
				base.WriteError(new ExportDestinationInvalidException(this.filename.PathName), ErrorCategory.WriteError, null);
			}
			catch (IOException ex)
			{
				char[] trimChars = new char[]
				{
					'.',
					'\r',
					'\n'
				};
				base.WriteError(new ExportIOFailureException(ex.Message.TrimEnd(trimChars)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x00173240 File Offset: 0x00171440
		private void GetAdamPort()
		{
			try
			{
				this.edgeSubscriptionData.AdamSslPort = AdamUserManagement.ReadSslPortFromRegistry();
			}
			catch (Exception ex)
			{
				base.WriteError(new InvalidOperationException(Strings.FailedToReadAdamPortFromRegistry(ex.Message)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x00173290 File Offset: 0x00171490
		private void CompleteSubscription()
		{
			try
			{
				this.LoadSubscriptionFile();
			}
			catch (Exception ex)
			{
				base.WriteError(new InvalidOperationException(Strings.FailedToLoadSubscriptionFile(ex.Message)), ErrorCategory.InvalidOperation, null);
			}
			if (this.edgeSubscriptionData.EffectiveDate > DateTime.UtcNow.Ticks)
			{
				base.WriteError(new InvalidOperationException(Strings.ClockOutofSync), ErrorCategory.InvalidOperation, null);
			}
			if (DateTime.UtcNow.Ticks + TimeSpan.FromMinutes(5.0).Ticks > this.edgeSubscriptionData.EffectiveDate + this.edgeSubscriptionData.Duration)
			{
				base.WriteError(new InvalidOperationException(Strings.BootstrapAccountExpireTooSoon), ErrorCategory.InvalidOperation, null);
			}
			this.SetSiteId();
			if (!this.CheckSubscriptionVersion())
			{
				return;
			}
			if (!this.ProvisionEdgeServerObject())
			{
				return;
			}
			this.UpdateAllBridgeheadServerObject();
			this.WriteWarning(Strings.ConfirmationMessageNewEdgeSubscriptionWarnEdgeFQDNMustbeResolvable(this.site.ToString(), this.edgeSubscriptionData.EdgeServerFQDN, this.edgeSubscriptionData.AdamSslPort.ToString()));
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x001733AC File Offset: 0x001715AC
		private void LoadSubscriptionFile()
		{
			using (MemoryStream memoryStream = new MemoryStream(this.FileData))
			{
				this.edgeSubscriptionData = (EdgeSubscriptionData)this.serializer.Deserialize(memoryStream);
			}
			if (string.IsNullOrEmpty(this.edgeSubscriptionData.EdgeServerFQDN) || string.IsNullOrEmpty(this.edgeSubscriptionData.EdgeServerName) || string.IsNullOrEmpty(this.edgeSubscriptionData.ESRAUsername) || string.IsNullOrEmpty(this.edgeSubscriptionData.ESRAPassword))
			{
				throw new InvalidOperationException(Strings.InvalidSubscriptionFile);
			}
			try
			{
				this.tlsCertificate = new X509Certificate2(this.edgeSubscriptionData.EdgeCertificateBlob);
			}
			catch (CryptographicException)
			{
				throw new InvalidOperationException(Strings.InvalidSubscriptionFile);
			}
			if (TlsCertificateInfo.IsCNGProvider(this.tlsCertificate))
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "Edge Subscription only supports CAPI certificate in EdgeSubscription. Current certificate in the subscription file with thumbprint {0} is not a CAPI certificate.", this.tlsCertificate.Thumbprint);
				throw new InvalidOperationException(Strings.EdgeSubscriptionRequiresCAPICert(this.tlsCertificate.Thumbprint));
			}
			try
			{
				DirectTrust.Load();
				SecurityIdentifier left = DirectTrust.MapCertToSecurityIdentifier(this.tlsCertificate);
				if (left == WellKnownSids.HubTransportServers)
				{
					throw new InvalidOperationException(Strings.EdgeAndHubSharingSameDirectTrustCertNotAllowed(this.tlsCertificate.Thumbprint));
				}
			}
			finally
			{
				DirectTrust.Unload();
			}
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x00173518 File Offset: 0x00171718
		private Server FindAnyHubServer()
		{
			TaskLogger.LogEnter();
			Server result;
			try
			{
				ADPagedReader<Server> adpagedReader = this.configurationSession.FindAllServersWithVersionNumber(Server.E2007MinVersion);
				foreach (Server server in adpagedReader)
				{
					if (server.IsHubTransportServer && server.ServerSite.Equals(this.siteId))
					{
						TaskLogger.LogExit();
						return server;
					}
				}
				TaskLogger.LogExit();
				result = null;
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "A transient AD error is preventing us from loading all E12 servers {0}", ex.Message);
				TaskLogger.Trace("A transient AD error is preventing us from loading all E12 servers" + ex.Message, new object[0]);
				TaskLogger.LogExit();
				base.WriteError(new InvalidOperationException(Strings.FailedBecauseofADTransientException(ex.Message)), ErrorCategory.InvalidOperation, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x00173608 File Offset: 0x00171808
		private void SetSiteId()
		{
			IEnumerable<ADSite> objects = this.site.GetObjects<ADSite>(null, this.configurationSession);
			using (IEnumerator<ADSite> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					this.siteId = (ADObjectId)enumerator.Current.Identity;
					if (enumerator.MoveNext())
					{
						base.WriteError(new InvalidOperationException(Strings.MultipleSubscriptionSites), ErrorCategory.InvalidOperation, null);
					}
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.CannotFindSubscriptionSite), ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x001736A4 File Offset: 0x001718A4
		private bool ProvisionEdgeServerObject()
		{
			TaskLogger.LogEnter();
			string edgeServerName = this.edgeSubscriptionData.EdgeServerName;
			ProtocolsContainer protocolsContainer = null;
			SmtpContainer smtpContainer = null;
			SmtpVirtualServerConfiguration smtpVirtualServerConfiguration = null;
			MicrosoftMTAConfiguration microsoftMTAConfiguration = null;
			try
			{
				Server server = this.configurationSession.FindServerByName(edgeServerName);
				Server server2 = this.FindAnyHubServer();
				if (server2 == null)
				{
					base.WriteError(new InvalidOperationException(Strings.NoHubInSubscribedSite), ErrorCategory.InvalidOperation, null);
					TaskLogger.LogExit();
					return false;
				}
				ADObjectId adobjectId = null;
				if (server == null)
				{
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Provision a new GatewayServer {0}", edgeServerName);
					TaskLogger.Trace("Create Edge server " + this.edgeSubscriptionData.EdgeServerName, new object[0]);
					ADObjectId childId = this.configurationSession.GetAdministrativeGroupId().GetChildId("Servers");
					adobjectId = childId.GetChildId(edgeServerName);
					server = new Server();
					server.SetId(adobjectId);
					server.ServerSite = this.siteId;
					server.CurrentServerRole = ServerRole.Edge;
					server.Name = this.edgeSubscriptionData.EdgeServerName;
					server.Heuristics = 12288;
					server.HomeRoutingGroup = server2.HomeRoutingGroup;
					TcpNetworkAddress item = new TcpNetworkAddress(NetworkProtocol.TcpIP, this.edgeSubscriptionData.EdgeServerFQDN);
					server.NetworkAddress.Add(item);
					string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}/cn=Configuration/cn=Servers", new object[]
					{
						this.configurationSession.GetAdministrativeGroup().LegacyExchangeDN
					});
					server.ExchangeLegacyDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, server);
					protocolsContainer = new ProtocolsContainer();
					ADObjectId childId2 = adobjectId.GetChildId("Protocols");
					protocolsContainer.SetId(childId2);
					microsoftMTAConfiguration = new MicrosoftMTAConfiguration();
					microsoftMTAConfiguration.LocalDesig = edgeServerName;
					microsoftMTAConfiguration.TransRetryMins = 5;
					microsoftMTAConfiguration.TransTimeoutMins = 20;
					ADObjectId childId3 = adobjectId.GetChildId("Microsoft MTA");
					microsoftMTAConfiguration.SetId(childId3);
					microsoftMTAConfiguration.ExchangeLegacyDN = string.Format("{0}/cn={1}", server.ExchangeLegacyDN, "Microsoft MTA");
					smtpContainer = new SmtpContainer();
					ADObjectId childId4 = childId2.GetChildId("SMTP");
					smtpContainer.SetId(childId4);
					smtpVirtualServerConfiguration = new SmtpVirtualServerConfiguration();
					ADObjectId childId5 = childId4.GetChildId("1");
					smtpVirtualServerConfiguration.SetId(childId5);
					smtpVirtualServerConfiguration.SmtpFqdn = this.edgeSubscriptionData.EdgeServerFQDN;
					ExTraceGlobals.SubscriptionTracer.TraceDebug<ADObjectId>(0L, "Site: {0}", server.ServerSite);
					ExTraceGlobals.SubscriptionTracer.TraceDebug<int>(0L, "VersionNumber: {0}", server.VersionNumber);
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Name: {0}", server.Name);
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "FQDN: {0}", this.edgeSubscriptionData.EdgeServerFQDN);
				}
				else
				{
					if (!server.IsEdgeServer)
					{
						base.WriteError(new InvalidOperationException(Strings.ServerNameConflict), ErrorCategory.InvalidOperation, null);
						return false;
					}
					if (!ADObjectId.Equals(server.ServerSite, this.siteId))
					{
						base.WriteError(new InvalidOperationException(Strings.CannotSubscribeToMultipleSites(server.Name, server.ServerSite.Name)), ErrorCategory.InvalidOperation, null);
						return false;
					}
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Re-subscribe for Edge server: {0}", server.DistinguishedName);
				}
				if ((this.CreateInternetSendConnector || this.CreateInboundSendConnector) && server.ServerSite.Name.Length > 41)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSiteNameIsLongerThanLimit(server.ServerSite.Name)), ErrorCategory.InvalidOperation, null);
				}
				server.VersionNumber = this.edgeSubscriptionData.VersionNumber;
				ServerVersion adminDisplayVersion = null;
				if (!string.IsNullOrEmpty(this.edgeSubscriptionData.SerialNumber) && ServerVersion.TryParseFromSerialNumber(this.edgeSubscriptionData.SerialNumber, out adminDisplayVersion))
				{
					server.AdminDisplayVersion = adminDisplayVersion;
				}
				else
				{
					server.AdminDisplayVersion = this.defaultEdgeServerVersion;
				}
				server.ServerType = this.edgeSubscriptionData.ServerType;
				server.ProductID = this.edgeSubscriptionData.ProductID;
				if (this.edgeSubscriptionData.AdamSslPort != 0)
				{
					server.EdgeSyncAdamSslPort = this.edgeSubscriptionData.AdamSslPort;
				}
				server.GatewayEdgeSyncSubscribed = true;
				server.EdgeSyncCredentials.Clear();
				server.InternalTransportCertificate = this.edgeSubscriptionData.EdgeCertificateBlob;
				this.configurationSession.Save(server);
				if (adobjectId != null)
				{
					ExTraceGlobals.SubscriptionTracer.TraceDebug(0L, "Allows all E12 BHs to read/write subscribed GW object in AD");
					server = this.configurationSession.Read<Server>(adobjectId);
					RawSecurityDescriptor rawSecurityDescriptor = server.ReadSecurityDescriptor();
					ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
					activeDirectorySecurity.SetSecurityDescriptorSddlForm(rawSecurityDescriptor.GetSddlForm(AccessControlSections.All));
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1087, "ProvisionEdgeServerObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\NewEdgeSubscription.cs");
					SecurityIdentifier sidForExchangeKnownGuid = NewEdgeSubscription.GetSidForExchangeKnownGuid(tenantOrRootOrgRecipientSession, WellKnownGuid.ExSWkGuid, this.configurationSession.ConfigurationNamingContext.DistinguishedName);
					ActiveDirectoryAccessRule rule = new ActiveDirectoryAccessRule(sidForExchangeKnownGuid, ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All);
					activeDirectorySecurity.AddAccessRule(rule);
					server.SaveSecurityDescriptor(new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0));
				}
				if (protocolsContainer != null)
				{
					this.configurationSession.Save(protocolsContainer);
				}
				if (microsoftMTAConfiguration != null)
				{
					this.configurationSession.Save(microsoftMTAConfiguration);
				}
				if (smtpContainer != null)
				{
					this.configurationSession.Save(smtpContainer);
				}
				if (smtpVirtualServerConfiguration != null)
				{
					this.configurationSession.Save(smtpVirtualServerConfiguration);
				}
				base.WriteObject(new EdgeSubscription(server));
				if (this.CreateInternetSendConnector)
				{
					this.EnsureDefaultToInternet(server);
				}
				if (this.CreateInboundSendConnector)
				{
					this.EnsureDefaultInbound(server);
				}
				TaskLogger.LogExit();
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "A transient AD error has occurred {0}", ex.Message);
				TaskLogger.LogExit();
				base.WriteError(new InvalidOperationException(Strings.FailedBecauseofADTransientException(ex.Message)), ErrorCategory.InvalidOperation, null);
				return false;
			}
			catch (DefaultAdministrativeGroupNotFoundException ex2)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "Default Administrative Group Not Found Exception occured {0}", ex2.Message);
				TaskLogger.LogExit();
				base.WriteError(new InvalidOperationException(Strings.FailedBecauseofDefaultAdministrativeGroupNotFoundException(ex2.Message)), ErrorCategory.InvalidOperation, null);
				return false;
			}
			catch (LocalizedException ex3)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "LocalizedException occured {0}", ex3.Message);
				TaskLogger.LogExit();
				base.WriteError(ex3, ErrorCategory.InvalidOperation, null);
				return false;
			}
			return true;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x00173D7C File Offset: 0x00171F7C
		private bool CheckSubscriptionVersion()
		{
			int[] servers = new int[4];
			int totalServers = 0;
			ADOperationResult adoperationResult;
			if (!ADNotificationAdapter.TryReadConfigurationPaged<Server>(() => this.configurationSession.FindAllServersWithVersionNumber(Server.E2007MinVersion), delegate(Server server)
			{
				ServerVersion serverVersion = null;
				if (server.IsHubTransportServer && this.siteId.Equals(server.ServerSite))
				{
					ExchangeRelease exchangeRelease2 = NewEdgeSubscription.GetExchangeRelease(server.VersionNumber, out serverVersion);
					servers[(int)exchangeRelease2]++;
					totalServers++;
				}
			}, out adoperationResult))
			{
				base.WriteError(new InvalidOperationException(Strings.FailedBecauseofADTransientException(adoperationResult.Exception.Message)), ErrorCategory.InvalidOperation, null);
			}
			ExchangeRelease exchangeRelease = NewEdgeSubscription.GetExchangeRelease(this.edgeSubscriptionData.VersionNumber, out this.defaultEdgeServerVersion);
			if (servers[(int)exchangeRelease] == 0)
			{
				base.WriteError(new InvalidOperationException(Strings.NoVersionMatch), ErrorCategory.InvalidOperation, null);
				return false;
			}
			return servers[(int)exchangeRelease] == totalServers || this.force || base.ShouldContinue(Strings.PartialVerisonMatch);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x00173E58 File Offset: 0x00172058
		private void UpdateAllBridgeheadServerObject()
		{
			TaskLogger.LogEnter();
			string text = null;
			try
			{
				ADPagedReader<Server> adpagedReader = this.configurationSession.FindAllServersWithVersionNumber(Server.E2007MinVersion);
				bool flag = false;
				foreach (Server server in adpagedReader)
				{
					if (server.IsHubTransportServer && this.siteId.Equals(server.ServerSite))
					{
						flag = true;
						this.SetEdgeSyncBootStrapAccount(server);
						this.SetEdgeSyncPermenantAccount(server);
						text = server.Fqdn;
						this.configurationSession.Save(server);
					}
				}
				if (!flag)
				{
					ExTraceGlobals.SubscriptionTracer.TraceError(0L, "Didn't detect any Exchange 2007 HubTransport servers");
					TaskLogger.Trace("Didn't detect any Exchange 2007 HubTransport servers", new object[0]);
					TaskLogger.LogExit();
					return;
				}
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "A transient AD error is preventing us from loading or saving all E12 servers {0}", ex.Message);
				TaskLogger.Trace("A transient AD error is preventing us from loading or saving all E12 servers" + ex.Message, new object[0]);
				base.WriteError(new InvalidOperationException(Strings.FailedBecauseofADTransientException(ex.Message)), ErrorCategory.InvalidOperation, null);
				TaskLogger.LogExit();
				return;
			}
			catch (DataValidationException ex2)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "A data validation AD error is preventing us from loading or saving all E12 servers {0}", ex2.Message);
				TaskLogger.Trace("A data validation AD error is preventing us from loading or saving all E12 servers" + ex2.Message, new object[0]);
				base.WriteError(new InvalidOperationException((text != null) ? Strings.FailedToSaveCorruptServerException(text, ex2.Message) : Strings.FailedBecauseofADDataValidationException(ex2.Message)), ErrorCategory.InvalidOperation, null);
				TaskLogger.LogExit();
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x00174010 File Offset: 0x00172210
		private void SetEdgeSyncBootStrapAccount(Server bhServer)
		{
			if (bhServer.InternalTransportCertificate == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "BH {0} doesn't have TLS cert, skip SetEdgeSyncBootStrapAccount", bhServer.Name);
				return;
			}
			MultiValuedProperty<byte[]> multiValuedProperty = new MultiValuedProperty<byte[]>();
			if (bhServer.EdgeSyncCredentials != null)
			{
				for (int i = 0; i < bhServer.EdgeSyncCredentials.Count; i++)
				{
					byte[] array = bhServer.EdgeSyncCredentials[i];
					EdgeSyncCredential edgeSyncCredential = EdgeSyncCredential.DeserializeEdgeSyncCredential(array);
					if (!string.Equals(edgeSyncCredential.EdgeServerFQDN, this.edgeSubscriptionData.EdgeServerFQDN, StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty.Add(array);
					}
				}
			}
			EdgeSyncCredential edgeSyncCredential2 = new EdgeSyncCredential();
			edgeSyncCredential2.EdgeServerFQDN = this.edgeSubscriptionData.EdgeServerFQDN;
			edgeSyncCredential2.ESRAUsername = this.edgeSubscriptionData.ESRAUsername;
			edgeSyncCredential2.EffectiveDate = this.edgeSubscriptionData.EffectiveDate;
			edgeSyncCredential2.Duration = this.edgeSubscriptionData.Duration;
			edgeSyncCredential2.IsBootStrapAccount = true;
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "SetEdgeSyncBootStrapAccount for BH {0}", bhServer.Name);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EdgeServerFQDN:{0}", edgeSyncCredential2.EdgeServerFQDN);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Username:{0}", edgeSyncCredential2.ESRAUsername);
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EffectiveDate {0}", new DateTime(edgeSyncCredential2.EffectiveDate).ToString("u", DateTimeFormatInfo.InvariantInfo));
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Duration {0}", new TimeSpan(edgeSyncCredential2.Duration).ToString());
			TaskLogger.Trace("Updating BH " + bhServer.Name + " with EdgeSync bootstrap credential " + edgeSyncCredential2.ESRAUsername, new object[0]);
			TaskLogger.Trace(edgeSyncCredential2.ESRAUsername + " effective date is " + edgeSyncCredential2.EffectiveDate, new object[0]);
			TaskLogger.Trace(edgeSyncCredential2.ESRAUsername + " Duration is " + edgeSyncCredential2.EffectiveDate, new object[0]);
			X509Certificate2 cert = null;
			try
			{
				cert = new X509Certificate2(bhServer.InternalTransportCertificate);
			}
			catch (CryptographicException)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFindCertificateOnServerObject), ErrorCategory.ObjectNotFound, bhServer.Name);
			}
			edgeSyncCredential2.EncryptedESRAPassword = AdamUserManagement.EncryptPassword(cert, this.edgeSubscriptionData.ESRAPassword);
			byte[] item = EdgeSyncCredential.SerializeEdgeSyncCredential(edgeSyncCredential2);
			multiValuedProperty.Add(item);
			bhServer.EdgeSyncCredentials = multiValuedProperty;
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0017427C File Offset: 0x0017247C
		private void SetEdgeSyncPermenantAccount(Server bhServer)
		{
			if (bhServer.InternalTransportCertificate == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "BH {0} doesn't have TLS cert, skip SetEdgeSyncPermenantAccount", bhServer.Name);
				return;
			}
			MultiValuedProperty<byte[]> edgeSyncCredentials = bhServer.EdgeSyncCredentials;
			long num = 0L;
			string text = this.edgeSubscriptionData.ESRAUsername.Substring(this.edgeSubscriptionData.ESRAUsername.IndexOf(',') + 1);
			Server server = this.configurationSession.FindServerByFqdn(this.edgeSubscriptionData.EdgeServerFQDN);
			if (server == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "Unable to find {0}, skip SetEdgeSyncPermenantAccount", this.edgeSubscriptionData.EdgeServerFQDN);
				return;
			}
			ExTraceGlobals.SubscriptionTracer.TraceDebug<string, string>(0L, "SetEdgeSyncPermenantAccount for BH {0} to GW {1}", bhServer.Name, server.Name);
			for (int i = 0; i < 2; i++)
			{
				EdgeSyncCredential edgeSyncCredential = new EdgeSyncCredential();
				edgeSyncCredential.EdgeServerFQDN = this.edgeSubscriptionData.EdgeServerFQDN;
				edgeSyncCredential.ESRAUsername = string.Concat(new string[]
				{
					"cn=ESRA.",
					this.edgeSubscriptionData.EdgeServerName,
					".",
					bhServer.Name,
					".",
					i.ToString(CultureInfo.InvariantCulture),
					",",
					text
				});
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Creating {0}", edgeSyncCredential.ESRAUsername);
				if (i == 0)
				{
					edgeSyncCredential.EffectiveDate = this.edgeSubscriptionData.EffectiveDate + this.edgeSubscriptionData.Duration;
					num = edgeSyncCredential.EffectiveDate;
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EffectiveDate {0}", new DateTime(edgeSyncCredential.EffectiveDate).ToString("u", DateTimeFormatInfo.InvariantInfo));
				}
				else
				{
					edgeSyncCredential.EffectiveDate = num + this.accountExpiryDuration.Ticks;
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EffectiveDate {0}", new DateTime(edgeSyncCredential.EffectiveDate).ToString("u", DateTimeFormatInfo.InvariantInfo));
				}
				edgeSyncCredential.Duration = this.accountExpiryDuration.Ticks;
				edgeSyncCredential.IsBootStrapAccount = false;
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Duration {0}", new TimeSpan(edgeSyncCredential.Duration).ToString());
				TaskLogger.Trace("Updating BH " + bhServer.Name + " with EdgeSync standard credential " + edgeSyncCredential.ESRAUsername, new object[0]);
				TaskLogger.Trace(edgeSyncCredential.ESRAUsername + " effective date is " + edgeSyncCredential.EffectiveDate, new object[0]);
				TaskLogger.Trace(edgeSyncCredential.ESRAUsername + " Duration is " + edgeSyncCredential.EffectiveDate, new object[0]);
				X509Certificate2 cert = null;
				try
				{
					cert = new X509Certificate2(bhServer.InternalTransportCertificate);
				}
				catch (CryptographicException)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorFindCertificateOnServerObject), ErrorCategory.ObjectNotFound, bhServer.Name);
				}
				X509Certificate2 cert2 = null;
				try
				{
					cert2 = new X509Certificate2(server.InternalTransportCertificate);
				}
				catch (CryptographicException)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorFindCertificateOnServerObject), ErrorCategory.ObjectNotFound, server.Name);
				}
				string text2 = AdamUserManagement.GeneratePassword();
				edgeSyncCredential.EncryptedESRAPassword = AdamUserManagement.EncryptPassword(cert, text2);
				edgeSyncCredential.EdgeEncryptedESRAPassword = AdamUserManagement.EncryptPassword(cert2, text2);
				using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
				{
					byte[] bytes = Encoding.ASCII.GetBytes(text2);
					hashAlgorithm.TransformFinalBlock(bytes, 0, bytes.Length);
					byte[] hash = hashAlgorithm.Hash;
					ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "Passwordhash {0}", Convert.ToBase64String(hash));
				}
				byte[] item = EdgeSyncCredential.SerializeEdgeSyncCredential(edgeSyncCredential);
				edgeSyncCredentials.Add(item);
			}
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x00174650 File Offset: 0x00172850
		private void EnsureDefaultToInternet(Server server)
		{
			string text = Util.MakeSiteConnectorName(server);
			SmtpSendConnectorConfig smtpSendConnectorConfig = Util.TryFindConnector(this.configurationSession, text);
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			multiValuedProperty.Add(server.Id);
			if (smtpSendConnectorConfig == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EnsureDefaultToInternet: {0} Not Found", text);
				ADObjectId childId = this.configurationSession.GetRoutingGroupId().GetChildId("Connections");
				smtpSendConnectorConfig = new SmtpSendConnectorConfig();
				smtpSendConnectorConfig.SetId(childId.GetChildId(text));
				smtpSendConnectorConfig.AddressSpaces.Add(new AddressSpace("smtp", "*", 100));
				smtpSendConnectorConfig.SourceTransportServers = multiValuedProperty;
				smtpSendConnectorConfig.DomainSecureEnabled = true;
				ManageSendConnectors.SetConnectorHomeMta(smtpSendConnectorConfig, this.configurationSession);
				smtpSendConnectorConfig.MaxMessageSize = ByteQuantifiedSize.FromMB(10UL);
				this.configurationSession.Save(smtpSendConnectorConfig);
				this.SetDefaultSendConnectorSecurityDescriptor(smtpSendConnectorConfig);
			}
			else
			{
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EnsureDefaultToInternet: {0} Found", text);
				if (smtpSendConnectorConfig.SourceTransportServers != null)
				{
					foreach (ADObjectId adobjectId in smtpSendConnectorConfig.SourceTransportServers)
					{
						if (adobjectId.Name != server.Id.Name)
						{
							multiValuedProperty.Add(adobjectId);
						}
					}
				}
				smtpSendConnectorConfig.SourceTransportServers = multiValuedProperty;
			}
			this.configurationSession.Save(smtpSendConnectorConfig);
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x001747B4 File Offset: 0x001729B4
		private void EnsureDefaultInbound(Server server)
		{
			string text = Util.MakeInboundConnectorName(server);
			SmtpSendConnectorConfig smtpSendConnectorConfig = Util.TryFindConnector(this.configurationSession, text);
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			multiValuedProperty.Add(server.Id);
			if (smtpSendConnectorConfig == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EnsureDefaultInbound: {0} Not Found", text);
				ADObjectId childId = this.configurationSession.GetRoutingGroupId().GetChildId("Connections");
				smtpSendConnectorConfig = new SmtpSendConnectorConfig();
				smtpSendConnectorConfig.SetId(childId.GetChildId(text));
				smtpSendConnectorConfig.AddressSpaces.Add(new AddressSpace("smtp", "--", 100));
				smtpSendConnectorConfig.SourceTransportServers = multiValuedProperty;
				ManageSendConnectors.SetConnectorHomeMta(smtpSendConnectorConfig, this.configurationSession);
				smtpSendConnectorConfig.MaxMessageSize = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				smtpSendConnectorConfig.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer;
				smtpSendConnectorConfig.SmartHosts = new SmartHost[]
				{
					SmartHost.Parse("--")
				};
				smtpSendConnectorConfig.DNSRoutingEnabled = false;
				this.configurationSession.Save(smtpSendConnectorConfig);
				this.SetDefaultSendConnectorSecurityDescriptor(smtpSendConnectorConfig);
			}
			else
			{
				ExTraceGlobals.SubscriptionTracer.TraceDebug<string>(0L, "EnsureDefaultInbound: {0} Found", text);
				if (smtpSendConnectorConfig.SourceTransportServers != null)
				{
					foreach (ADObjectId adobjectId in smtpSendConnectorConfig.SourceTransportServers)
					{
						if (adobjectId.Name != server.Id.Name)
						{
							multiValuedProperty.Add(adobjectId);
						}
					}
				}
				smtpSendConnectorConfig.SourceTransportServers = multiValuedProperty;
			}
			this.configurationSession.Save(smtpSendConnectorConfig);
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x00174938 File Offset: 0x00172B38
		private void SetDefaultSendConnectorSecurityDescriptor(SmtpSendConnectorConfig connector)
		{
			PrincipalPermissionList defaultPermission = NewSendConnector.GetDefaultPermission(true);
			RawSecurityDescriptor sd = defaultPermission.AddExtendedRightsToSecurityDescriptor(this.configurationSession.ReadSecurityDescriptor(connector.Id));
			this.configurationSession.SaveSecurityDescriptor(connector.OriginalId, sd);
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x00174978 File Offset: 0x00172B78
		private void LoadCertificate()
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			try
			{
				if (this.serverObject.InternalTransportCertificate == null)
				{
					ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "failed to find TLS certificate on local server {0}", this.serverObject.Name);
					base.WriteError(new InvalidOperationException(Strings.ErrorFindCertificateOnServerObject), ErrorCategory.ObjectNotFound, this.serverObject.Name);
				}
				X509Certificate2 x509Certificate = null;
				try
				{
					x509Certificate = new X509Certificate2(this.serverObject.InternalTransportCertificate);
				}
				catch (CryptographicException)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorFindCertificateOnServerObject), ErrorCategory.ObjectNotFound, this.serverObject.Name);
				}
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, x509Certificate.Thumbprint, false);
				if (x509Certificate2Collection.Count <= 0)
				{
					ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "failed to find TLS certificate for Thumbprint {0}", x509Certificate.Thumbprint);
					base.WriteError(new InvalidOperationException(Strings.ErrorLoadCertificateOnEdge), ErrorCategory.ObjectNotFound, this.serverObject.Fqdn);
					return;
				}
				this.tlsCertificate = x509Certificate2Collection[0];
				if (TlsCertificateInfo.IsCNGProvider(this.tlsCertificate))
				{
					ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "Edge Subscription only supports CAPI certificate in EdgeSubscription. Current certificate for the Edge server with thumbprint {0} is not a CAPI certificate.", x509Certificate.Thumbprint);
					base.WriteError(new InvalidOperationException(Strings.EdgeSubscriptionRequiresCAPICert(x509Certificate.Thumbprint)), ErrorCategory.ObjectNotFound, this.serverObject.Fqdn);
					return;
				}
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
				return;
			}
			finally
			{
				x509Store.Close();
			}
			this.InstallCertificateToADAMServiceStore();
			this.edgeSubscriptionData.EdgeCertificateBlob = this.tlsCertificate.Export(X509ContentType.SerializedCert);
		}

		// Token: 0x040032E9 RID: 13033
		internal const string ParamCreateInternetSendConnector = "CreateInternetSendConnector";

		// Token: 0x040032EA RID: 13034
		internal const string ParamCreateInboundSendConnector = "CreateInboundSendConnector";

		// Token: 0x040032EB RID: 13035
		internal const int MaxADSiteNameLength = 41;

		// Token: 0x040032EC RID: 13036
		private static readonly TimeSpan DefaultBootStrapEdgeSyncAccountExpiry = TimeSpan.FromDays(1.0);

		// Token: 0x040032ED RID: 13037
		private static readonly TimeSpan DefaultEdgeSyncAccountExpiry = TimeSpan.FromDays(15.0);

		// Token: 0x040032EE RID: 13038
		private static readonly TimeSpan MinimumAccountExpiry = TimeSpan.FromMinutes(2.0);

		// Token: 0x040032EF RID: 13039
		private TimeSpan accountExpiryDuration = TimeSpan.MinValue;

		// Token: 0x040032F0 RID: 13040
		private RunningLocation runningLocation;

		// Token: 0x040032F1 RID: 13041
		private EdgeSubscriptionData edgeSubscriptionData;

		// Token: 0x040032F2 RID: 13042
		private LongPath filename;

		// Token: 0x040032F3 RID: 13043
		private AdSiteIdParameter site;

		// Token: 0x040032F4 RID: 13044
		private ADObjectId siteId;

		// Token: 0x040032F5 RID: 13045
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x040032F6 RID: 13046
		private Server serverObject;

		// Token: 0x040032F7 RID: 13047
		private X509Certificate2 tlsCertificate;

		// Token: 0x040032F8 RID: 13048
		private EdgeSubscriptionDataSerializer serializer;

		// Token: 0x040032F9 RID: 13049
		private SwitchParameter force;

		// Token: 0x040032FA RID: 13050
		private ServerVersion defaultEdgeServerVersion;
	}
}
