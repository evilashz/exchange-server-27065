using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.AddressBookPolicyRoutingAgent
{
	// Token: 0x02000002 RID: 2
	internal sealed class AddressBookPolicyRoutingAgent : RoutingAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AddressBookPolicyRoutingAgent(TimeoutCache<Guid, Guid> abpCache, TimeoutCache<OrganizationId, AddressBookBase[]> orgToGalCache, TimeoutCache<ADObjectId, ADMultiValuedProperty<ADObjectId>> userToAddrListCache, TimeSpan cacheTimeout, TimeSpan deferTimeout)
		{
			if (abpCache == null)
			{
				throw new ArgumentNullException("abpCache");
			}
			if (orgToGalCache == null)
			{
				throw new ArgumentNullException("orgToGalCache");
			}
			if (userToAddrListCache == null)
			{
				throw new ArgumentNullException("userToAddrListCache");
			}
			this.cacheExpirationInterval = cacheTimeout;
			this.deferTimeout = deferTimeout;
			this.abpToGalCache = abpCache;
			this.orgToGalCache = orgToGalCache;
			this.userToAddrListCache = userToAddrListCache;
			base.OnResolvedMessage += this.OnResolvedMessageHandler;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002144 File Offset: 0x00000344
		internal static ExEventLog EventLogger
		{
			get
			{
				return AddressBookPolicyRoutingAgent.eventLogger;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000214C File Offset: 0x0000034C
		private void OnResolvedMessageHandler(ResolvedMessageEventSource source, QueuedMessageEventArgs args)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)args.MailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			if (!transportMailItem.TransportSettings.AddressBookPolicyRoutingEnabled)
			{
				return;
			}
			Header header = args.MailItem.Message.RootPart.Headers.FindFirst("X-MS-Exchange-Forest-GAL-Scope");
			if (header != null)
			{
				args.MailItem.Message.RootPart.Headers.RemoveAll("X-MS-Exchange-Forest-GAL-Scope");
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			Guid guid = Guid.Empty;
			Guid guid2 = Guid.Empty;
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>();
			foreach (EnvelopeRecipient envelopeRecipient in args.MailItem.Recipients)
			{
				ADObjectId adobjectId = null;
				object obj;
				if (envelopeRecipient.Properties.TryGetValue("Microsoft.Exchange.Transport.MailRecipient.AddressBookPolicy", out obj) && obj is ADObjectId)
				{
					adobjectId = (ADObjectId)obj;
				}
				Guid empty = Guid.Empty;
				OrganizationId organizationId = transportMailItem.OrganizationIdAsObject as OrganizationId;
				if (!this.TryGetGalForAbp(adobjectId, organizationId, source, out empty))
				{
					return;
				}
				if (empty == Guid.Empty)
				{
					flag3 = true;
				}
				else if (!flag)
				{
					guid2 = empty;
					guid = adobjectId.ObjectGuid;
					flag = true;
					flag4 = this.IsShownInAddressList(envelopeRecipient, guid2, transportMailItem);
					list.Add(envelopeRecipient);
				}
				else if (guid2 == empty)
				{
					bool flag5 = this.IsShownInAddressList(envelopeRecipient, guid2, transportMailItem);
					if (flag5 && flag4)
					{
						list.Add(envelopeRecipient);
					}
					else if (!flag5 && !flag4)
					{
						if (this.IsVisibleToAllRecipients(envelopeRecipient, list, transportMailItem))
						{
							list.Add(envelopeRecipient);
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = true;
				}
			}
			if (list.Count != 0)
			{
				if (flag2 || flag3)
				{
					source.Fork(list);
				}
				args.MailItem.Message.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Forest-GAL-Scope", guid2.ToString()));
				args.MailItem.Message.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-ABP-GUID", guid.ToString()));
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023EC File Offset: 0x000005EC
		private bool TryGetGalForAbp(ADObjectId policyId, OrganizationId organizationId, ResolvedMessageEventSource source, out Guid galGuid)
		{
			galGuid = Guid.Empty;
			if (policyId == null)
			{
				return true;
			}
			if (this.abpToGalCache.TryGetValue(policyId.ObjectGuid, out galGuid))
			{
				return true;
			}
			AddressBookMailboxPolicy addressBookPolicy = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 284, "TryGetGalForAbp", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\AddressBookPolicyRoutingAgent\\AddressBookPolicyRoutingAgent.cs");
				addressBookPolicy = tenantOrTopologyConfigurationSession.Read<AddressBookMailboxPolicy>(policyId);
			});
			if (adoperationResult.ErrorCode == ADOperationErrorCode.Success && addressBookPolicy != null)
			{
				galGuid = addressBookPolicy.GlobalAddressList.ObjectGuid;
				if (galGuid != Guid.Empty)
				{
					this.abpToGalCache.InsertAbsolute(policyId.ObjectGuid, galGuid, this.cacheExpirationInterval, null);
				}
				return true;
			}
			source.Defer(this.deferTimeout, AddressBookPolicyRoutingAgent.FailedToRetrieveAddressBookPolicyResponse);
			AddressBookPolicyRoutingAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_AddressBookPolicyLoadingError, policyId.ToString(), new object[]
			{
				policyId.ToString(),
				organizationId.ToString(),
				adoperationResult.ErrorCode,
				(adoperationResult.Exception != null) ? adoperationResult.Exception.ToString() : string.Empty
			});
			return false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000025D8 File Offset: 0x000007D8
		private AddressBookBase[] GetGlobalAddressLists(OrganizationId organizationId)
		{
			AddressBookBase[] result;
			if (this.orgToGalCache.TryGetValue(organizationId, out result))
			{
				return result;
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 339, "GetGlobalAddressLists", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\AddressBookPolicyRoutingAgent\\AddressBookPolicyRoutingAgent.cs");
				ADObjectId adobjectId;
				if (organizationId == null || organizationId.ConfigurationUnit == null)
				{
					adobjectId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
				}
				else
				{
					adobjectId = organizationId.ConfigurationUnit;
				}
				ADObjectId descendantId = adobjectId.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization);
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsDefaultGlobalAddressList, false);
				result = tenantOrTopologyConfigurationSession.Find<AddressBookBase>(descendantId, QueryScope.SubTree, filter, null, 0);
			});
			if (adoperationResult.ErrorCode == ADOperationErrorCode.Success && result != null)
			{
				this.orgToGalCache.InsertAbsolute(organizationId, result, this.cacheExpirationInterval, null);
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002658 File Offset: 0x00000858
		private bool IsShownInAddressList(EnvelopeRecipient envelopeRecipient, Guid galGuid, ITransportMailItemFacade tmi)
		{
			TransportMiniRecipient transportMiniRecipient = this.ExtractTransportMiniRecipient(envelopeRecipient, tmi);
			if (transportMiniRecipient != null)
			{
				OrganizationId organizationId = tmi.OrganizationIdAsObject as OrganizationId;
				ADMultiValuedProperty<ADObjectId> recipientAddressListMembership = this.GetRecipientAddressListMembership(transportMiniRecipient, organizationId);
				if (recipientAddressListMembership != null)
				{
					foreach (ADObjectId adobjectId in recipientAddressListMembership)
					{
						if (adobjectId.ObjectGuid == galGuid)
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000026DC File Offset: 0x000008DC
		private bool IsVisibleToAllRecipients(EnvelopeRecipient envelopeRecipient, List<EnvelopeRecipient> selectedRecipients, ITransportMailItemFacade tmi)
		{
			TransportMiniRecipient transportMiniRecipient = this.ExtractTransportMiniRecipient(envelopeRecipient, tmi);
			if (transportMiniRecipient == null)
			{
				return false;
			}
			OrganizationId organizationId = tmi.OrganizationIdAsObject as OrganizationId;
			ADMultiValuedProperty<ADObjectId> recipientGalMembership = this.GetRecipientGalMembership(transportMiniRecipient, organizationId);
			foreach (EnvelopeRecipient envelopeRecipient2 in selectedRecipients)
			{
				TransportMiniRecipient transportMiniRecipient2 = this.ExtractTransportMiniRecipient(envelopeRecipient2, tmi);
				if (transportMiniRecipient2 != null)
				{
					ADMultiValuedProperty<ADObjectId> recipientGalMembership2 = this.GetRecipientGalMembership(transportMiniRecipient2, organizationId);
					if (!recipientGalMembership.Intersect(recipientGalMembership2).Any<ADObjectId>())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000027DC File Offset: 0x000009DC
		private ADMultiValuedProperty<ADObjectId> GetRecipientAddressListMembership(TransportMiniRecipient recipient, OrganizationId organizationId)
		{
			ADMultiValuedProperty<ADObjectId> admultiValuedProperty;
			if (this.userToAddrListCache.TryGetValue(recipient.Id, out admultiValuedProperty))
			{
				return admultiValuedProperty;
			}
			ADRawEntry rawEntry = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 448, "GetRecipientAddressListMembership", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\AddressBookPolicyRoutingAgent\\AddressBookPolicyRoutingAgent.cs");
				rawEntry = tenantOrRootOrgRecipientSession.ReadADRawEntry(recipient.Id, new PropertyDefinition[]
				{
					ADRecipientSchema.AddressListMembership
				});
			});
			if (adoperationResult.ErrorCode == ADOperationErrorCode.Success && rawEntry != null)
			{
				admultiValuedProperty = (rawEntry[ADRecipientSchema.AddressListMembership] as ADMultiValuedProperty<ADObjectId>);
				if (admultiValuedProperty != null)
				{
					this.userToAddrListCache.InsertAbsolute(recipient.Id, admultiValuedProperty, this.cacheExpirationInterval, null);
				}
			}
			return admultiValuedProperty;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000287C File Offset: 0x00000A7C
		private ADMultiValuedProperty<ADObjectId> GetRecipientGalMembership(TransportMiniRecipient recipient, OrganizationId organizationId)
		{
			ADMultiValuedProperty<ADObjectId> admultiValuedProperty = new ADMultiValuedProperty<ADObjectId>();
			AddressBookBase[] globalAddressLists = this.GetGlobalAddressLists(organizationId);
			if (globalAddressLists != null && globalAddressLists.Length > 0)
			{
				ADMultiValuedProperty<ADObjectId> recipientAddressListMembership = this.GetRecipientAddressListMembership(recipient, organizationId);
				if (recipientAddressListMembership != null)
				{
					foreach (ADObjectId adobjectId in recipientAddressListMembership)
					{
						foreach (AddressBookBase addressBookBase in globalAddressLists)
						{
							if (addressBookBase.Id != null && addressBookBase.Id.ObjectGuid == adobjectId.ObjectGuid)
							{
								admultiValuedProperty.Add(adobjectId);
							}
						}
					}
				}
			}
			return admultiValuedProperty;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002934 File Offset: 0x00000B34
		private TransportMiniRecipient ExtractTransportMiniRecipient(EnvelopeRecipient envelopeRecipient, ITransportMailItemFacade tmi)
		{
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = tmi.ADRecipientCacheAsObject as ADRecipientCache<TransportMiniRecipient>;
			Result<TransportMiniRecipient> result;
			if (adrecipientCache != null && adrecipientCache.TryGetValue(new SmtpProxyAddress(envelopeRecipient.Address.ToString(), true), out result))
			{
				return result.Data;
			}
			return null;
		}

		// Token: 0x04000001 RID: 1
		private static readonly SmtpResponse FailedToRetrieveAddressBookPolicyResponse = new SmtpResponse("452", "4.3.2", new string[]
		{
			"Failed to retrieve Address Book Policy"
		});

		// Token: 0x04000002 RID: 2
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.AddressBookPolicyRoutingAgentTracer.Category, "MSExchange Antispam");

		// Token: 0x04000003 RID: 3
		private readonly TimeSpan cacheExpirationInterval;

		// Token: 0x04000004 RID: 4
		private readonly TimeSpan deferTimeout;

		// Token: 0x04000005 RID: 5
		private readonly TimeoutCache<Guid, Guid> abpToGalCache;

		// Token: 0x04000006 RID: 6
		private readonly TimeoutCache<OrganizationId, AddressBookBase[]> orgToGalCache;

		// Token: 0x04000007 RID: 7
		private readonly TimeoutCache<ADObjectId, ADMultiValuedProperty<ADObjectId>> userToAddrListCache;
	}
}
