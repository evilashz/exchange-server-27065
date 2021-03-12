using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x0200087B RID: 2171
	[Cmdlet("Set", "HybridMailflow", SupportsShouldProcess = true)]
	public sealed class SetHybridMailflow : HybridMailflowTaskBase
	{
		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x001377CF File Offset: 0x001359CF
		// (set) Token: 0x06004B2C RID: 19244 RVA: 0x001377E6 File Offset: 0x001359E6
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public SmtpDomainWithSubdomains[] OutboundDomains
		{
			get
			{
				return (SmtpDomainWithSubdomains[])base.Fields["OutboundDomains"];
			}
			set
			{
				base.Fields["OutboundDomains"] = value;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x001377F9 File Offset: 0x001359F9
		// (set) Token: 0x06004B2E RID: 19246 RVA: 0x00137810 File Offset: 0x00135A10
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public IPRange[] InboundIPs
		{
			get
			{
				return (IPRange[])base.Fields["InboundIPs"];
			}
			set
			{
				base.Fields["InboundIPs"] = value;
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00137823 File Offset: 0x00135A23
		// (set) Token: 0x06004B30 RID: 19248 RVA: 0x0013783A File Offset: 0x00135A3A
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Fqdn OnPremisesFQDN
		{
			get
			{
				return (Fqdn)base.Fields["OnPremisesFQDN"];
			}
			set
			{
				base.Fields["OnPremisesFQDN"] = value;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x0013784D File Offset: 0x00135A4D
		// (set) Token: 0x06004B32 RID: 19250 RVA: 0x00137864 File Offset: 0x00135A64
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string CertificateSubject
		{
			get
			{
				return (string)base.Fields["CertificateSubject"];
			}
			set
			{
				base.Fields["CertificateSubject"] = value;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x00137877 File Offset: 0x00135A77
		// (set) Token: 0x06004B34 RID: 19252 RVA: 0x0013788E File Offset: 0x00135A8E
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public bool? SecureMailEnabled
		{
			get
			{
				return (bool?)base.Fields["SecureMailEnabled"];
			}
			set
			{
				base.Fields["SecureMailEnabled"] = value;
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06004B35 RID: 19253 RVA: 0x001378A6 File Offset: 0x00135AA6
		// (set) Token: 0x06004B36 RID: 19254 RVA: 0x001378BD File Offset: 0x00135ABD
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public bool? CentralizedTransportEnabled
		{
			get
			{
				return (bool?)base.Fields["CentralizedTransportEnabled"];
			}
			set
			{
				base.Fields["CentralizedTransportEnabled"] = value;
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x001378D5 File Offset: 0x00135AD5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetHybridMailflow;
			}
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x001378DC File Offset: 0x00135ADC
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x001378E4 File Offset: 0x00135AE4
		protected override void InternalProcessRecord()
		{
			this.SetHybridMailflowSettingsWithConnectors();
			base.InternalProcessRecord();
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x001378F4 File Offset: 0x00135AF4
		private void SetHybridMailflowSettingsWithConnectors()
		{
			bool flag = null == base.OriginalInboundConnector;
			List<CommandParameter> inboundConnectorParameters = this.GetInboundConnectorParameters(flag);
			if (inboundConnectorParameters.Count > 1)
			{
				base.InvokeConnectorCmdlet(flag ? "New-InboundConnector" : "Set-InboundConnector", inboundConnectorParameters);
			}
			bool flag2 = null == base.OriginalOutboundConnector;
			List<CommandParameter> outboundConnectorParameters = this.GetOutboundConnectorParameters(flag2);
			if (outboundConnectorParameters.Count > 1)
			{
				base.InvokeConnectorCmdlet(flag2 ? "New-OutboundConnector" : "Set-OutboundConnector", outboundConnectorParameters);
			}
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00137968 File Offset: 0x00135B68
		private List<CommandParameter> GetInboundConnectorParameters(bool isNew)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			if (this.InboundIPs != null)
			{
				List<string> list2 = new List<string>();
				foreach (IPRange iprange in this.InboundIPs)
				{
					list2.Add(iprange.ToString());
				}
				list.Add(new CommandParameter("SenderIPAddresses", list2));
			}
			if (this.CertificateSubject != null)
			{
				list.Add(new CommandParameter("TlsSenderCertificateName", this.CertificateSubject));
			}
			if (this.SecureMailEnabled != null)
			{
				list.Add(new CommandParameter("RequireTls", this.SecureMailEnabled.Value));
			}
			if (isNew)
			{
				if (base.OrganizationName != null)
				{
					list.Add(new CommandParameter("Organization", base.OrganizationName));
				}
				list.Add(new CommandParameter("Name", "Hybrid Mail Flow Inbound Connector"));
				list.Add(new CommandParameter("Comment", Strings.HybridMailflowInboundConnectorComment));
				list.Add(new CommandParameter("SenderDomains", "*"));
				list.Add(new CommandParameter("Enabled", true));
				list.Add(new CommandParameter("ConnectorType", "OnPremises"));
				list.Add(new CommandParameter("ConnectorSource", "HybridWizard"));
				list.Add(new CommandParameter("CloudServicesMailEnabled", true));
			}
			else
			{
				list.Add(new CommandParameter("Identity", base.OriginalInboundConnector.Identity));
			}
			return list;
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00137AF4 File Offset: 0x00135CF4
		private List<CommandParameter> GetOutboundConnectorParameters(bool isNew)
		{
			List<CommandParameter> list = new List<CommandParameter>();
			if (this.CentralizedTransportEnabled != null || this.OutboundDomains != null)
			{
				bool addWildcard = false;
				if (this.CentralizedTransportEnabled != null)
				{
					addWildcard = this.CentralizedTransportEnabled.Value;
				}
				else if (base.OriginalInboundConnector != null && base.OriginalOutboundConnector != null)
				{
					HybridMailflowConfiguration hybridMailflowConfiguration = HybridMailflowTaskBase.ConvertToHybridMailflowConfiguration(base.OriginalInboundConnector, base.OriginalOutboundConnector);
					addWildcard = (hybridMailflowConfiguration.CentralizedTransportEnabled != null && hybridMailflowConfiguration.CentralizedTransportEnabled.Value);
				}
				if (this.OutboundDomains != null && HybridMailflowTaskBase.IsRecipientDomainsWildcard(this.OutboundDomains))
				{
					addWildcard = true;
				}
				List<SmtpDomainWithSubdomains> value = (this.OutboundDomains != null) ? HybridMailflowTaskBase.GetRecipientDomains(this.OutboundDomains, addWildcard) : HybridMailflowTaskBase.GetRecipientDomains(base.OriginalOutboundConnector, addWildcard);
				list.Add(new CommandParameter("RecipientDomains", value));
			}
			if (this.CentralizedTransportEnabled != null && this.CentralizedTransportEnabled.Value)
			{
				list.Add(new CommandParameter("RouteAllMessagesViaOnPremises", this.CentralizedTransportEnabled.Value));
			}
			if (this.OnPremisesFQDN != null)
			{
				list.Add(new CommandParameter("SmartHosts", this.OnPremisesFQDN.ToString()));
				list.Add(new CommandParameter("UseMxRecord", false));
			}
			if (this.CertificateSubject != null)
			{
				list.Add(new CommandParameter("TlsDomain", this.CertificateSubject));
			}
			if (this.SecureMailEnabled != null)
			{
				list.Add(new CommandParameter("TlsSettings", this.SecureMailEnabled.Value ? new TlsAuthLevel?(TlsAuthLevel.DomainValidation) : null));
			}
			if (isNew)
			{
				if (base.OrganizationName != null)
				{
					list.Add(new CommandParameter("Organization", base.OrganizationName));
				}
				list.Add(new CommandParameter("Name", "Hybrid Mail Flow Outbound Connector"));
				list.Add(new CommandParameter("Comment", Strings.HybridMailflowOutboundConnectorComment));
				list.Add(new CommandParameter("Enabled", true));
				list.Add(new CommandParameter("ConnectorType", "OnPremises"));
				list.Add(new CommandParameter("ConnectorSource", "HybridWizard"));
				list.Add(new CommandParameter("CloudServicesMailEnabled", true));
			}
			else
			{
				list.Add(new CommandParameter("Identity", base.OriginalOutboundConnector.Identity));
			}
			return list;
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00137D88 File Offset: 0x00135F88
		private static string[] ConvertToStringArray<T>(T[] taskParameter)
		{
			string[] array = null;
			if (taskParameter != null)
			{
				array = new string[taskParameter.Length];
				int num = 0;
				foreach (T t in taskParameter)
				{
					array[num++] = t.ToString();
				}
			}
			return array;
		}
	}
}
