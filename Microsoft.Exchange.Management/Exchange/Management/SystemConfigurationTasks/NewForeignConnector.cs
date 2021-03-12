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
	// Token: 0x02000B39 RID: 2873
	[Cmdlet("New", "ForeignConnector", SupportsShouldProcess = true, DefaultParameterSetName = "AddressSpaces")]
	public sealed class NewForeignConnector : NewSystemConfigurationObjectTask<ForeignConnector>
	{
		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x06006757 RID: 26455 RVA: 0x001ABBCF File Offset: 0x001A9DCF
		// (set) Token: 0x06006758 RID: 26456 RVA: 0x001ABBDC File Offset: 0x001A9DDC
		[Parameter(ParameterSetName = "AddressSpaces", Mandatory = true)]
		public MultiValuedProperty<AddressSpace> AddressSpaces
		{
			get
			{
				return this.DataObject.AddressSpaces;
			}
			set
			{
				this.DataObject.AddressSpaces = value;
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06006759 RID: 26457 RVA: 0x001ABBEA File Offset: 0x001A9DEA
		// (set) Token: 0x0600675A RID: 26458 RVA: 0x001ABC01 File Offset: 0x001A9E01
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

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x0600675B RID: 26459 RVA: 0x001ABC14 File Offset: 0x001A9E14
		// (set) Token: 0x0600675C RID: 26460 RVA: 0x001ABC21 File Offset: 0x001A9E21
		[Parameter]
		public bool IsScopedConnector
		{
			get
			{
				return this.DataObject.IsScopedConnector;
			}
			set
			{
				this.DataObject.IsScopedConnector = value;
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x0600675D RID: 26461 RVA: 0x001ABC2F File Offset: 0x001A9E2F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewForeignConnectorAddressSpaces(base.Name, base.FormatMultiValuedProperty(this.AddressSpaces));
			}
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x001ABC48 File Offset: 0x001A9E48
		protected override IConfigurable PrepareDataObject()
		{
			ForeignConnector foreignConnector = (ForeignConnector)base.PrepareDataObject();
			if (!MultiValuedPropertyBase.IsNullOrEmpty(this.SourceTransportServers))
			{
				foreignConnector.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, null);
			}
			else
			{
				Server server = null;
				try
				{
					server = ((ITopologyConfigurationSession)base.DataSession).ReadLocalServer();
				}
				catch (TransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ResourceUnavailable, this.DataObject);
				}
				if (ForeignConnectorTaskUtil.IsHubServer(server))
				{
					foreignConnector.SourceTransportServers = new MultiValuedProperty<ADObjectId>(false, SendConnectorSchema.SourceTransportServers, new ADObjectId[]
					{
						server.Id
					});
				}
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(foreignConnector.SourceTransportServers))
			{
				ManageSendConnectors.SetConnectorHomeMta(foreignConnector, (IConfigurationSession)base.DataSession);
			}
			if (string.IsNullOrEmpty(foreignConnector.DropDirectory))
			{
				foreignConnector.DropDirectory = foreignConnector.Name;
			}
			ManageSendConnectors.SetConnectorId(foreignConnector, ((ITopologyConfigurationSession)base.DataSession).GetRoutingGroupId());
			return foreignConnector;
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x001ABD5C File Offset: 0x001A9F5C
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

		// Token: 0x06006760 RID: 26464 RVA: 0x001ABDCC File Offset: 0x001A9FCC
		protected override void InternalProcessRecord()
		{
			ForeignConnector dataObject = this.DataObject;
			if (dataObject.IsScopedConnector)
			{
				ManageSendConnectors.AdjustAddressSpaces(dataObject);
			}
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}
	}
}
