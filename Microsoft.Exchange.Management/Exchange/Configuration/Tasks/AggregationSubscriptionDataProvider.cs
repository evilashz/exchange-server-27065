using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.TransportSync;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.SendAsVerification;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.SendAs;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000CCE RID: 3278
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationSubscriptionDataProvider : IConfigDataProvider
	{
		// Token: 0x06007E56 RID: 32342 RVA: 0x00204648 File Offset: 0x00202848
		public AggregationSubscriptionDataProvider(AggregationTaskType taskType, IRecipientSession session, ADUser adUser)
		{
			session.LinkResolutionServer = ADSession.GetCurrentConfigDC(session.SessionSettings.GetAccountOrResourceForestFqdn());
			session.UseGlobalCatalog = false;
			this.taskType = taskType;
			this.adUser = adUser;
			this.recipientSession = session;
			this.aggregationSubscriptionConstraintChecker = new AggregationSubscriptionConstraintChecker();
			if (this.adUser != null)
			{
				try
				{
					this.primaryExchangePrincipal = ExchangePrincipal.FromADUser(this.recipientSession.SessionSettings, this.adUser, RemotingOptions.AllowCrossSite);
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new MailboxFailureException(innerException);
				}
			}
		}

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x06007E57 RID: 32343 RVA: 0x002046E4 File Offset: 0x002028E4
		public IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x06007E58 RID: 32344 RVA: 0x002046EC File Offset: 0x002028EC
		public string Source
		{
			get
			{
				return "AggregationSubscriptionDataProvider";
			}
		}

		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x06007E59 RID: 32345 RVA: 0x002046F3 File Offset: 0x002028F3
		public ADUser ADUser
		{
			get
			{
				return this.adUser;
			}
		}

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x06007E5A RID: 32346 RVA: 0x002046FB File Offset: 0x002028FB
		public ExchangePrincipal SubscriptionExchangePrincipal
		{
			get
			{
				return this.primaryExchangePrincipal;
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x06007E5B RID: 32347 RVA: 0x00204703 File Offset: 0x00202903
		public string UserLegacyDN
		{
			get
			{
				return this.primaryExchangePrincipal.LegacyDn;
			}
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x06007E5C RID: 32348 RVA: 0x00204710 File Offset: 0x00202910
		// (set) Token: 0x06007E5D RID: 32349 RVA: 0x00204718 File Offset: 0x00202918
		public bool LoadReport
		{
			get
			{
				return this.loadReport;
			}
			set
			{
				this.loadReport = value;
			}
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x00204724 File Offset: 0x00202924
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			AggregationSubscriptionIdentity aggregationSubscriptionIdentity = (AggregationSubscriptionIdentity)identity;
			try
			{
				using (MailboxSession mailboxSession = this.OpenMailboxSession(aggregationSubscriptionIdentity))
				{
					bool upgradeIfRequired = this.ShouldUpgradeIfRequired();
					PimAggregationSubscription pimAggregationSubscription = (PimAggregationSubscription)SubscriptionManager.GetSubscription(mailboxSession, aggregationSubscriptionIdentity.SubscriptionId, upgradeIfRequired);
					PimSubscriptionProxy pimSubscriptionProxy = pimAggregationSubscription.CreateSubscriptionProxy();
					if (this.loadReport)
					{
						ReportData reportData = SkippedItemUtilities.GetReportData(aggregationSubscriptionIdentity.SubscriptionId);
						reportData.Load(mailboxSession.Mailbox.MapiStore);
						pimSubscriptionProxy.Report = reportData.ToReport();
					}
					return pimSubscriptionProxy;
				}
			}
			catch (LocalizedException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)1365UL, AggregationTaskUtils.Tracer, "Read: {0} hit exception: {1}.", new object[]
				{
					aggregationSubscriptionIdentity,
					ex
				});
			}
			return null;
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x00204800 File Offset: 0x00202A00
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.GetSubscriptions((AggregationSubscriptionQueryFilter)filter);
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x00204810 File Offset: 0x00202A10
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			PimSubscriptionProxy[] subscriptions = this.GetSubscriptions((AggregationSubscriptionQueryFilter)filter);
			List<T> list = new List<T>(subscriptions.Length);
			foreach (PimSubscriptionProxy configurable in subscriptions)
			{
				list.Add((T)((object)configurable));
			}
			return list;
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x0020485C File Offset: 0x00202A5C
		public void Save(IConfigurable instance)
		{
			PimSubscriptionProxy pimSubscriptionProxy = instance as PimSubscriptionProxy;
			switch (pimSubscriptionProxy.ObjectState)
			{
			case ObjectState.New:
				this.NewAggregationSubscription(pimSubscriptionProxy);
				return;
			case ObjectState.Unchanged:
				return;
			case ObjectState.Changed:
				this.UpdateAggregationSubscription(pimSubscriptionProxy);
				return;
			case ObjectState.Deleted:
				throw new InvalidOperationException("Calling Save() on a deleted object is not permitted. Delete() should be used instead.");
			default:
				return;
			}
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x002048AC File Offset: 0x00202AAC
		public virtual void Delete(IConfigurable instance)
		{
			PimSubscriptionProxy pimSubscriptionProxy = instance as PimSubscriptionProxy;
			this.RemoveAggregationSubscription(pimSubscriptionProxy.Subscription);
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x002048CC File Offset: 0x00202ACC
		protected MailboxSession OpenMailboxSession(AggregationSubscriptionIdentity subscriptionIdentity)
		{
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromLegacyDN(this.recipientSession.SessionSettings, subscriptionIdentity.LegacyDN, RemotingOptions.AllowCrossSite);
			return this.OpenMailboxSession(exchangePrincipal);
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x002048F8 File Offset: 0x00202AF8
		private List<AggregationSubscription> GetAllSubscriptions()
		{
			List<AggregationSubscription> list = null;
			try
			{
				using (MailboxSession mailboxSession = this.OpenMailboxSession())
				{
					bool upgradeIfRequired = this.ShouldUpgradeIfRequired();
					list = SubscriptionManager.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.All, upgradeIfRequired);
					if (list != null && this.loadReport)
					{
						this.reports.Clear();
						foreach (AggregationSubscription aggregationSubscription in list)
						{
							Guid subscriptionGuid = aggregationSubscription.SubscriptionGuid;
							ReportData reportData = SkippedItemUtilities.GetReportData(subscriptionGuid);
							reportData.Load(mailboxSession.Mailbox.MapiStore);
							if (!this.reports.ContainsKey(subscriptionGuid))
							{
								this.reports.Add(aggregationSubscription.SubscriptionGuid, reportData.ToReport());
							}
						}
					}
				}
			}
			catch (LocalizedException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)1492UL, AggregationTaskUtils.Tracer, "GetSubscriptions: Hit exception: {0}.", new object[]
				{
					ex
				});
			}
			return list;
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x00204A20 File Offset: 0x00202C20
		private PimSubscriptionProxy[] GetSubscriptions(AggregationSubscriptionQueryFilter queryFilter)
		{
			List<PimSubscriptionProxy> list = new List<PimSubscriptionProxy>(3);
			IList<AggregationSubscription> allSubscriptions = this.GetAllSubscriptions();
			if (allSubscriptions != null)
			{
				foreach (AggregationSubscription aggregationSubscription in allSubscriptions)
				{
					PimAggregationSubscription pimAggregationSubscription = (PimAggregationSubscription)aggregationSubscription;
					if (queryFilter == null || queryFilter.Match(pimAggregationSubscription))
					{
						PimSubscriptionProxy pimSubscriptionProxy = pimAggregationSubscription.CreateSubscriptionProxy();
						if (this.loadReport && this.reports.ContainsKey(aggregationSubscription.SubscriptionGuid))
						{
							pimSubscriptionProxy.Report = this.reports[aggregationSubscription.SubscriptionGuid];
						}
						list.Add(pimSubscriptionProxy);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x00204AD4 File Offset: 0x00202CD4
		private void NewAggregationSubscription(PimSubscriptionProxy pimSubscriptionProxy)
		{
			PimAggregationSubscription subscription = pimSubscriptionProxy.Subscription;
			ExchangePrincipal exchangePrincipal = this.primaryExchangePrincipal;
			IList<AggregationSubscription> allSubscriptions = this.GetAllSubscriptions();
			int userMaximumSubscriptionAllowed = this.GetUserMaximumSubscriptionAllowed();
			this.aggregationSubscriptionConstraintChecker.CheckNewSubscriptionConstraints(subscription, allSubscriptions, userMaximumSubscriptionAllowed);
			bool flag = false;
			try
			{
				DelayedEmailSender delayedEmailSender = null;
				if (pimSubscriptionProxy.SendAsCheckNeeded)
				{
					delayedEmailSender = this.SetAppropriateSendAsState(subscription);
				}
				flag = true;
				using (MailboxSession mailboxSession = this.OpenMailboxSession(exchangePrincipal))
				{
					SubscriptionManager.CreateSubscription(mailboxSession, subscription);
					if (pimSubscriptionProxy.SendAsCheckNeeded)
					{
						this.PostSaveSendAsStateProcessing(subscription, delayedEmailSender, mailboxSession);
					}
				}
			}
			catch (LocalizedException ex)
			{
				if (!flag)
				{
					CommonLoggingHelper.SyncLogSession.LogError((TSLID)1504UL, AggregationTaskUtils.Tracer, "NewAggregationSubscription: {0}. Failed to set send as state with exception: {1}.", new object[]
					{
						subscription.Name,
						ex
					});
				}
				else
				{
					CommonLoggingHelper.SyncLogSession.LogError((TSLID)1505UL, AggregationTaskUtils.Tracer, "NewAggregationSubscription: {0}. Failed to open mailbox session with exception: {1}.", new object[]
					{
						subscription.Name,
						ex
					});
				}
				throw new FailedCreateAggregationSubscriptionException(subscription.Name, ex);
			}
		}

		// Token: 0x06007E67 RID: 32359 RVA: 0x00204C00 File Offset: 0x00202E00
		private void RemoveAggregationSubscription(PimAggregationSubscription subscription)
		{
			try
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromLegacyDN(this.recipientSession.SessionSettings, subscription.UserLegacyDN, RemotingOptions.AllowCrossSite);
				using (MailboxSession mailboxSession = this.OpenMailboxSession(exchangePrincipal))
				{
					SubscriptionManager.Instance.DeleteSubscription(mailboxSession, subscription, true);
				}
			}
			catch (LocalizedException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)1506UL, AggregationTaskUtils.Tracer, "RemoveAggregationSubscription: {0} hit exception: {1}.", new object[]
				{
					subscription.Name,
					ex
				});
				throw new FailedDeleteAggregationSubscriptionException(subscription.Name, ex);
			}
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x00204CA8 File Offset: 0x00202EA8
		private void UpdateAggregationSubscription(PimSubscriptionProxy pimSubscriptionProxy)
		{
			PimAggregationSubscription subscription = pimSubscriptionProxy.Subscription;
			try
			{
				using (MailboxSession mailboxSession = this.OpenMailboxSession(subscription.SubscriptionIdentity))
				{
					IList<AggregationSubscription> allSubscriptions = this.GetAllSubscriptions();
					this.aggregationSubscriptionConstraintChecker.CheckUpdateSubscriptionConstraints(subscription, allSubscriptions);
					DelayedEmailSender delayedEmailSender = null;
					if (pimSubscriptionProxy.SendAsCheckNeeded)
					{
						delayedEmailSender = this.SetAppropriateSendAsState(subscription);
					}
					SubscriptionManager.SetSubscriptionAndSyncNow(mailboxSession, subscription);
					if (pimSubscriptionProxy.SendAsCheckNeeded)
					{
						this.PostSaveSendAsStateProcessing(subscription, delayedEmailSender, mailboxSession);
					}
				}
			}
			catch (LocalizedException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)1246UL, AggregationTaskUtils.Tracer, "UpdateAggregationSubscription: {0} hit exception: {1}.", new object[]
				{
					subscription.Name,
					ex
				});
				throw new FailedSetAggregationSubscriptionException(subscription.Name, ex);
			}
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x00204D7C File Offset: 0x00202F7C
		private MailboxSession OpenMailboxSession()
		{
			return this.OpenMailboxSession(this.primaryExchangePrincipal);
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x00204D8A File Offset: 0x00202F8A
		private MailboxSession OpenMailboxSession(ExchangePrincipal exchangePrincipal)
		{
			return SubscriptionManager.OpenMailbox(exchangePrincipal, ExchangeMailboxOpenType.AsAdministrator, AggregationSubscriptionDataProvider.ClientInfoString);
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x00204D98 File Offset: 0x00202F98
		private int GetUserMaximumSubscriptionAllowed()
		{
			if (this.adUser.RemoteAccountPolicy == null)
			{
				return int.Parse(((ADPropertyDefinition)RemoteAccountPolicySchema.MaxSyncAccounts).DefaultValue.ToString());
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, this.recipientSession.SessionSettings, 614, "GetUserMaximumSubscriptionAllowed", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\Tasks\\AggregationSubscriptionDataProvider.cs");
			RemoteAccountPolicy remoteAccountPolicy = tenantOrTopologyConfigurationSession.Read<RemoteAccountPolicy>(this.adUser.RemoteAccountPolicy);
			if (remoteAccountPolicy != null)
			{
				return remoteAccountPolicy.MaxSyncAccounts;
			}
			return int.Parse(((ADPropertyDefinition)RemoteAccountPolicySchema.MaxSyncAccounts).DefaultValue.ToString());
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x00204E28 File Offset: 0x00203028
		private DelayedEmailSender SetAppropriateSendAsState(PimAggregationSubscription subscription)
		{
			IEmailSender toWrap = subscription.CreateEmailSenderFor(this.adUser, this.primaryExchangePrincipal);
			DelayedEmailSender delayedEmailSender = new DelayedEmailSender(toWrap);
			SendAsAutoProvision sendAsAutoProvision = new SendAsAutoProvision();
			sendAsAutoProvision.SetAppropriateSendAsState(subscription, delayedEmailSender);
			return delayedEmailSender;
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x00204E60 File Offset: 0x00203060
		private void PostSaveSendAsStateProcessing(PimAggregationSubscription subscription, DelayedEmailSender delayedEmailSender, MailboxSession mailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("delayedEmailSender", delayedEmailSender);
			if (subscription.SendAsState == SendAsState.Enabled)
			{
				AggregationTaskUtils.EnableAlwaysShowFrom(this.primaryExchangePrincipal);
			}
			if (!delayedEmailSender.SendAttempted)
			{
				return;
			}
			IEmailSender emailSender = delayedEmailSender.TriggerDelayedSend();
			SendAsManager sendAsManager = new SendAsManager();
			sendAsManager.UpdateSubscriptionWithDiagnostics(subscription, emailSender);
			SubscriptionManager.SetSubscription(mailboxSession, subscription);
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x00204EB1 File Offset: 0x002030B1
		private bool ShouldUpgradeIfRequired()
		{
			return this.taskType != AggregationTaskType.Get;
		}

		// Token: 0x04003E27 RID: 15911
		private static readonly string ClientInfoString = "Client=TransportSync;Action=Tasks";

		// Token: 0x04003E28 RID: 15912
		private readonly AggregationTaskType taskType;

		// Token: 0x04003E29 RID: 15913
		private readonly IRecipientSession recipientSession;

		// Token: 0x04003E2A RID: 15914
		private readonly ADUser adUser;

		// Token: 0x04003E2B RID: 15915
		private ExchangePrincipal primaryExchangePrincipal;

		// Token: 0x04003E2C RID: 15916
		private AggregationSubscriptionConstraintChecker aggregationSubscriptionConstraintChecker;

		// Token: 0x04003E2D RID: 15917
		private bool loadReport;

		// Token: 0x04003E2E RID: 15918
		private Dictionary<Guid, Report> reports = new Dictionary<Guid, Report>();
	}
}
