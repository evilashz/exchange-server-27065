using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200002C RID: 44
	internal sealed class AdDataCache
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00007E78 File Offset: 0x00006078
		public override string ToString()
		{
			return "AdDataCache: ";
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007ED4 File Offset: 0x000060D4
		internal List<ELCFolder> GetAllFolders(IExchangePrincipal mailboxOwner)
		{
			this.LoadCache<List<ELCFolder>>(mailboxOwner, "FoldersCache", this.allFolders, this.orgsToRefreshForFolders, this.folderLock, InfoWorkerEventLogConstants.Tuple_CorruptionInADElcFolders, delegate
			{
				List<ELCFolder> value = new List<ELCFolder>();
				AdFolderReader.LoadFoldersInOrg(mailboxOwner.MailboxInfo.OrganizationId, value);
				this.allFolders[mailboxOwner.MailboxInfo.OrganizationId] = value;
			});
			return this.allFolders[mailboxOwner.MailboxInfo.OrganizationId];
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007F98 File Offset: 0x00006198
		internal Dictionary<Guid, AdTagData> GetAllTags(IExchangePrincipal mailboxOwner)
		{
			this.LoadCache<Dictionary<Guid, AdTagData>>(mailboxOwner, "TagsCache", this.allTags, this.orgsToRefreshForTags, this.tagLock, InfoWorkerEventLogConstants.Tuple_CorruptionInADElcTags, delegate
			{
				Dictionary<Guid, AdTagData> value = new Dictionary<Guid, AdTagData>();
				AdTagReader.LoadTagsInOrg(mailboxOwner.MailboxInfo.OrganizationId, value);
				this.allTags[mailboxOwner.MailboxInfo.OrganizationId] = value;
			});
			return this.allTags[mailboxOwner.MailboxInfo.OrganizationId];
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008008 File Offset: 0x00006208
		internal void MarkOrgsToRefresh(Guid mailboxGuid)
		{
			if (mailboxGuid.Equals(Guid.Empty))
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache>((long)this.GetHashCode(), "{0}: Run now was called for all mailboxes on the server. We'll mark all loaded orgs for refresh.", this);
				this.FlipBits();
			}
			else
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache, Guid>((long)this.GetHashCode(), "{0}: Run now was called for a specific mailbox {1}. Save off the mbx guid.", this, mailboxGuid);
				lock (this.runNowMailboxesLock)
				{
					if (!this.runNowMailboxes.Contains(mailboxGuid))
					{
						this.runNowMailboxes.Add(mailboxGuid);
					}
				}
			}
			this.runNowCalled = true;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000080A8 File Offset: 0x000062A8
		internal void MarkOrgsToRefresh()
		{
			if (this.runNowCalled)
			{
				lock (this.runNowMailboxesLock)
				{
					if (this.runNowMailboxes.Count == 0)
					{
						AdDataCache.Tracer.TraceDebug<AdDataCache>((long)this.GetHashCode(), "{0}: Run now was called for all mailboxes on the server. Don't do anything as we've already refreshed.", this);
						this.runNowCalled = false;
					}
					else
					{
						AdDataCache.Tracer.TraceDebug<AdDataCache>((long)this.GetHashCode(), "{0}: Run now was called for a specific mailbox. Nothing to do at this point.", this);
					}
					return;
				}
			}
			DateTime utcNow = DateTime.UtcNow;
			AdDataCache.Tracer.TraceDebug<AdDataCache, DateTime, DateTime>((long)this.GetHashCode(), "{0}: Scheduled window began. Check if we need to mark the cache based on time. Now: {1}. lastLoadTime: {2}", this, utcNow, this.lastLoadTime);
			if ((utcNow - this.lastLoadTime).TotalMinutes > 10.0)
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache>((long)this.GetHashCode(), "{0}: Scheduled window began. We'll mark the cache.", this);
				this.FlipBits();
				this.lastLoadTime = utcNow;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008198 File Offset: 0x00006398
		internal Participant GetMSExchangeAccount(OrganizationId orgID)
		{
			if (this.orgsMSExchangeAccounts.ContainsKey(orgID))
			{
				return this.orgsMSExchangeAccounts[orgID];
			}
			Participant participant = new Participant(this.GetTenantMailbox(orgID));
			this.orgsMSExchangeAccounts.Add(orgID, participant);
			return participant;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000081DC File Offset: 0x000063DC
		private void FlipBits()
		{
			foreach (OrganizationId key in this.allTags.Keys)
			{
				this.orgsToRefreshForTags[key] = true;
			}
			foreach (OrganizationId key2 in this.allFolders.Keys)
			{
				this.orgsToRefreshForFolders[key2] = true;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008288 File Offset: 0x00006488
		private void LoadCache<T>(IExchangePrincipal mailboxOwner, string cacheType, Dictionary<OrganizationId, T> orgsLoaded, Dictionary<OrganizationId, bool> orgsToRefresh, object lockObj, ExEventLog.EventTuple eventLogTuple, AdDataCache.GetAdData getAdDataDelegate)
		{
			try
			{
				AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Before first check - if cache needs loading. OrgId: {2}. Mailbox: {3}", new object[]
				{
					this,
					cacheType,
					mailboxOwner.MailboxInfo.OrganizationId,
					mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
				});
				if (this.NeedToLoadCache<T>(mailboxOwner, orgsLoaded, orgsToRefresh))
				{
					AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Before lock. OrgId: {2}. Mailbox: {3}", new object[]
					{
						this,
						cacheType,
						mailboxOwner.MailboxInfo.OrganizationId,
						mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
					});
					lock (lockObj)
					{
						AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : After lock. OrgId: {2}. Mailbox: {3}", new object[]
						{
							this,
							cacheType,
							mailboxOwner.MailboxInfo.OrganizationId,
							mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
						});
						if (this.NeedToLoadCache<T>(mailboxOwner, orgsLoaded, orgsToRefresh))
						{
							AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : We've determined that we need to load cache. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								cacheType,
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
							getAdDataDelegate();
							AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Loaded cache from AD. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								cacheType,
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
							this.DoPostLoadProcessing(mailboxOwner, orgsToRefresh);
							AdDataCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Done post load processing. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								cacheType,
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
						}
					}
				}
			}
			catch (DataValidationException ex)
			{
				Globals.Logger.LogEvent(eventLogTuple, null, new object[]
				{
					ex.ToString()
				});
				throw new SkipException(ex);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008544 File Offset: 0x00006744
		private bool NeedToLoadCache<T>(IExchangePrincipal mailboxOwner, Dictionary<OrganizationId, T> orgsLoaded, Dictionary<OrganizationId, bool> orgsToRefresh)
		{
			if (!orgsLoaded.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId))
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load cache because orgsLoaded doesn't contain this org. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return true;
			}
			if (orgsToRefresh.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId) && orgsToRefresh[mailboxOwner.MailboxInfo.OrganizationId])
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load cache because orgsToRefresh says so. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return true;
			}
			lock (this.runNowMailboxesLock)
			{
				if (this.runNowCalled && this.runNowMailboxes.Contains(mailboxOwner.MailboxInfo.MailboxGuid))
				{
					AdDataCache.Tracer.TraceDebug<AdDataCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load cache because Run now was called for this mailbox. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008698 File Offset: 0x00006898
		private void DoPostLoadProcessing(IExchangePrincipal mailboxOwner, Dictionary<OrganizationId, bool> orgsToRefresh)
		{
			if (orgsToRefresh.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId))
			{
				AdDataCache.Tracer.TraceDebug<AdDataCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Resetting bit in orgsToRefresh. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				orgsToRefresh[mailboxOwner.MailboxInfo.OrganizationId] = false;
			}
			lock (this.runNowMailboxesLock)
			{
				if (this.runNowCalled && this.runNowMailboxes.Contains(mailboxOwner.MailboxInfo.MailboxGuid))
				{
					AdDataCache.Tracer.TraceDebug<AdDataCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Resetting runNowCalled and runNowMailbox. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					this.runNowMailboxes.Remove(mailboxOwner.MailboxInfo.MailboxGuid);
					if (this.runNowMailboxes.Count == 0)
					{
						this.runNowCalled = false;
					}
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000087B8 File Offset: 0x000069B8
		private ExchangePrincipal GetTenantMailbox(OrganizationId organizationId)
		{
			if (!(organizationId == null) && !(organizationId == OrganizationId.ForestWideOrgId))
			{
				string name = organizationId.OrganizationalUnit.Name;
			}
			ExchangePrincipal result = null;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 513, "GetTenantMailbox", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\AdDataCache.cs");
			try
			{
				result = ExchangePrincipal.FromADUser(MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession), null);
			}
			catch (ObjectNotFoundException ex)
			{
				throw new DataSourceOperationException(ex.LocalizedString, ex);
			}
			return result;
		}

		// Token: 0x04000135 RID: 309
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x04000136 RID: 310
		private Dictionary<OrganizationId, Dictionary<Guid, AdTagData>> allTags = new Dictionary<OrganizationId, Dictionary<Guid, AdTagData>>();

		// Token: 0x04000137 RID: 311
		private Dictionary<OrganizationId, List<ELCFolder>> allFolders = new Dictionary<OrganizationId, List<ELCFolder>>();

		// Token: 0x04000138 RID: 312
		private object folderLock = new object();

		// Token: 0x04000139 RID: 313
		private object tagLock = new object();

		// Token: 0x0400013A RID: 314
		private DateTime lastLoadTime = DateTime.MinValue;

		// Token: 0x0400013B RID: 315
		private bool runNowCalled;

		// Token: 0x0400013C RID: 316
		private List<Guid> runNowMailboxes = new List<Guid>();

		// Token: 0x0400013D RID: 317
		private object runNowMailboxesLock = new object();

		// Token: 0x0400013E RID: 318
		private Dictionary<OrganizationId, bool> orgsToRefreshForTags = new Dictionary<OrganizationId, bool>();

		// Token: 0x0400013F RID: 319
		private Dictionary<OrganizationId, bool> orgsToRefreshForFolders = new Dictionary<OrganizationId, bool>();

		// Token: 0x04000140 RID: 320
		private Dictionary<OrganizationId, Participant> orgsMSExchangeAccounts = new Dictionary<OrganizationId, Participant>();

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000147 RID: 327
		private delegate void GetAdData();
	}
}
