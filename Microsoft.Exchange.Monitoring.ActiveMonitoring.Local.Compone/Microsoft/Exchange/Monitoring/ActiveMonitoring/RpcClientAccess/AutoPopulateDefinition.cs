using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x020001F0 RID: 496
	internal class AutoPopulateDefinition
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0005D981 File Offset: 0x0005BB81
		internal AutoPopulateDefinition(ProbeType probeType, ProbeDefinition definition)
		{
			this.probeType = probeType;
			this.probeDefinition = definition;
			this.targetResource = definition.TargetResource;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0005D9D0 File Offset: 0x0005BBD0
		private MailboxDatabaseInfo MailboxDatabaseInfo
		{
			get
			{
				if (this.mailboxDatabaseInfo == null)
				{
					ICollection<MailboxDatabaseInfo> source;
					if (this.IsProbeOnCafe)
					{
						source = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
					}
					else
					{
						source = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
					}
					if (!string.IsNullOrEmpty(this.targetResource))
					{
						this.mailboxDatabaseInfo = (from dbInfo in source
						where dbInfo.MailboxDatabaseName == this.targetResource
						select dbInfo).First<MailboxDatabaseInfo>();
					}
					else
					{
						this.mailboxDatabaseInfo = (from dbInfo in source
						where !string.IsNullOrEmpty(dbInfo.MonitoringAccount)
						select dbInfo into db
						orderby db.MonitoringAccount
						select db).First<MailboxDatabaseInfo>();
					}
				}
				return this.mailboxDatabaseInfo;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0005DA9B File Offset: 0x0005BC9B
		private bool IsProbeOnCafe
		{
			get
			{
				return this.probeType == ProbeType.Ctp;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0005DAA8 File Offset: 0x0005BCA8
		private bool IsPasswordRequired
		{
			get
			{
				return this.IsProbeOnCafe || VariantConfiguration.InvariantNoFlightingSnapshot.Global.DistributedKeyManagement.Enabled;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0005DADC File Offset: 0x0005BCDC
		internal void ValidateAndAutoFill(Dictionary<string, string> overridesPropertyBag)
		{
			if (overridesPropertyBag.ContainsKey("Endpoint"))
			{
				this.probeDefinition.Endpoint = overridesPropertyBag["Endpoint"];
			}
			else
			{
				this.probeDefinition.Endpoint = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			}
			if (overridesPropertyBag.ContainsKey("ExtensionAttributes"))
			{
				Dictionary<string, string> dictionary = DefinitionHelperBase.ConvertExtensionAttributesToDictionary(overridesPropertyBag["ExtensionAttributes"]);
				if (dictionary != null)
				{
					foreach (string key in AutoPopulateDefinition.ExtensionAttributeNames)
					{
						if (dictionary.ContainsKey(key))
						{
							this.probeDefinition.Attributes[key] = dictionary[key];
						}
					}
				}
			}
			if (overridesPropertyBag.ContainsKey("Account"))
			{
				string text = overridesPropertyBag["Account"];
				if (!this.IsPasswordRequired)
				{
					this.probeDefinition.SetAccountCommonAccessToken(text);
				}
				else
				{
					this.probeDefinition.Account = text;
				}
				if (overridesPropertyBag.ContainsKey("Password"))
				{
					this.probeDefinition.AccountPassword = overridesPropertyBag["Password"];
				}
				if (this.IsPasswordRequired)
				{
					if (!overridesPropertyBag.ContainsKey("Password"))
					{
						throw new LocalizedException(Strings.InputPasswordRequired);
					}
				}
				else if (overridesPropertyBag.ContainsKey("Password"))
				{
					throw new LocalizedException(Strings.InputPasswordNotRequired);
				}
				if (overridesPropertyBag.ContainsKey("AccountDisplayName"))
				{
					this.probeDefinition.AccountDisplayName = overridesPropertyBag["AccountDisplayName"];
				}
			}
			else
			{
				if (!this.probeDefinition.Attributes.ContainsKey("AccountLegacyDN"))
				{
					this.probeDefinition.Attributes.Add("AccountLegacyDN", this.MailboxDatabaseInfo.MonitoringAccountLegacyDN);
				}
				if (!this.probeDefinition.Attributes.ContainsKey("PersonalizedServerName"))
				{
					this.probeDefinition.Attributes.Add("PersonalizedServerName", string.Format("{0}@{1}", this.MailboxDatabaseInfo.MonitoringAccountMailboxGuid, this.MailboxDatabaseInfo.MonitoringAccountDomain));
				}
				if (!this.probeDefinition.Attributes.ContainsKey("AccountDisplayName"))
				{
					this.probeDefinition.Attributes.Add("AccountDisplayName", this.MailboxDatabaseInfo.MonitoringAccount);
				}
				if (this.IsPasswordRequired)
				{
					this.probeDefinition.AuthenticateAsUser(this.MailboxDatabaseInfo);
				}
				else
				{
					this.probeDefinition.AuthenticateAsCafeServer(this.MailboxDatabaseInfo);
				}
			}
			if (overridesPropertyBag.ContainsKey("SecondaryEndpoint"))
			{
				this.probeDefinition.SecondaryEndpoint = overridesPropertyBag["SecondaryEndpoint"];
			}
			else if (!this.IsProbeOnCafe)
			{
				this.probeDefinition.SecondaryEndpoint = this.probeDefinition.Endpoint;
			}
			else if (this.probeDefinition.Attributes.ContainsKey("PersonalizedServerName"))
			{
				this.probeDefinition.SecondaryEndpoint = this.probeDefinition.Attributes["PersonalizedServerName"];
			}
			if (overridesPropertyBag.ContainsKey("TimeoutSeconds"))
			{
				int timeoutSeconds;
				if (int.TryParse(overridesPropertyBag["TimeoutSeconds"], out timeoutSeconds))
				{
					this.probeDefinition.TimeoutSeconds = timeoutSeconds;
					return;
				}
			}
			else
			{
				this.probeDefinition.MakeTemplateForOnDemandExecution();
				this.probeDefinition.Enabled = true;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0005DDF8 File Offset: 0x0005BFF8
		private static string GetSerializedAccessToken(string accountName)
		{
			string result;
			using (WindowsIdentity windowsIdentity = new WindowsIdentity(accountName))
			{
				using (ClientSecurityContext clientSecurityContext = windowsIdentity.CreateClientSecurityContext(true))
				{
					SerializedAccessToken serializedAccessToken = new SerializedAccessToken(windowsIdentity.GetSafeName(true), windowsIdentity.AuthenticationType, clientSecurityContext);
					result = serializedAccessToken.ToString();
				}
			}
			return result;
		}

		// Token: 0x04000A5A RID: 2650
		private readonly string targetResource;

		// Token: 0x04000A5B RID: 2651
		private MailboxDatabaseInfo mailboxDatabaseInfo;

		// Token: 0x04000A5C RID: 2652
		private ProbeType probeType;

		// Token: 0x04000A5D RID: 2653
		private ProbeDefinition probeDefinition;

		// Token: 0x04000A5E RID: 2654
		private static string[] ExtensionAttributeNames = new string[]
		{
			"AccountDisplayName",
			"AccountLegacyDN",
			"MailboxLegacyDN",
			"PersonalizedServerName",
			"RpcProxyPort",
			"RpcProxyAuthenticationType",
			"RpcAuthenticationType"
		};
	}
}
