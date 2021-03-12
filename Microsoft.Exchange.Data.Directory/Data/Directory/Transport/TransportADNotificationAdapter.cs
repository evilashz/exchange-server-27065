using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Transport
{
	// Token: 0x02000A0C RID: 2572
	internal class TransportADNotificationAdapter
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060076D7 RID: 30423 RVA: 0x00187624 File Offset: 0x00185824
		// (remove) Token: 0x060076D8 RID: 30424 RVA: 0x0018765C File Offset: 0x0018585C
		private event ADNotificationCallback AcceptedDomainDeleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060076D9 RID: 30425 RVA: 0x00187694 File Offset: 0x00185894
		// (remove) Token: 0x060076DA RID: 30426 RVA: 0x001876CC File Offset: 0x001858CC
		private event ADNotificationCallback ADSiteLinkDeleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060076DB RID: 30427 RVA: 0x00187704 File Offset: 0x00185904
		// (remove) Token: 0x060076DC RID: 30428 RVA: 0x0018773C File Offset: 0x0018593C
		private event ADNotificationCallback DatabaseDeleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060076DD RID: 30429 RVA: 0x00187774 File Offset: 0x00185974
		// (remove) Token: 0x060076DE RID: 30430 RVA: 0x001877AC File Offset: 0x001859AC
		private event ADNotificationCallback DeliveryAgentConnectorDeleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060076DF RID: 30431 RVA: 0x001877E4 File Offset: 0x001859E4
		// (remove) Token: 0x060076E0 RID: 30432 RVA: 0x0018781C File Offset: 0x00185A1C
		private event ADNotificationCallback ForeignConnectorDeleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060076E1 RID: 30433 RVA: 0x00187854 File Offset: 0x00185A54
		// (remove) Token: 0x060076E2 RID: 30434 RVA: 0x0018788C File Offset: 0x00185A8C
		private event ADNotificationCallback JournalRuleDeleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060076E3 RID: 30435 RVA: 0x001878C4 File Offset: 0x00185AC4
		// (remove) Token: 0x060076E4 RID: 30436 RVA: 0x001878FC File Offset: 0x00185AFC
		private event ADNotificationCallback ReceiveConnectorDeleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060076E5 RID: 30437 RVA: 0x00187934 File Offset: 0x00185B34
		// (remove) Token: 0x060076E6 RID: 30438 RVA: 0x0018796C File Offset: 0x00185B6C
		private event ADNotificationCallback RemoteDomainDeleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060076E7 RID: 30439 RVA: 0x001879A4 File Offset: 0x00185BA4
		// (remove) Token: 0x060076E8 RID: 30440 RVA: 0x001879DC File Offset: 0x00185BDC
		private event ADNotificationCallback SmtpSendConnectorDeleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060076E9 RID: 30441 RVA: 0x00187A14 File Offset: 0x00185C14
		// (remove) Token: 0x060076EA RID: 30442 RVA: 0x00187A4C File Offset: 0x00185C4C
		private event ADNotificationCallback TransportRuleDeleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060076EB RID: 30443 RVA: 0x00187A84 File Offset: 0x00185C84
		// (remove) Token: 0x060076EC RID: 30444 RVA: 0x00187ABC File Offset: 0x00185CBC
		private event ADNotificationCallback ExchangeServerDeleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060076ED RID: 30445 RVA: 0x00187AF4 File Offset: 0x00185CF4
		// (remove) Token: 0x060076EE RID: 30446 RVA: 0x00187B2C File Offset: 0x00185D2C
		private event ADNotificationCallback InterceptorRuleDeleted;

		// Token: 0x060076EF RID: 30447 RVA: 0x00187B64 File Offset: 0x00185D64
		private TransportADNotificationAdapter()
		{
			this.needExplicitDeletedObjectSubscription = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.ExplicitDeletedObjectNotifications.Enabled;
			if (!this.NeedExplicitDeletedObjectSubscription)
			{
				return;
			}
			this.ADSiteLinkDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.AcceptedDomainDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.DatabaseDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.DeliveryAgentConnectorDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.ForeignConnectorDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.JournalRuleDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.ReceiveConnectorDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.RemoteDomainDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.SmtpSendConnectorDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.TransportRuleDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.ExchangeServerDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
			this.InterceptorRuleDeleted += TransportADNotificationAdapter.EventLogDeleteNotification;
		}

		// Token: 0x17002A82 RID: 10882
		// (get) Token: 0x060076F0 RID: 30448 RVA: 0x00187C80 File Offset: 0x00185E80
		public static TransportADNotificationAdapter Instance
		{
			get
			{
				return TransportADNotificationAdapter.singletonInstance;
			}
		}

		// Token: 0x060076F1 RID: 30449 RVA: 0x00187C87 File Offset: 0x00185E87
		public static ADNotificationRequestCookie RegisterForNonDeletedNotifications<TConfig>(ADObjectId baseDN, ADNotificationCallback callback) where TConfig : ADConfigurationObject, new()
		{
			return ADNotificationAdapter.RegisterChangeNotification<TConfig>(baseDN, callback);
		}

		// Token: 0x060076F2 RID: 30450 RVA: 0x00187CD4 File Offset: 0x00185ED4
		public static ADOperationResult TryRegisterNotifications(Func<ADObjectId> baseDNGetter, ADNotificationCallback callback, TransportADNotificationAdapter.TransportADNotificationRegister registerDelegate, int retryCount, out ADNotificationRequestCookie cookie)
		{
			ADNotificationRequestCookie adCookie = null;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId root = (baseDNGetter == null) ? null : baseDNGetter();
				adCookie = registerDelegate(root, callback);
			}, retryCount);
			cookie = adCookie;
			return result;
		}

		// Token: 0x060076F3 RID: 30451 RVA: 0x00187D20 File Offset: 0x00185F20
		public void RegisterForMsExchangeTransportServiceDeletedEvents()
		{
			if (!this.NeedExplicitDeletedObjectSubscription)
			{
				return;
			}
			ADObjectId rootOrgContainerIdForLocalForest = TransportADNotificationAdapter.GetRootOrgContainerIdForLocalForest();
			ADObjectId childId = rootOrgContainerIdForLocalForest.GetChildId("Administrative Groups").GetChildId(AdministrativeGroup.DefaultName);
			ADObjectId childId2 = childId.GetChildId(ServersContainer.DefaultName);
			ADObjectId childId3 = childId2.GetChildId(Environment.MachineName).GetChildId(ProtocolsContainer.DefaultName).GetChildId(ReceiveConnector.DefaultName);
			this.RegisterChangeNotificationForDeletedObject<Server>(childId2, new ADNotificationCallback(this.HandleExchangeServerDeleted));
			this.RegisterChangeNotificationForDeletedObject<ReceiveConnector>(childId3, new ADNotificationCallback(this.HandleReceiveConnectorDeleted));
		}

		// Token: 0x060076F4 RID: 30452 RVA: 0x00187DA4 File Offset: 0x00185FA4
		public void RegisterForEdgeTransportEvents()
		{
			if (!this.NeedExplicitDeletedObjectSubscription)
			{
				return;
			}
			ADObjectId rootOrgContainerIdForLocalForest = TransportADNotificationAdapter.GetRootOrgContainerIdForLocalForest();
			ADObjectId childId = rootOrgContainerIdForLocalForest.GetChildId("Administrative Groups").GetChildId(AdministrativeGroup.DefaultName);
			ADObjectId childId2 = rootOrgContainerIdForLocalForest.GetChildId(AcceptedDomain.AcceptedDomainContainer.Parent.Name);
			ADObjectId childId3 = childId.GetChildId(ServersContainer.DefaultName);
			ADObjectId childId4 = childId2.GetChildId(AcceptedDomain.AcceptedDomainContainer.Name);
			ADObjectId childId5 = childId3.GetChildId(Environment.MachineName).GetChildId(ProtocolsContainer.DefaultName).GetChildId(ReceiveConnector.DefaultName);
			ADObjectId childId6 = rootOrgContainerIdForLocalForest.GetChildId("Global Settings").GetChildId("Internet Message Formats");
			ADObjectId childId7 = childId.GetChildId(DatabasesContainer.DefaultName);
			ADObjectId childId8 = childId2.GetChildId("Rules").GetChildId("TransportVersioned");
			ADObjectId childId9 = childId2.GetChildId("Rules").GetChildId("JournalingVersioned");
			ADObjectId descendantId = rootOrgContainerIdForLocalForest.GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			ADObjectId childId10 = ADSession.GetConfigurationNamingContextForLocalForest().GetChildId(SitesContainer.DefaultName);
			ADObjectId childId11 = childId10.GetChildId("Inter-Site Transports").GetChildId("IP");
			ADObjectId childId12 = childId.GetChildId(RoutingGroupsContainer.DefaultName).GetChildId(RoutingGroup.DefaultName).GetChildId("Connections");
			ADObjectId parentContainerId = childId12;
			ADObjectId parentContainerId2 = childId12;
			this.RegisterChangeNotificationForDeletedObject<AcceptedDomain>(childId4, new ADNotificationCallback(this.HandleAcceptedDomainDeleted));
			this.RegisterChangeNotificationForDeletedObject<ADSiteLink>(childId11, new ADNotificationCallback(this.HandleADSiteLinkDeleted));
			this.RegisterChangeNotificationForDeletedObject<DeliveryAgentConnector>(parentContainerId, new ADNotificationCallback(this.HandleDeliveryAgentConnectorDeleted));
			this.RegisterChangeNotificationForDeletedObject<DomainContentConfig>(childId6, new ADNotificationCallback(this.HandleRemoteDomainDeleted));
			this.RegisterChangeNotificationForDeletedObject<ForeignConnector>(parentContainerId2, new ADNotificationCallback(this.HandleForeignConnectorDeleted));
			this.RegisterChangeNotificationForDeletedObject<MailboxDatabase>(childId7, new ADNotificationCallback(this.HandleDatabaseDeleted));
			this.RegisterChangeNotificationForDeletedObject<ReceiveConnector>(childId5, new ADNotificationCallback(this.HandleReceiveConnectorDeleted));
			this.RegisterChangeNotificationForDeletedObject<Server>(childId3, new ADNotificationCallback(this.HandleExchangeServerDeleted));
			this.RegisterChangeNotificationForDeletedObject<SmtpSendConnectorConfig>(childId12, new ADNotificationCallback(this.HandleSmtpSendConnectorDeleted));
			this.RegisterChangeNotificationForDeletedObject<TransportRule>(childId8, new ADNotificationCallback(this.HandleTransportRuleDeleted));
			this.RegisterChangeNotificationForDeletedObject<TransportRule>(childId9, new ADNotificationCallback(this.HandleJournalRuleDeleted));
			this.RegisterChangeNotificationForDeletedObject<InterceptorRule>(descendantId, new ADNotificationCallback(this.HandleInterceptorRuleDeleted));
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x00187FCC File Offset: 0x001861CC
		public void RegisterForSubmissionServiceEvents()
		{
			if (!this.NeedExplicitDeletedObjectSubscription)
			{
				return;
			}
			ADObjectId descendantId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			this.RegisterChangeNotificationForDeletedObject<InterceptorRule>(descendantId, new ADNotificationCallback(this.HandleInterceptorRuleDeleted));
		}

		// Token: 0x060076F6 RID: 30454 RVA: 0x00188008 File Offset: 0x00186208
		public ADNotificationRequestCookie RegisterForExchangeServerNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<Server>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<Server>();
			}
			return result;
		}

		// Token: 0x060076F7 RID: 30455 RVA: 0x0018802C File Offset: 0x0018622C
		public ADNotificationRequestCookie RegisterForLocalServerReceiveConnectorNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<ReceiveConnector>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<ReceiveConnector>();
			}
			return result;
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x00188050 File Offset: 0x00186250
		public ADNotificationRequestCookie RegisterForMailGatewayNotifications(ADObjectId rootId, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<MailGateway>(rootId, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<SmtpSendConnectorConfig>();
				this.VerifyRootWasRegistered<DeliveryAgentConnector>();
				this.VerifyRootWasRegistered<ForeignConnector>();
			}
			return result;
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x00188080 File Offset: 0x00186280
		public ADNotificationRequestCookie RegisterForRemoteDomainNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<DomainContentConfig>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<DomainContentConfig>();
			}
			return result;
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x001880A4 File Offset: 0x001862A4
		public ADNotificationRequestCookie RegisterForTransportRuleNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<TransportRule>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<TransportRule>();
			}
			return result;
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x001880C8 File Offset: 0x001862C8
		public ADNotificationRequestCookie RegisterForADSiteNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			return ADNotificationAdapter.RegisterChangeNotification<ADSite>(baseDN, callback);
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x001880E0 File Offset: 0x001862E0
		public ADNotificationRequestCookie RegisterForADSiteLinkNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<ADSiteLink>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<ADSiteLink>();
			}
			return result;
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x00188104 File Offset: 0x00186304
		public ADNotificationRequestCookie RegisterForDatabaseNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<MailboxDatabase>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<MailboxDatabase>();
			}
			return result;
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x00188128 File Offset: 0x00186328
		public ADNotificationRequestCookie RegisterForAcceptedDomainNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<AcceptedDomain>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<AcceptedDomain>();
			}
			return result;
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x0018814C File Offset: 0x0018634C
		public ADNotificationRequestCookie RegisterForJournalRuleNotifications(ADObjectId baseDN, ADNotificationCallback callback)
		{
			ADNotificationRequestCookie result = ADNotificationAdapter.RegisterChangeNotification<TransportRule>(baseDN, callback);
			if (this.NeedExplicitDeletedObjectSubscription)
			{
				this.VerifyRootWasRegistered<TransportRule>();
			}
			return result;
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x00188170 File Offset: 0x00186370
		public void UnregisterChangeNotification(ADNotificationRequestCookie cookie)
		{
			ADNotificationAdapter.UnregisterChangeNotification(cookie);
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x00188178 File Offset: 0x00186378
		private static void EventLogDeleteNotification(ADNotificationEventArgs args)
		{
			if (args.ChangeType != ADNotificationChangeType.Delete)
			{
				return;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_TransportDeletedADNotificationReceived, null, new object[]
			{
				args.ChangeType.ToString(),
				args.Context,
				args.Id,
				args.LastKnownParent,
				args.Type
			});
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x001881F0 File Offset: 0x001863F0
		private static ADObjectId GetRootOrgContainerIdForLocalForest()
		{
			ADObjectId rootOrgContainerIdForLocalForest = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			});
			return rootOrgContainerIdForLocalForest;
		}

		// Token: 0x06007703 RID: 30467 RVA: 0x00188224 File Offset: 0x00186424
		private void RegisterChangeNotificationForDeletedObject<TConfigObject>(ADObjectId parentContainerId, ADNotificationCallback adNotificationCallback) where TConfigObject : ADConfigurationObject, new()
		{
			TConfigObject tconfigObject = Activator.CreateInstance<TConfigObject>();
			ADNotificationRequest request = new ADNotificationRequest(tconfigObject.GetType(), tconfigObject.MostDerivedObjectClass, parentContainerId, adNotificationCallback, null);
			ADNotificationListener.RegisterChangeNotificationForDeletedObjects(request);
			this.typesRegisteredForDeleteNotifications.Add(tconfigObject.GetType());
		}

		// Token: 0x06007704 RID: 30468 RVA: 0x00188279 File Offset: 0x00186479
		private void VerifyRootWasRegistered<TConfigObject>() where TConfigObject : ADConfigurationObject, new()
		{
			if (!this.typesRegisteredForDeleteNotifications.Contains(typeof(TConfigObject)))
			{
				throw new InvalidOperationException(string.Format("Type '{0}' is not registered to receive deleted notifications.", typeof(TConfigObject)));
			}
		}

		// Token: 0x06007705 RID: 30469 RVA: 0x001882AC File Offset: 0x001864AC
		private void HandleExchangeServerDeleted(ADNotificationEventArgs args)
		{
			if (this.ExchangeServerDeleted != null)
			{
				this.ExchangeServerDeleted(args);
			}
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x001882C2 File Offset: 0x001864C2
		private void HandleAcceptedDomainDeleted(ADNotificationEventArgs args)
		{
			if (this.AcceptedDomainDeleted != null)
			{
				this.AcceptedDomainDeleted(args);
			}
		}

		// Token: 0x06007707 RID: 30471 RVA: 0x001882D8 File Offset: 0x001864D8
		private void HandleReceiveConnectorDeleted(ADNotificationEventArgs args)
		{
			if (this.ReceiveConnectorDeleted != null)
			{
				this.ReceiveConnectorDeleted(args);
			}
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x001882EE File Offset: 0x001864EE
		private void HandleRemoteDomainDeleted(ADNotificationEventArgs args)
		{
			if (this.RemoteDomainDeleted != null)
			{
				this.RemoteDomainDeleted(args);
			}
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x00188304 File Offset: 0x00186504
		private void HandleTransportRuleDeleted(ADNotificationEventArgs args)
		{
			if (this.TransportRuleDeleted != null)
			{
				this.TransportRuleDeleted(args);
			}
		}

		// Token: 0x0600770A RID: 30474 RVA: 0x0018831A File Offset: 0x0018651A
		private void HandleJournalRuleDeleted(ADNotificationEventArgs args)
		{
			if (this.JournalRuleDeleted != null)
			{
				this.JournalRuleDeleted(args);
			}
		}

		// Token: 0x0600770B RID: 30475 RVA: 0x00188330 File Offset: 0x00186530
		private void HandleInterceptorRuleDeleted(ADNotificationEventArgs args)
		{
			if (this.InterceptorRuleDeleted != null)
			{
				this.InterceptorRuleDeleted(args);
			}
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x00188346 File Offset: 0x00186546
		private void HandleADSiteLinkDeleted(ADNotificationEventArgs args)
		{
			if (this.ADSiteLinkDeleted != null)
			{
				this.ADSiteLinkDeleted(args);
			}
		}

		// Token: 0x0600770D RID: 30477 RVA: 0x0018835C File Offset: 0x0018655C
		private void HandleDatabaseDeleted(ADNotificationEventArgs args)
		{
			if (this.DatabaseDeleted != null)
			{
				this.DatabaseDeleted(args);
			}
		}

		// Token: 0x0600770E RID: 30478 RVA: 0x00188372 File Offset: 0x00186572
		private void HandleSmtpSendConnectorDeleted(ADNotificationEventArgs args)
		{
			if (this.SmtpSendConnectorDeleted != null)
			{
				this.SmtpSendConnectorDeleted(args);
			}
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x00188388 File Offset: 0x00186588
		private void HandleDeliveryAgentConnectorDeleted(ADNotificationEventArgs args)
		{
			if (this.DeliveryAgentConnectorDeleted != null)
			{
				this.DeliveryAgentConnectorDeleted(args);
			}
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x0018839E File Offset: 0x0018659E
		private void HandleForeignConnectorDeleted(ADNotificationEventArgs args)
		{
			if (this.ForeignConnectorDeleted != null)
			{
				this.ForeignConnectorDeleted(args);
			}
		}

		// Token: 0x17002A83 RID: 10883
		// (get) Token: 0x06007711 RID: 30481 RVA: 0x001883B4 File Offset: 0x001865B4
		private bool NeedExplicitDeletedObjectSubscription
		{
			get
			{
				return this.needExplicitDeletedObjectSubscription;
			}
		}

		// Token: 0x04004C53 RID: 19539
		private readonly bool needExplicitDeletedObjectSubscription;

		// Token: 0x04004C54 RID: 19540
		private static TransportADNotificationAdapter singletonInstance = new TransportADNotificationAdapter();

		// Token: 0x04004C55 RID: 19541
		private HashSet<Type> typesRegisteredForDeleteNotifications = new HashSet<Type>();

		// Token: 0x02000A0D RID: 2573
		// (Invoke) Token: 0x06007714 RID: 30484
		public delegate ADNotificationRequestCookie TransportADNotificationRegister(ADObjectId root, ADNotificationCallback callback);
	}
}
