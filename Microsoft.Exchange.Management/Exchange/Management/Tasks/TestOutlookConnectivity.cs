using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005DD RID: 1501
	[Cmdlet("Test", "OutlookConnectivity")]
	public sealed class TestOutlookConnectivity : DataAccessTask<Server>
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000DA338 File Offset: 0x000D8538
		// (set) Token: 0x06003515 RID: 13589 RVA: 0x000DA340 File Offset: 0x000D8540
		[Parameter(Mandatory = false, HelpMessage = "The serverId parameter on which the probe should be executed; default is the local machine.")]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter RunFromServerId { get; set; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000DA349 File Offset: 0x000D8549
		// (set) Token: 0x06003517 RID: 13591 RVA: 0x000DA360 File Offset: 0x000D8560
		[ValidateNotNullOrEmpty]
		[Parameter(Position = 0, Mandatory = true, HelpMessage = "The probe identity to invoke, followed by the optional [\\target resource] for certain probes. Example: 'OutlookMailboxDeepTest\\Mailbox Database XXX'.")]
		public string ProbeIdentity
		{
			get
			{
				return (string)base.Fields["ProbeIdentity"];
			}
			set
			{
				base.Fields["ProbeIdentity"] = value;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000DA373 File Offset: 0x000D8573
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x000DA38A File Offset: 0x000D858A
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The Mailbox Id Parameter for the mailbox you want to logon.")]
		public MailboxIdParameter MailboxId
		{
			get
			{
				return (MailboxIdParameter)base.Fields["MailboxId"];
			}
			set
			{
				base.Fields["MailboxId"] = value;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000DA39D File Offset: 0x000D859D
		// (set) Token: 0x0600351B RID: 13595 RVA: 0x000DA3B4 File Offset: 0x000D85B4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, HelpMessage = "The endpoint you wish to hit.  Default value is the local to the probe's server.")]
		public string Hostname
		{
			get
			{
				return (string)base.Fields["Hostname"];
			}
			set
			{
				base.Fields["Hostname"] = value;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000DA3C7 File Offset: 0x000D85C7
		// (set) Token: 0x0600351D RID: 13597 RVA: 0x000DA3DE File Offset: 0x000D85DE
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string TimeOutSeconds
		{
			get
			{
				return (string)base.Fields["TimeOutSeconds"];
			}
			set
			{
				base.Fields["TimeOutSeconds"] = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000DA3F1 File Offset: 0x000D85F1
		// (set) Token: 0x0600351F RID: 13599 RVA: 0x000DA408 File Offset: 0x000D8608
		[Parameter(Mandatory = false, HelpMessage = "The credential used to authenticate on connect.  Default value is the Monitoring Test Account credentials for that database.")]
		[ValidateNotNullOrEmpty]
		public PSCredential Credential
		{
			get
			{
				return (PSCredential)base.Fields["Credential"];
			}
			set
			{
				base.Fields["Credential"] = value;
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000DA41B File Offset: 0x000D861B
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 215, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestOutlookConnectivity.cs");
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000DA448 File Offset: 0x000D8648
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.RunFromServerId == null)
				{
					this.RunFromServerId = new ServerIdParameter(new Fqdn(ComputerInformation.DnsPhysicalFullyQualifiedDomainName));
				}
				this.server = (Server)base.GetDataObject<Server>(this.RunFromServerId, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.RunFromServerId.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.RunFromServerId.ToString())));
				this.WriteInfo(Strings.RunFromServer(this.RunFromServerId.ToString()));
				if (this.MailboxId != null)
				{
					this.mailboxAdUser = (ADUser)base.GetDataObject<ADUser>(this.MailboxId, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.MailboxId.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.MailboxId.ToString())));
				}
				this.WriteInfo((this.mailboxAdUser == null) ? Strings.UsingTargetMonitoringMailbox : Strings.UsingTargetMailbox(this.mailboxAdUser.PrimarySmtpAddress.ToString()));
				if (!TestOutlookConnectivity.IsMailboxCredentialEmpty(this.Credential))
				{
					if (this.MailboxId == null)
					{
						base.WriteError(new ArgumentException(Strings.MissingMailboxId(this.Credential.UserName)), (ErrorCategory)1000, null);
					}
					this.authenticateAsUser = (ADUser)base.GetDataObject<ADUser>(new RecipientIdParameter(this.Credential.UserName), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.OutlookConnectivityErrorUserNotFound(this.Credential.UserName.ToString())), new LocalizedString?(Strings.OutlookConnectivityErrorUserNotUnique(this.Credential.UserName.ToString())));
				}
				this.WriteInfo((this.Credential != null) ? Strings.UsingAuthenticationCredentials(this.Credential.UserName) : Strings.UsingMonitoringMailboxAuthenticationCredentials);
				string probeIdentity = this.ProbeIdentity;
				ProbeIdentity probeIdentity2 = null;
				try
				{
					probeIdentity2 = OutlookConnectivity.ResolveIdentity(probeIdentity, TestOutlookConnectivity.IsDcOrDedicated);
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, (ErrorCategory)1000, null);
				}
				this.ProbeIdentity = probeIdentity2.GetIdentity(true);
				this.WriteInfo(Strings.UsingProbeIdentity(this.ProbeIdentity));
				if (!TestOutlookConnectivity.IsMailboxCredentialEmpty(this.Credential) && !this.IsProbePasswordAuthenticated)
				{
					this.WriteWarning(Strings.IgnoredAuthenticationWarning);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000DA6C0 File Offset: 0x000D88C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				Dictionary<string, string> dictionary = this.CreatePropertyBag();
				string text = null;
				if (dictionary.TryGetValue("ItemTargetExtension", out text))
				{
					dictionary.Remove("ItemTargetExtension");
				}
				string text2 = string.Empty;
				if (dictionary.Count != 0)
				{
					text2 = CrimsonHelper.ConvertDictionaryToXml(dictionary);
				}
				text = (text ?? string.Empty);
				text2 = (text2 ?? string.Empty);
				RpcInvokeMonitoringProbe.Reply reply = RpcInvokeMonitoringProbe.Invoke(this.server.Fqdn, this.ProbeIdentity, text2, text, 300000);
				if (reply != null)
				{
					if (!string.IsNullOrEmpty(reply.ErrorMessage))
					{
						throw new InvalidOperationException(reply.ErrorMessage);
					}
					MonitoringProbeResult monitoringProbeResult = new MonitoringProbeResult(this.server.Fqdn, reply.ProbeResult);
					this.WriteInfo(Strings.ProbeResult(monitoringProbeResult.ResultType.ToString()));
					if (monitoringProbeResult.ResultType != ResultType.Succeeded)
					{
						string failedProbeResultDetailsString = string.Format("Error: {0}\r\nException: {1}", monitoringProbeResult.Error, monitoringProbeResult.Exception);
						this.WriteWarning(Strings.FailedProbeResultDetails(failedProbeResultDetailsString));
					}
					base.WriteObject(monitoringProbeResult);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000DA7E8 File Offset: 0x000D89E8
		protected override bool IsKnownException(Exception exception)
		{
			return exception is InvalidVersionException || exception is InvalidIdentityException || exception is InvalidDurationException || exception is ActiveMonitoringServerException || exception is ActiveMonitoringServerTransientException || base.IsKnownException(exception);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000DA81B File Offset: 0x000D8A1B
		private void WriteInfo(LocalizedString text)
		{
			base.WriteVerbose(text);
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000DA824 File Offset: 0x000D8A24
		private bool IsCtpTest
		{
			get
			{
				return MonitoringItemIdentity.MonitorIdentityId.GetMonitor(this.ProbeIdentity).IndexOf("ctp", StringComparison.InvariantCultureIgnoreCase) >= 0;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000DA842 File Offset: 0x000D8A42
		private bool IsProbePasswordAuthenticated
		{
			get
			{
				return this.IsCtpTest || TestOutlookConnectivity.IsDcOrDedicated;
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000DA858 File Offset: 0x000D8A58
		private Dictionary<string, string> CreatePropertyBag()
		{
			Dictionary<string, string> dictionary = null;
			try
			{
				dictionary = TestOutlookConnectivity.CreateTestOutlookConnectivityPropertyBag(this.mailboxAdUser, this.IsProbePasswordAuthenticated, this.IsCtpTest, this.Credential, this.authenticateAsUser, this.Hostname);
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "TimeoutSeconds", this.TimeOutSeconds);
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "ServiceName", MonitoringItemIdentity.MonitorIdentityId.GetHealthSet(this.ProbeIdentity));
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "Name", MonitoringItemIdentity.MonitorIdentityId.GetMonitor(this.ProbeIdentity));
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "TargetResource", MonitoringItemIdentity.MonitorIdentityId.GetTargetResource(this.ProbeIdentity));
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, (ErrorCategory)1000, null);
			}
			return dictionary;
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000DA908 File Offset: 0x000D8B08
		private static void AddToPropertyBag(Dictionary<string, string> propertyBag, string key, string value)
		{
			if (value != null && key != null)
			{
				propertyBag.Add(key, value);
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000DA918 File Offset: 0x000D8B18
		internal static Dictionary<string, string> CreateTestOutlookConnectivityPropertyBag(ADUser mailboxAdUser, bool isPasswordAuthenticated, bool isCtpTest, PSCredential mailboxCredential, ADUser adUserCorrespondingToMailboxCredential, string endpoint)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(0);
			AutodiscoverCommonUserSettings autodiscoverCommonUserSettings = null;
			if (mailboxAdUser != null)
			{
				TestOutlookConnectivity.AddToPropertyBag(dictionary2, "MailboxLegacyDN", mailboxAdUser.LegacyExchangeDN);
				autodiscoverCommonUserSettings = AutodiscoverCommonUserSettings.GetSettingsFromRecipient(mailboxAdUser, mailboxAdUser.PrimarySmtpAddress.ToString());
				TestOutlookConnectivity.AddToPropertyBag(dictionary2, "PersonalizedServerName", string.Format("{0}@{1}", autodiscoverCommonUserSettings.MailboxGuid, autodiscoverCommonUserSettings.PrimarySmtpAddress.Domain));
				if (!TestOutlookConnectivity.IsMailboxCredentialEmpty(mailboxCredential))
				{
					if (adUserCorrespondingToMailboxCredential == null)
					{
						throw new ArgumentException("This should never happen.  If mailboxCredential is passed in, then adUserCorrespondingToMailboxCredential should match.");
					}
					TestOutlookConnectivity.AddToPropertyBag(dictionary, "Account", TestOutlookConnectivity.GetAccountLoginName(adUserCorrespondingToMailboxCredential));
					TestOutlookConnectivity.AddToPropertyBag(dictionary2, "AccountLegacyDN", adUserCorrespondingToMailboxCredential.LegacyExchangeDN);
					if (isPasswordAuthenticated)
					{
						string value = TestOutlookConnectivity.ConvertSecureStringToPlainString(mailboxCredential.Password);
						TestOutlookConnectivity.AddToPropertyBag(dictionary, "Password", value);
					}
				}
			}
			TestOutlookConnectivity.AddToPropertyBag(dictionary, "Endpoint", endpoint);
			if (!isCtpTest)
			{
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "SecondaryEndpoint", endpoint);
			}
			if (autodiscoverCommonUserSettings != null && !string.IsNullOrEmpty(autodiscoverCommonUserSettings.RpcServer) && isCtpTest)
			{
				TestOutlookConnectivity.AddToPropertyBag(dictionary, "SecondaryEndpoint", autodiscoverCommonUserSettings.RpcServer);
			}
			TestOutlookConnectivity.AddToPropertyBag(dictionary, "ItemTargetExtension", WorkDefinition.SerializeExtensionAttributes(dictionary2));
			return dictionary;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000DAA40 File Offset: 0x000D8C40
		private static string GetAccountLoginName(ADUser accountAdUser)
		{
			if (Datacenter.IsLiveIDForExchangeLogin(true))
			{
				return accountAdUser.WindowsLiveID.ToString();
			}
			return accountAdUser.UserPrincipalName;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000DAA70 File Offset: 0x000D8C70
		private static bool IsMailboxCredentialEmpty(PSCredential credential)
		{
			return credential == null || string.IsNullOrEmpty(credential.UserName) || credential == PSCredential.Empty;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000DAA8C File Offset: 0x000D8C8C
		private static string ConvertSecureStringToPlainString(SecureString secureString)
		{
			if (secureString == null)
			{
				return null;
			}
			IntPtr intPtr = Marshal.SecureStringToBSTR(secureString);
			string result;
			try
			{
				result = Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeBSTR(intPtr);
			}
			return result;
		}

		// Token: 0x0400248A RID: 9354
		public const string ItemTargetExtensionPropBagEntry = "ItemTargetExtension";

		// Token: 0x0400248B RID: 9355
		public const string TimeoutSecondsPropBagEntry = "TimeoutSeconds";

		// Token: 0x0400248C RID: 9356
		public const string HealthSetServiceNamePropBagEntry = "ServiceName";

		// Token: 0x0400248D RID: 9357
		public const string ProbeNamePropBagEntry = "Name";

		// Token: 0x0400248E RID: 9358
		public const string TargetResourcePropBagEntry = "TargetResource";

		// Token: 0x0400248F RID: 9359
		public const string PasswordPropBagEntry = "Password";

		// Token: 0x04002490 RID: 9360
		public const string AccountPropBagEntry = "Account";

		// Token: 0x04002491 RID: 9361
		public const string AccountLegacyDNAttributePropBagEntry = "AccountLegacyDN";

		// Token: 0x04002492 RID: 9362
		public const string PersonalizedServernameAttributePropBagEntry = "PersonalizedServerName";

		// Token: 0x04002493 RID: 9363
		public const string MailboxLegacyDNAttributePropBagEntry = "MailboxLegacyDN";

		// Token: 0x04002494 RID: 9364
		public const string EndpointPropBagEntry = "Endpoint";

		// Token: 0x04002495 RID: 9365
		public const string SecondaryEndpointPropBagEntry = "SecondaryEndpoint";

		// Token: 0x04002496 RID: 9366
		private Server server;

		// Token: 0x04002497 RID: 9367
		private ADUser mailboxAdUser;

		// Token: 0x04002498 RID: 9368
		private ADUser authenticateAsUser;

		// Token: 0x04002499 RID: 9369
		private static bool IsDcOrDedicated = VariantConfiguration.InvariantNoFlightingSnapshot.Global.DistributedKeyManagement.Enabled;
	}
}
