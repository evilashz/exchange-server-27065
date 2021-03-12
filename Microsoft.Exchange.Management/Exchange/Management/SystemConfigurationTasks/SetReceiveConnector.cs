using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B50 RID: 2896
	[Cmdlet("Set", "ReceiveConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetReceiveConnector : SetTopologySystemConfigurationObjectTask<ReceiveConnectorIdParameter, ReceiveConnector>
	{
		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x001B0E8D File Offset: 0x001AF08D
		// (set) Token: 0x06006915 RID: 26901 RVA: 0x001B0EA4 File Offset: 0x001AF0A4
		[Parameter(Mandatory = false)]
		public AcceptedDomainIdParameter DefaultDomain
		{
			get
			{
				return (AcceptedDomainIdParameter)base.Fields["DefaultDomain"];
			}
			set
			{
				base.Fields["DefaultDomain"] = value;
			}
		}

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x06006916 RID: 26902 RVA: 0x001B0EB7 File Offset: 0x001AF0B7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetReceiveConnector(this.Identity.ToString());
			}
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x001B0ECC File Offset: 0x001AF0CC
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ReceiveConnector receiveConnector = (ReceiveConnector)dataObject;
			this.serverObject = (Server)base.GetDataObject<Server>(new ServerIdParameter(receiveConnector.Server), base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(receiveConnector.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(receiveConnector.Server.ToString())));
			if (this.serverObject == null)
			{
				base.WriteError(new NullServerObjectException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			try
			{
				receiveConnector.SetPermissionGroupsBasedOnSecurityDescriptor(this.serverObject);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
				return;
			}
			receiveConnector.ResetChangeTracking();
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x001B0F88 File Offset: 0x001AF188
		protected override void InternalProcessRecord()
		{
			bool flag = this.DataObject.IsModified(ReceiveConnectorSchema.PermissionGroups);
			if (flag)
			{
				if (this.serverObject.IsEdgeServer && (this.DataObject.PermissionGroups & PermissionGroups.ExchangeLegacyServers) != PermissionGroups.None)
				{
					base.WriteError(new UnSupportedPermissionGroupsForEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				if ((this.DataObject.PermissionGroups & PermissionGroups.Custom) != PermissionGroups.None)
				{
					base.WriteError(new CustomCannotBeSetForPermissionGroupsException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				base.WriteVerbose(Strings.SecurityDescriptorBeingUpdatedMsg);
				try
				{
					this.DataObject.SaveNewSecurityDescriptor(this.serverObject);
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
					return;
				}
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.ChunkingEnabled) && !this.DataObject.ChunkingEnabled && this.DataObject.BinaryMimeEnabled)
			{
				base.WriteError(new ChunkingEnabledSettingConflictException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.LongAddressesEnabled) && this.DataObject.LongAddressesEnabled && this.serverObject.IsEdgeServer)
			{
				base.WriteError(new LongAddressesEnabledOnEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.SuppressXAnonymousTls))
			{
				if (this.DataObject.SuppressXAnonymousTls && this.serverObject.IsEdgeServer)
				{
					base.WriteError(new SuppressXAnonymousTlsEnabledOnEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				if (this.DataObject.SuppressXAnonymousTls && !this.serverObject.UseDowngradedExchangeServerAuth)
				{
					base.WriteError(new SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.TransportRole) && (this.DataObject.TransportRole & (ServerRole.Mailbox | ServerRole.HubTransport | ServerRole.Edge | ServerRole.FrontendTransport)) == ServerRole.None)
			{
				base.WriteError(new InvalidTransportRoleOnReceiveConnectorException(), ErrorCategory.InvalidData, this.DataObject);
			}
			if (this.DataObject.ObjectState != ObjectState.Unchanged || !flag)
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x001B1174 File Offset: 0x001AF374
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			string message;
			if (!NewReceiveConnector.ValidataName(this.DataObject.Name, out message))
			{
				base.WriteError(new ArgumentException(message), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("DefaultDomain"))
			{
				AcceptedDomainIdParameter defaultDomain = this.DefaultDomain;
				if (defaultDomain != null)
				{
					AcceptedDomain acceptedDomain = (AcceptedDomain)base.GetDataObject<AcceptedDomain>(defaultDomain, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorDefaultDomainNotFound(defaultDomain)), new LocalizedString?(Strings.ErrorDefaultDomainNotUnique(defaultDomain)));
					this.DataObject.DefaultDomain = acceptedDomain.Id;
				}
				else
				{
					this.DataObject.DefaultDomain = null;
				}
			}
			LocalizedException exception;
			if (!ReceiveConnectorNoMappingConflictCondition.Verify(this.DataObject, base.DataSession as IConfigurationSession, out exception))
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (!this.serverObject.IsEdgeServer && (this.DataObject.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None && !ReceiveConnectorFqdnCondition.Verify(this.DataObject, this.serverObject, out exception))
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.AdvertiseClientSettings && (this.DataObject.PermissionGroups & PermissionGroups.ExchangeUsers) != PermissionGroups.ExchangeUsers)
			{
				base.WriteError(new AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x040036A3 RID: 13987
		private const string DefaultDomainField = "DefaultDomain";

		// Token: 0x040036A4 RID: 13988
		private Server serverObject;
	}
}
