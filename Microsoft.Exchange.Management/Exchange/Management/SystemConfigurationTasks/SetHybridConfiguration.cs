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
	// Token: 0x020008DB RID: 2267
	[Cmdlet("Set", "HybridConfiguration", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = "Identity")]
	public sealed class SetHybridConfiguration : SetSingletonSystemConfigurationObjectTask<HybridConfiguration>
	{
		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x001503C7 File Offset: 0x0014E5C7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return HybridStrings.ConfirmationMessageSetHybridConfiguration;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x001503CE File Offset: 0x0014E5CE
		protected override ObjectId RootId
		{
			get
			{
				return HybridConfiguration.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x001503DB File Offset: 0x0014E5DB
		// (set) Token: 0x06005062 RID: 20578 RVA: 0x001503F2 File Offset: 0x0014E5F2
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

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x00150405 File Offset: 0x0014E605
		// (set) Token: 0x06005064 RID: 20580 RVA: 0x0015041C File Offset: 0x0014E61C
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

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x0015042F File Offset: 0x0014E62F
		// (set) Token: 0x06005066 RID: 20582 RVA: 0x00150446 File Offset: 0x0014E646
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

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06005067 RID: 20583 RVA: 0x00150459 File Offset: 0x0014E659
		// (set) Token: 0x06005068 RID: 20584 RVA: 0x00150470 File Offset: 0x0014E670
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

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06005069 RID: 20585 RVA: 0x00150483 File Offset: 0x0014E683
		// (set) Token: 0x0600506A RID: 20586 RVA: 0x0015049F File Offset: 0x0014E69F
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

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x001504BD File Offset: 0x0014E6BD
		// (set) Token: 0x0600506C RID: 20588 RVA: 0x001504D4 File Offset: 0x0014E6D4
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

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x001504E7 File Offset: 0x0014E6E7
		// (set) Token: 0x0600506E RID: 20590 RVA: 0x001504FE File Offset: 0x0014E6FE
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

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x00150511 File Offset: 0x0014E711
		// (set) Token: 0x06005070 RID: 20592 RVA: 0x00150528 File Offset: 0x0014E728
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

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0015053B File Offset: 0x0014E73B
		// (set) Token: 0x06005072 RID: 20594 RVA: 0x00150552 File Offset: 0x0014E752
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<IPRange> ExternalIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["ExternalIPAddresses"];
			}
			set
			{
				base.Fields["ExternalIPAddresses"] = ((value == null) ? new MultiValuedProperty<IPRange>() : value);
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x0015056F File Offset: 0x0014E76F
		// (set) Token: 0x06005074 RID: 20596 RVA: 0x00150586 File Offset: 0x0014E786
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

		// Token: 0x06005075 RID: 20597 RVA: 0x001505A0 File Offset: 0x0014E7A0
		protected override IConfigurable PrepareDataObject()
		{
			this.DataObject = (HybridConfiguration)base.PrepareDataObject();
			return SetHybridConfigurationLogic.PrepareDataObject(base.Fields, this.DataObject, base.DataSession, new HybridConfigurationTaskUtility.GetUniqueObject(base.GetDataObject<Server>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x001505F8 File Offset: 0x0014E7F8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			int count = HybridConfigurationTaskUtility.GetCount<ADObjectId>(this.DataObject.EdgeTransportServers);
			int count2 = HybridConfigurationTaskUtility.GetCount<ADObjectId>(this.DataObject.SendingTransportServers);
			int count3 = HybridConfigurationTaskUtility.GetCount<ADObjectId>(this.DataObject.ReceivingTransportServers);
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
			SetHybridConfigurationLogic.Validate(this.DataObject, base.HasErrors, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}
	}
}
