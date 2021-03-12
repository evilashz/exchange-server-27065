using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B51 RID: 2897
	[Cmdlet("Set", "SendConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSendConnector : SetTopologySystemConfigurationObjectTask<SendConnectorIdParameter, SmtpSendConnectorConfig>
	{
		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x0600691B RID: 26907 RVA: 0x001B12BB File Offset: 0x001AF4BB
		// (set) Token: 0x0600691C RID: 26908 RVA: 0x001B12C3 File Offset: 0x001AF4C3
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

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x0600691D RID: 26909 RVA: 0x001B12CC File Offset: 0x001AF4CC
		// (set) Token: 0x0600691E RID: 26910 RVA: 0x001B12E3 File Offset: 0x001AF4E3
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

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x0600691F RID: 26911 RVA: 0x001B12F6 File Offset: 0x001AF4F6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSendConnector(this.Identity.ToString());
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x001B1308 File Offset: 0x001AF508
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
			if (base.Fields.IsModified("SourceTransportServers"))
			{
				if (this.SourceTransportServers != null)
				{
					smtpSendConnectorConfig.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, null);
					if (smtpSendConnectorConfig.SourceTransportServers.Count > 0)
					{
						ManageSendConnectors.SetConnectorHomeMta(smtpSendConnectorConfig, (IConfigurationSession)base.DataSession);
					}
				}
				else
				{
					smtpSendConnectorConfig.SourceTransportServers = null;
				}
			}
			return smtpSendConnectorConfig;
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x001B13D8 File Offset: 0x001AF5D8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			SmtpSendConnectorConfig smtpSendConnectorConfig = dataObject as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig != null)
			{
				smtpSendConnectorConfig.DNSRoutingEnabled = string.IsNullOrEmpty(smtpSendConnectorConfig.SmartHostsString);
				smtpSendConnectorConfig.IsScopedConnector = smtpSendConnectorConfig.GetScopedConnector();
				smtpSendConnectorConfig.ResetChangeTracking();
				this.dashDashConnector = smtpSendConnectorConfig.IsInitialSendConnector();
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x001B1428 File Offset: 0x001AF628
		protected override void InternalProcessRecord()
		{
			if (this.dashDashConnector && (this.DataObject.IsChanged(MailGatewaySchema.AddressSpaces) || this.DataObject.IsChanged(SmtpSendConnectorConfigSchema.SmartHostsString)) && !this.force && !base.ShouldContinue(Strings.ConfirmationMessageSetEdgeSyncSendConnectorAddressSpaceOrSmartHosts))
			{
				return;
			}
			Exception ex = null;
			if (this.DataObject.DNSRoutingEnabled)
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
			if (this.DataObject.TlsCertificateName != null)
			{
				ex = this.ValidateCertificateForSmtp(this.DataObject);
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject.Identity);
				return;
			}
			NewSendConnector.ClearSmartHostsListIfNecessary(this.DataObject);
			NewSendConnector.SetSmartHostAuthMechanismIfNecessary(this.DataObject);
			ManageSendConnectors.AdjustAddressSpaces(this.DataObject);
			base.InternalProcessRecord();
			if (!TopologyProvider.IsAdamTopology())
			{
				ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.ThrowTerminatingError));
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x001B153C File Offset: 0x001AF73C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (Server.IsSubscribedGateway((ITopologyConfigurationSession)base.DataSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
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

		// Token: 0x06006924 RID: 26916 RVA: 0x001B15B8 File Offset: 0x001AF7B8
		private Exception ValidateCertificateForSmtp(SmtpSendConnectorConfig sendConnector)
		{
			SmtpX509Identifier tlsCertificateName = sendConnector.TlsCertificateName;
			if (sendConnector.SourceTransportServers.Count > 0)
			{
				ADObjectId adobjectId = sendConnector.SourceTransportServers[0];
				ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
				ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
				byte[] outputBlob = null;
				try
				{
					byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
					ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(adobjectId.Name);
					outputBlob = exchangeCertificateRpcClient.GetCertificate2(0, inBlob);
					exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version2;
				}
				catch (RpcException)
				{
					exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
				}
				if (exchangeCertificateRpcVersion == ExchangeCertificateRpcVersion.Version1 && adobjectId.Name != null && adobjectId.DomainId != null && !string.IsNullOrEmpty(adobjectId.DistinguishedName))
				{
					try
					{
						byte[] inBlob2 = exchangeCertificateRpc.SerializeInputParameters(exchangeCertificateRpcVersion);
						ExchangeCertificateRpcClient exchangeCertificateRpcClient2 = new ExchangeCertificateRpcClient(adobjectId.Name);
						outputBlob = exchangeCertificateRpcClient2.GetCertificate(0, inBlob2);
					}
					catch (RpcException)
					{
						return null;
					}
				}
				ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
				foreach (ExchangeCertificate exchangeCertificate in exchangeCertificateRpc2.ReturnCertList)
				{
					if (exchangeCertificate.Issuer.Equals(tlsCertificateName.CertificateIssuer) && exchangeCertificate.Subject.Equals(tlsCertificateName.CertificateSubject) && (exchangeCertificate.Services & AllowedServices.SMTP) != AllowedServices.SMTP)
					{
						return new InvalidOperationException(Strings.SMTPNotEnabledForTlsCertificate);
					}
				}
			}
			return null;
		}

		// Token: 0x040036A5 RID: 13989
		private Server localServer;

		// Token: 0x040036A6 RID: 13990
		private SwitchParameter force;

		// Token: 0x040036A7 RID: 13991
		private bool dashDashConnector;
	}
}
