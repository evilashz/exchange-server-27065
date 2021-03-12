using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Compliance;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200002E RID: 46
	internal sealed class DiscoveryHoldQueryCache
	{
		// Token: 0x0600014A RID: 330 RVA: 0x000088D1 File Offset: 0x00006AD1
		public override string ToString()
		{
			return "DiscoveryHoldQueryCache: ";
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000088D8 File Offset: 0x00006AD8
		internal List<InPlaceHoldConfiguration> GetInPlaceHoldConfigurationForMailbox(MailboxSession mailboxSession, IList<string> inPlaceHoldIdsOnMailbox, int maxQueryLengthLimit, StatisticsLogEntry logEntry)
		{
			this.TraceInformation("GetInPlaceHoldConfigurationForMailbox", "Before LoadCache: " + mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			this.TraceInformation(this.GetCacheDataAsStringForTracing());
			List<InPlaceHoldConfiguration> result = null;
			if (this.TryLoadCache(mailboxSession.MailboxOwner, this.orgsToRefresh, this.cacheLock, logEntry))
			{
				result = this.GetHoldPoliciesForMailboxFromCache(mailboxSession, inPlaceHoldIdsOnMailbox, maxQueryLengthLimit);
			}
			this.TraceInformation("GetInPlaceHoldConfigurationForMailbox", "After LoadCache: " + mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			this.TraceInformation(this.GetCacheDataAsStringForTracing());
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000898C File Offset: 0x00006B8C
		private List<InPlaceHoldConfiguration> GetHoldPoliciesForMailboxFromCache(MailboxSession mailboxSession, IList<string> inPlaceHoldIdsOnMailbox, int maxQueryLengthLimit)
		{
			bool flag = false;
			List<InPlaceHoldConfiguration> list = new List<InPlaceHoldConfiguration>(inPlaceHoldIdsOnMailbox.Count);
			Dictionary<string, InPlaceHoldConfiguration> dictionary;
			if (this.allInPlaceHoldConfiguration.TryGetValue(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId, out dictionary))
			{
				int num = 0;
				using (IEnumerator<string> enumerator = inPlaceHoldIdsOnMailbox.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						InPlaceHoldConfiguration inPlaceHoldConfiguration;
						if (!dictionary.TryGetValue(key, out inPlaceHoldConfiguration))
						{
							flag = true;
							break;
						}
						if (inPlaceHoldConfiguration.Enabled)
						{
							flag = !this.VerifyHoldPolicy(inPlaceHoldConfiguration, mailboxSession.MailboxOwner, maxQueryLengthLimit, ref num);
							if (flag)
							{
								break;
							}
							list.Add(inPlaceHoldConfiguration);
						}
					}
					goto IL_BC;
				}
			}
			this.LogDiscoveryQueryLoadFailure(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.ToString(), mailboxSession.MailboxOwner, InfoWorkerEventLogConstants.Tuple_DiscoverySearchObjectNotFoundForOrg);
			flag = true;
			IL_BC:
			if (flag)
			{
				return null;
			}
			return list;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008A6C File Offset: 0x00006C6C
		private bool VerifyHoldPolicy(InPlaceHoldConfiguration inPlaceHoldConfiguration, IExchangePrincipal mailboxOwner, int maxQueryLengthLimit, ref int queryLength)
		{
			bool flag;
			if (!inPlaceHoldConfiguration.IsValid)
			{
				this.LogDiscoveryQueryLoadFailure(mailboxOwner, inPlaceHoldConfiguration.Name, InfoWorkerEventLogConstants.Tuple_CorruptDiscoverySearchObject);
				flag = true;
			}
			else if (inPlaceHoldConfiguration.QueryFilter == null)
			{
				flag = true;
			}
			else
			{
				queryLength += inPlaceHoldConfiguration.QueryString.Length;
				if (queryLength >= maxQueryLengthLimit)
				{
					this.TraceInformation(string.Format("{0}: This mailbox {1} has exceeded the MaxSearchQueryLengthLimit of {2}. This mailbox will be skipped for discovery hold processing.", this, mailboxOwner, maxQueryLengthLimit));
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryHoldsSkippedForTooManyQueries, null, new object[]
					{
						mailboxOwner,
						maxQueryLengthLimit
					});
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return !flag;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008B04 File Offset: 0x00006D04
		private void LogDiscoveryQueryLoadFailure(IExchangePrincipal mailboxOwner, string holdPolicyName, ExEventLog.EventTuple message)
		{
			Globals.Logger.LogEvent(message, null, new object[]
			{
				holdPolicyName,
				mailboxOwner
			});
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008B30 File Offset: 0x00006D30
		private void LogDiscoveryQueryLoadFailure(IExchangePrincipal mailboxOwner, ExEventLog.EventTuple message)
		{
			Globals.Logger.LogEvent(message, null, new object[]
			{
				mailboxOwner
			});
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008B58 File Offset: 0x00006D58
		private void LogDiscoveryQueryLoadFailure(string orgId, IExchangePrincipal mailboxOwner, ExEventLog.EventTuple message)
		{
			Globals.Logger.LogEvent(message, null, new object[]
			{
				orgId,
				mailboxOwner
			});
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008B84 File Offset: 0x00006D84
		private void LogDiscoveryQueryLoadFailure(string orgId, ExEventLog.EventTuple message, Exception ex)
		{
			Globals.Logger.LogEvent(message, null, new object[]
			{
				orgId,
				(ex == null) ? string.Empty : ex.ToString()
			});
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008BC0 File Offset: 0x00006DC0
		private void LogDiscoveryQueryLoadFailure(IExchangePrincipal mailboxOwner, ExEventLog.EventTuple message, Exception ex)
		{
			Globals.Logger.LogEvent(message, null, new object[]
			{
				mailboxOwner,
				(ex == null) ? string.Empty : ex.ToString()
			});
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008BFC File Offset: 0x00006DFC
		private Dictionary<string, InPlaceHoldConfiguration> LoadInPlaceHoldConfigurationInOrg(OrganizationId orgId, StatisticsLogEntry logEntry)
		{
			this.TraceInformation("Load All hold policy Objects in Organization " + orgId);
			new List<InPlaceHoldConfiguration>();
			DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(orgId);
			IEnumerable<MailboxDiscoverySearch> all = discoverySearchDataProvider.GetAll<MailboxDiscoverySearch>();
			Dictionary<string, InPlaceHoldConfiguration> dictionary = new Dictionary<string, InPlaceHoldConfiguration>();
			foreach (MailboxDiscoverySearch mailboxDiscoverySearch in all)
			{
				if (!dictionary.ContainsKey(mailboxDiscoverySearch.InPlaceHoldIdentity))
				{
					InPlaceHoldConfiguration value = new InPlaceHoldConfiguration(mailboxDiscoverySearch);
					dictionary.Add(mailboxDiscoverySearch.InPlaceHoldIdentity, value);
				}
			}
			bool flag = false;
			try
			{
				flag = discoverySearchDataProvider.Mailbox.GetConfiguration().MailboxAssistants.UnifiedPolicyHold.Enabled;
			}
			catch (CannotDetermineExchangeModeException)
			{
				this.TraceInformation("Failed to load unifiedHold flight information");
			}
			if (flag)
			{
				try
				{
					PolicyConfigProvider policyConfigProvider = PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForProcessingEngine(orgId);
					if (policyConfigProvider != null)
					{
						ExComplianceServiceProvider exComplianceServiceProvider = new ExComplianceServiceProvider();
						IEnumerable<PolicyDefinitionConfig> enumerable = policyConfigProvider.FindByName<PolicyDefinitionConfig>("*");
						if (enumerable != null && exComplianceServiceProvider != null)
						{
							foreach (PolicyDefinitionConfig policyDefinitionConfig in enumerable)
							{
								string holdId = ExMailboxComplianceItemContainer.GetHoldId(policyDefinitionConfig.Identity);
								if (policyDefinitionConfig.Mode == Mode.Enforce && policyDefinitionConfig.Scenario == PolicyScenario.Hold)
								{
									IEnumerable<PolicyRuleConfig> enumerable2 = policyConfigProvider.FindByPolicyDefinitionConfigId<PolicyRuleConfig>(policyDefinitionConfig.Identity);
									if (enumerable2 == null)
									{
										continue;
									}
									using (IEnumerator<PolicyRuleConfig> enumerator3 = enumerable2.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											PolicyRuleConfig rule = enumerator3.Current;
											if (dictionary.ContainsKey(holdId))
											{
												this.TraceInformation(string.Format("Hold Id contained twice.  HoldId: {0}", holdId));
												break;
											}
											InPlaceHoldConfiguration value2 = new InPlaceHoldConfiguration(policyDefinitionConfig, rule, exComplianceServiceProvider.GetRuleParser(), DiscoveryHoldQueryCache.Tracer);
											dictionary.Add(holdId, value2);
										}
										continue;
									}
								}
								this.TraceInformation(string.Format("Hold not loaded. HoldId: {0} Mode: {1} Scenario: {2}", holdId, policyDefinitionConfig.Mode.ToString(), policyDefinitionConfig.Scenario.ToString()));
							}
						}
					}
				}
				catch (Exception ex)
				{
					DiscoveryHoldQueryCache.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "Failed to load hold queries from PolicyConfigProvider.  Exception: {0}", ex);
					if (logEntry != null)
					{
						logEntry.FailedToLoadUnifiedPolicies = ex.Message;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008EAC File Offset: 0x000070AC
		internal void MarkOrgsToRefresh(Guid mailboxGuid)
		{
			if (!mailboxGuid.Equals(Guid.Empty))
			{
				DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, Guid>((long)this.GetHashCode(), "{0}: Run now was called for a specific mailbox {1}. Save off the mbx guid.", this, mailboxGuid);
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

		// Token: 0x06000155 RID: 341 RVA: 0x00008F30 File Offset: 0x00007130
		internal void MarkOrgsToRefresh()
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, ExDateTime, ExDateTime>((long)this.GetHashCode(), "{0}: Scheduled window began. Check if we need to mark the discoveryhold cache based on time. Now: {1}. lastLoadTime: {2}", this, utcNow, this.lastLoadTime);
			if ((utcNow - this.lastLoadTime).TotalMinutes > 10.0)
			{
				DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache>((long)this.GetHashCode(), "{0}: Scheduled window began. We'll mark the discoveryhold cache.", this);
				this.FlipBits();
				this.lastLoadTime = utcNow;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008FA4 File Offset: 0x000071A4
		private void FlipBits()
		{
			foreach (OrganizationId key in this.allInPlaceHoldConfiguration.Keys)
			{
				this.orgsToRefresh[key] = true;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009004 File Offset: 0x00007204
		private bool TryLoadCache(IExchangePrincipal mailboxOwner, Dictionary<OrganizationId, bool> orgsToRefresh, object lockObj, StatisticsLogEntry logEntry)
		{
			bool flag = false;
			try
			{
				DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Before first check - if discovery hold cache needs loading. OrgId: {2}. Mailbox: {3}", new object[]
				{
					this,
					"DiscoveryHoldQueryCache",
					mailboxOwner.MailboxInfo.OrganizationId,
					mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
				});
				if (this.NeedToLoadCache(mailboxOwner, orgsToRefresh))
				{
					DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Before lock. OrgId: {2}. Mailbox: {3}", new object[]
					{
						this,
						"DiscoveryHoldQueryCache",
						mailboxOwner.MailboxInfo.OrganizationId,
						mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
					});
					lock (lockObj)
					{
						DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : After lock. OrgId: {2}. Mailbox: {3}", new object[]
						{
							this,
							"DiscoveryHoldQueryCache",
							mailboxOwner.MailboxInfo.OrganizationId,
							mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
						});
						if (this.NeedToLoadCache(mailboxOwner, orgsToRefresh))
						{
							DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : We've determined that we need to load cache. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								"DiscoveryHoldQueryCache",
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
							Dictionary<string, InPlaceHoldConfiguration> value = this.LoadInPlaceHoldConfigurationInOrg(mailboxOwner.MailboxInfo.OrganizationId, logEntry);
							this.allInPlaceHoldConfiguration[mailboxOwner.MailboxInfo.OrganizationId] = value;
							DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Loaded cache from Arbitration mailbox and releasing lock. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								"DiscoveryHoldQueryCache",
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
							this.DoPostLoadProcessing(mailboxOwner, orgsToRefresh);
							DiscoveryHoldQueryCache.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} : Done post load processing. OrgId: {2}. Mailbox: {3}", new object[]
							{
								this,
								"DiscoveryHoldQueryCache",
								mailboxOwner.MailboxInfo.OrganizationId,
								mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
							});
						}
					}
				}
			}
			catch (DataSourceOperationException ex)
			{
				this.LogDiscoveryQueryLoadFailure(mailboxOwner.MailboxInfo.OrganizationId.ToString(), InfoWorkerEventLogConstants.Tuple_DiscoverySearchObjectLoadError, ex);
				flag = true;
			}
			return !flag;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000092F8 File Offset: 0x000074F8
		private bool NeedToLoadCache(IExchangePrincipal mailboxOwner, Dictionary<OrganizationId, bool> orgsToRefresh)
		{
			if (!this.allInPlaceHoldConfiguration.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId))
			{
				this.TraceInformation("Need to load cache because org not found in cache " + mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load discoveryhold cache because orgsLoaded doesn't contain this org. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return true;
			}
			if (orgsToRefresh.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId) && orgsToRefresh[mailboxOwner.MailboxInfo.OrganizationId])
			{
				this.TraceInformation("Need to load cache because orgsToRefresh was marked as true " + mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load discoveryhold cache because orgsToRefresh says so. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return true;
			}
			lock (this.runNowMailboxesLock)
			{
				if (this.runNowCalled && this.runNowMailboxes.Contains(mailboxOwner.MailboxInfo.MailboxGuid))
				{
					this.TraceInformation("Need to load cache because runnow was called for " + mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Need to load discoveryhold cache because Run now was called for this mailbox. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000094D0 File Offset: 0x000076D0
		private void DoPostLoadProcessing(IExchangePrincipal mailboxOwner, Dictionary<OrganizationId, bool> orgsToRefresh)
		{
			if (orgsToRefresh.ContainsKey(mailboxOwner.MailboxInfo.OrganizationId))
			{
				DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Resetting bit in discoveryhold orgsToRefresh. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				orgsToRefresh[mailboxOwner.MailboxInfo.OrganizationId] = false;
			}
			lock (this.runNowMailboxesLock)
			{
				if (this.runNowCalled && this.runNowMailboxes.Contains(mailboxOwner.MailboxInfo.MailboxGuid))
				{
					DiscoveryHoldQueryCache.Tracer.TraceDebug<DiscoveryHoldQueryCache, OrganizationId, string>((long)this.GetHashCode(), "{0}: Resetting runNowCalled and runNowMailbox in discovery hold cache. OrgId: {1}. Mailbox: {2}", this, mailboxOwner.MailboxInfo.OrganizationId, mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					this.runNowMailboxes.Remove(mailboxOwner.MailboxInfo.MailboxGuid);
					if (this.runNowMailboxes.Count == 0)
					{
						this.runNowCalled = false;
					}
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000095F0 File Offset: 0x000077F0
		private void TraceInformation(string message)
		{
			this.TraceInformation(null, message);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000095FC File Offset: 0x000077FC
		private void TraceInformation(string functionName, string message)
		{
			DiscoveryHoldQueryCache.Tracer.TraceInformation(1, (long)this.GetHashCode(), string.Format(string.Concat(new object[]
			{
				ExDateTime.Now,
				" {0}: ",
				(functionName == null) ? string.Empty : functionName,
				": ",
				this.EscapeDataString(message)
			}), this));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009664 File Offset: 0x00007864
		private string EscapeDataString(string message)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = message.Length / 32766;
			int i;
			for (i = 0; i < num; i++)
			{
				stringBuilder.Append(Uri.EscapeDataString(message.Substring(i * 32766, 32766)));
			}
			stringBuilder.Append(Uri.EscapeDataString(message.Substring(i * 32766)));
			return stringBuilder.ToString();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000096D0 File Offset: 0x000078D0
		private string GetCacheDataAsStringForTracing()
		{
			string value = "-------------------------------------";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(value);
			stringBuilder.AppendLine("DiscoveryHoldQueryCache:" + ExDateTime.UtcNow);
			lock (this.cacheLock)
			{
				foreach (OrganizationId organizationId in this.allInPlaceHoldConfiguration.Keys)
				{
					stringBuilder.AppendLine(value);
					stringBuilder.AppendLine("OrganizationId" + organizationId);
					Dictionary<string, InPlaceHoldConfiguration> dictionary = this.allInPlaceHoldConfiguration[organizationId];
					foreach (string text in dictionary.Keys)
					{
						stringBuilder.AppendLine("InPlaceHoldIdentity:" + text);
						InPlaceHoldConfiguration inPlaceHoldConfiguration = dictionary[text];
						stringBuilder.AppendLine("In-place hold configuration: " + inPlaceHoldConfiguration.Name);
					}
				}
			}
			stringBuilder.AppendLine(value);
			return stringBuilder.ToString();
		}

		// Token: 0x04000141 RID: 321
		private const string CacheType = "DiscoveryHoldQueryCache";

		// Token: 0x04000142 RID: 322
		private const int DefaultMinimumRefreshIntervalMins = 10;

		// Token: 0x04000143 RID: 323
		private const int MaximumEscapableStringLength = 32766;

		// Token: 0x04000144 RID: 324
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x04000145 RID: 325
		private Dictionary<OrganizationId, Dictionary<string, InPlaceHoldConfiguration>> allInPlaceHoldConfiguration = new Dictionary<OrganizationId, Dictionary<string, InPlaceHoldConfiguration>>();

		// Token: 0x04000146 RID: 326
		private object cacheLock = new object();

		// Token: 0x04000147 RID: 327
		private ExDateTime lastLoadTime = ExDateTime.MinValue;

		// Token: 0x04000148 RID: 328
		private bool runNowCalled;

		// Token: 0x04000149 RID: 329
		private List<Guid> runNowMailboxes = new List<Guid>();

		// Token: 0x0400014A RID: 330
		private object runNowMailboxesLock = new object();

		// Token: 0x0400014B RID: 331
		private Dictionary<OrganizationId, bool> orgsToRefresh = new Dictionary<OrganizationId, bool>();
	}
}
