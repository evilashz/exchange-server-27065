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
	// Token: 0x02000B4C RID: 2892
	[Cmdlet("Set", "ForeignConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetForeignConnector : SetSystemConfigurationObjectTask<ForeignConnectorIdParameter, ForeignConnector>
	{
		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x060068D1 RID: 26833 RVA: 0x001AFC82 File Offset: 0x001ADE82
		// (set) Token: 0x060068D2 RID: 26834 RVA: 0x001AFC99 File Offset: 0x001ADE99
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

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x060068D3 RID: 26835 RVA: 0x001AFCAC File Offset: 0x001ADEAC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetForeignConnector(this.Identity.ToString());
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x060068D4 RID: 26836 RVA: 0x001AFCBE File Offset: 0x001ADEBE
		// (set) Token: 0x060068D5 RID: 26837 RVA: 0x001AFCC6 File Offset: 0x001ADEC6
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

		// Token: 0x060068D6 RID: 26838 RVA: 0x001AFCD0 File Offset: 0x001ADED0
		protected override IConfigurable PrepareDataObject()
		{
			ForeignConnector foreignConnector = (ForeignConnector)base.PrepareDataObject();
			if (!MultiValuedPropertyBase.IsNullOrEmpty(this.SourceTransportServers))
			{
				foreignConnector.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, null);
				if (this.SourceTransportServers.Count > 0)
				{
					ManageSendConnectors.SetConnectorHomeMta(foreignConnector, (IConfigurationSession)base.DataSession);
				}
			}
			return foreignConnector;
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x001AFD50 File Offset: 0x001ADF50
		protected override void InternalValidate()
		{
			base.InternalValidate();
			try
			{
				ForeignConnectorTaskUtil.CheckTopology();
				ForeignConnectorTaskUtil.ValidateObject(this.DataObject, (IConfigurationSession)base.DataSession, this);
			}
			catch (MultiSiteSourceServersException ex)
			{
				base.WriteWarning(ex.Message);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x001AFDC0 File Offset: 0x001ADFC0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ForeignConnector foreignConnector = dataObject as ForeignConnector;
			this.currentlyEnabled = foreignConnector.Enabled;
			foreignConnector.IsScopedConnector = foreignConnector.GetScopedConnector();
			foreignConnector.ResetChangeTracking();
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x001AFDFC File Offset: 0x001ADFFC
		protected override void InternalProcessRecord()
		{
			ForeignConnector dataObject = this.DataObject;
			if (this.currentlyEnabled && !dataObject.Enabled && !this.force && !base.ShouldContinue(Strings.ConfirmationMessageDisableSendConnector))
			{
				return;
			}
			ManageSendConnectors.AdjustAddressSpaces(dataObject);
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}

		// Token: 0x04003696 RID: 13974
		private SwitchParameter force;

		// Token: 0x04003697 RID: 13975
		private bool currentlyEnabled;
	}
}
