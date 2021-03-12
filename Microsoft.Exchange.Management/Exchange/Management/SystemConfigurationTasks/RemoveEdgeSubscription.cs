using System;
using System.DirectoryServices.Protocols;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C4 RID: 2500
	[Cmdlet("Remove", "EdgeSubscription", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveEdgeSubscription : RemoveSystemConfigurationObjectTask<TransportServerIdParameter, Server>
	{
		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06005901 RID: 22785 RVA: 0x00175117 File Offset: 0x00173317
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveEdgeSubscription(this.Identity.ToString());
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06005902 RID: 22786 RVA: 0x00175129 File Offset: 0x00173329
		// (set) Token: 0x06005903 RID: 22787 RVA: 0x0017513B File Offset: 0x0017333B
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			internal get
			{
				return new SwitchParameter(this.force);
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00175144 File Offset: 0x00173344
		protected override void InternalProcessRecord()
		{
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			Server server = null;
			try
			{
				server = topologyConfigurationSession.ReadLocalServer();
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, base.DataObject);
				return;
			}
			catch (TransientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ResourceUnavailable, base.DataObject);
				return;
			}
			catch (LocalServerNotFoundException)
			{
			}
			if (server != null && server.IsEdgeServer)
			{
				this.HandleRemoveOnEdge(topologyConfigurationSession);
				return;
			}
			this.HandleRemoveInsideOrg(topologyConfigurationSession);
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x001751E4 File Offset: 0x001733E4
		private void HandleRemoveInsideOrg(IConfigurationSession scSession)
		{
			Server dataObject = base.DataObject;
			if (!dataObject.IsEdgeServer)
			{
				base.WriteError(new InvalidOperationException(Strings.WrongSubscriptionIdentity), ErrorCategory.InvalidOperation, null);
			}
			try
			{
				this.RemoveEdgeFromConnectorSourceServers(scSession, dataObject);
				AdamUserManagement.RemoveSubscriptionCredentialsOnAllBHs(dataObject.Fqdn);
				scSession.DeleteTree(base.DataObject, delegate(ADTreeDeleteNotFinishedException de)
				{
					base.WriteVerbose(de.LocalizedString);
				});
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00175268 File Offset: 0x00173468
		private void HandleRemoveOnEdge(ITopologyConfigurationSession scSession)
		{
			try
			{
				this.RemoveEdgeSyncedObjects<DomainContentConfig>(scSession);
				this.RemoveEdgeSyncedObjects<MicrosoftExchangeRecipient>(scSession);
				this.RemoveEdgeSyncedObjects<AcceptedDomain>(scSession);
				this.RemoveEdgeSyncedObjects<MessageClassification>(scSession);
				this.RemoveEdgeSyncedObjects<MailGateway>(scSession);
				this.RemoveEdgeSyncedHubServerObjects(scSession);
				AdamUserManagement.RemoveAllADAMPrincipals();
				base.DataObject.EdgeSyncLease = null;
				base.DataObject.EdgeSyncCredentials = null;
				base.DataObject.EdgeSyncStatus = null;
				base.DataObject.GatewayEdgeSyncSubscribed = false;
				base.DataObject.EdgeSyncCookies = null;
				base.DataObject.EdgeSyncSourceGuid = null;
				scSession.Save(base.DataObject);
				this.RestoreTransportSettings(scSession);
				if (this.force || base.ShouldContinue(Strings.ConfirmationMessageRemoveEdgeSubscriptionRecipients))
				{
					this.RemoveAllEdgeSyncedRecipients();
				}
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (MessageSecurityException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
			catch (LdapException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00175370 File Offset: 0x00173570
		private void RemoveEdgeFromConnectorSourceServers(IConfigurationSession scSession, Server server)
		{
			SmtpSendConnectorConfig[] array = scSession.Find<SmtpSendConnectorConfig>(null, QueryScope.SubTree, null, null, -1);
			foreach (SmtpSendConnectorConfig smtpSendConnectorConfig in array)
			{
				if (smtpSendConnectorConfig != null && smtpSendConnectorConfig.SourceTransportServers != null)
				{
					MultiValuedProperty<ADObjectId> sourceTransportServers = smtpSendConnectorConfig.SourceTransportServers;
					ADObjectId adobjectId = null;
					foreach (ADObjectId adobjectId2 in sourceTransportServers)
					{
						if (adobjectId2.Name == server.Id.Name)
						{
							if (smtpSendConnectorConfig.SourceTransportServers.Count == 1)
							{
								scSession.Delete(smtpSendConnectorConfig);
							}
							else
							{
								adobjectId = adobjectId2;
							}
						}
					}
					if (adobjectId != null)
					{
						sourceTransportServers.Remove(adobjectId);
						smtpSendConnectorConfig.SourceTransportServers = sourceTransportServers;
						scSession.Save(smtpSendConnectorConfig);
					}
				}
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00175450 File Offset: 0x00173650
		private void RestoreTransportSettings(IConfigurationSession scSession)
		{
			ADPagedReader<TransportConfigContainer> adpagedReader = scSession.FindPaged<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 0);
			foreach (TransportConfigContainer transportConfigContainer in adpagedReader)
			{
				transportConfigContainer.TLSSendDomainSecureList = null;
				transportConfigContainer.TLSReceiveDomainSecureList = null;
				transportConfigContainer.InternalSMTPServers = null;
				scSession.Save(transportConfigContainer);
			}
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x001754CC File Offset: 0x001736CC
		private void RemoveEdgeSyncedHubServerObjects(ITopologyConfigurationSession scSession)
		{
			ADPagedReader<Server> adpagedReader = scSession.FindAllServersWithVersionNumber(Server.E2007MinVersion);
			foreach (Server server in adpagedReader)
			{
				if (server.IsHubTransportServer)
				{
					scSession.DeleteTree(server, delegate(ADTreeDeleteNotFinishedException de)
					{
						base.WriteVerbose(de.LocalizedString);
					});
				}
			}
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0017553C File Offset: 0x0017373C
		private void RemoveAllEdgeSyncedRecipients()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 324, "RemoveAllEdgeSyncedRecipients", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\RemoveEdgeSubscription.cs");
			ADPagedReader<ADRecipient> adpagedReader = tenantOrRootOrgRecipientSession.FindPaged(null, QueryScope.SubTree, new TextFilter(ADRecipientSchema.EmailAddresses, "sh:", MatchOptions.Prefix, MatchFlags.IgnoreCase), null, 0);
			foreach (ADRecipient instanceToDelete in adpagedReader)
			{
				tenantOrRootOrgRecipientSession.Delete(instanceToDelete);
			}
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x001755D4 File Offset: 0x001737D4
		private void RemoveEdgeSyncedObjects<T>(IConfigurationSession scSession) where T : ADConfigurationObject, new()
		{
			ADPagedReader<T> adpagedReader = scSession.FindPaged<T>(null, QueryScope.SubTree, null, null, 0);
			foreach (T t in adpagedReader)
			{
				scSession.DeleteTree(t, delegate(ADTreeDeleteNotFinishedException de)
				{
					base.WriteVerbose(de.LocalizedString);
				});
			}
		}

		// Token: 0x0400330A RID: 13066
		private SwitchParameter force;
	}
}
