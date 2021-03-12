using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B33 RID: 2867
	internal static class ManageSendConnectors
	{
		// Token: 0x0600670B RID: 26379 RVA: 0x001AA554 File Offset: 0x001A8754
		public static LocalizedException ValidateTransportServers(IConfigurationSession session, SendConnector sendConnector, ref ADObjectId routingGroupId, Task task, bool sourceValidation, out bool multiSiteConnector)
		{
			bool flag;
			return ManageSendConnectors.ValidateTransportServers(session, sendConnector, ref routingGroupId, false, sourceValidation, task, out flag, out multiSiteConnector);
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x001AA574 File Offset: 0x001A8774
		public static LocalizedException ValidateTransportServers(IConfigurationSession session, SendConnector connector, ref ADObjectId routingGroupId, bool allowEdgeServers, bool sourceValidation, Task task, out bool edgeConnector, out bool multiSiteConnector)
		{
			edgeConnector = false;
			multiSiteConnector = false;
			MultiValuedProperty<ADObjectId> sourceTransportServers = connector.SourceTransportServers;
			if (sourceTransportServers != null && sourceTransportServers.Count != 0)
			{
				bool flag = false;
				ADObjectId adobjectId = null;
				MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				List<string> list = new List<string>();
				foreach (ADObjectId adobjectId2 in sourceTransportServers)
				{
					if (adobjectId2 != null)
					{
						Server server = session.Read<Server>(adobjectId2);
						if (server == null)
						{
							list.Add(adobjectId2.Name);
						}
						else
						{
							if (adobjectId == null)
							{
								if (routingGroupId == null)
								{
									routingGroupId = ManageSendConnectors.GetServerRoutingGroup(server);
									if (routingGroupId == null)
									{
										return new SendConnectorUndefinedServerRgException(adobjectId2.Name);
									}
								}
								flag = ManageSendConnectors.IsE12RoutingGroup(routingGroupId);
								if (allowEdgeServers)
								{
									edgeConnector = server.IsEdgeServer;
								}
								adobjectId = server.ServerSite;
							}
							else if (!multiSiteConnector)
							{
								multiSiteConnector = !adobjectId.Equals(server.ServerSite);
							}
							if (!routingGroupId.Equals(server.HomeRoutingGroup))
							{
								bool flag2 = false;
								if (!flag)
								{
									flag2 = true;
								}
								else if (server.HomeRoutingGroup != null)
								{
									flag2 = true;
								}
								else if (!server.IsExchange2007OrLater)
								{
									flag2 = true;
								}
								if (flag2)
								{
									return sourceValidation ? new SendConnectorWrongSourceServerRgException(adobjectId2.Name) : new SendConnectorWrongTargetServerRgException(adobjectId2.Name);
								}
							}
							if (flag)
							{
								if ((!server.IsEdgeServer || !allowEdgeServers) && !server.IsHubTransportServer)
								{
									return sourceValidation ? new SendConnectorWrongSourceServerRoleException(adobjectId2.Name) : new SendConnectorWrongTargetServerRoleException(adobjectId2.Name);
								}
								if (edgeConnector != server.IsEdgeServer)
								{
									return new SendConnectorMixedSourceServerRolesException();
								}
							}
							multiValuedProperty.Add(adobjectId2);
						}
					}
				}
				if (multiValuedProperty.Count != 0)
				{
					if (multiValuedProperty.Count != connector.SourceTransportServers.Count)
					{
						connector.SourceTransportServers = multiValuedProperty;
						if (task != null)
						{
							task.WriteWarning(Strings.WarningSourceServersSkipped(string.Join(", ", list)));
						}
					}
					return null;
				}
				if (!sourceValidation)
				{
					return new SendConnectorValidTargetServerNotFoundException();
				}
				return new SendConnectorValidSourceServerNotFoundException();
			}
			if (!sourceValidation)
			{
				return new SendConnectorTargetServersNotSetException();
			}
			return new SendConnectorSourceServersNotSetException();
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x001AA79C File Offset: 0x001A899C
		public static ADObjectId GetServerRoutingGroup(Server server)
		{
			ADObjectId adobjectId = server.HomeRoutingGroup;
			if (adobjectId == null && server.IsExchange2007OrLater)
			{
				ADObjectId parent = server.Id.Parent.Parent;
				adobjectId = parent.GetChildId("Routing Groups").GetChildId(RoutingGroup.DefaultName);
			}
			return adobjectId;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x001AA7E3 File Offset: 0x001A89E3
		public static bool IsE12RoutingGroup(ADObjectId routingGroupId)
		{
			return RoutingGroup.DefaultName.Equals(routingGroupId.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x001AA7F8 File Offset: 0x001A89F8
		public static void SetConnectorId(SendConnector connector, ADObjectId sourceRgId)
		{
			connector.SetId(sourceRgId.GetChildId("Connections").GetChildId(connector.Name));
			TaskLogger.Trace("Set connector ID to '{0}'", new object[]
			{
				connector.Id
			});
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x001AA83C File Offset: 0x001A8A3C
		public static void SetConnectorHomeMta(SendConnector connector, IConfigurationSession configSession)
		{
			Server server = configSession.Read<Server>(connector.SourceTransportServers[0]);
			connector.HomeMTA = server.ResponsibleMTA;
			TaskLogger.Trace("Set connector homeMTA to '{0}'", new object[]
			{
				connector.HomeMTA
			});
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x001AA884 File Offset: 0x001A8A84
		public static void AddServersToGroup(IList<ADObjectId> serverIds, Guid groupGuid, ITopologyConfigurationSession configSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			IRecipientSession recipSession;
			ITopologyConfigurationSession gcSession;
			ADGroup wellKnownGroup = ManageSendConnectors.GetWellKnownGroup(groupGuid, configSession, throwDelegate, out recipSession, out gcSession);
			ManageSendConnectors.AddServersToGroup(serverIds, wellKnownGroup, recipSession, gcSession, throwDelegate);
			TaskLogger.LogExit();
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x001AA8B4 File Offset: 0x001A8AB4
		public static void AddServersToGroup(IList<ADObjectId> serverIds, ADGroup group, IRecipientSession recipSession, ITopologyConfigurationSession gcSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			if (serverIds.Count == 0)
			{
				TaskLogger.LogExit();
				return;
			}
			foreach (ADObjectId adobjectId in serverIds)
			{
				ADComputer computerObject = ManageSendConnectors.GetComputerObject(adobjectId.Name, gcSession, throwDelegate);
				if (!group.Members.Contains(computerObject.Id))
				{
					TaskLogger.Trace("Adding server '{0}' to group '{1}'", new object[]
					{
						adobjectId.Name,
						group.Name
					});
					group.Members.Add(computerObject.Id);
				}
			}
			recipSession.Save(group);
			TaskLogger.LogExit();
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x001AA96C File Offset: 0x001A8B6C
		public static void RemoveServersFromGroup(IList<ADObjectId> serverIds, Guid groupGuid, ITopologyConfigurationSession configSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			IRecipientSession recipSession;
			ITopologyConfigurationSession gcSession;
			ADGroup wellKnownGroup = ManageSendConnectors.GetWellKnownGroup(groupGuid, configSession, throwDelegate, out recipSession, out gcSession);
			ManageSendConnectors.RemoveServersFromGroup(serverIds, wellKnownGroup, recipSession, gcSession, throwDelegate);
			TaskLogger.LogExit();
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x001AA99C File Offset: 0x001A8B9C
		public static void RemoveServersFromGroup(IList<ADObjectId> serverIds, ADGroup group, IRecipientSession recipSession, ITopologyConfigurationSession gcSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			if (serverIds.Count == 0)
			{
				TaskLogger.LogExit();
				return;
			}
			foreach (ADObjectId adobjectId in serverIds)
			{
				ADComputer computerObject = ManageSendConnectors.GetComputerObject(adobjectId.Name, gcSession, throwDelegate);
				if (group.Members.Contains(computerObject.Id))
				{
					TaskLogger.Trace("Removing server '{0}' from group '{1}'", new object[]
					{
						adobjectId.Name,
						group.Name
					});
					group.Members.Remove(computerObject.Id);
				}
			}
			recipSession.Save(group);
			TaskLogger.LogExit();
		}

		// Token: 0x06006715 RID: 26389 RVA: 0x001AAA54 File Offset: 0x001A8C54
		public static ADGroup GetWellKnownGroup(Guid groupGuid, IConfigurationSession configSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate, out IRecipientSession recipSession, out ITopologyConfigurationSession gcSession)
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = null;
			recipSession = null;
			recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 530, "GetWellKnownGroup", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\ManageSendConnectors.cs");
			try
			{
				recipSession.UseGlobalCatalog = true;
				adgroup = recipSession.ResolveWellKnownGuid<ADGroup>(groupGuid, configSession.ConfigurationNamingContext);
				recipSession.UseGlobalCatalog = false;
				adgroup = (ADGroup)recipSession.Read(adgroup.Id);
			}
			finally
			{
				recipSession.UseGlobalCatalog = false;
			}
			if (adgroup == null)
			{
				try
				{
					ADDomain addomain = ADForest.GetLocalForest().FindRootDomain(true);
					if (addomain != null)
					{
						recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(addomain.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 564, "GetWellKnownGroup", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\ManageSendConnectors.cs");
						adgroup = recipSession.ResolveWellKnownGuid<ADGroup>(groupGuid, configSession.ConfigurationNamingContext);
					}
				}
				catch (ADReferralException)
				{
				}
			}
			if (adgroup == null)
			{
				throwDelegate(new ErrorExchangeGroupNotFoundException(groupGuid), ErrorCategory.ObjectNotFound, null);
			}
			gcSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 589, "GetWellKnownGroup", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\ManageSendConnectors.cs");
			gcSession.UseConfigNC = false;
			gcSession.UseGlobalCatalog = true;
			TaskLogger.LogExit();
			return adgroup;
		}

		// Token: 0x06006716 RID: 26390 RVA: 0x001AAB84 File Offset: 0x001A8D84
		public static IList<RoutingGroupConnector> GetAlternateRgcs(RoutingGroupConnector connectorBeingProcessed, IConfigurationSession session, out RoutingGroupConnector savedConnector)
		{
			TaskLogger.Trace("Reading alternate RGCs for RG '{0}'", new object[]
			{
				connectorBeingProcessed.SourceRoutingGroup
			});
			ADPagedReader<RoutingGroupConnector> adpagedReader = session.FindPaged<RoutingGroupConnector>(connectorBeingProcessed.SourceRoutingGroup, QueryScope.SubTree, null, null, ADGenericPagedReader<RoutingGroupConnector>.DefaultPageSize);
			savedConnector = null;
			IList<RoutingGroupConnector> list = new List<RoutingGroupConnector>();
			foreach (RoutingGroupConnector routingGroupConnector in adpagedReader)
			{
				if (connectorBeingProcessed.TargetRoutingGroup.Equals(routingGroupConnector.TargetRoutingGroup))
				{
					if (connectorBeingProcessed.Id.Equals(routingGroupConnector.Id))
					{
						savedConnector = routingGroupConnector;
					}
					else
					{
						list.Add(routingGroupConnector);
					}
				}
			}
			TaskLogger.Trace("Found {0} alternate RGCs", new object[]
			{
				list.Count
			});
			return list;
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x001AAC5C File Offset: 0x001A8E5C
		public static void PruneServerList(IList<ADObjectId> serverIds, IList<RoutingGroupConnector> alternateConnectors)
		{
			TaskLogger.LogEnter();
			int i = 0;
			while (i < serverIds.Count)
			{
				bool flag = false;
				foreach (RoutingGroupConnector routingGroupConnector in alternateConnectors)
				{
					if (routingGroupConnector.SourceTransportServers.Contains(serverIds[i]))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					TaskLogger.Trace("Removing server '{0}' from the list", new object[]
					{
						serverIds[i].Name
					});
					serverIds.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x001AAD04 File Offset: 0x001A8F04
		public static void UpdateGwartLastModified(ITopologyConfigurationSession configSession, ADObjectId sourceRoutingGroup, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			if (ManageSendConnectors.IsE12RoutingGroup(sourceRoutingGroup))
			{
				configSession.UpdateGwartLastModified();
				TaskLogger.Trace("Updated E12 Legacy GWART {0}", new object[]
				{
					LegacyGwart.DefaultName
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x001AAD44 File Offset: 0x001A8F44
		internal static void AdjustAddressSpaces(MailGateway connector)
		{
			if (MultiValuedPropertyBase.IsNullOrEmpty(connector.AddressSpaces))
			{
				return;
			}
			MultiValuedProperty<AddressSpace> multiValuedProperty = new MultiValuedProperty<AddressSpace>();
			foreach (AddressSpace addressSpace in connector.AddressSpaces)
			{
				if (addressSpace.IsLocal != connector.IsScopedConnector)
				{
					multiValuedProperty.Add(addressSpace);
				}
			}
			if (multiValuedProperty.Count > 0)
			{
				foreach (AddressSpace addressSpace2 in multiValuedProperty)
				{
					AddressSpace item = addressSpace2.ToggleScope();
					connector.AddressSpaces.Remove(addressSpace2);
					connector.AddressSpaces.Add(item);
				}
			}
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x001AAE1C File Offset: 0x001A901C
		private static ADComputer GetComputerObject(string name, ITopologyConfigurationSession gcSession, ManageSendConnectors.ThrowTerminatingErrorDelegate throwDelegate)
		{
			TaskLogger.LogEnter();
			ADComputer adcomputer = gcSession.FindComputerByHostName(name);
			if (adcomputer == null)
			{
				throwDelegate(new SendConnectorComputerNotFoundException(name), ErrorCategory.ObjectNotFound, null);
			}
			TaskLogger.LogExit();
			return adcomputer;
		}

		// Token: 0x02000B34 RID: 2868
		// (Invoke) Token: 0x0600671C RID: 26396
		public delegate void ThrowTerminatingErrorDelegate(Exception exception, ErrorCategory category, object target);

		// Token: 0x02000B35 RID: 2869
		// (Invoke) Token: 0x06006720 RID: 26400
		public delegate IConfigurable GetServerDelegate<Server>(ADIdParameter dataObjectId, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError);
	}
}
