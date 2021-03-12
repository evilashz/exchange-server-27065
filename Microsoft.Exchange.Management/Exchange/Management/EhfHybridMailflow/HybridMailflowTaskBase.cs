using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000873 RID: 2163
	public abstract class HybridMailflowTaskBase : Task
	{
		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06004AED RID: 19181 RVA: 0x00136B57 File Offset: 0x00134D57
		// (set) Token: 0x06004AEE RID: 19182 RVA: 0x00136B6E File Offset: 0x00134D6E
		[Parameter(Position = 0, Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00136B81 File Offset: 0x00134D81
		private void WriteErrorAndMonitoringEvent(Exception exception, ExchangeErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, (ErrorCategory)errorCategory, target);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00136BAC File Offset: 0x00134DAC
		private Runspace GetConnectorPowershellRunspace()
		{
			RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
			PSSnapInException ex = null;
			runspaceConfiguration.AddPSSnapIn("Microsoft.Exchange.Management.PowerShell.E2010", out ex);
			if (ex != null)
			{
				throw ex;
			}
			return RunspaceFactory.CreateRunspace(runspaceConfiguration);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00136BE0 File Offset: 0x00134DE0
		private void AppendCommonParameters(ref List<CommandParameter> parameters)
		{
			Dictionary<string, object> boundParameters = base.MyInvocation.BoundParameters;
			string[] array = new string[]
			{
				"Verbose",
				"Debug",
				"ErrorAction",
				"WarningAction",
				"ErrorVariable",
				"WarningVariable",
				"OutVariable",
				"OutBuffer"
			};
			foreach (string text in array)
			{
				object value = null;
				if (boundParameters.TryGetValue(text, out value))
				{
					parameters.Add(new CommandParameter(text, value));
				}
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x00136C83 File Offset: 0x00134E83
		protected TenantInboundConnector OriginalInboundConnector
		{
			get
			{
				if (this.originalInboundConnector == null)
				{
					this.originalInboundConnector = this.GetHybridMailflowInboundConnector();
				}
				return this.originalInboundConnector;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x00136C9F File Offset: 0x00134E9F
		protected TenantOutboundConnector OriginalOutboundConnector
		{
			get
			{
				if (this.originalOutboundConnector == null)
				{
					this.originalOutboundConnector = this.GetHybridMailflowOutboundConnector();
				}
				return this.originalOutboundConnector;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x00136CBC File Offset: 0x00134EBC
		protected string OrganizationName
		{
			get
			{
				if (this.organizationName == null)
				{
					if (this.Organization != null)
					{
						this.organizationName = this.Organization.ToString();
					}
					else if (base.ExecutingUserOrganizationId != null && base.ExecutingUserOrganizationId.ConfigurationUnit != null)
					{
						this.organizationName = base.ExecutingUserOrganizationId.ConfigurationUnit.ToString();
					}
					else
					{
						base.WriteError(new ApplicationException(Strings.HybridMailflowNoOrganizationError), ErrorCategory.InvalidData, null);
					}
				}
				return this.organizationName;
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00136D3C File Offset: 0x00134F3C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00136D57 File Offset: 0x00134F57
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00136D60 File Offset: 0x00134F60
		protected static HybridMailflowConfiguration ConvertToHybridMailflowConfiguration(TenantInboundConnector inbound, TenantOutboundConnector outbound)
		{
			ArgumentValidator.ThrowIfNull("inbound", inbound);
			ArgumentValidator.ThrowIfNull("outbound", outbound);
			HybridMailflowConfiguration hybridMailflowConfiguration = new HybridMailflowConfiguration();
			hybridMailflowConfiguration.OutboundDomains = new List<SmtpDomainWithSubdomains>();
			foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in outbound.RecipientDomains)
			{
				if (!HybridMailflowTaskBase.IsWildcardDomain(smtpDomainWithSubdomains))
				{
					hybridMailflowConfiguration.OutboundDomains.Add(new SmtpDomainWithSubdomains(smtpDomainWithSubdomains.Address));
				}
			}
			if (inbound.SenderIPAddresses != null)
			{
				hybridMailflowConfiguration.InboundIPs = new List<IPRange>();
				foreach (IPRange item in inbound.SenderIPAddresses)
				{
					hybridMailflowConfiguration.InboundIPs.Add(item);
				}
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(outbound.SmartHosts))
			{
				SmartHost smartHost = outbound.SmartHosts[0];
				if (smartHost.IsIPAddress)
				{
					hybridMailflowConfiguration.OnPremisesFQDN = new Fqdn(smartHost.Address.ToString());
				}
				else
				{
					hybridMailflowConfiguration.OnPremisesFQDN = new Fqdn(smartHost.ToString());
				}
			}
			if (inbound.TlsSenderCertificateName != null)
			{
				hybridMailflowConfiguration.CertificateSubject = inbound.TlsSenderCertificateName.ToString();
			}
			hybridMailflowConfiguration.SecureMailEnabled = new bool?(inbound.RequireTls && outbound.TlsSettings != null && outbound.TlsSettings == TlsAuthLevel.DomainValidation);
			hybridMailflowConfiguration.CentralizedTransportEnabled = new bool?((inbound.RestrictDomainsToIPAddresses && HybridMailflowTaskBase.IsRecipientDomainsWildcard(outbound.RecipientDomains)) || outbound.RouteAllMessagesViaOnPremises);
			return hybridMailflowConfiguration;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00136F24 File Offset: 0x00135124
		protected static bool IsRecipientDomainsWildcard(IEnumerable<SmtpDomainWithSubdomains> domains)
		{
			if (domains != null)
			{
				foreach (SmtpDomainWithSubdomains domain in domains)
				{
					if (HybridMailflowTaskBase.IsWildcardDomain(domain))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00136F78 File Offset: 0x00135178
		protected static bool IsWildcardDomain(SmtpDomainWithSubdomains domain)
		{
			return domain != null && domain.Equals(SmtpDomainWithSubdomains.StarDomain);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00136F8C File Offset: 0x0013518C
		protected static List<SmtpDomainWithSubdomains> GetRecipientDomains(TenantOutboundConnector outboundConnector, bool addWildcard)
		{
			IEnumerable<SmtpDomainWithSubdomains> domains = null;
			if (outboundConnector != null && outboundConnector.RecipientDomains != null)
			{
				domains = outboundConnector.RecipientDomains;
			}
			return HybridMailflowTaskBase.GetRecipientDomains(domains, addWildcard);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00136FB4 File Offset: 0x001351B4
		protected static List<SmtpDomainWithSubdomains> GetRecipientDomains(IEnumerable<SmtpDomainWithSubdomains> domains, bool addWildcard)
		{
			List<SmtpDomainWithSubdomains> result = new List<SmtpDomainWithSubdomains>();
			if (addWildcard)
			{
				HybridMailflowTaskBase.AddRecipientDomain(ref result, SmtpDomainWithSubdomains.StarDomain);
			}
			if (domains != null)
			{
				foreach (SmtpDomainWithSubdomains domain in domains)
				{
					if (!HybridMailflowTaskBase.IsWildcardDomain(domain))
					{
						HybridMailflowTaskBase.AddRecipientDomain(ref result, domain);
					}
				}
			}
			return result;
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00137020 File Offset: 0x00135220
		protected static void AddRecipientDomain(ref List<SmtpDomainWithSubdomains> recipientDomains, SmtpDomainWithSubdomains domain)
		{
			if (!recipientDomains.Contains(domain))
			{
				recipientDomains.Add(domain);
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00137034 File Offset: 0x00135234
		protected TenantInboundConnector GetHybridMailflowInboundConnector()
		{
			Collection<PSObject> collection = this.InvokeConnectorCmdletWithOrganization("Get-InboundConnector", "Hybrid Mail Flow Inbound Connector", true);
			TenantInboundConnector result = null;
			if (collection != null && collection.Count > 0)
			{
				foreach (PSObject psobject in collection)
				{
					if (psobject.BaseObject is TenantInboundConnector)
					{
						TenantInboundConnector tenantInboundConnector = psobject.BaseObject as TenantInboundConnector;
						if (tenantInboundConnector.ConnectorType == TenantConnectorType.OnPremises)
						{
							result = tenantInboundConnector;
							break;
						}
						this.WriteWarning(Strings.HybridMailflowConnectorNameAndTypeWarning("Hybrid Mail Flow Inbound Connector"));
					}
					else
					{
						base.WriteError(new ApplicationException(Strings.HybridMailflowUnexpectedType("Get-InboundConnector")), ErrorCategory.InvalidData, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x001370F4 File Offset: 0x001352F4
		protected TenantOutboundConnector GetHybridMailflowOutboundConnector()
		{
			Collection<PSObject> collection = this.InvokeConnectorCmdletWithOrganization("Get-OutboundConnector", "Hybrid Mail Flow Outbound Connector", true);
			TenantOutboundConnector result = null;
			if (collection != null && collection.Count > 0)
			{
				foreach (PSObject psobject in collection)
				{
					if (psobject.BaseObject is TenantOutboundConnector)
					{
						TenantOutboundConnector tenantOutboundConnector = psobject.BaseObject as TenantOutboundConnector;
						if (tenantOutboundConnector.ConnectorType == TenantConnectorType.OnPremises)
						{
							result = tenantOutboundConnector;
							break;
						}
						this.WriteWarning(Strings.HybridMailflowConnectorNameAndTypeWarning("Hybrid Mail Flow Outbound Connector"));
					}
					else
					{
						base.WriteError(new ApplicationException(Strings.HybridMailflowUnexpectedType("Get-OutboundConnector")), ErrorCategory.InvalidData, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x001371B4 File Offset: 0x001353B4
		protected Collection<PSObject> InvokeConnectorCmdlet(string CmdletName, string Identity)
		{
			return this.InvokeConnectorCmdlet(CmdletName, Identity, false);
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x001371C0 File Offset: 0x001353C0
		protected Collection<PSObject> InvokeConnectorCmdlet(string CmdletName, string Identity, bool IgnoreErrors)
		{
			return this.InvokeConnectorCmdlet(CmdletName, new List<CommandParameter>
			{
				new CommandParameter("identity", Identity)
			}, IgnoreErrors);
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x001371F0 File Offset: 0x001353F0
		protected Collection<PSObject> InvokeConnectorCmdletWithOrganization(string CmdletName, string Identity, bool IgnoreErrors)
		{
			return this.InvokeConnectorCmdlet(CmdletName, new List<CommandParameter>
			{
				new CommandParameter("identity", Identity),
				new CommandParameter("Organization", this.OrganizationName)
			}, IgnoreErrors);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00137233 File Offset: 0x00135433
		protected Collection<PSObject> InvokeConnectorCmdlet(string CmdletName, List<CommandParameter> Parameters)
		{
			return this.InvokeConnectorCmdlet(CmdletName, Parameters, false);
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00137240 File Offset: 0x00135440
		protected Collection<PSObject> InvokeConnectorCmdlet(string CmdletName, List<CommandParameter> Parameters, bool IgnoreErrors)
		{
			Collection<PSObject> result = null;
			Runspace connectorPowershellRunspace = this.GetConnectorPowershellRunspace();
			connectorPowershellRunspace.Open();
			Pipeline pipeline = connectorPowershellRunspace.CreatePipeline();
			Command command = new Command(CmdletName);
			this.AppendCommonParameters(ref Parameters);
			base.WriteDebug(string.Format("Invoking {0}", CmdletName));
			foreach (CommandParameter commandParameter in Parameters)
			{
				base.WriteDebug(string.Format("  -{0}:{1}", commandParameter.Name, (commandParameter.Value == null) ? "null" : commandParameter.Value.ToString()));
				command.Parameters.Add(commandParameter);
			}
			pipeline.Commands.Add(command);
			try
			{
				result = pipeline.Invoke();
				if (!IgnoreErrors && pipeline.Error.Count > 0)
				{
					while (!pipeline.Error.EndOfPipeline)
					{
						PSObject psobject = pipeline.Error.Read() as PSObject;
						if (psobject != null)
						{
							ErrorRecord errorRecord = psobject.BaseObject as ErrorRecord;
							if (errorRecord != null)
							{
								base.WriteError(errorRecord.Exception, errorRecord.CategoryInfo.Category, errorRecord.TargetObject);
							}
						}
					}
				}
			}
			finally
			{
				if (connectorPowershellRunspace.RunspaceStateInfo.State == RunspaceState.Opened)
				{
					connectorPowershellRunspace.Close();
					connectorPowershellRunspace.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x001373A4 File Offset: 0x001355A4
		protected static LocalizedString HybridMailflowUnexpectedTypeMessage(string CmdletName)
		{
			return Strings.HybridMailflowUnexpectedType(CmdletName);
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x001373AC File Offset: 0x001355AC
		protected LocalizedString NullInboundConnectorMessage
		{
			get
			{
				return Strings.HybridMailflowNullConnector("Get-InboundConnector", "Hybrid Mail Flow Inbound Connector");
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x001373BD File Offset: 0x001355BD
		protected LocalizedString NullOutboundConnectorMessage
		{
			get
			{
				return Strings.HybridMailflowNullConnector("Get-OutboundConnector", "Hybrid Mail Flow Outbound Connector");
			}
		}

		// Token: 0x04002D01 RID: 11521
		private const string CmdletNoun = "HybridMailflow";

		// Token: 0x04002D02 RID: 11522
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring HybridMailflow";

		// Token: 0x04002D03 RID: 11523
		private MonitoringData monitoringData = new MonitoringData();

		// Token: 0x04002D04 RID: 11524
		private TenantInboundConnector originalInboundConnector;

		// Token: 0x04002D05 RID: 11525
		private TenantOutboundConnector originalOutboundConnector;

		// Token: 0x04002D06 RID: 11526
		private string organizationName;
	}
}
