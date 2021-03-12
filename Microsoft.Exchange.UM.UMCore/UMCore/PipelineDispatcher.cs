using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002CA RID: 714
	internal class PipelineDispatcher : IUMAsyncComponent
	{
		// Token: 0x060015B6 RID: 5558 RVA: 0x0005CB90 File Offset: 0x0005AD90
		private PipelineDispatcher()
		{
			this.resources = new PipelineResource[3];
			this.resources[0] = PipelineResource.CreatePipelineResource(PipelineDispatcher.PipelineResourceType.LowPriorityCpuBound);
			this.resources[1] = PipelineResource.CreatePipelineResource(PipelineDispatcher.PipelineResourceType.CpuBound);
			this.resources[2] = PipelineResource.CreatePipelineResource(PipelineDispatcher.PipelineResourceType.NetworkBound);
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060015B7 RID: 5559 RVA: 0x0005CC18 File Offset: 0x0005AE18
		// (remove) Token: 0x060015B8 RID: 5560 RVA: 0x0005CC50 File Offset: 0x0005AE50
		internal event EventHandler<EventArgs> OnShutdown;

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0005CC85 File Offset: 0x0005AE85
		public bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x0005CC8D File Offset: 0x0005AE8D
		public AutoResetEvent StoppedEvent
		{
			get
			{
				return this.stageShutdownEvent;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0005CC95 File Offset: 0x0005AE95
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0005CCA2 File Offset: 0x0005AEA2
		internal static PipelineDispatcher Instance
		{
			get
			{
				return PipelineDispatcher.instance;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0005CCAC File Offset: 0x0005AEAC
		internal bool IsShuttingDown
		{
			get
			{
				bool result;
				lock (this.lockObj)
				{
					result = this.isShuttingDown;
				}
				return result;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005CCF0 File Offset: 0x0005AEF0
		public bool IsPipelineHealthy
		{
			get
			{
				bool isPipelineHealthy;
				lock (this.lockObj)
				{
					isPipelineHealthy = this.throttleManager.IsPipelineHealthy;
				}
				return isPipelineHealthy;
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0005CD38 File Offset: 0x0005AF38
		public void CleanupAfterStopped()
		{
			lock (this.lockObj)
			{
				if (this.diskQueueSemaphore != null)
				{
					this.diskQueueSemaphore.Release();
				}
				this.CleanUp();
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0005CD8C File Offset: 0x0005AF8C
		public void StartNow(StartupStage stage)
		{
			if (stage == StartupStage.WPActivation)
			{
				lock (this.lockObj)
				{
					CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Initializing SMTP Submission Services.", new object[]
					{
						9786
					});
					CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "{0} starting in stage {1}", new object[]
					{
						this.Name,
						stage
					});
					if (this.isInitialized)
					{
						throw new InvalidOperationException();
					}
					this.isInitialized = true;
				}
				if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcquireDiskQueueAndStartProcessing)))
				{
					throw new PipelineInitializationException();
				}
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0005CE54 File Offset: 0x0005B054
		public void StopAsync()
		{
			lock (this.lockObj)
			{
				this.isShuttingDown = true;
			}
			if (this.OnShutdown != null)
			{
				this.OnShutdown(this, null);
			}
			if (this.numberOfActiveStages <= 0)
			{
				this.stageShutdownEvent.Set();
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
		internal int GetTotalResourceCount(PipelineDispatcher.PipelineResourceType resourceType)
		{
			return this.resources[(int)resourceType].TotalCount;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0005CED0 File Offset: 0x0005B0D0
		public PipelineSubmitStatus CanSubmitWorkItem(string key, PipelineDispatcher.ThrottledWorkItemType wiType)
		{
			ValidateArgument.NotNullOrEmpty(key, "key");
			PipelineSubmitStatus result;
			lock (this.lockObj)
			{
				result = this.throttleManager.GetWorkItemThrottler(wiType).CanSubmitWorkItem(key, null);
			}
			return result;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005CF2C File Offset: 0x0005B12C
		public PipelineSubmitStatus CanSubmitWorkItem(string key, string recipientId, PipelineDispatcher.ThrottledWorkItemType wiType)
		{
			ValidateArgument.NotNullOrEmpty(key, "key");
			ValidateArgument.NotNullOrEmpty(recipientId, "recipientId");
			PipelineSubmitStatus result;
			lock (this.lockObj)
			{
				result = this.throttleManager.GetWorkItemThrottler(wiType).CanSubmitWorkItem(key, recipientId);
			}
			return result;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0005CF94 File Offset: 0x0005B194
		public PipelineSubmitStatus CanSubmitLowPriorityWorkItem(string key, PipelineDispatcher.ThrottledWorkItemType wiType)
		{
			ValidateArgument.NotNullOrEmpty(key, "key");
			PipelineSubmitStatus result;
			lock (this.lockObj)
			{
				result = this.throttleManager.GetWorkItemThrottler(wiType).CanSubmitLowPriorityWorkItem(key, null);
			}
			return result;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
		public PipelineSubmitStatus CanSubmitLowPriorityWorkItem(string key, string recipientId, PipelineDispatcher.ThrottledWorkItemType wiType)
		{
			ValidateArgument.NotNullOrEmpty(key, "key");
			ValidateArgument.NotNullOrEmpty(recipientId, "recipientId");
			PipelineSubmitStatus result;
			lock (this.lockObj)
			{
				result = this.throttleManager.GetWorkItemThrottler(wiType).CanSubmitLowPriorityWorkItem(key, recipientId);
			}
			return result;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0005D058 File Offset: 0x0005B258
		private static bool IsWatsonNeeded(Exception exception)
		{
			return GlobCfg.GenerateWatsonsForPipelineCleanup || !PipelineDispatcher.IsExpectedUMException(exception);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0005D070 File Offset: 0x0005B270
		private static bool IsExpectedUMException(Exception exception)
		{
			return exception == null || exception is CDROperationException || exception is UserNotUmEnabledException || exception is InvalidObjectGuidException || exception is InvalidTenantGuidException || exception is InvalidAcceptedDomainException || exception is IOException || exception is UnauthorizedAccessException || exception is SmtpSubmissionException || exception is TransientException || exception is StoragePermanentException || exception is PipelineFullException;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0005D0DC File Offset: 0x0005B2DC
		private static bool MoveVoicemailToBadVoiceMailFolder(string filename)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			DirectoryInfo directoryInfo = new DirectoryInfo(Utils.VoiceMailFilePath);
			FileInfo[] files = directoryInfo.GetFiles(fileNameWithoutExtension + ".*");
			bool result = false;
			int num;
			if (Utils.TryReadRegValue("System\\CurrentControlSet\\Services\\MSExchange Unified Messaging\\Parameters", "EnableBadVoiceMailFolder", out num) && num == 1)
			{
				result = true;
				PipelineDispatcher.MoveVoicemailToBadVoiceMailFolderHelper(filename, files, true);
			}
			else
			{
				PipelineDispatcher.MoveVoicemailToBadVoiceMailFolderHelper(filename, files, false);
			}
			return result;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0005D140 File Offset: 0x0005B340
		private static void MoveVoicemailToBadVoiceMailFolderHelper(string headerFilePath, FileInfo[] allCorruptedFiles, bool movetoBVM)
		{
			for (int i = 0; i < allCorruptedFiles.Length; i++)
			{
				try
				{
					if (movetoBVM)
					{
						File.Move(allCorruptedFiles[i].FullName, Path.Combine(Utils.UMBadMailFilePath, allCorruptedFiles[i].Name));
					}
					else if (Path.GetFileName(headerFilePath).Equals(allCorruptedFiles[i].Name))
					{
						File.Move(allCorruptedFiles[i].FullName, Path.Combine(Utils.UMTempPath, allCorruptedFiles[i].Name));
					}
					else
					{
						Util.TryDeleteFile(allCorruptedFiles[i].FullName);
					}
				}
				catch (IOException)
				{
					Util.TryDeleteFile(allCorruptedFiles[i].FullName);
				}
				catch (UnauthorizedAccessException)
				{
					Util.TryDeleteFile(allCorruptedFiles[i].FullName);
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0005D20C File Offset: 0x0005B40C
		private static void LogKillWorkItem(string fileName, Exception exception, bool moveToBVMFolder)
		{
			CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Work Item with header file {0} is going to be killed. Final exception responsible: {1}. Moving to BVM: {2}", new object[]
			{
				fileName,
				exception,
				moveToBVMFolder
			});
			if (moveToBVMFolder)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_KillWorkItemAndMoveToBadVMFolder, null, new object[]
				{
					fileName,
					Utils.UMBadMailFilePath,
					CommonUtil.ToEventLogString(exception)
				});
				return;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_KillWorkItemAndDelete, null, new object[]
			{
				fileName,
				CommonUtil.ToEventLogString(exception)
			});
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0005D2A0 File Offset: 0x0005B4A0
		private void AcquireDiskQueueAndStartProcessing(object o)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineDispatcher.AcquireDiskQueueAndStartProcessing", new object[0]);
			while (this.diskQueueSemaphore == null && !this.IsShuttingDown)
			{
				Semaphore semaphore = null;
				try
				{
					semaphore = new Semaphore(1, 1, "0a90da68-66cb-11dc-8314-0800200c9a66");
					if (!semaphore.WaitOne(PipelineDispatcher.DiskQueueAcquisitionWaitInterval, false))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Unable to acquire exclusive access to disk queue.", new object[0]);
					}
					else
					{
						lock (this.lockObj)
						{
							if (!this.IsShuttingDown)
							{
								this.diskQueueSemaphore = semaphore;
								semaphore = null;
							}
						}
					}
				}
				catch (AbandonedMutexException)
				{
				}
				finally
				{
					if (semaphore != null)
					{
						semaphore.Close();
						semaphore = null;
					}
				}
			}
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Successfully acquired access to disk queue and starting to process files", new object[0]);
					HealthCheckPipelineContext.TryDeleteHealthCheckFiles();
					this.InitialializeDiskQueueWatchers();
					this.CreateWorkItemsFromDiskQueue();
					this.DispatchWork();
				}
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0005D3E8 File Offset: 0x0005B5E8
		private void InitialializeDiskQueueWatchers()
		{
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					this.diskQueueWatcher = new FileSystemWatcher(Utils.VoiceMailFilePath, "*.txt");
					this.diskQueueWatcher.NotifyFilter = NotifyFilters.LastWrite;
					this.diskQueueWatcher.Changed += this.OnDiskQueueFileChanged;
					this.diskQueueWatcher.IncludeSubdirectories = false;
					this.diskQueueWatcher.EnableRaisingEvents = true;
					this.diskQueueFullScanTimer = new Timer(new TimerCallback(this.OnDiskQueueFullScanTimeout), null, GlobCfg.VoiceMessagePollingTime, GlobCfg.VoiceMessagePollingTime);
				}
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0005D4A0 File Offset: 0x0005B6A0
		private void OnDiskQueueFileChanged(object source, FileSystemEventArgs args)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "OnDiskQueueFileChanged file={0}, changeType={1}", new object[]
			{
				args.FullPath,
				args.ChangeType
			});
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					FileInfo diskQueueItem = new FileInfo(args.FullPath);
					this.CreateAndQueueWorkItem(diskQueueItem);
					this.DispatchWork();
				}
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0005D534 File Offset: 0x0005B734
		private void OnDiskQueueFullScanTimeout(object o)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "OnDiskQueueFullScanTimeout @{0}", new object[]
			{
				ExDateTime.UtcNow
			});
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					this.CreateWorkItemsFromDiskQueue();
					this.DispatchWork();
				}
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		private void CreateWorkItemsFromDiskQueue()
		{
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					try
					{
						Util.SetCounter(AvailabilityCounters.TotalQueuedMessages, (long)this.queuedWorkItemIds.Count);
						DirectoryInfo directoryInfo = new DirectoryInfo(Utils.VoiceMailFilePath);
						FileInfo[] files = directoryInfo.GetFiles("*.txt");
						List<FileInfo> list = new List<FileInfo>(files.Length);
						for (int i = 0; i < files.Length; i++)
						{
							try
							{
								new ExDateTime(ExTimeZone.UtcTimeZone, files[i].LastWriteTimeUtc);
								list.Add(files[i]);
							}
							catch (IOException)
							{
							}
						}
						list.Sort((FileInfo lhs, FileInfo rhs) => Comparer<ExDateTime>.Default.Compare(new ExDateTime(ExTimeZone.UtcTimeZone, lhs.LastWriteTimeUtc), new ExDateTime(ExTimeZone.UtcTimeZone, rhs.LastWriteTimeUtc)));
						foreach (FileInfo diskQueueItem in list)
						{
							this.CreateAndQueueWorkItem(diskQueueItem);
						}
					}
					catch (IOException ex)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "ProccessDiskQueue IOException={0}", new object[]
						{
							ex
						});
					}
				}
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0005D764 File Offset: 0x0005B964
		private void CreateAndQueueWorkItem(FileInfo diskQueueItem)
		{
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(diskQueueItem.Name);
					Guid guid;
					if (!GuidHelper.TryParseGuid(fileNameWithoutExtension, out guid))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "CreateAndQueueWorkItem ignoring invalid GUID ={0}", new object[]
						{
							fileNameWithoutExtension
						});
						this.KillWorkItem(new LocalizedException(Strings.KillWorkItemInvalidGuid(fileNameWithoutExtension)), diskQueueItem.FullName, null);
					}
					if (this.queuedWorkItemIds.ContainsKey(guid))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "CreateAndQueueWorkItem ignoring known file={0}", new object[]
						{
							fileNameWithoutExtension
						});
					}
					else
					{
						PipelineWorkItem pipelineWorkItem = null;
						try
						{
							if (PipelineWorkItem.TryCreate(diskQueueItem, guid, out pipelineWorkItem))
							{
								if (pipelineWorkItem.Message is HealthCheckPipelineContext)
								{
									this.workQueue.Insert(0, pipelineWorkItem);
								}
								else
								{
									this.workQueue.Add(pipelineWorkItem);
								}
								this.queuedWorkItemIds.Add(guid, null);
								this.throttleManager.AddWorkItem(pipelineWorkItem);
								Util.SetCounter(AvailabilityCounters.TotalQueuedMessages, (long)this.queuedWorkItemIds.Count);
							}
						}
						catch (TransientException)
						{
						}
						catch (Exception ex)
						{
							if (!GrayException.IsGrayException(ex))
							{
								throw ex;
							}
							this.KillWorkItem(ex, diskQueueItem.FullName, pipelineWorkItem);
						}
					}
				}
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0005D8F8 File Offset: 0x0005BAF8
		private void DispatchWork()
		{
			lock (this.lockObj)
			{
				if (!this.IsShuttingDown)
				{
					for (int i = 0; i < this.workQueue.Count; i++)
					{
						PipelineWorkItem pipelineWorkItem = this.workQueue[i];
						if (!pipelineWorkItem.IsRunning)
						{
							this.UpdateWorkItemSLACounterIfRequired(pipelineWorkItem);
							if (!pipelineWorkItem.HeaderFileExists)
							{
								this.KillWorkItem(new LocalizedException(Strings.KillWorkItemHeaderFileNotExist(pipelineWorkItem.HeaderFilename)), pipelineWorkItem.HeaderFilename, pipelineWorkItem);
							}
							else
							{
								PipelineStageBase currentStage = pipelineWorkItem.CurrentStage;
								PipelineResource pipelineResource = this.resources[(int)currentStage.ResourceType];
								if ((currentStage.RetrySchedule.TimeToTry || currentStage.MarkedForLastChanceHandling) && pipelineResource.TryAcquire(pipelineWorkItem))
								{
									CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "DispatchWork. about to run DispatchWorkAsync for stage={0}, workId={1}.", new object[]
									{
										currentStage,
										pipelineWorkItem.WorkId
									});
									currentStage.DispatchWorkAsync(new StageCompletionCallback(this.OnStageCompleted));
									this.numberOfActiveStages++;
									pipelineWorkItem.IsRunning = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0005DA44 File Offset: 0x0005BC44
		private void OnStageCompleted(PipelineStageBase stage, PipelineWorkItem workItem, Exception error)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "OnStageCompleted.  sendingState={0}, workId={1}, NumActiveStages={2}, ", new object[]
			{
				stage,
				workItem.WorkId,
				this.numberOfActiveStages
			});
			lock (this.lockObj)
			{
				this.numberOfActiveStages--;
				this.resources[(int)stage.ResourceType].Release(workItem);
				workItem.IsRunning = false;
				if (this.IsShuttingDown)
				{
					if (this.numberOfActiveStages == 0 && this.stageShutdownEvent != null)
					{
						this.stageShutdownEvent.Set();
					}
				}
				else
				{
					if (error == null)
					{
						this.HandleSuccessfulStageCompletion(workItem);
					}
					else
					{
						this.HandleFailedStageCompletion(workItem, error);
					}
					this.DispatchWork();
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0005DB24 File Offset: 0x0005BD24
		private void HandleSuccessfulStageCompletion(PipelineWorkItem workItem)
		{
			workItem.AdvanceToNextStage();
			if (workItem.IsComplete)
			{
				this.StopProcessing(workItem);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0005DB3C File Offset: 0x0005BD3C
		private void HandleFailedStageCompletion(PipelineWorkItem workItem, Exception error)
		{
			CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "PipeLineDispatcher. HandleError: Last error ={0}", new object[]
			{
				error
			});
			string text = CommonUtil.ToEventLogString(error);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PipeLineError, text.GetHashCode().ToString(), new object[]
			{
				workItem.HeaderFilename,
				text
			});
			if (!GrayException.IsGrayException(error) && !PipelineDispatcher.IsExpectedUMException(error))
			{
				throw error;
			}
			if (error is WorkItemNeedsToBeRequeuedException)
			{
				this.StopProcessing(workItem);
				return;
			}
			if (workItem.CurrentStage.MarkedForLastChanceHandling)
			{
				CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "An Optional stage couldnt handle its last lifeline", new object[0]);
				this.KillWorkItem(error, workItem.HeaderFilename, workItem);
				return;
			}
			if (!workItem.CurrentStage.RetrySchedule.IsTimeToGiveUp)
			{
				CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Leaving the stage unchanged. The stage still has more time to try.", new object[0]);
				return;
			}
			if (workItem.CurrentStage.RetrySchedule.IsStageOptional)
			{
				CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Tried the WI for max number of retries. Still it didnt succeed. It was an optional WI. Last error ={0}, total delay ={1}", new object[]
				{
					error,
					workItem.CurrentStage.RetrySchedule.TotalDelayDueToThisStage
				});
				workItem.CurrentStage.MarkedForLastChanceHandling = true;
				return;
			}
			CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Tried the WI for max number of retries. Still it didnt succeed. It was a NON optional WI. Last error ={0}, total delay ={1}", new object[]
			{
				error,
				workItem.CurrentStage.RetrySchedule.TotalDelayDueToThisStage
			});
			this.KillWorkItem(error, workItem.HeaderFilename, workItem);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0005DCD8 File Offset: 0x0005BED8
		private void KillWorkItem(Exception e, string headerFileName, PipelineWorkItem workItem)
		{
			if (workItem != null)
			{
				workItem.IsRejected = true;
				workItem.SLARecorded = false;
			}
			this.StopProcessing(workItem);
			bool flag = PipelineDispatcher.MoveVoicemailToBadVoiceMailFolder(headerFileName);
			PipelineDispatcher.LogKillWorkItem(headerFileName, e, flag);
			string text = flag ? Path.Combine(Utils.UMBadMailFilePath, Path.GetFileName(headerFileName)) : Path.Combine(Utils.UMTempPath, Path.GetFileName(headerFileName));
			if (PipelineDispatcher.IsWatsonNeeded(e))
			{
				if (!GrayException.IsGrayException(e))
				{
					throw e;
				}
				ExceptionHandling.SendWatsonWithExtraData(new PipelineCleanupGeneratedWatson(e), text, false);
			}
			if (!flag)
			{
				Util.TryDeleteFile(text);
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		private void CleanUp()
		{
			lock (this.lockObj)
			{
				if (this.diskQueueSemaphore != null)
				{
					this.diskQueueSemaphore.Close();
				}
				if (this.stageShutdownEvent != null)
				{
					this.stageShutdownEvent.Close();
				}
				if (this.diskQueueWatcher != null)
				{
					this.diskQueueWatcher.Dispose();
				}
				if (this.diskQueueFullScanTimer != null)
				{
					this.diskQueueFullScanTimer.Dispose();
				}
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0005DDE4 File Offset: 0x0005BFE4
		private void StopProcessing(PipelineWorkItem workItem)
		{
			this.UpdateWorkItemSLACounterIfRequired(workItem);
			if (workItem != null)
			{
				try
				{
					lock (this.lockObj)
					{
						this.queuedWorkItemIds.Remove(workItem.WorkId);
						bool flag2 = this.workQueue.Remove(workItem);
						if (flag2)
						{
							this.throttleManager.RemoveWorkItem(workItem);
						}
					}
					Util.SetCounter(AvailabilityCounters.TotalQueuedMessages, (long)this.queuedWorkItemIds.Count);
					this.UpdateWorkItemCompletionLatency(workItem);
				}
				finally
				{
					workItem.Dispose();
				}
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0005DE88 File Offset: 0x0005C088
		private void UpdateWorkItemSLACounterIfRequired(PipelineWorkItem workItem)
		{
			if (workItem != null)
			{
				if (!workItem.SLARecorded)
				{
					bool flag = !workItem.IsRejected && workItem.TimeInQueue <= workItem.ExpectedRunTime;
					if (workItem.IsComplete || !flag)
					{
						if (workItem.IsRejected)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineDispatcher::UpdateWorkItemSLA() SLA not met because the WorkItem {0} has been rejected. ActualRunTime = {1}", new object[]
							{
								workItem.WorkId,
								workItem.TimeInQueue.ToString()
							});
						}
						else
						{
							if (!flag)
							{
								UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PipelineWorkItemSLAFailure, null, new object[]
								{
									workItem.HeaderFilename,
									workItem.ExpectedRunTime.TotalMinutes
								});
							}
							CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineDispatcher::UpdateWorkItemSLA() WorkItem = {0} ExpectedRunTime (SLA) = {1} ActualRunTime = {2}", new object[]
							{
								workItem.WorkId,
								workItem.ExpectedRunTime,
								workItem.IsComplete ? workItem.TimeInQueue.ToString() : "WorkItem still in progress"
							});
						}
						Util.SetCounter(AvailabilityCounters.UMPipelineSLA, (long)PipelineDispatcher.workItemsMeetingSLA.Update(flag));
						workItem.SLARecorded = true;
						return;
					}
				}
			}
			else
			{
				Util.SetCounter(AvailabilityCounters.UMPipelineSLA, (long)PipelineDispatcher.workItemsMeetingSLA.Update(false));
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineDispatcher::UpdateWorkItemSLA(). SLA not met because WorkItem could not be created.", new object[0]);
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0005E010 File Offset: 0x0005C210
		private void UpdateWorkItemCompletionLatency(PipelineWorkItem workItem)
		{
			ValidateArgument.NotNull(workItem, "workItem");
			if (workItem.IsComplete)
			{
				long num = PipelineDispatcher.averageProcessingLatency.Update(workItem.TimeInQueue.TotalSeconds);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineDispatcher::UpdateWorkItemLatency() AvgProcessingLatency = {0} after processing WorkItem = {1} ", new object[]
				{
					num,
					workItem.WorkId
				});
				Util.SetCounter(AvailabilityCounters.UMPipelineAverageLatency, num);
			}
		}

		// Token: 0x04000CEC RID: 3308
		private const string DiskQueueSemaphoreName = "0a90da68-66cb-11dc-8314-0800200c9a66";

		// Token: 0x04000CED RID: 3309
		private static readonly TimeSpan DiskQueueAcquisitionWaitInterval = new TimeSpan(0, 0, 10);

		// Token: 0x04000CEE RID: 3310
		private readonly PipelineResource[] resources;

		// Token: 0x04000CEF RID: 3311
		private static TimeSpan diskQueueFullScanInterval = new TimeSpan(0, 1, 0);

		// Token: 0x04000CF0 RID: 3312
		private static PipelineDispatcher instance = new PipelineDispatcher();

		// Token: 0x04000CF1 RID: 3313
		private static MovingAverage averageProcessingLatency = new MovingAverage(50);

		// Token: 0x04000CF2 RID: 3314
		private static PercentageBooleanSlidingCounter workItemsMeetingSLA = PercentageBooleanSlidingCounter.CreateSuccessCounter(1000, TimeSpan.FromHours(1.0));

		// Token: 0x04000CF3 RID: 3315
		private Semaphore diskQueueSemaphore;

		// Token: 0x04000CF4 RID: 3316
		private AutoResetEvent stageShutdownEvent = new AutoResetEvent(false);

		// Token: 0x04000CF5 RID: 3317
		private bool isShuttingDown;

		// Token: 0x04000CF6 RID: 3318
		private bool isInitialized;

		// Token: 0x04000CF7 RID: 3319
		private FileSystemWatcher diskQueueWatcher;

		// Token: 0x04000CF8 RID: 3320
		private Timer diskQueueFullScanTimer;

		// Token: 0x04000CF9 RID: 3321
		private int numberOfActiveStages;

		// Token: 0x04000CFA RID: 3322
		private Dictionary<Guid, object> queuedWorkItemIds = new Dictionary<Guid, object>(64);

		// Token: 0x04000CFB RID: 3323
		private List<PipelineWorkItem> workQueue = new List<PipelineWorkItem>(64);

		// Token: 0x04000CFC RID: 3324
		private PipelineDispatcher.WorkItemThrottleManager throttleManager = new PipelineDispatcher.WorkItemThrottleManager();

		// Token: 0x04000CFD RID: 3325
		private object lockObj = new object();

		// Token: 0x020002CB RID: 715
		internal enum PipelineResourceType
		{
			// Token: 0x04000D01 RID: 3329
			LowPriorityCpuBound,
			// Token: 0x04000D02 RID: 3330
			CpuBound,
			// Token: 0x04000D03 RID: 3331
			NetworkBound,
			// Token: 0x04000D04 RID: 3332
			Count
		}

		// Token: 0x020002CC RID: 716
		public enum ThrottledWorkItemType
		{
			// Token: 0x04000D06 RID: 3334
			CDRWorkItem,
			// Token: 0x04000D07 RID: 3335
			NonCDRWorkItem
		}

		// Token: 0x020002CD RID: 717
		public class WIThrottleData
		{
			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x060015DD RID: 5597 RVA: 0x0005E0E3 File Offset: 0x0005C2E3
			// (set) Token: 0x060015DE RID: 5598 RVA: 0x0005E0EB File Offset: 0x0005C2EB
			public string Key { get; set; }

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x060015DF RID: 5599 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
			// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0005E0FC File Offset: 0x0005C2FC
			public string RecipientId { get; set; }

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005E105 File Offset: 0x0005C305
			// (set) Token: 0x060015E2 RID: 5602 RVA: 0x0005E10D File Offset: 0x0005C30D
			public PipelineDispatcher.ThrottledWorkItemType WorkItemType { get; set; }
		}

		// Token: 0x020002CE RID: 718
		private class WorkItemThrottleManager
		{
			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0005E11E File Offset: 0x0005C31E
			public bool IsPipelineHealthy
			{
				get
				{
					return this.nonCDRWIThrottler.Count < GlobCfg.MaxNonCDRMessagesPendingInPipeline;
				}
			}

			// Token: 0x060015E5 RID: 5605 RVA: 0x0005E134 File Offset: 0x0005C334
			public PipelineDispatcher.WorkItemThrottler GetWorkItemThrottler(PipelineDispatcher.ThrottledWorkItemType workItemType)
			{
				switch (workItemType)
				{
				case PipelineDispatcher.ThrottledWorkItemType.CDRWorkItem:
					return this.cdrWIThrottler;
				case PipelineDispatcher.ThrottledWorkItemType.NonCDRWorkItem:
					return this.nonCDRWIThrottler;
				default:
					throw new NotImplementedException("Unknown ThrottledWorkItemType");
				}
			}

			// Token: 0x060015E6 RID: 5606 RVA: 0x0005E16C File Offset: 0x0005C36C
			public void AddWorkItem(PipelineWorkItem wi)
			{
				PipelineDispatcher.WIThrottleData throttlingData = wi.GetThrottlingData();
				if (throttlingData != null)
				{
					this.GetWorkItemThrottler(throttlingData.WorkItemType).AddWorkItem(throttlingData.Key, throttlingData.RecipientId);
				}
			}

			// Token: 0x060015E7 RID: 5607 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
			public void RemoveWorkItem(PipelineWorkItem wi)
			{
				PipelineDispatcher.WIThrottleData throttlingData = wi.GetThrottlingData();
				if (throttlingData != null)
				{
					this.GetWorkItemThrottler(throttlingData.WorkItemType).RemoveWorkItem(throttlingData.Key, throttlingData.RecipientId);
				}
			}

			// Token: 0x04000D0B RID: 3339
			private PipelineDispatcher.CDRWorkItemThrottler cdrWIThrottler = new PipelineDispatcher.CDRWorkItemThrottler();

			// Token: 0x04000D0C RID: 3340
			private PipelineDispatcher.NonCDRWorkItemThrottler nonCDRWIThrottler = new PipelineDispatcher.NonCDRWorkItemThrottler();
		}

		// Token: 0x020002CF RID: 719
		internal abstract class WorkItemThrottler
		{
			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060015E9 RID: 5609
			public abstract int MaxItemsInTablePerKey { get; }

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060015EA RID: 5610
			public abstract int MaxItemsInPipeline { get; }

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060015EB RID: 5611 RVA: 0x0005E1F2 File Offset: 0x0005C3F2
			// (set) Token: 0x060015EC RID: 5612 RVA: 0x0005E1FA File Offset: 0x0005C3FA
			public int Count { get; protected set; }

			// Token: 0x060015ED RID: 5613
			public abstract void AddWorkItem(string key, string recipientId);

			// Token: 0x060015EE RID: 5614
			public abstract void RemoveWorkItem(string key, string recipientId);

			// Token: 0x060015EF RID: 5615 RVA: 0x0005E203 File Offset: 0x0005C403
			public PipelineSubmitStatus CanSubmitWorkItem(string key, string recipientId)
			{
				return this.CanSubmitWorkItem(key, recipientId, 1f);
			}

			// Token: 0x060015F0 RID: 5616 RVA: 0x0005E212 File Offset: 0x0005C412
			public PipelineSubmitStatus CanSubmitLowPriorityWorkItem(string key, string recipientId)
			{
				return this.CanSubmitWorkItem(key, recipientId, 0.5f);
			}

			// Token: 0x060015F1 RID: 5617
			protected abstract PipelineSubmitStatus CanSubmitWorkItem(string key, string recipientId, float allowedPipelinePercentageFull);

			// Token: 0x04000D0D RID: 3341
			private const float NormalPriorityAllowedPipelinePercentageFull = 1f;

			// Token: 0x04000D0E RID: 3342
			private const float LowPriorityAllowedPipelinePercentageFull = 0.5f;
		}

		// Token: 0x020002D0 RID: 720
		internal class CDRWorkItemThrottler : PipelineDispatcher.WorkItemThrottler
		{
			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0005E229 File Offset: 0x0005C429
			public override int MaxItemsInTablePerKey
			{
				get
				{
					return AppConfig.Instance.Service.MaxCDRMessagesInPipeline / 4;
				}
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0005E23C File Offset: 0x0005C43C
			public override int MaxItemsInPipeline
			{
				get
				{
					return AppConfig.Instance.Service.MaxCDRMessagesInPipeline;
				}
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x0005E250 File Offset: 0x0005C450
			public override void AddWorkItem(string key, string recipientId)
			{
				ValidateArgument.NotNull(key, "key");
				if (recipientId != null)
				{
					throw new ArgumentException("CDRWorkItemThrottler.AddWorkItem: recipientId not null");
				}
				if (!this.workItemTable.ContainsKey(key))
				{
					this.workItemTable[key] = 0;
				}
				Dictionary<string, int> dictionary;
				(dictionary = this.workItemTable)[key] = dictionary[key] + 1;
				base.Count++;
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x0005E2B8 File Offset: 0x0005C4B8
			public override void RemoveWorkItem(string key, string recipientId)
			{
				ValidateArgument.NotNull(key, "key");
				if (recipientId != null)
				{
					throw new ArgumentException("CDRWorkItemThrottler.RemoveWorkItem: recipientId not null");
				}
				Dictionary<string, int> dictionary;
				(dictionary = this.workItemTable)[key] = dictionary[key] - 1;
				base.Count--;
				if (this.workItemTable[key] == 0)
				{
					this.workItemTable.Remove(key);
				}
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x0005E320 File Offset: 0x0005C520
			protected override PipelineSubmitStatus CanSubmitWorkItem(string key, string recipientId, float allowedPipelinePercentageFull)
			{
				ValidateArgument.NotNull(key, "key");
				if (recipientId != null)
				{
					throw new ArgumentException("CDRWorkItemThrottler.CanSubmitWorkItem: recipientId not null");
				}
				int num = 0;
				this.workItemTable.TryGetValue(key, out num);
				if ((double)base.Count < Math.Round((double)((float)this.MaxItemsInPipeline * allowedPipelinePercentageFull)) && (double)num < Math.Round((double)((float)this.MaxItemsInTablePerKey * allowedPipelinePercentageFull)))
				{
					return PipelineSubmitStatus.Ok;
				}
				return PipelineSubmitStatus.PipelineFull;
			}

			// Token: 0x04000D10 RID: 3344
			private Dictionary<string, int> workItemTable = new Dictionary<string, int>(16);
		}

		// Token: 0x020002D1 RID: 721
		internal class NonCDRWorkItemThrottler : PipelineDispatcher.WorkItemThrottler
		{
			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0005E39A File Offset: 0x0005C59A
			public override int MaxItemsInPipeline
			{
				get
				{
					return GlobCfg.MaxNonCDRMessagesPendingInPipeline;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060015FA RID: 5626 RVA: 0x0005E3A1 File Offset: 0x0005C5A1
			public override int MaxItemsInTablePerKey
			{
				get
				{
					return AppConfig.Instance.Service.MaxMessagesPerMailboxServer;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060015FB RID: 5627 RVA: 0x0005E3B2 File Offset: 0x0005C5B2
			public int RecipientStartThrottlingThresholdPercent
			{
				get
				{
					return AppConfig.Instance.Service.RecipientStartThrottlingThresholdPercent;
				}
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x060015FC RID: 5628 RVA: 0x0005E3C3 File Offset: 0x0005C5C3
			public int RecipientThrottlingPercent
			{
				get
				{
					return AppConfig.Instance.Service.RecipientThrottlingPercent;
				}
			}

			// Token: 0x060015FD RID: 5629 RVA: 0x0005E3D4 File Offset: 0x0005C5D4
			public override void AddWorkItem(string key, string recipientId)
			{
				ValidateArgument.NotNull(key, "key");
				ValidateArgument.NotNull(recipientId, "recipientId");
				base.Count++;
				PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue keyEntryValue;
				if (!this.workItemTable.TryGetValue(key, out keyEntryValue))
				{
					keyEntryValue = new PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue();
					this.workItemTable.Add(key, keyEntryValue);
				}
				keyEntryValue.Count++;
				if (!keyEntryValue.RecipientTable.ContainsKey(recipientId))
				{
					keyEntryValue.RecipientTable[recipientId] = 0;
				}
				Dictionary<string, int> recipientTable;
				(recipientTable = keyEntryValue.RecipientTable)[recipientId] = recipientTable[recipientId] + 1;
				this.Log("AddWorkItem", string.Empty, key, recipientId, keyEntryValue.Count, keyEntryValue.RecipientTable[recipientId]);
			}

			// Token: 0x060015FE RID: 5630 RVA: 0x0005E490 File Offset: 0x0005C690
			public override void RemoveWorkItem(string key, string recipientId)
			{
				ValidateArgument.NotNull(key, "key");
				ValidateArgument.NotNull(recipientId, "recipientId");
				base.Count--;
				PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue keyEntryValue = this.workItemTable[key];
				keyEntryValue.Count--;
				Dictionary<string, int> recipientTable;
				(recipientTable = keyEntryValue.RecipientTable)[recipientId] = recipientTable[recipientId] - 1;
				int recipientCount = keyEntryValue.RecipientTable[recipientId];
				if (keyEntryValue.RecipientTable[recipientId] == 0)
				{
					keyEntryValue.RecipientTable.Remove(recipientId);
				}
				if (keyEntryValue.Count == 0)
				{
					this.workItemTable.Remove(key);
				}
				this.Log("RemoveWorkItem", string.Empty, key, recipientId, keyEntryValue.Count, recipientCount);
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x0005E54C File Offset: 0x0005C74C
			public int GetRecipientThrottlingThreshold(float allowedPipelinePercentageFull)
			{
				return (int)Math.Round((double)((float)(this.GetMaxItemsPerKey(allowedPipelinePercentageFull) * this.RecipientStartThrottlingThresholdPercent) / 100f));
			}

			// Token: 0x06001600 RID: 5632 RVA: 0x0005E578 File Offset: 0x0005C778
			public int GetMaxItemsPerRecipient(float allowedPipelinePercentageFull)
			{
				return (int)Math.Round((double)((float)(this.GetMaxItemsPerKey(allowedPipelinePercentageFull) * this.RecipientThrottlingPercent) / 100f));
			}

			// Token: 0x06001601 RID: 5633 RVA: 0x0005E5A4 File Offset: 0x0005C7A4
			protected override PipelineSubmitStatus CanSubmitWorkItem(string key, string recipientId, float allowedPipelinePercentageFull)
			{
				ValidateArgument.NotNull(key, "key");
				ValidateArgument.NotNull(recipientId, "recipientId");
				int count = base.Count;
				int num = 0;
				int num2 = 0;
				PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue keyEntryValue;
				if (this.workItemTable.TryGetValue(key, out keyEntryValue))
				{
					num = keyEntryValue.Count;
					keyEntryValue.RecipientTable.TryGetValue(recipientId, out num2);
				}
				if ((double)count >= Math.Round((double)((float)this.MaxItemsInPipeline * allowedPipelinePercentageFull)))
				{
					this.Log("CanSubmitWorkItem", "no - pipeline full", key, recipientId, num, num2);
					return PipelineSubmitStatus.PipelineFull;
				}
				if (num >= this.GetMaxItemsPerKey(allowedPipelinePercentageFull))
				{
					this.Log("CanSubmitWorkItem", "no - reached max per key", key, recipientId, num, num2);
					return PipelineSubmitStatus.PipelineFull;
				}
				if (num < this.GetRecipientThrottlingThreshold(allowedPipelinePercentageFull))
				{
					this.Log("CanSubmitWorkItem", "yes (throttling inactive)", key, recipientId, num, num2);
					return PipelineSubmitStatus.Ok;
				}
				if (num2 < this.GetMaxItemsPerRecipient(allowedPipelinePercentageFull))
				{
					this.Log("CanSubmitWorkItem", "yes - (throttling active)", key, recipientId, num, num2);
					return PipelineSubmitStatus.Ok;
				}
				this.Log("CanSubmitWorkItem", "no - (throttling active)", key, recipientId, num, num2);
				return PipelineSubmitStatus.RecipientThrottled;
			}

			// Token: 0x06001602 RID: 5634 RVA: 0x0005E698 File Offset: 0x0005C898
			private int GetMaxItemsPerKey(float allowedPipelinePercentageFull)
			{
				return (int)Math.Round((double)((float)this.MaxItemsInTablePerKey * allowedPipelinePercentageFull));
			}

			// Token: 0x06001603 RID: 5635 RVA: 0x0005E6B8 File Offset: 0x0005C8B8
			private void Log(string method, string msg, string mailboxServer, string recipientId, int mailboxServerCount, int recipientCount)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "NonCDRWorkItemThrottler.{0} {1} [{2}] [{3}] Pipeline: {4} Mailbox server {5} Recipient: {6}", new object[]
				{
					method,
					msg,
					mailboxServer,
					recipientId,
					base.Count,
					mailboxServerCount,
					recipientCount
				});
			}

			// Token: 0x04000D11 RID: 3345
			private Dictionary<string, PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue> workItemTable = new Dictionary<string, PipelineDispatcher.NonCDRWorkItemThrottler.KeyEntryValue>(16);

			// Token: 0x020002D2 RID: 722
			private class KeyEntryValue
			{
				// Token: 0x17000585 RID: 1413
				// (get) Token: 0x06001605 RID: 5637 RVA: 0x0005E72A File Offset: 0x0005C92A
				// (set) Token: 0x06001606 RID: 5638 RVA: 0x0005E732 File Offset: 0x0005C932
				public int Count { get; set; }

				// Token: 0x04000D12 RID: 3346
				public Dictionary<string, int> RecipientTable = new Dictionary<string, int>(128);
			}
		}
	}
}
