using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000031 RID: 49
	internal class ELCAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase, IDisposable, IDemandJobNotification
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000A438 File Offset: 0x00008638
		private bool ElcAssistantRunnerTestMode
		{
			get
			{
				if (this.elcTestMode == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.TestELCAssistantModeName);
					if (obj != null && obj is int)
					{
						this.elcTestMode = new int?((int)obj);
					}
					else
					{
						this.elcTestMode = new int?(0);
					}
				}
				return this.elcTestMode.Value == 1;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A49C File Offset: 0x0000869C
		public ELCAssistant(DatabaseInfo databaseInfo, ELCAssistantType elcAssistantType, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.healthMonitor = new ELCHealthMonitor(databaseInfo.Guid, databaseInfo.ToString(), this.GetResourceDependencies().ToArray());
			this.elcFolderSubAssistant = new ElcFolderSubAssistant(databaseInfo, elcAssistantType, this.healthMonitor);
			this.elcTagSubAssistant = new ElcTagSubAssistant(databaseInfo, elcAssistantType, this.healthMonitor);
			this.sysCleanupSubAssistant = new SysCleanupSubAssistant(databaseInfo, elcAssistantType, this.healthMonitor);
			this.discoveryHoldSynchronizer = new DiscoveryHoldSynchronizer();
			this.elcAssistantType = elcAssistantType;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A52C File Offset: 0x0000872C
		public void OnBeforeDemandJob(Guid mailboxGuid, Guid databaseGuid)
		{
			this.elcAssistantType.AdCache.MarkOrgsToRefresh(mailboxGuid);
			this.elcAssistantType.DiscoveryHoldCache.MarkOrgsToRefresh(mailboxGuid);
			this.isOnDemandJob = true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A558 File Offset: 0x00008758
		public override List<ResourceKey> GetResourceDependencies()
		{
			List<ResourceKey> resourceDependencies = base.GetResourceDependencies();
			ResourceKey item = new MdbReplicationResourceHealthMonitorKey(base.DatabaseInfo.Guid);
			if (!resourceDependencies.Contains(item))
			{
				resourceDependencies.Add(item);
			}
			ResourceKey item2 = new MdbAvailabilityResourceHealthMonitorKey(base.DatabaseInfo.Guid);
			if (!resourceDependencies.Contains(item2))
			{
				resourceDependencies.Add(item2);
			}
			return resourceDependencies;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A5B0 File Offset: 0x000087B0
		public override AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			if (!(context.Args.StoreSession is MailboxSession))
			{
				return null;
			}
			return new ElcDataTaskContext(context.MailboxData, context.Job, new AssistantStep(this.DoWork), 0);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A5F4 File Offset: 0x000087F4
		private AssistantTaskContext DoWork(AssistantTaskContext context)
		{
			ELCAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "ElcAssistant.InitialStep");
			MailboxSession mailboxSession = context.Args.StoreSession as MailboxSession;
			ElcDataTaskContext elcDataTaskContext = context as ElcDataTaskContext;
			ExAssert.RetailAssert(mailboxSession != null, "Collection step invoked with an invalid session. {0}", new object[]
			{
				(context.Args.StoreSession == null) ? "null" : context.Args.StoreSession.GetType().FullName
			});
			ExAssert.RetailAssert(elcDataTaskContext != null, "Collection step invoked with an invalid task context. {0}", new object[]
			{
				context.GetType().FullName
			});
			elcDataTaskContext.RetriesAttempted++;
			bool flag = this.InvokeInternalAssistant(mailboxSession, context.Args, context.CustomDataToLog, elcDataTaskContext.RetriesAttempted);
			if (flag)
			{
				ELCAssistant.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "ELCAssistant is not done processing, this mailbox will be retried, mailbox {0} , Total attempts {1}", mailboxSession.MailboxOwner, elcDataTaskContext.RetriesAttempted);
				return new ElcDataTaskContext(context.MailboxData, context.Job, new AssistantStep(this.DoWork), elcDataTaskContext.RetriesAttempted);
			}
			ELCAssistant.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "ELCAssistant is done processing mailbox {0} , Total attempts {1}", mailboxSession.MailboxOwner, elcDataTaskContext.RetriesAttempted);
			return null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A734 File Offset: 0x00008934
		public void OnWorkCycleCheckpoint()
		{
			ELCAssistant.Tracer.TraceDebug<ELCAssistant>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint entered.", this);
			this.elcFolderSubAssistant.OnWindowEnd();
			this.elcTagSubAssistant.OnWindowEnd();
			this.sysCleanupSubAssistant.OnWindowEnd();
			this.elcFolderSubAssistant.OnWindowBegin();
			this.elcTagSubAssistant.OnWindowBegin();
			this.sysCleanupSubAssistant.OnWindowBegin();
			ELCAssistant.Tracer.TraceDebug<ELCAssistant>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint exited.", this);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A7B1 File Offset: 0x000089B1
		public void Dispose()
		{
			this.elcFolderSubAssistant.Dispose();
			this.elcTagSubAssistant.Dispose();
			this.sysCleanupSubAssistant.Dispose();
			this.healthMonitor.Dispose();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000A7DF File Offset: 0x000089DF
		protected override void OnShutdownInternal()
		{
			this.elcFolderSubAssistant.OnShutdown();
			this.elcTagSubAssistant.OnShutdown();
			this.sysCleanupSubAssistant.OnShutdown();
			this.healthMonitor.OnShutdown();
			this.discoveryHoldSynchronizer.Shutdown();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000A818 File Offset: 0x00008A18
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (this.ElcAssistantRunnerTestMode)
			{
				MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
				if (mailboxSession == null)
				{
					return;
				}
				this.InvokeInternalAssistant(mailboxSession, invokeArgs, customDataToLog, 0);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000A9BC File Offset: 0x00008BBC
		private bool InvokeInternalAssistant(MailboxSession mailboxSession, InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog, int totalAttempts)
		{
			ELCAssistant.<>c__DisplayClass5 CS$<>8__locals1 = new ELCAssistant.<>c__DisplayClass5();
			CS$<>8__locals1.mailboxSession = mailboxSession;
			CS$<>8__locals1.<>4__this = this;
			bool result = false;
			CS$<>8__locals1.isSuccessful = true;
			ExDateTime utcNow = ExDateTime.UtcNow;
			CS$<>8__locals1.parameters = invokeArgs.Parameters;
			CS$<>8__locals1.logEntry = new StatisticsLogEntry
			{
				ProcessingStartTime = utcNow.ToString("s"),
				IsOnDemandJob = this.isOnDemandJob,
				OrganizationName = ((CS$<>8__locals1.mailboxSession.MailboxOwner.MailboxInfo.OrganizationId == null) ? string.Empty : ((CS$<>8__locals1.mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit == null) ? string.Empty : CS$<>8__locals1.mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit.Name)),
				MailboxGuid = CS$<>8__locals1.mailboxSession.MailboxGuid,
				IsArchive = CS$<>8__locals1.mailboxSession.MailboxOwner.MailboxInfo.IsArchive,
				MoveToArchiveLimitReached = false,
				RetryCount = totalAttempts
			};
			try
			{
				ELCAssistant.<>c__DisplayClass7 CS$<>8__locals2 = new ELCAssistant.<>c__DisplayClass7();
				CS$<>8__locals2.CS$<>8__locals6 = CS$<>8__locals1;
				CS$<>8__locals2.exception = null;
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<InvokeInternalAssistant>b__1)), new FilterDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<InvokeInternalAssistant>b__2)), new CatchDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<InvokeInternalAssistant>b__3)));
			}
			catch (ElcEwsException ex)
			{
				result = false;
				ELCAssistant.Tracer.TraceDebug<ELCAssistant, IExchangePrincipal, ElcEwsException>((long)this.GetHashCode(), "{0}: ElcEwsException encountered for mailbox owner {1}, Exception {2}. Skipping this mailbox.", this, CS$<>8__locals1.mailboxSession.MailboxOwner, ex);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailboxTransientException, null, new object[]
				{
					CS$<>8__locals1.mailboxSession.MailboxOwner,
					ex.ToString()
				});
				ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, ex, ELCAssistant.NotificationType.Permanent, null);
			}
			catch (StorageTransientException ex2)
			{
				StorageTransientException ex3 = ex2;
				if (ex3.InnerException is MapiExceptionTimeout || ex3.InnerException is MapiExceptionRpcServerTooBusy)
				{
					result = false;
					ELCAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: StorageTransientException caused by {1} encountered for mailbox owner {2}, Exception {3}. It will be handled by AI.", new object[]
					{
						this,
						ex3.InnerException.GetType(),
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						ex3
					});
				}
				else
				{
					result = false;
					ELCAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: StorageTransientException{1} encountered for mailbox owner {2}, Exception {3}. Skipping this mailbox.", new object[]
					{
						this,
						(ex2.InnerException != null) ? (" caused by " + ex2.InnerException.GetType()) : "",
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						ex2
					});
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailboxTransientException, null, new object[]
					{
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						ex3.ToString()
					});
					ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, ex3, ELCAssistant.NotificationType.Permanent, null);
				}
				throw ex3;
			}
			catch (WrongServerException ex4)
			{
				ELCAssistant.Tracer.TraceDebug<ELCAssistant, IExchangePrincipal, WrongServerException>((long)this.GetHashCode(), "{0}: WrongServerException encountered for mailbox owner {1}. Skipping this mailbox. Exception {2}", this, CS$<>8__locals1.mailboxSession.MailboxOwner, ex4);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailboxTransientException, null, new object[]
				{
					CS$<>8__locals1.mailboxSession.MailboxOwner,
					ex4.ToString()
				});
				ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, ex4, ELCAssistant.NotificationType.Permanent, null);
			}
			catch (TransientMailboxException ex5)
			{
				result = false;
				ELCAssistant.Tracer.TraceDebug<ELCAssistant, IExchangePrincipal, TransientMailboxException>((long)this.GetHashCode(), "{0}: TransientMailboxException encountered for mailbox owner {1}. Exception {2}. Skipping this mailbox.", this, CS$<>8__locals1.mailboxSession.MailboxOwner, ex5);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailboxTransientException, null, new object[]
				{
					CS$<>8__locals1.mailboxSession.MailboxOwner,
					ex5.ToString()
				});
				ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, ex5, ELCAssistant.NotificationType.Permanent, null);
			}
			catch (ResourceUnhealthyException ex6)
			{
				if (totalAttempts < 5)
				{
					result = true;
					ELCAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ResourceUnhealthyException encountered for mailbox owner {1}, this is attempt # {2}, Exception {3}", new object[]
					{
						this,
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						totalAttempts,
						ex6
					});
				}
				else
				{
					result = false;
					ELCAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ResourceUnhealthyException encountered for mailbox owner {1}, Exceeded MaxRetries {2} for Store database resource unhealthy exception {3}. Skipping this mailbox.", new object[]
					{
						this,
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						totalAttempts,
						ex6
					});
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailboxTransientException, null, new object[]
					{
						CS$<>8__locals1.mailboxSession.MailboxOwner,
						ex6.ToString()
					});
					ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, ex6, ELCAssistant.NotificationType.Transient, null);
				}
			}
			finally
			{
				this.elcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfMailboxesProcessed, 1L);
				this.elcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfWarnings, CS$<>8__locals1.logEntry.NumberOfBatchesFailedToExpireInExpirationExecutor + CS$<>8__locals1.logEntry.NumberOfBatchesFailedToMoveInArchiveProcessor);
				ExDateTime utcNow2 = ExDateTime.UtcNow;
				CS$<>8__locals1.logEntry.ProcessingEndTime = utcNow2.ToString("s");
				CS$<>8__locals1.logEntry.TotalProcessingTime = (long)(utcNow2 - utcNow).TotalMilliseconds;
				if ((double)CS$<>8__locals1.logEntry.TotalProcessingTime > TimeSpan.FromMinutes((double)ElcGlobals.MailboxProcessingTimeFirstLevelThresholdInMin).TotalMilliseconds)
				{
					this.elcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold, 1L);
				}
				if ((double)CS$<>8__locals1.logEntry.TotalProcessingTime > TimeSpan.FromMinutes((double)ElcGlobals.MailboxProcessingTimeSecondLevelThresholdInMin).TotalMilliseconds)
				{
					this.elcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold, 1L);
				}
				CS$<>8__locals1.mailboxSession.Mailbox.Load(ELCAssistant.ExtendedPropertiesToUpdate);
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunArchivedFromDumpsterItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsArchivedByDumpsterExpirationEnforcer;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunArchivedFromRootItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsArchivedByDefaultTag + CS$<>8__locals1.logEntry.NumberOfItemsArchivedByPersonalTag;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunDeletedFromDumpsterItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsDeletedByDiscoveryHoldEnforcer + CS$<>8__locals1.logEntry.NumberOfItemsDeletedByDumpsterExpirationEnforcer + CS$<>8__locals1.logEntry.NumberOfItemsDeletedByDumpsterQuotaEnforcer;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunDeletedFromRootItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsDeletedByPersonalTag + CS$<>8__locals1.logEntry.NumberOfItemsDeletedBySystemTag + CS$<>8__locals1.logEntry.NumberOfItemsDeletedByDefaultTag;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunSubAssistantProcessingTime] = CS$<>8__locals1.logEntry.TagSubAssistantProcessingTime;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunTaggedFolderCount] = CS$<>8__locals1.logEntry.NumberOfFoldersTaggedByPersonalArchiveTag + CS$<>8__locals1.logEntry.NumberOfFoldersTaggedByPersonalExpiryTag + CS$<>8__locals1.logEntry.NumberOfFoldersTaggedBySystemExpiryTag + CS$<>8__locals1.logEntry.NumberOfFoldersTaggedByUncertaionExpiryTag;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunTaggedWithArchiveItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsTaggedByPersonalArchiveTag;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunTaggedWithExpiryItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsTaggedByPersonalExpiryTag + CS$<>8__locals1.logEntry.NumberOfItemsTaggedBySystemExpiryTag + CS$<>8__locals1.logEntry.NumberOfItemsTaggedByUncertaionExpiryTag + CS$<>8__locals1.logEntry.NumberOfItemsTaggedByDefaultExpiryTag;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunTotalProcessingTime] = CS$<>8__locals1.logEntry.TotalProcessingTime;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunUpdatedFolderCount] = CS$<>8__locals1.logEntry.NumberOfFoldersUpdated;
				CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ElcLastRunUpdatedItemCount] = CS$<>8__locals1.logEntry.NumberOfItemsUpdated;
				ExDateTime utcNow3 = ExDateTime.UtcNow;
				if (!CS$<>8__locals1.isSuccessful)
				{
					ExDateTime? valueOrDefault = CS$<>8__locals1.mailboxSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.ELCLastSuccessTimestamp, null);
					if (valueOrDefault == null)
					{
						ELCAssistant.Tracer.TraceDebug<ELCAssistant, ExDateTime>((long)this.GetHashCode(), "{0}: Property ELCLastSuccessTimestamp didn't have a value. Set today {1} to the property.", this, utcNow3);
						CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ELCLastSuccessTimestamp] = utcNow3;
					}
					else
					{
						double totalDays = (utcNow3 - valueOrDefault.Value).TotalDays;
						if (totalDays > (double)ElcGlobals.MaxIntervalBetweenTwoSuccessfulElcRunsInDays)
						{
							string text = string.Format("The difference: {0} days between today: {1} and the date of last successful ELC run: {2} for mailbox: {3} is above the threshold: {4}", new object[]
							{
								totalDays,
								utcNow3,
								valueOrDefault.Value,
								CS$<>8__locals1.mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid.ToString(),
								ElcGlobals.MaxIntervalBetweenTwoSuccessfulElcRunsInDays
							});
							ELCAssistant.Tracer.TraceDebug<ELCAssistant, string>((long)this.GetHashCode(), "{0}: {1}", this, text);
							ELCAssistant.PublishMonitoringResult(CS$<>8__locals1.mailboxSession, null, ELCAssistant.NotificationType.LastSuccessTooLongAgo, text);
						}
					}
					this.elcAssistantType.PerfCountersWrapper.Increment(ELCPerfmon.NumberOfFailures, 1L);
				}
				else
				{
					ExDateTime? valueOrDefault2 = CS$<>8__locals1.mailboxSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.ELCLastSuccessTimestamp, null);
					ELCAssistant.Tracer.TraceDebug<ELCAssistant, string>((long)this.GetHashCode(), "{0}: The previous value of ELCLastSuccessTimestamp is {1}", this, (valueOrDefault2 != null) ? valueOrDefault2.Value.ToString("G") : "Null");
					CS$<>8__locals1.mailboxSession.Mailbox[MailboxSchema.ELCLastSuccessTimestamp] = utcNow3;
				}
				CS$<>8__locals1.mailboxSession.Mailbox.Save();
				List<KeyValuePair<string, object>> list = CS$<>8__locals1.logEntry.FormatCustomData();
				if (list != null && list.Count > 0 && customDataToLog != null)
				{
					customDataToLog.AddRange(list);
				}
				AssistantsLog.LogEndProcessingMailboxEvent(invokeArgs.ActivityId, this, customDataToLog, CS$<>8__locals1.mailboxSession.MailboxGuid, string.Empty, null);
				this.isOnDemandJob = false;
			}
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B49C File Offset: 0x0000969C
		internal void SetElcTagSubAssistantCalendarDelay(int timeInMinutes)
		{
			this.elcTagSubAssistant.ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes = timeInMinutes;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B4AC File Offset: 0x000096AC
		internal static void PublishMonitoringResult(MailboxSession session, Exception ex, ELCAssistant.NotificationType type, string message)
		{
			string component = "ELCComponent_" + type.ToString();
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Compliance.Name, component, string.Empty, ResultSeverityLevel.Error);
			eventNotificationItem.StateAttribute1 = session.MailboxOwner.ObjectId.ToCanonicalName();
			eventNotificationItem.StateAttribute2 = session.MailboxOwner.MailboxInfo.MailboxGuid.ToString();
			eventNotificationItem.StateAttribute3 = session.MailboxOwner.MailboxInfo.OrganizationId.ToString();
			if (session.MailboxOwner.MailboxInfo.IsArchive)
			{
				eventNotificationItem.StateAttribute4 = "IsArchiveMailbox = True";
			}
			else
			{
				eventNotificationItem.StateAttribute4 = "IsArchiveMailbox = False";
			}
			if (ex != null)
			{
				eventNotificationItem.StateAttribute5 = ex.ToString();
			}
			else if (message != null)
			{
				eventNotificationItem.StateAttribute5 = message;
			}
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B588 File Offset: 0x00009788
		private bool IsEHAMailbox(MailboxSession session)
		{
			session.Mailbox.Load(new PropertyDefinition[]
			{
				StoreObjectSchema.RetentionFlags
			});
			object obj = session.Mailbox.TryGetProperty(StoreObjectSchema.RetentionFlags);
			return obj != null && obj is int && (RetentionAndArchiveFlags)obj == RetentionAndArchiveFlags.EHAMigration;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B5DC File Offset: 0x000097DC
		private void InvokeCore(MailboxSession mailboxSession, StatisticsLogEntry logEntry, ElcParameters parameters)
		{
			MailboxDataForTags mailboxDataForTags = null;
			try
			{
				this.discoveryHoldSynchronizer.Start(mailboxSession);
				this.healthMonitor.EnableLoadTrackingOnSession(mailboxSession);
				mailboxDataForTags = this.BuildMailboxData(mailboxSession, logEntry);
				this.sysCleanupSubAssistant.Invoke(mailboxSession, mailboxDataForTags, parameters);
				if (parameters == ElcParameters.None)
				{
					if (mailboxSession.MailboxOwner.RecipientType == RecipientType.MailUser)
					{
						this.elcTagSubAssistant.Invoke(mailboxSession, mailboxDataForTags);
					}
					else
					{
						int mailboxElcVersion = this.GetMailboxElcVersion(mailboxSession, mailboxDataForTags);
						if (mailboxElcVersion == 1)
						{
							if (!ElcGlobals.Configuration.GetConfig<bool>("IgnoreManagedFolder"))
							{
								this.elcFolderSubAssistant.Invoke(mailboxSession, mailboxDataForTags);
							}
						}
						else if (mailboxElcVersion == 2)
						{
							this.elcTagSubAssistant.Invoke(mailboxSession, mailboxDataForTags);
						}
					}
				}
			}
			finally
			{
				if (mailboxDataForTags != null)
				{
					mailboxDataForTags.Dispose();
					mailboxDataForTags = null;
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000B698 File Offset: 0x00009898
		private Exception TranslateException(Exception exception)
		{
			if (exception is IWPermanentException || exception is QuotaExceededException)
			{
				return new SkipException(exception);
			}
			if (exception is IWTransientException)
			{
				return new TransientMailboxException(exception);
			}
			if (exception is SkipException)
			{
				return exception;
			}
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000B6CC File Offset: 0x000098CC
		private MailboxDataForTags BuildMailboxData(MailboxSession mailboxSession, StatisticsLogEntry logEntry)
		{
			bool flag = false;
			MailboxDataForTags mailboxDataForTags = null;
			try
			{
				mailboxDataForTags = new MailboxDataForTags();
				mailboxDataForTags.Init(mailboxSession);
				mailboxDataForTags.ElcUserTagInformation.AllAdTags = this.elcAssistantType.AdCache.GetAllTags(mailboxSession.MailboxOwner);
				mailboxDataForTags.ElcUserTagInformation.OrgCache = this.elcAssistantType.OrgCache;
				mailboxDataForTags.ElcUserTagInformation.Build();
				mailboxDataForTags.StatisticsLogEntry = logEntry;
				if (mailboxDataForTags.InPlaceHoldEnabled && !mailboxDataForTags.AbsoluteLitigationHoldEnabled)
				{
					mailboxDataForTags.ElcUserInformation.InPlaceHoldConfiguration = this.elcAssistantType.DiscoveryHoldCache.GetInPlaceHoldConfigurationForMailbox(mailboxSession, mailboxDataForTags.ElcUserInformation.InPlaceHolds, mailboxDataForTags.ElcUserInformation.MaxSearchQueriesLimit, logEntry);
				}
				mailboxDataForTags.StatisticsLogEntry.IsLitigationHoldEnabled = mailboxDataForTags.LitigationHoldEnabled;
				mailboxDataForTags.StatisticsLogEntry.IsInPlaceHoldEnabled = mailboxDataForTags.InPlaceHoldEnabled;
				mailboxDataForTags.StatisticsLogEntry.IsAuditEnabled = mailboxDataForTags.AuditEnabled;
				mailboxDataForTags.StatisticsLogEntry.IsInactiveMailbox = mailboxDataForTags.ElcUserInformation.ADUser.IsInactiveMailbox;
				flag = true;
			}
			finally
			{
				if (!flag && mailboxDataForTags != null)
				{
					mailboxDataForTags.Dispose();
					mailboxDataForTags = null;
				}
			}
			return mailboxDataForTags;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000B7EC File Offset: 0x000099EC
		private int GetMailboxElcVersion(MailboxSession mailboxSession, MailboxData mailboxData)
		{
			ADUser aduser = mailboxData.ElcUserInformation.ADUser;
			if (aduser == null)
			{
				ELCAssistant.Tracer.TraceError<ELCAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: ADUser object for user {1} could not be retrieved from AD. Skipping it.", this, mailboxSession.MailboxOwner);
				throw new SkipException(Strings.descADUserLookupFailure(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			if ((aduser.ElcMailboxFlags & ElcMailboxFlags.ElcV2) != ElcMailboxFlags.None)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000B8EE File Offset: 0x00009AEE
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000B8FE File Offset: 0x00009AFE
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000159 RID: 345
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x0400015A RID: 346
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400015B RID: 347
		private static readonly PropertyDefinition[] ExtendedPropertiesToUpdate = new PropertyDefinition[]
		{
			MailboxSchema.ElcLastRunArchivedFromDumpsterItemCount,
			MailboxSchema.ElcLastRunArchivedFromRootItemCount,
			MailboxSchema.ElcLastRunDeletedFromDumpsterItemCount,
			MailboxSchema.ElcLastRunDeletedFromRootItemCount,
			MailboxSchema.ElcLastRunSubAssistantProcessingTime,
			MailboxSchema.ElcLastRunTaggedFolderCount,
			MailboxSchema.ElcLastRunTaggedWithArchiveItemCount,
			MailboxSchema.ElcLastRunTaggedWithExpiryItemCount,
			MailboxSchema.ElcLastRunTotalProcessingTime,
			MailboxSchema.ElcLastRunUpdatedFolderCount,
			MailboxSchema.ElcLastRunUpdatedItemCount,
			MailboxSchema.ELCLastSuccessTimestamp
		};

		// Token: 0x0400015C RID: 348
		private ElcFolderSubAssistant elcFolderSubAssistant;

		// Token: 0x0400015D RID: 349
		private ElcTagSubAssistant elcTagSubAssistant;

		// Token: 0x0400015E RID: 350
		private SysCleanupSubAssistant sysCleanupSubAssistant;

		// Token: 0x0400015F RID: 351
		private ELCAssistantType elcAssistantType;

		// Token: 0x04000160 RID: 352
		private ELCHealthMonitor healthMonitor;

		// Token: 0x04000161 RID: 353
		private DiscoveryHoldSynchronizer discoveryHoldSynchronizer;

		// Token: 0x04000162 RID: 354
		private bool isOnDemandJob;

		// Token: 0x04000163 RID: 355
		private int? elcTestMode = null;

		// Token: 0x02000032 RID: 50
		internal enum NotificationType
		{
			// Token: 0x04000165 RID: 357
			Permanent,
			// Token: 0x04000166 RID: 358
			Transient,
			// Token: 0x04000167 RID: 359
			LastSuccessTooLongAgo,
			// Token: 0x04000168 RID: 360
			DumpsterWarningQuota,
			// Token: 0x04000169 RID: 361
			ArchiveWarningQuota
		}
	}
}
