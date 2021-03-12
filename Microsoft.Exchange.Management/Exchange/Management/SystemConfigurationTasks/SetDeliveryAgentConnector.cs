using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B4B RID: 2891
	[Cmdlet("Set", "DeliveryAgentConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeliveryAgentConnector : SetTopologySystemConfigurationObjectTask<DeliveryAgentConnectorIdParameter, DeliveryAgentConnector>
	{
		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x060068C7 RID: 26823 RVA: 0x001AFA84 File Offset: 0x001ADC84
		// (set) Token: 0x060068C8 RID: 26824 RVA: 0x001AFA9B File Offset: 0x001ADC9B
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

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x060068C9 RID: 26825 RVA: 0x001AFAAE File Offset: 0x001ADCAE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDeliveryAgentConnector(this.Identity.ToString());
			}
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x060068CA RID: 26826 RVA: 0x001AFAC0 File Offset: 0x001ADCC0
		// (set) Token: 0x060068CB RID: 26827 RVA: 0x001AFAC8 File Offset: 0x001ADCC8
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

		// Token: 0x060068CC RID: 26828 RVA: 0x001AFAD4 File Offset: 0x001ADCD4
		protected override IConfigurable PrepareDataObject()
		{
			DeliveryAgentConnector deliveryAgentConnector = (DeliveryAgentConnector)base.PrepareDataObject();
			if (base.Fields.IsModified("SourceTransportServers"))
			{
				if (this.SourceTransportServers != null)
				{
					deliveryAgentConnector.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, null);
					if (deliveryAgentConnector.SourceTransportServers.Count > 0)
					{
						ManageSendConnectors.SetConnectorHomeMta(deliveryAgentConnector, (IConfigurationSession)base.DataSession);
					}
				}
				else
				{
					deliveryAgentConnector.SourceTransportServers = null;
				}
			}
			return deliveryAgentConnector;
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x001AFB6C File Offset: 0x001ADD6C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ADObjectId sourceRoutingGroup = this.DataObject.SourceRoutingGroup;
			bool flag;
			bool flag2;
			LocalizedException ex = ManageSendConnectors.ValidateTransportServers((IConfigurationSession)base.DataSession, this.DataObject, ref sourceRoutingGroup, true, true, this, out flag, out flag2);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject);
				return;
			}
			if (flag2)
			{
				this.WriteWarning(Strings.WarningMultiSiteSourceServers);
			}
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x001AFBCC File Offset: 0x001ADDCC
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			DeliveryAgentConnector deliveryAgentConnector = (DeliveryAgentConnector)dataObject;
			this.currentlyEnabled = deliveryAgentConnector.Enabled;
			deliveryAgentConnector.IsScopedConnector = deliveryAgentConnector.GetScopedConnector();
			deliveryAgentConnector.ResetChangeTracking();
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x001AFC08 File Offset: 0x001ADE08
		protected override void InternalProcessRecord()
		{
			DeliveryAgentConnector dataObject = this.DataObject;
			if (this.currentlyEnabled && !dataObject.Enabled && !this.force && !base.ShouldContinue(Strings.ConfirmationMessageDisableSendConnector))
			{
				return;
			}
			ManageSendConnectors.AdjustAddressSpaces(dataObject);
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}

		// Token: 0x04003694 RID: 13972
		private SwitchParameter force;

		// Token: 0x04003695 RID: 13973
		private bool currentlyEnabled;
	}
}
