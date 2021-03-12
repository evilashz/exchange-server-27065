using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BD RID: 445
	internal class ProxyHubSelector : IProxyHubSelector
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x00052770 File Offset: 0x00050970
		public ProxyHubSelector(IMailRouter router, OrganizationMailboxDatabaseCache orgMailboxCache, ProxyHubSelectorPerformanceCounters perfCounters, TransportAppConfig.RoutingConfig routingConfig)
		{
			if (router == null)
			{
				throw new ArgumentNullException("router");
			}
			if (perfCounters == null)
			{
				throw new ArgumentNullException("perfCounters");
			}
			if (routingConfig == null)
			{
				throw new ArgumentNullException("routingConfig");
			}
			this.router = router;
			this.orgMailboxCache = orgMailboxCache;
			this.perfCounters = perfCounters;
			this.routingConfig = routingConfig;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000527CC File Offset: 0x000509CC
		public bool TrySelectHubServersForClientProxy(MiniRecipient recipient, out IEnumerable<INextHopServer> hubServers)
		{
			hubServers = null;
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SelectHubServersForClientProxy.Enabled)
			{
				throw new NotSupportedException("This method is not enabled in the current environment");
			}
			this.perfCounters.IncrementHubSelectionRequestsTotal();
			IList<ADObjectId> databaseIds = null;
			if (recipient.Database != null)
			{
				databaseIds = new ADObjectId[]
				{
					recipient.Database
				};
			}
			else
			{
				this.perfCounters.IncrementFallbackRoutingRequests();
			}
			if (!this.router.TrySelectHubServersForDatabases(databaseIds, null, out hubServers))
			{
				this.perfCounters.IncrementRoutingFailures();
				return false;
			}
			return true;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00052868 File Offset: 0x00050A68
		public bool TrySelectHubServers(IReadOnlyMailItem mailItem, out IEnumerable<INextHopServer> hubServers)
		{
			hubServers = null;
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (mailItem.ADRecipientCache == null)
			{
				throw new InvalidOperationException("TrySelectHubServers() must not be invoked before the mail item is attributed and assigned a properly-scoped recipient cache");
			}
			if (mailItem.OrganizationId == null)
			{
				throw new InvalidOperationException("TrySelectHubServers() must not be invoked before the mail item is attributed and assigned a proper OrganizationId");
			}
			this.perfCounters.IncrementHubSelectionRequestsTotal();
			bool flag = false;
			if (this.routingConfig.DagSelectorEnabled && this.router.IsJournalMessage(mailItem))
			{
				if (this.TryRouteUsingDagSelector(mailItem, out hubServers))
				{
					return true;
				}
				flag = true;
			}
			IList<ADObjectId> list = this.GetRecipientDatabases(mailItem);
			if (list == null || list.Count == 0)
			{
				if (!flag && this.routingConfig.DagSelectorEnabled && this.TryRouteUsingDagSelector(mailItem, out hubServers))
				{
					return true;
				}
				list = this.GetOrganizationMailboxDatabases(mailItem);
				if (list == null || list.Count == 0)
				{
					this.perfCounters.IncrementFallbackRoutingRequests();
				}
			}
			if (!this.router.TrySelectHubServersForDatabases(list, new Guid?(mailItem.ExternalOrganizationId), out hubServers))
			{
				ProxyHubSelectorComponent.SystemProbeTracer.TraceFail<string>(mailItem, 0L, "Failed to select any Hub servers for mail item with message id <{0}>", mailItem.InternetMessageId);
				this.perfCounters.IncrementRoutingFailures();
				return false;
			}
			return true;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00052978 File Offset: 0x00050B78
		private static void AddRoutedUsingDagSelectorHeader(IReadOnlyMailItem mailItem)
		{
			Header newChild = Header.Create("X-MS-Exchange-Organization-RoutedUsingDagSelector");
			mailItem.RootPart.Headers.AppendChild(newChild);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000529A2 File Offset: 0x00050BA2
		private bool TryRouteUsingDagSelector(IReadOnlyMailItem mailItem, out IEnumerable<INextHopServer> hubServers)
		{
			if (this.router.TrySelectHubServersUsingDagSelector(mailItem.ExternalOrganizationId, out hubServers))
			{
				this.perfCounters.IncrementMessagesRoutedUsingDagSelector();
				ProxyHubSelector.AddRoutedUsingDagSelectorHeader(mailItem);
				return true;
			}
			hubServers = null;
			return false;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000529D0 File Offset: 0x00050BD0
		private static IList<ADObjectId> GetDatabasesOfResolvedRecipients(IList<ProxyAddress> recipientAddresses, IList<Result<TransportMiniRecipient>> resolvedRecipients, IReadOnlyMailItem mailItem)
		{
			if (recipientAddresses.Count != resolvedRecipients.Count)
			{
				throw new InvalidOperationException(string.Format("Number of requested recipients ({0}) does not match number of results ({1})", recipientAddresses.Count, resolvedRecipients.Count));
			}
			HashSet<ADObjectId> hashSet = new HashSet<ADObjectId>();
			for (int i = 0; i < resolvedRecipients.Count; i++)
			{
				if (resolvedRecipients[i].Data != null)
				{
					ADObjectId database = resolvedRecipients[i].Data.Database;
					if (database != null)
					{
						hashSet.Add(database);
					}
				}
				else if (resolvedRecipients[i].Error != ProviderError.NotFound)
				{
					ProxyHubSelectorComponent.SystemProbeTracer.TraceFail<ProxyAddress, object>(mailItem, 0L, "Failed to resolve recipient <{0}> in AD; error: {1}", recipientAddresses[i], resolvedRecipients[i].Error ?? "<none>");
				}
			}
			ADObjectId[] array = new ADObjectId[hashSet.Count];
			hashSet.CopyTo(array);
			return array;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00052AE8 File Offset: 0x00050CE8
		private IList<ADObjectId> GetRecipientDatabases(IReadOnlyMailItem mailItem)
		{
			int num = Math.Min(mailItem.Recipients.Count, ADRecipientCache<TransportMiniRecipient>.BatchSize);
			List<ProxyAddress> recipientAddresses = new List<ProxyAddress>(num);
			foreach (MailRecipient mailRecipient in mailItem.Recipients.AllUnprocessed)
			{
				recipientAddresses.Add(new SmtpProxyAddress(mailRecipient.Email.ToString(), false));
				if (recipientAddresses.Count == num)
				{
					break;
				}
			}
			IList<Result<TransportMiniRecipient>> recipientResults = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				recipientResults = mailItem.ADRecipientCache.FindAndCacheRecipients(recipientAddresses);
			}, 0);
			if (!adoperationResult.Succeeded)
			{
				ProxyHubSelectorComponent.SystemProbeTracer.TraceFail(mailItem, 0L, "Failed to resolve recipients in AD; exception: {0}", new object[]
				{
					adoperationResult.Exception ?? "<none>"
				});
				this.perfCounters.IncrementResolverFailures();
				return null;
			}
			IList<ADObjectId> databasesOfResolvedRecipients = ProxyHubSelector.GetDatabasesOfResolvedRecipients(recipientAddresses, recipientResults, mailItem);
			if (databasesOfResolvedRecipients.Count == 0)
			{
				ProxyHubSelectorComponent.SystemProbeTracer.TracePass<string>(mailItem, 0L, "Mail item with message id <{0}> does not contain any mailbox recipients", mailItem.InternetMessageId);
				this.perfCounters.IncrementMessagesWithoutMailboxRecipients();
			}
			return databasesOfResolvedRecipients;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00052C68 File Offset: 0x00050E68
		private IList<ADObjectId> GetOrganizationMailboxDatabases(IReadOnlyMailItem mailItem)
		{
			if (this.orgMailboxCache == null)
			{
				return null;
			}
			IList<ADObjectId> list;
			if (!this.orgMailboxCache.TryGetOrganizationMailboxDatabases(mailItem.OrganizationId, out list))
			{
				ProxyHubSelectorComponent.SystemProbeTracer.TraceFail<OrganizationId, string>(mailItem, 0L, "Failed to obtain Organization Mailbox Databases for organization <{0}>; message id {1}", mailItem.OrganizationId, mailItem.InternetMessageId);
				this.perfCounters.IncrementOrganizationMailboxFailures();
				return null;
			}
			if (list == null || list.Count == 0)
			{
				ProxyHubSelectorComponent.SystemProbeTracer.TraceFail<OrganizationId, string>(mailItem, 0L, "Organization <{0}> does not contain any tenant mailboxes; message id {1}", mailItem.OrganizationId, mailItem.InternetMessageId);
				this.perfCounters.IncrementMessagesWithoutOrganizationMailboxes();
			}
			else
			{
				ProxyHubSelectorComponent.Tracer.TraceDebug<int, OrganizationId, string>(0L, "Found {0} organization mailboxes for Organization <{1}>; message id {2}", list.Count, mailItem.OrganizationId, mailItem.InternetMessageId);
			}
			return list;
		}

		// Token: 0x04000A5E RID: 2654
		public const string MessageRoutedUsingDagSelectorHeader = "X-MS-Exchange-Organization-RoutedUsingDagSelector";

		// Token: 0x04000A5F RID: 2655
		private IMailRouter router;

		// Token: 0x04000A60 RID: 2656
		private OrganizationMailboxDatabaseCache orgMailboxCache;

		// Token: 0x04000A61 RID: 2657
		private ProxyHubSelectorPerformanceCounters perfCounters;

		// Token: 0x04000A62 RID: 2658
		private TransportAppConfig.RoutingConfig routingConfig;
	}
}
