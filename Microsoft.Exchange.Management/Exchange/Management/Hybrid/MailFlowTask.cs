using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000916 RID: 2326
	internal class MailFlowTask : SessionTask
	{
		// Token: 0x06005293 RID: 21139 RVA: 0x00153F30 File Offset: 0x00152130
		public MailFlowTask() : base(HybridStrings.MailFlowTaskName, 2)
		{
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x00153F43 File Offset: 0x00152143
		private HybridConfiguration HybridConfiguration
		{
			get
			{
				return base.TaskContext.HybridConfigurationObject;
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x00153F50 File Offset: 0x00152150
		private int ServiceInstance
		{
			get
			{
				return this.HybridConfiguration.ServiceInstance;
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x00153F5D File Offset: 0x0015215D
		private SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return this.HybridConfiguration.TlsCertificateName;
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x00153F6A File Offset: 0x0015216A
		private SmtpReceiveDomainCapabilities TlsDomainCapabilities
		{
			get
			{
				return Configuration.TlsDomainCapabilities(this.ServiceInstance);
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x00153F77 File Offset: 0x00152177
		private string TlsCertificateSubjectDomainName
		{
			get
			{
				return TaskCommon.GetDomainFromSubject(this.TlsCertificateName);
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x00153F84 File Offset: 0x00152184
		private string FopeCertificateSubjectDomainName
		{
			get
			{
				return Configuration.FopeCertificateDomain(this.ServiceInstance);
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00153F91 File Offset: 0x00152191
		private MultiValuedProperty<ADObjectId> ReceivingTransportServers
		{
			get
			{
				return this.HybridConfiguration.ReceivingTransportServers;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00153F9E File Offset: 0x0015219E
		private MultiValuedProperty<ADObjectId> SendingTransportServers
		{
			get
			{
				return this.HybridConfiguration.SendingTransportServers;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x00153FAC File Offset: 0x001521AC
		private IEnumerable<ADObjectId> SendingAndReceivingTransportServers
		{
			get
			{
				List<ADObjectId> list = this.SendingTransportServers.ToList<ADObjectId>();
				foreach (ADObjectId item in this.ReceivingTransportServers)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				return list;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x00154018 File Offset: 0x00152218
		private MultiValuedProperty<ADObjectId> EdgeTransportServers
		{
			get
			{
				return this.HybridConfiguration.EdgeTransportServers;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x00154025 File Offset: 0x00152225
		private bool EnableSecureMail
		{
			get
			{
				return this.HybridConfiguration.SecureMailEnabled;
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00154034 File Offset: 0x00152234
		private MultiValuedProperty<SmtpDomain> HybridDomains
		{
			get
			{
				MultiValuedProperty<SmtpDomain> multiValuedProperty = new MultiValuedProperty<SmtpDomain>();
				foreach (AutoDiscoverSmtpDomain item in this.HybridConfiguration.Domains)
				{
					multiValuedProperty.Add(item);
				}
				return multiValuedProperty;
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x00154094 File Offset: 0x00152294
		private string SmartHost
		{
			get
			{
				SmtpDomain onPremisesSmartHost = this.HybridConfiguration.OnPremisesSmartHost;
				if (onPremisesSmartHost != null)
				{
					return new Fqdn(onPremisesSmartHost.Domain).Domain;
				}
				return null;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x001540C2 File Offset: 0x001522C2
		private string TenantCoexistenceDomain
		{
			get
			{
				return base.TaskContext.Parameters.Get<string>("_hybridDomain");
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x001540DC File Offset: 0x001522DC
		private string DefaultInboundConnectorName
		{
			get
			{
				return string.Format("Inbound from {0}", this.OnPremOrgConfig.Guid.ToString());
			}
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x0015410C File Offset: 0x0015230C
		private string DefaultOutboundConnectorName
		{
			get
			{
				return string.Format("Outbound to {0}", this.OnPremOrgConfig.Guid.ToString());
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x0015413C File Offset: 0x0015233C
		private string DefaultSendConnectorName
		{
			get
			{
				return "Outbound to Office 365";
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00154143 File Offset: 0x00152343
		private bool CentralizedTransportFeatureEnabled
		{
			get
			{
				return this.HybridConfiguration.Features.Contains(HybridFeature.CentralizedTransport);
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00154156 File Offset: 0x00152356
		private OrganizationRelationship TenantOrganizationRelationship
		{
			get
			{
				return base.TaskContext.Parameters.Get<OrganizationRelationship>("_tenantOrgRel");
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x0015416D File Offset: 0x0015236D
		private IOrganizationConfig OnPremOrgConfig
		{
			get
			{
				return base.OnPremisesSession.GetOrganizationConfig();
			}
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0015417C File Offset: 0x0015237C
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				return false;
			}
			if (this.ReceivingTransportServers.Count == 0 && this.SendingTransportServers.Count == 0 && this.EdgeTransportServers.Count == 0)
			{
				throw new LocalizedException(HybridStrings.HybridErrorNoTransportServersSet);
			}
			if (this.EdgeTransportServers.Count > 0 && (this.ReceivingTransportServers.Count > 0 || this.SendingTransportServers.Count > 0))
			{
				throw new LocalizedException(HybridStrings.HybridErrorMixedTransportServersSet);
			}
			if (this.SmartHost == null)
			{
				throw new LocalizedException(HybridStrings.HybridErrorNoSmartHostSet);
			}
			if (this.TlsCertificateName == null)
			{
				throw new LocalizedException(HybridStrings.HybridErrorNoTlsCertificateNameSet);
			}
			return this.CheckCertPrereqs();
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00154227 File Offset: 0x00152427
		public override bool NeedsConfiguration(ITaskContext taskContext)
		{
			return base.NeedsConfiguration(taskContext) || this.CheckOrVerifyConfiguration(false);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0015423B File Offset: 0x0015243B
		public override bool Configure(ITaskContext taskContext)
		{
			if (!base.Configure(taskContext))
			{
				return false;
			}
			this.ConfigureMailFlow();
			return true;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0015424F File Offset: 0x0015244F
		public override bool ValidateConfiguration(ITaskContext taskContext)
		{
			return base.ValidateConfiguration(taskContext) && !this.CheckOrVerifyConfiguration(true);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x00154266 File Offset: 0x00152466
		private void Reset()
		{
			this.receiveConnectorsByTransportServer = new Dictionary<string, Tuple<MailFlowTask.Operation, IReceiveConnector>>();
			this.onpremisesRemoteDomains = new Dictionary<string, Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig>>();
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x0015427E File Offset: 0x0015247E
		private IReceiveConnector BuildExpectedReceiveConnector(ADObjectId server)
		{
			return base.OnPremisesSession.BuildExpectedReceiveConnector(server, this.TlsCertificateName, this.TlsDomainCapabilities);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x00154298 File Offset: 0x00152498
		private ISendConnector BuildExpectedSendConnector()
		{
			MultiValuedProperty<ADObjectId> servers = (this.SendingTransportServers.Count > 0) ? this.SendingTransportServers : this.EdgeTransportServers;
			string fqdn = (this.EdgeTransportServers.Count == 0) ? null : this.TlsCertificateSubjectDomainName;
			return base.OnPremisesSession.BuildExpectedSendConnector(this.DefaultSendConnectorName, this.TenantCoexistenceDomain, servers, fqdn, this.FopeCertificateSubjectDomainName, this.TlsCertificateName, this.EnableSecureMail);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00154304 File Offset: 0x00152504
		private IInboundConnector BuildExpectedInboundConnector(ADObjectId identity)
		{
			return base.TenantSession.BuildExpectedInboundConnector(identity, this.DefaultInboundConnectorName, this.TlsCertificateName);
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0015431E File Offset: 0x0015251E
		private IOutboundConnector BuildExpectedOutboundConnector(ADObjectId identity)
		{
			return base.TenantSession.BuildExpectedOutboundConnector(identity, this.DefaultOutboundConnectorName, this.TlsCertificateSubjectDomainName, this.HybridDomains, this.SmartHost, this.CentralizedTransportFeatureEnabled);
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0015434C File Offset: 0x0015254C
		private bool CheckCertPrereqs()
		{
			if (!Configuration.DisableCertificateChecks)
			{
				foreach (ADObjectId adobjectId in this.SendingAndReceivingTransportServers)
				{
					IExchangeCertificate exchangeCertificate = base.OnPremisesSession.GetExchangeCertificate(adobjectId.Name, this.TlsCertificateName);
					if (exchangeCertificate == null)
					{
						throw new LocalizedException(HybridStrings.ErrorSecureMailCertificateNotFound(this.HybridConfiguration.TlsCertificateName.CertificateSubject, this.HybridConfiguration.TlsCertificateName.CertificateIssuer, adobjectId.Name));
					}
					if (!exchangeCertificate.Services.ToString().ToUpper().Contains(AllowedServices.SMTP.ToString()))
					{
						throw new LocalizedException(HybridStrings.ErrorSecureMailCertificateNoSmtp(adobjectId.Name));
					}
					if (exchangeCertificate.IsSelfSigned)
					{
						throw new LocalizedException(HybridStrings.ErrorSecureMailCertificateSelfSigned(adobjectId.Name));
					}
					if (exchangeCertificate.NotBefore > (DateTime)ExDateTime.Now || exchangeCertificate.NotAfter < (DateTime)ExDateTime.Now)
					{
						throw new LocalizedException(HybridStrings.ErrorSecureMailCertificateInvalidDate(adobjectId.Name));
					}
				}
			}
			return true;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00154480 File Offset: 0x00152680
		private bool DoOnPremisesSendConnectorNeedConfiguration()
		{
			this.sendConnectorOperation = MailFlowTask.Operation.NOP;
			this.sendConnector = this.GetMatchingSendConnector(this.TenantCoexistenceDomain);
			if (this.sendConnector == null)
			{
				this.sendConnectorOperation = MailFlowTask.Operation.New;
				return true;
			}
			if (!this.sendConnector.Equals(this.BuildExpectedSendConnector()))
			{
				this.sendConnectorOperation = MailFlowTask.Operation.Update;
				return true;
			}
			return false;
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x001544D4 File Offset: 0x001526D4
		private bool DoOnPremisesReceiveConnectorNeedConfiguration()
		{
			this.receiveConnectorOperation = MailFlowTask.Operation.NOP;
			if (this.ReceivingTransportServers.Count > 0)
			{
				using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = this.ReceivingTransportServers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADObjectId adobjectId = enumerator.Current;
						IReceiveConnector obj = this.BuildExpectedReceiveConnector(adobjectId);
						IReceiveConnector receiveConnector = base.OnPremisesSession.GetReceiveConnector(adobjectId);
						if (receiveConnector == null)
						{
							throw new LocalizedException(HybridStrings.ErrorDefaultReceieveConnectorNotFound(adobjectId.Name));
						}
						Tuple<MailFlowTask.Operation, IReceiveConnector> value;
						if (receiveConnector.Equals(obj))
						{
							value = new Tuple<MailFlowTask.Operation, IReceiveConnector>(MailFlowTask.Operation.NOP, receiveConnector);
						}
						else
						{
							this.receiveConnectorOperation = MailFlowTask.Operation.Update;
							value = new Tuple<MailFlowTask.Operation, IReceiveConnector>(MailFlowTask.Operation.Update, receiveConnector);
						}
						this.receiveConnectorsByTransportServer[adobjectId.Name] = value;
					}
					goto IL_C2;
				}
			}
			if (this.EdgeTransportServers.Count > 0 && !this.edgeReceiveConnectorsWarningDisplayed)
			{
				this.receiveConnectorOperation = MailFlowTask.Operation.Update;
			}
			IL_C2:
			return this.receiveConnectorOperation != MailFlowTask.Operation.NOP;
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x001545C0 File Offset: 0x001527C0
		private bool DoTenantConnectorsNeedConfiguration()
		{
			this.onPremisesOrganizationOperation = MailFlowTask.Operation.NOP;
			this.inboundConnectorOperation = MailFlowTask.Operation.NOP;
			this.outboundConnectorOperation = MailFlowTask.Operation.NOP;
			this.onPremisesOrganization = base.TenantSession.GetOnPremisesOrganization(this.OnPremOrgConfig.Guid);
			string identity = this.DefaultInboundConnectorName;
			string identity2 = this.DefaultOutboundConnectorName;
			if (this.onPremisesOrganization == null)
			{
				this.onPremisesOrganizationOperation = MailFlowTask.Operation.New;
			}
			else
			{
				if (this.onPremisesOrganization.InboundConnector != null)
				{
					identity = this.onPremisesOrganization.InboundConnector.ToString();
				}
				if (this.onPremisesOrganization.OutboundConnector != null)
				{
					identity2 = this.onPremisesOrganization.OutboundConnector.ToString();
				}
			}
			this.inboundConnector = base.TenantSession.GetInboundConnector(identity);
			if (this.inboundConnector == null)
			{
				if (this.EnableSecureMail)
				{
					this.inboundConnectorOperation = MailFlowTask.Operation.New;
				}
			}
			else if (this.EnableSecureMail)
			{
				IInboundConnector obj = this.BuildExpectedInboundConnector(null);
				if (!this.inboundConnector.Equals(obj))
				{
					this.inboundConnectorOperation = MailFlowTask.Operation.Update;
				}
			}
			else
			{
				this.inboundConnectorOperation = MailFlowTask.Operation.Remove;
			}
			this.outboundConnector = base.TenantSession.GetOutboundConnector(identity2);
			if (this.outboundConnector == null)
			{
				if (this.EnableSecureMail)
				{
					this.outboundConnectorOperation = MailFlowTask.Operation.New;
				}
			}
			else if (this.EnableSecureMail)
			{
				IOutboundConnector obj2 = this.BuildExpectedOutboundConnector(null);
				if (!this.outboundConnector.Equals(obj2))
				{
					this.outboundConnectorOperation = MailFlowTask.Operation.Update;
				}
			}
			else
			{
				this.outboundConnectorOperation = MailFlowTask.Operation.Remove;
			}
			if (this.onPremisesOrganization != null)
			{
				ADObjectId b = (this.inboundConnector == null) ? null : this.inboundConnector.Identity;
				ADObjectId b2 = (this.outboundConnector == null) ? null : this.outboundConnector.Identity;
				if (this.inboundConnectorOperation == MailFlowTask.Operation.New || this.inboundConnectorOperation == MailFlowTask.Operation.Remove || this.outboundConnectorOperation == MailFlowTask.Operation.New || this.outboundConnectorOperation == MailFlowTask.Operation.Remove || !TaskCommon.AreEqual(this.onPremisesOrganization.InboundConnector, b) || !TaskCommon.AreEqual(this.onPremisesOrganization.OutboundConnector, b2) || !TaskCommon.ContainsSame<SmtpDomain>(this.onPremisesOrganization.HybridDomains, this.HybridDomains) || !string.Equals(this.onPremisesOrganization.OrganizationName, this.OnPremOrgConfig.Name, StringComparison.InvariantCultureIgnoreCase) || !TaskCommon.AreEqual(this.onPremisesOrganization.OrganizationRelationship, (ADObjectId)this.TenantOrganizationRelationship.Identity))
				{
					this.onPremisesOrganizationOperation = MailFlowTask.Operation.Update;
				}
			}
			return this.onPremisesOrganizationOperation != MailFlowTask.Operation.NOP || this.inboundConnectorOperation != MailFlowTask.Operation.NOP || this.outboundConnectorOperation != MailFlowTask.Operation.NOP;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0015481C File Offset: 0x00152A1C
		private void ConfigureOnPremisesConnectors()
		{
			switch (this.sendConnectorOperation)
			{
			case MailFlowTask.Operation.New:
				this.sendConnector = base.OnPremisesSession.NewSendConnector(this.BuildExpectedSendConnector());
				break;
			case MailFlowTask.Operation.Update:
				this.sendConnector.UpdateFrom(this.BuildExpectedSendConnector());
				base.OnPremisesSession.SetSendConnector(this.sendConnector);
				break;
			}
			if (this.receiveConnectorOperation == MailFlowTask.Operation.Update)
			{
				foreach (ADObjectId adobjectId in this.ReceivingTransportServers)
				{
					Tuple<MailFlowTask.Operation, IReceiveConnector> tuple = this.receiveConnectorsByTransportServer[adobjectId.Name];
					MailFlowTask.Operation item = tuple.Item1;
					if (item == MailFlowTask.Operation.Update)
					{
						IReceiveConnector item2 = tuple.Item2;
						item2.UpdateFrom(this.BuildExpectedReceiveConnector(adobjectId));
						base.OnPremisesSession.SetReceiveConnector(item2);
					}
				}
				foreach (ADObjectId adobjectId2 in this.EdgeTransportServers)
				{
					string identity = string.Format("Default Frontend {0}", adobjectId2.ToString());
					base.TaskContext.Warnings.Add(HybridStrings.WarningEdgeReceiveConnector(adobjectId2.ToString(), identity, this.TlsCertificateSubjectDomainName.Replace("*", "mail")));
					this.edgeReceiveConnectorsWarningDisplayed = true;
				}
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x00154994 File Offset: 0x00152B94
		private void ConfigureTenantConnectors()
		{
			switch (this.inboundConnectorOperation)
			{
			case MailFlowTask.Operation.New:
				this.inboundConnector = base.TenantSession.NewInboundConnector(this.BuildExpectedInboundConnector(null));
				break;
			case MailFlowTask.Operation.Update:
				base.TenantSession.SetInboundConnector(this.BuildExpectedInboundConnector(this.inboundConnector.Identity));
				break;
			case MailFlowTask.Operation.Remove:
				base.TenantSession.RemoveInboundConnector(this.inboundConnector.Identity);
				this.inboundConnector = null;
				break;
			}
			switch (this.outboundConnectorOperation)
			{
			case MailFlowTask.Operation.New:
				this.outboundConnector = base.TenantSession.NewOutboundConnector(this.BuildExpectedOutboundConnector(null));
				break;
			case MailFlowTask.Operation.Update:
				base.TenantSession.SetOutboundConnector(this.BuildExpectedOutboundConnector(this.outboundConnector.Identity));
				break;
			case MailFlowTask.Operation.Remove:
				base.TenantSession.RemoveOutboundConnector(this.outboundConnector.Identity);
				this.outboundConnector = null;
				break;
			}
			switch (this.onPremisesOrganizationOperation)
			{
			case MailFlowTask.Operation.New:
				this.onPremisesOrganization = base.TenantSession.NewOnPremisesOrganization(this.OnPremOrgConfig, this.HybridDomains, this.inboundConnector, this.outboundConnector, this.TenantOrganizationRelationship);
				return;
			case MailFlowTask.Operation.Update:
				base.TenantSession.SetOnPremisesOrganization(this.onPremisesOrganization, this.OnPremOrgConfig, this.HybridDomains, this.inboundConnector, this.outboundConnector, this.TenantOrganizationRelationship);
				return;
			default:
				return;
			}
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x00154AFC File Offset: 0x00152CFC
		private bool CheckOrVerifyConfiguration(bool verifyOnly)
		{
			bool result = false;
			this.Reset();
			if (this.DoOnPremisesSendConnectorNeedConfiguration())
			{
				result = true;
			}
			if (this.DoOnPremisesReceiveConnectorNeedConfiguration())
			{
				result = true;
			}
			if (this.DoTenantConnectorsNeedConfiguration())
			{
				result = true;
			}
			if (this.DoOnPremisesRemoteDomainsNeedConfiguration())
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00154B3A File Offset: 0x00152D3A
		private void ConfigureMailFlow()
		{
			this.ConfigureOnPremisesConnectors();
			this.ConfigureTenantConnectors();
			this.ConfigureOnPremisesRemoteDomains();
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00154B50 File Offset: 0x00152D50
		private ISendConnector GetMatchingSendConnector(string addressSpace)
		{
			ISendConnector sendConnector = null;
			IEnumerable<ISendConnector> enumerable = base.OnPremisesSession.GetSendConnector();
			foreach (ISendConnector sendConnector2 in enumerable)
			{
				foreach (AddressSpace addressSpace2 in sendConnector2.AddressSpaces)
				{
					if (addressSpace2.Address.Equals(addressSpace, StringComparison.InvariantCultureIgnoreCase))
					{
						if (sendConnector != null)
						{
							throw new LocalizedException(HybridStrings.ErrorDuplicateSendConnectorAddressSpace(addressSpace));
						}
						if (sendConnector2.AddressSpaces.Count > 1)
						{
							throw new LocalizedException(HybridStrings.ErrorSendConnectorAddressSpaceNotExclusive(addressSpace));
						}
						sendConnector = sendConnector2;
					}
				}
			}
			if (sendConnector == null)
			{
				foreach (ISendConnector sendConnector3 in enumerable)
				{
					if (string.Equals(sendConnector3.Name, this.DefaultSendConnectorName))
					{
						sendConnector = sendConnector3;
						break;
					}
				}
			}
			return sendConnector;
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00154CC0 File Offset: 0x00152EC0
		private bool DoOnPremisesRemoteDomainsNeedConfiguration()
		{
			if (this.EdgeTransportServers.Count == 0)
			{
				return false;
			}
			IEnumerable<DomainContentConfig> remoteDomain = base.OnPremisesSession.GetRemoteDomain();
			string domainName = this.TenantCoexistenceDomain;
			DomainContentConfig domainContentConfig = (from d in remoteDomain
			where string.Equals(d.DomainName.Domain, domainName, StringComparison.InvariantCultureIgnoreCase)
			select d).FirstOrDefault<DomainContentConfig>();
			MailFlowTask.Operation item;
			if (domainContentConfig == null)
			{
				item = MailFlowTask.Operation.New;
			}
			else if (domainContentConfig.AllowedOOFType == AllowedOOFType.InternalLegacy && domainContentConfig.TrustedMailOutboundEnabled && domainContentConfig.AutoReplyEnabled && domainContentConfig.AutoForwardEnabled && domainContentConfig.DeliveryReportEnabled && domainContentConfig.NDREnabled && domainContentConfig.DisplaySenderName && domainContentConfig.TNEFEnabled != null && domainContentConfig.TNEFEnabled.Value)
			{
				item = MailFlowTask.Operation.NOP;
			}
			else
			{
				item = MailFlowTask.Operation.Update;
			}
			this.onpremisesRemoteDomains[domainName] = new Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig>(MailFlowTask.RemoteDomainType.Coexistence, item, domainContentConfig);
			foreach (SmtpDomain smtpDomain in this.HybridDomains)
			{
				string domainName = smtpDomain.Domain;
				DomainContentConfig domainContentConfig2 = (from d in remoteDomain
				where string.Equals(d.DomainName.Domain, domainName, StringComparison.InvariantCultureIgnoreCase)
				select d).FirstOrDefault<DomainContentConfig>();
				MailFlowTask.Operation item2 = MailFlowTask.Operation.NOP;
				if (domainContentConfig2 == null)
				{
					item2 = MailFlowTask.Operation.New;
				}
				else if (!domainContentConfig2.TrustedMailInboundEnabled)
				{
					item2 = MailFlowTask.Operation.Update;
				}
				this.onpremisesRemoteDomains[domainName] = new Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig>(MailFlowTask.RemoteDomainType.HybridDomain, item2, domainContentConfig2);
			}
			return this.onpremisesRemoteDomains.Values.Any((Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig> t) => t.Item2 != MailFlowTask.Operation.NOP);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00154E68 File Offset: 0x00153068
		private void ConfigureOnPremisesRemoteDomains()
		{
			foreach (KeyValuePair<string, Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig>> keyValuePair in this.onpremisesRemoteDomains)
			{
				string key = keyValuePair.Key;
				MailFlowTask.RemoteDomainType item = keyValuePair.Value.Item1;
				MailFlowTask.Operation item2 = keyValuePair.Value.Item2;
				DomainContentConfig domainContentConfig = keyValuePair.Value.Item3;
				if (item2 == MailFlowTask.Operation.New)
				{
					string name = string.Format("Hybrid Domain - {0}", key);
					domainContentConfig = base.OnPremisesSession.NewRemoteDomain(key, name);
				}
				if (item2 == MailFlowTask.Operation.New || item2 == MailFlowTask.Operation.Update)
				{
					SessionParameters sessionParameters = new SessionParameters();
					if (item == MailFlowTask.RemoteDomainType.HybridDomain)
					{
						sessionParameters.Set("TrustedMailInbound", true);
					}
					else if (item == MailFlowTask.RemoteDomainType.Coexistence)
					{
						sessionParameters.Set("AllowedOOFType", AllowedOOFType.InternalLegacy);
						sessionParameters.Set("TrustedMailOutbound", true);
						sessionParameters.Set("AutoReplyEnabled", true);
						sessionParameters.Set("AutoForwardEnabled", true);
						sessionParameters.Set("DeliveryReportEnabled", true);
						sessionParameters.Set("NDREnabled", true);
						sessionParameters.Set("DisplaySenderName", true);
						sessionParameters.Set("TNEFEnabled", true);
					}
					base.OnPremisesSession.SetRemoteDomain(domainContentConfig.Identity.ToString(), sessionParameters);
				}
			}
		}

		// Token: 0x04002FFA RID: 12282
		private MailFlowTask.Operation sendConnectorOperation;

		// Token: 0x04002FFB RID: 12283
		private ISendConnector sendConnector;

		// Token: 0x04002FFC RID: 12284
		private MailFlowTask.Operation receiveConnectorOperation;

		// Token: 0x04002FFD RID: 12285
		private Dictionary<string, Tuple<MailFlowTask.Operation, IReceiveConnector>> receiveConnectorsByTransportServer;

		// Token: 0x04002FFE RID: 12286
		private MailFlowTask.Operation inboundConnectorOperation;

		// Token: 0x04002FFF RID: 12287
		private MailFlowTask.Operation outboundConnectorOperation;

		// Token: 0x04003000 RID: 12288
		private MailFlowTask.Operation onPremisesOrganizationOperation;

		// Token: 0x04003001 RID: 12289
		private IOnPremisesOrganization onPremisesOrganization;

		// Token: 0x04003002 RID: 12290
		private IInboundConnector inboundConnector;

		// Token: 0x04003003 RID: 12291
		private IOutboundConnector outboundConnector;

		// Token: 0x04003004 RID: 12292
		private bool edgeReceiveConnectorsWarningDisplayed;

		// Token: 0x04003005 RID: 12293
		private Dictionary<string, Tuple<MailFlowTask.RemoteDomainType, MailFlowTask.Operation, DomainContentConfig>> onpremisesRemoteDomains;

		// Token: 0x02000917 RID: 2327
		private enum Operation
		{
			// Token: 0x04003008 RID: 12296
			NOP,
			// Token: 0x04003009 RID: 12297
			New,
			// Token: 0x0400300A RID: 12298
			Update,
			// Token: 0x0400300B RID: 12299
			Remove
		}

		// Token: 0x02000918 RID: 2328
		private enum RemoteDomainType
		{
			// Token: 0x0400300D RID: 12301
			Coexistence,
			// Token: 0x0400300E RID: 12302
			HybridDomain
		}
	}
}
