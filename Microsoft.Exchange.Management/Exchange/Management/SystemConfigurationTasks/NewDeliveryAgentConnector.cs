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
	// Token: 0x02000B38 RID: 2872
	[Cmdlet("New", "DeliveryAgentConnector", SupportsShouldProcess = true, DefaultParameterSetName = "AddressSpaces")]
	public sealed class NewDeliveryAgentConnector : NewSystemConfigurationObjectTask<DeliveryAgentConnector>
	{
		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x001AB8E1 File Offset: 0x001A9AE1
		// (set) Token: 0x06006741 RID: 26433 RVA: 0x001AB8EE File Offset: 0x001A9AEE
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

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06006742 RID: 26434 RVA: 0x001AB8FC File Offset: 0x001A9AFC
		// (set) Token: 0x06006743 RID: 26435 RVA: 0x001AB909 File Offset: 0x001A9B09
		[Parameter(Mandatory = true)]
		public string DeliveryProtocol
		{
			get
			{
				return this.DataObject.DeliveryProtocol;
			}
			set
			{
				this.DataObject.DeliveryProtocol = value;
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06006744 RID: 26436 RVA: 0x001AB917 File Offset: 0x001A9B17
		// (set) Token: 0x06006745 RID: 26437 RVA: 0x001AB924 File Offset: 0x001A9B24
		[Parameter]
		public int MaxMessagesPerConnection
		{
			get
			{
				return this.DataObject.MaxMessagesPerConnection;
			}
			set
			{
				this.DataObject.MaxMessagesPerConnection = value;
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x001AB932 File Offset: 0x001A9B32
		// (set) Token: 0x06006747 RID: 26439 RVA: 0x001AB93F File Offset: 0x001A9B3F
		[Parameter]
		public int MaxConcurrentConnections
		{
			get
			{
				return this.DataObject.MaxConcurrentConnections;
			}
			set
			{
				this.DataObject.MaxConcurrentConnections = value;
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x001AB94D File Offset: 0x001A9B4D
		// (set) Token: 0x06006749 RID: 26441 RVA: 0x001AB95A File Offset: 0x001A9B5A
		[Parameter]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x001AB968 File Offset: 0x001A9B68
		// (set) Token: 0x0600674B RID: 26443 RVA: 0x001AB975 File Offset: 0x001A9B75
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

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x001AB983 File Offset: 0x001A9B83
		// (set) Token: 0x0600674D RID: 26445 RVA: 0x001AB990 File Offset: 0x001A9B90
		[Parameter]
		public Unlimited<ByteQuantifiedSize> MaxMessageSize
		{
			get
			{
				return this.DataObject.MaxMessageSize;
			}
			set
			{
				this.DataObject.MaxMessageSize = value;
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x001AB99E File Offset: 0x001A9B9E
		// (set) Token: 0x0600674F RID: 26447 RVA: 0x001AB9B5 File Offset: 0x001A9BB5
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

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x001AB9C8 File Offset: 0x001A9BC8
		// (set) Token: 0x06006751 RID: 26449 RVA: 0x001AB9D5 File Offset: 0x001A9BD5
		[Parameter]
		public string Comment
		{
			get
			{
				return this.DataObject.Comment;
			}
			set
			{
				this.DataObject.Comment = value;
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x001AB9E3 File Offset: 0x001A9BE3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewDeliveryAgentConnector(base.Name, base.FormatMultiValuedProperty(this.AddressSpaces));
			}
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x001AB9FC File Offset: 0x001A9BFC
		protected override IConfigurable PrepareDataObject()
		{
			DeliveryAgentConnector deliveryAgentConnector = (DeliveryAgentConnector)base.PrepareDataObject();
			if (!MultiValuedPropertyBase.IsNullOrEmpty(this.SourceTransportServers))
			{
				deliveryAgentConnector.SourceTransportServers = base.ResolveIdParameterCollection<ServerIdParameter, Server, ADObjectId>(this.SourceTransportServers, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorServerNotUnique), null, null);
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
				if (server != null && server.IsHubTransportServer && server.IsExchange2007OrLater)
				{
					deliveryAgentConnector.SourceTransportServers = new MultiValuedProperty<ADObjectId>(false, SendConnectorSchema.SourceTransportServers, new ADObjectId[]
					{
						server.Id
					});
				}
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(deliveryAgentConnector.SourceTransportServers))
			{
				ManageSendConnectors.SetConnectorHomeMta(deliveryAgentConnector, (IConfigurationSession)base.DataSession);
			}
			ManageSendConnectors.SetConnectorId(deliveryAgentConnector, ((ITopologyConfigurationSession)base.DataSession).GetRoutingGroupId());
			return deliveryAgentConnector;
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x001ABB00 File Offset: 0x001A9D00
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (TopologyProvider.IsAdamTopology())
			{
				base.WriteError(new LocalizedException(Strings.CannotRunDeliveryAgentConnectorTaskOnEdge), ErrorCategory.InvalidOperation, null);
			}
			ADObjectId sourceRoutingGroup = this.DataObject.SourceRoutingGroup;
			bool flag;
			bool flag2;
			LocalizedException ex = ManageSendConnectors.ValidateTransportServers((IConfigurationSession)base.DataSession, this.DataObject, ref sourceRoutingGroup, false, true, this, out flag, out flag2);
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

		// Token: 0x06006755 RID: 26453 RVA: 0x001ABB78 File Offset: 0x001A9D78
		protected override void InternalProcessRecord()
		{
			DeliveryAgentConnector dataObject = this.DataObject;
			if (dataObject.IsScopedConnector)
			{
				ManageSendConnectors.AdjustAddressSpaces(dataObject);
			}
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, this.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}
	}
}
