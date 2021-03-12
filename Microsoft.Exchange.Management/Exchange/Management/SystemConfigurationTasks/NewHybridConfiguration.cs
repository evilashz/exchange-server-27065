using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D8 RID: 2264
	[Cmdlet("New", "HybridConfiguration", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class NewHybridConfiguration : NewFixedNameSystemConfigurationObjectTask<HybridConfiguration>
	{
		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0014F9F9 File Offset: 0x0014DBF9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return HybridStrings.ConfirmationMessageNewHybridConfiguration;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0014FA00 File Offset: 0x0014DC00
		// (set) Token: 0x06005032 RID: 20530 RVA: 0x0014FA17 File Offset: 0x0014DC17
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<ServerIdParameter> ClientAccessServers
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["ClientAccessServers"];
			}
			set
			{
				base.Fields["ClientAccessServers"] = value;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x0014FA2A File Offset: 0x0014DC2A
		// (set) Token: 0x06005034 RID: 20532 RVA: 0x0014FA41 File Offset: 0x0014DC41
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<ServerIdParameter> ReceivingTransportServers
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["ReceivingTransportServers"];
			}
			set
			{
				base.Fields["ReceivingTransportServers"] = value;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x0014FA54 File Offset: 0x0014DC54
		// (set) Token: 0x06005036 RID: 20534 RVA: 0x0014FA6B File Offset: 0x0014DC6B
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<ServerIdParameter> SendingTransportServers
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["SendingTransportServers"];
			}
			set
			{
				base.Fields["SendingTransportServers"] = value;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x0014FA7E File Offset: 0x0014DC7E
		// (set) Token: 0x06005038 RID: 20536 RVA: 0x0014FA95 File Offset: 0x0014DC95
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<ServerIdParameter> EdgeTransportServers
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["EdgeTransportServers"];
			}
			set
			{
				base.Fields["EdgeTransportServers"] = value;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x0014FAA8 File Offset: 0x0014DCA8
		// (set) Token: 0x0600503A RID: 20538 RVA: 0x0014FAC4 File Offset: 0x0014DCC4
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return SmtpX509Identifier.Parse((string)base.Fields["TlsCertificateName"]);
			}
			set
			{
				base.Fields["TlsCertificateName"] = ((value == null) ? null : value.ToString());
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x0600503B RID: 20539 RVA: 0x0014FAE2 File Offset: 0x0014DCE2
		// (set) Token: 0x0600503C RID: 20540 RVA: 0x0014FAF9 File Offset: 0x0014DCF9
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SmtpDomain OnPremisesSmartHost
		{
			get
			{
				return (SmtpDomain)base.Fields["OnPremisesSmartHost"];
			}
			set
			{
				base.Fields["OnPremisesSmartHost"] = value;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x0014FB0C File Offset: 0x0014DD0C
		// (set) Token: 0x0600503E RID: 20542 RVA: 0x0014FB23 File Offset: 0x0014DD23
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<AutoDiscoverSmtpDomain> Domains
		{
			get
			{
				return (MultiValuedProperty<AutoDiscoverSmtpDomain>)base.Fields["Domains"];
			}
			set
			{
				base.Fields["Domains"] = value;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x0014FB36 File Offset: 0x0014DD36
		// (set) Token: 0x06005040 RID: 20544 RVA: 0x0014FB4D File Offset: 0x0014DD4D
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<HybridFeature> Features
		{
			get
			{
				return (MultiValuedProperty<HybridFeature>)base.Fields["Features"];
			}
			set
			{
				base.Fields["Features"] = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x0014FB60 File Offset: 0x0014DD60
		// (set) Token: 0x06005042 RID: 20546 RVA: 0x0014FB77 File Offset: 0x0014DD77
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<IPRange> ExternalIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["ExternalIPAddresses"];
			}
			set
			{
				base.Fields["ExternalIPAddresses"] = value;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x0014FB8A File Offset: 0x0014DD8A
		// (set) Token: 0x06005044 RID: 20548 RVA: 0x0014FBA1 File Offset: 0x0014DDA1
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public int ServiceInstance
		{
			get
			{
				return (int)base.Fields["ServiceInstance"];
			}
			set
			{
				base.Fields["ServiceInstance"] = value;
			}
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0014FBFC File Offset: 0x0014DDFC
		protected override IConfigurable PrepareDataObject()
		{
			HybridConfiguration hybridConfiguration = (HybridConfiguration)base.PrepareDataObject();
			IConfigurationSession session = base.DataSession as IConfigurationSession;
			hybridConfiguration.SetId(session, "Hybrid Configuration");
			if (base.Fields.IsModified("ClientAccessServers"))
			{
				HybridConfiguration hybridConfiguration2 = hybridConfiguration;
				ADPropertyDefinition clientAccessServers = HybridConfigurationSchema.ClientAccessServers;
				IConfigDataProvider dataSession = base.DataSession;
				MultiValuedProperty<ServerIdParameter> clientAccessServers2 = this.ClientAccessServers;
				HybridConfigurationTaskUtility.GetUniqueObject getServer = new HybridConfigurationTaskUtility.GetUniqueObject(base.GetDataObject<Server>);
				Task.TaskErrorLoggingDelegate writeError = new Task.TaskErrorLoggingDelegate(base.WriteError);
				HybridConfigurationTaskUtility.ServerCriterion[] array = new HybridConfigurationTaskUtility.ServerCriterion[2];
				array[0] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsClientAccessServer, new Func<string, LocalizedString>(HybridStrings.HybridErrorServerNotCAS));
				array[1] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsE14OrLater, new Func<string, LocalizedString>(HybridStrings.HybridErrorServerNotE14CAS));
				hybridConfiguration2.ClientAccessServers = HybridConfigurationTaskUtility.ValidateServers(clientAccessServers, dataSession, clientAccessServers2, getServer, writeError, array);
			}
			if (base.Fields.IsModified("SendingTransportServers"))
			{
				HybridConfiguration hybridConfiguration3 = hybridConfiguration;
				ADPropertyDefinition sendingTransportServers = HybridConfigurationSchema.SendingTransportServers;
				IConfigDataProvider dataSession2 = base.DataSession;
				MultiValuedProperty<ServerIdParameter> sendingTransportServers2 = this.SendingTransportServers;
				HybridConfigurationTaskUtility.GetUniqueObject getServer2 = new HybridConfigurationTaskUtility.GetUniqueObject(base.GetDataObject<Server>);
				Task.TaskErrorLoggingDelegate writeError2 = new Task.TaskErrorLoggingDelegate(base.WriteError);
				HybridConfigurationTaskUtility.ServerCriterion[] array2 = new HybridConfigurationTaskUtility.ServerCriterion[2];
				array2[0] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsHubTransportServer, new Func<string, LocalizedString>(HybridStrings.HybridErrorSendingTransportServerNotHub));
				array2[1] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsE15OrLater, new Func<string, LocalizedString>(HybridStrings.HybridErrorSendingTransportServerNotE15Hub));
				hybridConfiguration3.SendingTransportServers = HybridConfigurationTaskUtility.ValidateServers(sendingTransportServers, dataSession2, sendingTransportServers2, getServer2, writeError2, array2);
			}
			if (base.Fields.IsModified("ReceivingTransportServers"))
			{
				HybridConfiguration hybridConfiguration4 = hybridConfiguration;
				ADPropertyDefinition receivingTransportServers = HybridConfigurationSchema.ReceivingTransportServers;
				IConfigDataProvider dataSession3 = base.DataSession;
				MultiValuedProperty<ServerIdParameter> receivingTransportServers2 = this.ReceivingTransportServers;
				HybridConfigurationTaskUtility.GetUniqueObject getServer3 = new HybridConfigurationTaskUtility.GetUniqueObject(base.GetDataObject<Server>);
				Task.TaskErrorLoggingDelegate writeError3 = new Task.TaskErrorLoggingDelegate(base.WriteError);
				HybridConfigurationTaskUtility.ServerCriterion[] array3 = new HybridConfigurationTaskUtility.ServerCriterion[2];
				array3[0] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsFrontendTransportServer, new Func<string, LocalizedString>(HybridStrings.HybridErrorReceivingTransportServerNotFrontEnd));
				array3[1] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsE15OrLater, new Func<string, LocalizedString>(HybridStrings.HybridErrorReceivingTransportServerNotE15FrontEnd));
				hybridConfiguration4.ReceivingTransportServers = HybridConfigurationTaskUtility.ValidateServers(receivingTransportServers, dataSession3, receivingTransportServers2, getServer3, writeError3, array3);
			}
			if (base.Fields.IsModified("EdgeTransportServers"))
			{
				HybridConfiguration hybridConfiguration5 = hybridConfiguration;
				ADPropertyDefinition edgeTransportServers = HybridConfigurationSchema.EdgeTransportServers;
				IConfigDataProvider dataSession4 = base.DataSession;
				MultiValuedProperty<ServerIdParameter> edgeTransportServers2 = this.EdgeTransportServers;
				HybridConfigurationTaskUtility.GetUniqueObject getServer4 = new HybridConfigurationTaskUtility.GetUniqueObject(base.GetDataObject<Server>);
				Task.TaskErrorLoggingDelegate writeError4 = new Task.TaskErrorLoggingDelegate(base.WriteError);
				HybridConfigurationTaskUtility.ServerCriterion[] array4 = new HybridConfigurationTaskUtility.ServerCriterion[2];
				array4[0] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsEdgeServer, new Func<string, LocalizedString>(HybridStrings.HybridErrorServerNotEdge));
				array4[1] = new HybridConfigurationTaskUtility.ServerCriterion((Server s) => s.IsE14Sp1OrLater, new Func<string, LocalizedString>(HybridStrings.HybridErrorServerNotE14Edge));
				hybridConfiguration5.EdgeTransportServers = HybridConfigurationTaskUtility.ValidateServers(edgeTransportServers, dataSession4, edgeTransportServers2, getServer4, writeError4, array4);
			}
			if (base.Fields.IsModified("TlsCertificateName"))
			{
				this.DataObject.TlsCertificateName = this.TlsCertificateName;
			}
			if (base.Fields.IsModified("OnPremisesSmartHost"))
			{
				this.DataObject.OnPremisesSmartHost = this.OnPremisesSmartHost;
			}
			if (base.Fields.IsModified("Domains"))
			{
				this.DataObject.Domains = this.Domains;
			}
			if (base.Fields.IsModified("Features"))
			{
				this.DataObject.Features = this.Features;
			}
			if (base.Fields.IsModified("ExternalIPAddresses"))
			{
				this.DataObject.ExternalIPAddresses = HybridConfigurationTaskUtility.ValidateExternalIPAddresses(this.ExternalIPAddresses, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified("ServiceInstance"))
			{
				this.DataObject.ServiceInstance = this.ServiceInstance;
			}
			return hybridConfiguration;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0014FFE8 File Offset: 0x0014E1E8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			HybridConfiguration[] array = ((IConfigurationSession)base.DataSession).Find<HybridConfiguration>(null, QueryScope.SubTree, null, null, 0);
			if (array == null || array.Length == 0)
			{
				base.InternalProcessRecord();
			}
			else
			{
				base.WriteError(new HybridConfigurationAlreadyDefinedException(), ErrorCategory.InvalidArgument, this.DataObject.Name);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00150044 File Offset: 0x0014E244
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			int count = HybridConfigurationTaskUtility.GetCount<ServerIdParameter>(this.EdgeTransportServers);
			int count2 = HybridConfigurationTaskUtility.GetCount<ServerIdParameter>(this.SendingTransportServers);
			int count3 = HybridConfigurationTaskUtility.GetCount<ServerIdParameter>(this.ReceivingTransportServers);
			if (count > 0 && count2 + count3 > 0)
			{
				base.WriteError(new LocalizedException(HybridStrings.HybridErrorMixedTransportServersSet), ErrorCategory.InvalidArgument, this.DataObject.Name);
			}
			if (count2 + count3 > 0 && (count2 == 0 || count3 == 0))
			{
				base.WriteError(new LocalizedException(HybridStrings.HybridErrorBothTransportServersNotSet), ErrorCategory.InvalidArgument, this.DataObject.Name);
			}
			if (this.Domains != null)
			{
				if ((from d in this.Domains
				where d.AutoDiscover
				select d).Count<AutoDiscoverSmtpDomain>() > 1)
				{
					base.WriteError(new LocalizedException(HybridStrings.HybridErrorOnlyOneAutoDiscoverDomainMayBeSet), ErrorCategory.InvalidArgument, this.DataObject.Name);
				}
			}
			TaskLogger.LogExit();
		}
	}
}
